using SA.PTM.BLL.Services;
using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.DAL.Concrete
{
    public class BordroManager : BaseManager<Bordro>, IBordroService
    {
        BaseRepo<Bordro> _repository;
        public BordroManager(BaseRepo<Bordro> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
