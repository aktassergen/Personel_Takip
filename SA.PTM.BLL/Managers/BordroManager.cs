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

        public decimal AylikMaasHesapla(string tcNo, DateTime isBaslamaTarihi, decimal maas)
        {
            // Örnek bir hesaplama: Personelin işe başlama tarihinden bu yana geçen gün sayısını hesaplayın.
            int gecenGunler = (DateTime.Now - isBaslamaTarihi).Days;
            // Her gün için günlük maaşı hesaplayın.
            decimal gunlukMaas = maas / 30; // Basitçe 30 gün olarak kabul ediyoruz.
                                            // Geçen gün sayısına göre aylık maaşı hesaplayın.
            decimal aylikMaas = gecenGunler * gunlukMaas;
            return aylikMaas;
        }
    }
}
