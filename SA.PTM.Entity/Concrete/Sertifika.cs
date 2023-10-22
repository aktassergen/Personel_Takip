using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.Entity.Concrete
{
    public class Sertifika:BaseEntity
    {
        public int Id { get; set; }
        public string SertifikaAdi { get; set; }
        public string AlinanKurum { get; set; }
        public DateTime SertifikaTarihi { get; set; }

        public int EgitimBilgisiId { get; set; }
        public EgitimBilgisi EgitimBilgisi { get; set; }
    }
}
