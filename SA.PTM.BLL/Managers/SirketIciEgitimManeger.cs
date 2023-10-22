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
    public class SirketIciEgitimManeger : BaseManager<SirketIciEgitim>, ISirketIciEgitimService
    {
        BaseRepo<SirketIciEgitim> _repository;
        public SirketIciEgitimManeger(BaseRepo<SirketIciEgitim> repository) : base(repository)
        {
            _repository = repository;
        }
    }
   
}
