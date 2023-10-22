using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.Entity.Concrete
{
    public class Yonetici:BaseEntity
    {
        public int Id { get; set; }
        public string KullaniciMail { get; set; }
        public string KullaniciSifre { get; set; }

        public List<Personel> Personeller { get; set; } = new List<Personel>();
    }

}

