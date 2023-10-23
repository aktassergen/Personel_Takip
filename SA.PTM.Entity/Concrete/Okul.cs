using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.Entity.Concrete
{
    public class Okul:BaseEntity
    {
        public int Id { get; set; }
        public string OkulAdi { get; set; }
        public string MezuniyetDerecesi { get; set; }
        public DateTime MezuniyetTarihi { get; set; }
        public string? DiplomaOrnegi { get; set; }
        public string? EgitimTipleri { get; set; }

        public int EgitimBilgisiId { get; set; }
        public EgitimBilgisi EgitimBilgisi { get; set; }
    }
}
