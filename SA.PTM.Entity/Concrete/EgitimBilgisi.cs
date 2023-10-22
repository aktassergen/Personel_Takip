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
        public List<Okul> Okullar { get; set; }

        // 1-N ilişki ile Sertifikalar
        public List<Sertifika> Sertifikalar { get; set; }

        // 1-N ilişki ile Şirket İçi Eğitimler
        public List<SirketIciEgitim> SirketIciEgitimler { get; set; }
    }
}
