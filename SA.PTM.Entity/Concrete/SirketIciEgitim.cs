using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.Entity.Concrete
{
    public class SirketIciEgitim:BaseEntity
    {
        public int Id { get; set; }
        public string EgitimAdi { get; set; }
        public DateTime AlinmaTarihi { get; set; }

        public int EgitimBilgisiId { get; set; }
        public EgitimBilgisi EgitimBilgisi { get; set; }
    }
}
