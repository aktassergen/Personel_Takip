using SA.PTM.Entity.Abstruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.Entity.Concrete
{
    public class OzlukBilgisi : BaseEntity
    {
        public int Id { get; set; }
        public int PersonelId { get; set; } // İlgili personelin Id'si
        public Personel Personel { get; set; }
        public string? IkametAdresi { get; set; }
        public string? SaglikRaporu { get; set; }
        public string? AdliSicilBelgesi { get; set; }
        public string? Ehliyet { get; set; }
        public string? Sozlesme { get; set; }
        public string? CV { get; set; }
        public string? IsBasvurusu { get; set; }
        public string? AileCuzdani { get; set; }
        public string? AskerlikBelgesi { get; set; }
    }
}
