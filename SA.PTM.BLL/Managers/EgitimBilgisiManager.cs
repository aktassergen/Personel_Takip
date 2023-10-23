using SA.PTM.BLL.Services;
using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.DAL.Concrete
{
    public class EgitimBilgisiManager : BaseManager<EgitimBilgisi>, IEgitimBilgisiService
    {
        BaseRepo<EgitimBilgisi> _repository;
        public EgitimBilgisiManager(BaseRepo<EgitimBilgisi> repository) : base(repository)
        {
            _repository = repository;
        }

        public int EgitimIdId(int personelId)
        {
            var personel = _repository.GetAll().FirstOrDefault(p => p.PersonelId == personelId);
            if (personel != null)
            {
                return personel.Id;
            }
            return -1;
        }


    }
}
