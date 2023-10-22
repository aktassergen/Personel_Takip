using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.PTM.BLL.Services
{
    public interface IYoneticiService : IService<Yonetici>
    {
        public Yonetici PersonelGiris(string mail);
        public int UserLogin(string email, string password);
        public Yonetici GetUserByEmail(string email);
        public bool IsPasswordValid(Yonetici user, string password);
    }
}
