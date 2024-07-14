using Humanizer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.Domain.ValueObjects
{
    public class AyatAlkitab : IEquatable<AyatAlkitab>, IParsable<AyatAlkitab>
    {
        public Kitab Kitab { get; private set; }
        public int Pasal { get; private set; }
        public int[] Ayat { get; private set; } = Array.Empty<int>();

        private AyatAlkitab()
        {

        }

        public AyatAlkitab(Kitab kitab, int pasal, int[] ayat)
        {
            Kitab = kitab;
            Pasal = pasal;
            Ayat = ayat.Order().Distinct().ToArray();
        }

        public AyatAlkitab(Kitab kitab, int pasal, int ayat1, int ayat2)
        {
            Kitab = kitab;
            Pasal = pasal;
            Ayat = Enumerable.Range(ayat1, ayat2 + 1 - ayat1).ToArray();
        }

        public override string ToString()
        {
            var ayatString = "";

            if(Ayat.Length > 0)
            {
                ayatString = ":";
                if(IsAyatRange())
                {
                    ayatString += $"{Ayat.First()}-{Ayat.Last()}";
                }
                else
                {
                    ayatString += string.Join(",", Ayat);
                }
            }

            return $"{Kitab.Humanize()} {Pasal}{ayatString}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj is not AyatAlkitab other) return false;

            return other.Kitab == Kitab && other.Pasal == Pasal && other.Ayat.SequenceEqual(Ayat);
        }

        public bool Equals(AyatAlkitab? other)
        {
            return other is not null &&
                other.Kitab == Kitab && other.Pasal == Pasal && other.Ayat.SequenceEqual(Ayat);
        }

        public static bool operator==(AyatAlkitab a, AyatAlkitab b)
        {
            if(a is null || b is null) return false;

            return a.Equals(b); 
        }

        public static bool operator!=(AyatAlkitab a, AyatAlkitab b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Kitab.GetHashCode(), Pasal.GetHashCode(), Ayat.GetHashCode());
        }

        //Format : {Kitab} {Pasal}:{ayat}
        //Format : {Kitab} {Pasal}:{ayat}-{ayat}
        //Format : {Kitab} {Pasal}:{ayat},{ayat},{ayat}
        public static AyatAlkitab Parse(string s, IFormatProvider? provider)
        {
            var kitabCharArray = s.TakeWhile(c => c != ':');
            var kitabString = string
                .Join("", kitabCharArray.Reverse().SkipWhile(c => int.TryParse($"{c}", out _)).Reverse())
                .Trim();
            var kitab = kitabString.DehumanizeTo<Kitab>();

            var splits = s.Remove(0, kitabString.Length).Split(":");
            if (splits.Length != 2 && splits.Length != 1)
                throw new FormatException();

            var pasalString = splits[0];
            var pasal = int.Parse(pasalString);

            if(splits.Length == 2)
            {
                var ayatsString = splits[1];
                
                if (ayatsString.Contains('-'))
                {
                    var ayatsStringSplit = ayatsString.Split('-');

                    if(ayatsStringSplit.Length != 2)
                        throw new FormatException();

                    var ayat1 = int.Parse(ayatsStringSplit[0]);
                    var ayat2 = int.Parse(ayatsStringSplit[1]);

                    return new AyatAlkitab(kitab, pasal, ayat1, ayat2);
                }
                else
                {
                    var ayat = Array.Empty<int>();

                    var ayatsStringSplit = ayatsString.Split(',');

                    ayat = ayatsStringSplit.Select(c => int.Parse(c)).ToArray();

                    return new AyatAlkitab(kitab, pasal, ayat);
                }
            }
            else
            {
                return new AyatAlkitab(kitab, pasal, Array.Empty<int>());
            }
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out AyatAlkitab result)
        {
            try
            {
                var ayatAlkitab = Parse(s!, provider);
                result = ayatAlkitab;
                return true;
            }
            catch(Exception)
            {
                result = null;
                return false;
            }
        }

        public bool IsAyatRange()
        {
            if (Ayat.Length < 2) return false;

            return Ayat.Last() + 1 - Ayat.First() == Ayat.Length;
        }
    }

    public enum Kitab
    { 
        Kejadian = 1, Keluaran, Imamat, Bilangan, Ulangan, Yosua,

        [Display(Name = "Hakim-hakim", Description = "Hakim-hakim")]
        HakimHakim,

        Rut,

        [Display(Name = "1 Samuel", Description = "1 Samuel")]
        Samuel1,

        [Display(Name = "2 Samuel", Description = "2 Samuel")]
        Samuel2,

        [Display(Name = "1 Raja-Raja", Description = "1 Raja-Raja")]
        RajaRaja1,

        [Display(Name = "2 Raja-Raja", Description = "2 Raja-Raja")]
        RajaRaja2,

        [Display(Name = "1 Tawarikh", Description = "1 Tawarikh")]
        Tawarikh1,

        [Display(Name = "2 Tawarikh", Description = "2 Tawarikh")]
        Tawarikh2,

        Ezra, Nehemia, Ester, Ayub, Mazmur, Pengkhotbah,

        [Display(Name = "Kidung Agung", Description = "Kidung Agung")]
        KidungAgung,

        Yesaya, Yeremia, Ratapan, Yehezkiel, Daniel, Hosea, Yoel, Amos, Obaja, Yunus, Mikha,
        Nahum, Habakuk, Zefanya, Hagai, Zakharia, Maleakhi, Matius, Markus, Lukas, Yohanes,

        [Display(Name = "Kisah Para Rasul", Description = "Kisah Para Rasul")]
        KisahParaRasul,

        Roma,

        [Display(Name = "1 Korintus", Description = "1 Korintus")]
        Korintus1,

        [Display(Name = "2 Korintus", Description = "2 Korintus")]
        Korintus2,

        Galatia, Efesus, Filipi, Kolose,

        [Display(Name = "1 Tesalonika", Description = "1 Tesalonika")]
        Tesalonika1,

        [Display(Name = "2 Tesalonika", Description = "2 Tesalonika")]
        Tesalonika2,

        [Display(Name = "1 Timotius", Description = "1 Timotius")]
        Timotius1,

        [Display(Name = "2 Timotius", Description = "2 Timotius")]
        Timotius2,

        Titus, Filemon, Ibrani, Yakobus,

        [Display(Name = "1 Petrus", Description = "1 Petrus")]
        Petrus1,

        [Display(Name = "2 Petrus", Description = "2 Petrus")]
        Petrus2,

        [Display(Name = "1 Yohanes", Description = "1 Yohanes")]
        Yohanes1,

        [Display(Name = "2 Yohanes", Description = "2 Yohanes")]
        Yohanes2,

        [Display(Name = "3 Yohanes", Description = "3 Yohanes")]
        Yohanes3,

        Yudas, Wahyu
    }
}
