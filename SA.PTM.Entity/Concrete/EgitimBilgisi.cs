using SA.PTM.Entity.Abstruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.Entity.Concrete
{
    public class EgitimBilgisi : BaseEntity
    {
        public int Id { get; set; }
        public int PersonelId { get; set; }
        public Personel Personel { get; set; }
        public string OkulAdi { get; set; }
        public string MezuniyetDerecesi { get; set; }
        public DateTime MezuniyetTarihi { get; set; }
        public string? DiplomaninOrnegi { get; set; } // Diploma için byte dizisi
        public string SertifikaAdi { get; set; }
        public string SirketIciEgitim { get; set; }
    }
}
