using SA.PTM.BLL.Services;
using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.DAL.Concrete
{
    public class IzinBilgisiManager : BaseManager<IzinBilgisi>, IIzinBilgisiService
    {
        BaseRepo<IzinBilgisi> _repository;
        public IzinBilgisiManager(BaseRepo<IzinBilgisi> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
