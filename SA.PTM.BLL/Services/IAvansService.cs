using SA.PTM.DAL.Concrete;
using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.BLL.Services
{
    public interface IAvansService :IService<Avans>
    {
        public bool AvansVer(decimal maas, string tcNo, decimal verilenAvans, int odemeVadesi);
    }
}
