using SA.PTM.BLL.Services;
using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.DAL.Concrete
{
    public class OzlukBilgisiManager : BaseManager<OzlukBilgisi>, IOzlukBilgisiService
    {
        BaseRepo<OzlukBilgisi> _repository;
        public OzlukBilgisiManager(BaseRepo<OzlukBilgisi> repository) : base(repository)
        {
            _repository = repository;
        }

       
    }
}
