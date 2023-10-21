using SA.PTM.Entity.Abstruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.Entity.Concrete
{
    public class Personel : BaseEntity
    {
        public int Id { get; set; }
        public string TcNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string DogumYeri { get; set; }
        public string Anne { get; set; }
        public string Baba { get; set; }
        public string YasadigiSehir { get; set; }
        public string MedeniDurumu { get; set; }
        public string? Fotograf { get; set; } // Fotoğraf için byte dizisi
        public string Departman { get; set; }
        public string Rol { get; set; }

        public  ICollection<OzlukBilgisi> OzlukBilgileri { get; set; }
        public  ICollection<MaasBilgisi> MaasBilgileri { get; set; }
        public  ICollection<IzinBilgisi> IzinBilgileri { get; set; }
        public  ICollection<EgitimBilgisi> EgitimBilgileri { get; set; }
        public  ICollection<Bordro> Bordrolar { get; set; }
        public  ICollection<Avans> Avanslar { get; set; }

    }
}
