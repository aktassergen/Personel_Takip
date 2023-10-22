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
        public Personel PersonelGiris(string TC)
        {
            // TC'ye göre ilgili personeli getir
            return _repository.GetAll().FirstOrDefault(p => p.TcNo == TC);
        }

    }
}

