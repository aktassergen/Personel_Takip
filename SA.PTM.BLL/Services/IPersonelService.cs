﻿using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.BLL.Services
{
    public interface IPersonelService:IService<Personel>
    {
        public Personel PersonelGiris(string TC);
    }
}
