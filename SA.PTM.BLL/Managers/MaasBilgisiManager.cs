using SA.PTM.BLL.Services;
using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.DAL.Concrete
{
    public class MaasBilgisiManager : BaseManager<MaasBilgisi>, IMaasBilgisiService
    {
        BaseRepo<MaasBilgisi> _repository;
        public MaasBilgisiManager(BaseRepo<MaasBilgisi> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
