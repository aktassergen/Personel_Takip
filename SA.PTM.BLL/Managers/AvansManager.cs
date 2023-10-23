using SA.PTM.BLL.Services;
using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.DAL.Concrete
{
    public class AvansManager : BaseManager<Avans>, IAvansService
    {
        BaseRepo<Avans> _repository;
        public AvansManager(BaseRepo<Avans> repository) : base(repository)
        {
            _repository = repository;
        }

        public bool AvansVer(decimal maas,string tcNo, decimal verilenAvans,int odemeVadesi)
        {
            if (verilenAvans <= (2 * maas) && odemeVadesi <= 12)
            {
                return true;
            }
            return false;
        }
    }
}
