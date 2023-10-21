﻿using SA.PTM.Entity.Abstruct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.Entity.Concrete
{
    public class Avans : BaseEntity
    {
        public int Id { get; set; }
        public int PersonelId { get; set; }
        public Personel Personel { get; set; }
        public decimal AvansMiktar { get; set; }
        public DateTime VerilisTarihi { get; set; }
        public int OdemeVadesi { get; set; }
    }
}
