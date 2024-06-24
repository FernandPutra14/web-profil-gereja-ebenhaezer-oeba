using PKMGerejaEbenhaezer.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace PKMGerejaEbenhaezer.Domain
{
    public class Rayon : BaseEntity
    {
        public string Nama { get; set; }
        public string KetuaRayon { get; set; }
        public byte[] FotoKetua { get; set; }

        public int JumlahJemaat { get => JumlahLakiLaki + JumlahPerempuan; }

        public int JumlahLakiLaki { get; set; }
        public int JumlahPerempuan { get; set; }

        public int JumlahAnak { get; set; }
        public int JumlahRemaja { get; set; }
        public int JumlahPemuda { get; set; }
        public int JumlahDewasa { get; set; }
        public int JumlahLansia { get; set; }
    }
}
