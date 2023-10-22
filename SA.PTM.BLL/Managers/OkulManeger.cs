using SA.PTM.BLL.Services;
using SA.PTM.DAL.Concrete;
using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.BLL.Managers
{
    public class OkulManeger : BaseManager<Okul>, IOkulService
    {
        BaseRepo<Okul> _repository;
        public OkulManeger(BaseRepo<Okul> repository) : base(repository)
        {
            _repository = repository;
        }
    }
    
}
