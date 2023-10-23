using SA.PTM.BLL.Services;
using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.DAL.Concrete
{
    public class PersonelManager : BaseManager<Personel>, IPersonelService
    {
       private BaseRepo<Personel> _repository;
        public PersonelManager(BaseRepo<Personel> repository) : base(repository)
        {
            _repository = repository;
        }
        public int PersonelGiris(string TC)
        {
            var personel = _repository.GetAll().FirstOrDefault(p => p.TcNo == TC);
            if (personel != null)
            {
                return personel.Id;
            }
            return -1; 
        }

    }
}

