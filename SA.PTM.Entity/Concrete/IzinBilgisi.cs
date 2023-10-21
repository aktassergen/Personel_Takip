using SA.PTM.Entity.Abstruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.Entity.Concrete
{
    public class IzinBilgisi : BaseEntity
    {
        public int Id { get; set; }
        public int PersonelId { get; set; }
        public Personel Personel { get; set; }
        public DateTime IzinBaslangicTarihi { get; set; }
        public DateTime IzinBitisTarihi { get; set; }
        public string IzinTuru { get; set; }
    }
}
