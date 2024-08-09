using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using PKMGerejaEbenhaezer.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;

namespace PKMGerejaEbenhaezer.Web.Services.FileHelper
{
    public class FileHelperService : IFileHelperService
    {
        // If you require a check on specific characters in the IsValidFileExtensionAndSignature
        // method, supply the characters in the _allowedChars field.
        private static readonly byte[] _allowedChars = { };
        // For more file signatures, see the File Signatures Database (https://www.filesignatures.net/)
        // and the official specifications for the file types you wish to add.
        //TODO:Tambah file signature untuk file pdf
        private static readonly Dictionary<string, List<byte[]>> _fileSignature = new Dictionary<string, List<byte[]>>
        {
            { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
            { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
            { ".jpeg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                }
            },
            { ".jpg", new List<byte[]>
                {
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                    new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
                }
            },
            { ".zip", new List<byte[]>
                {
                    new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                    new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45 },
                    new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
                    new byte[] { 0x50, 0x4B, 0x05, 0x06 },
                    new byte[] { 0x50, 0x4B, 0x07, 0x08 },
                    new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 },
                }
            },
        };

        // **WARNING!**
        // In the following file processing methods, the file's content isn't scanned.
        // In most production scenarios, an anti-virus/anti-malware scanner API is
        // used on the file before making the file available to users or other
        // systems. For more information, see the topic that accompanies this sample
        // app.
        //TODO: Tambah API antivirus
        public async Task<Result<byte[]>> ProcessFormFile<T>(IFormFile formFile,
            string[] permittedExtensions,
            long minSizeLimit,
            long maxSizeLimit)
        {
            var fieldDisplayName = string.Empty;

            // Use reflection to obtain the display name for the model
            // property associated with this IFormFile. If a display
            // name isn't found, error messages simply won't show
            // a display name.
            MemberInfo? property =
                typeof(T).GetProperty(
                    formFile.Name.Substring(formFile.Name.IndexOf(".",
                    StringComparison.Ordinal) + 1));

            if (property is not null)
            {
                if (property.GetCustomAttribute(typeof(DisplayAttribute)) is
                    DisplayAttribute displayAttribute)
                {
                    fieldDisplayName = $"{displayAttribute.Name} ";
                }
            }

            // Don't trust the file name sent by the client. To display
            // the file name, HTML-encode the value.
            var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                formFile.FileName);

            // Check the file length. This check doesn't catch files that only have 
            // a BOM as their content.
            if (formFile.Length == 0)
                return new Error( "FileHelpers.FileEmpty", 
                    $"{fieldDisplayName}({trustedFileNameForDisplay}) kosong");

            if (formFile.Length < minSizeLimit)
            {
                var megabyteSizeLimit = minSizeLimit / (double)1048576;

                return new Error(
                    "FileHelpers.FileSizeTooSmall",
                    $"Ukuran {fieldDisplayName}({trustedFileNameForDisplay}) kurang dari " +
                    $"{megabyteSizeLimit:N3} MB.");
            }

            if (formFile.Length > maxSizeLimit)
            {
                var megabyteSizeLimit = maxSizeLimit / (double)1048576;

                return new Error(
                    "FileHelpers.FileSizeTooBig",
                    $"Ukuran {fieldDisplayName}({trustedFileNameForDisplay}) lebih besar dari " +
                    $"{megabyteSizeLimit:N3} MB.");
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);

                    // Check the content length in case the file's only
                    // content was a BOM and the content is actually
                    // empty after removing the BOM.
                    if (memoryStream.Length == 0)
                        return new Error(
                            "FileHelpers.FileEmpty", $"{fieldDisplayName}({trustedFileNameForDisplay}) kosong.");

                    if (!IsValidFileExtensionAndSignature(
                        formFile.FileName, memoryStream, permittedExtensions))
                        return new Error(
                            "FileHelpers.ExtensionAndSignatureNotValid",
                            $"Tipe file {fieldDisplayName}({trustedFileNameForDisplay}) " +
                            $"tidak didukung atau signature file tidak cocok dengan ekstensi file");

                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                // Log the exception

                return new Error(
                    "FileHelpers.UploadFailed",
                    $"{fieldDisplayName}({trustedFileNameForDisplay}) upload failed. " +
                    $"Please contact the Help Desk for support. Error: {ex.HResult}");
            }
        }

        public async Task<Result<byte[]>> ProcessStreamedFile(
            MultipartSection section, ContentDispositionHeaderValue contentDisposition,
            string[] permittedExtensions, long sizeLimit)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await section.Body.CopyToAsync(memoryStream);

                    // Check if the file is empty or exceeds the size limit.
                    if (memoryStream.Length == 0)
                        return new Error("FileHelpers.FileEmpty", "File kosong");

                    if (memoryStream.Length > sizeLimit)
                    {
                        var megabyteSizeLimit = sizeLimit / 1048576;

                        return new Error("FileHelpers.FileSizeToBig", 
                            $"Ukuran file melebihi {megabyteSizeLimit:N1} MB.");
                    }

                    if (!IsValidFileExtensionAndSignature(
                        contentDisposition.FileName.Value!, memoryStream,
                        permittedExtensions))
                        return new Error(
                            "FileHelpers.InvalidTypeOrSignatureDontMatch",
                            "The file type isn't permitted or the file's " +
                            "signature doesn't match the file's extension.");

                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                return new Error(
                    "FileHelpers.UploadFailed",
                    $"Upload failed. " +
                    $"Please contact the Help Desk for support. Error: {ex.HResult}");
            }
        }

        private bool IsValidFileExtensionAndSignature(string fileName,
            Stream data, string[] permittedExtensions)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
            {
                return false;
            }

            var ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return false;
            }

            data.Position = 0;

            using (var reader = new BinaryReader(data))
            {
                if (ext.Equals(".txt") || ext.Equals(".csv") || ext.Equals(".prn"))
                {
                    if (_allowedChars.Length == 0)
                    {
                        // Limits characters to ASCII encoding.
                        for (var i = 0; i < data.Length; i++)
                        {
                            if (reader.ReadByte() > sbyte.MaxValue)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        // Limits characters to ASCII encoding and
                        // values of the _allowedChars array.
                        for (var i = 0; i < data.Length; i++)
                        {
                            var b = reader.ReadByte();
                            if (b > sbyte.MaxValue ||
                                !_allowedChars.Contains(b))
                            {
                                return false;
                            }
                        }
                    }

                    return true;
                }

                // Uncomment the following code block if you must permit
                // files whose signature isn't provided in the _fileSignature
                // dictionary. We recommend that you add file signatures
                // for files (when possible) for all file types you intend
                // to allow on the system and perform the file signature
                // check.

                if (!_fileSignature.ContainsKey(ext))
                {
                    return true;
                }


                // File signature check
                // --------------------
                // With the file signatures provided in the _fileSignature
                // dictionary, the following code tests the input content's
                // file signature.
                var signatures = _fileSignature[ext];
                var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                return signatures.Any(signature =>
                    headerBytes.Take(signature.Length).SequenceEqual(signature));
            }
        }
    }
}
