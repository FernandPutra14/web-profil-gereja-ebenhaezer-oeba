using PKMGerejaEbenhaezer.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.Domain.DomainErrors
{
    public static class NoWaDomainErrors
    {
        public static readonly Error Empty = new("NoWa.Empty", "No. WA tidak boleh kosong");

        public static readonly Error NotValid = new("NoWa.NotValid", "Nomor WA yang dimasukan tidak valid");

        public static Error InvalidLength(int length) =>
            new("NoWa.InvalidLength", $"Panjang No. WA harus {length}");
    }
}
