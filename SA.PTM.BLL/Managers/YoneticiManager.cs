using Microsoft.EntityFrameworkCore;
using SA.PTM.BLL.Services;
using SA.PTM.DAL.Abstract;
using SA.PTM.DAL.Concrete;
using SA.PTM.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace SA.PTM.BLL.Managers
{
    public class YoneticiManager : BaseManager<Yonetici>, IYoneticiService
    {
        BaseRepo<Yonetici> _repository;
        public readonly DbSet<Yonetici> _yoneticiDbSet;

        public YoneticiManager(BaseRepo<Yonetici> repository) : base(repository)
        {
            _repository = repository;
            _yoneticiDbSet = _repository._dbSet;
        }

        public Yonetici PersonelGiris(string mail)
        {
            // TC'ye göre ilgili personeli getir
            return _repository.GetAll().FirstOrDefault(p => p.KullaniciMail == mail);
        }
        //    public static string KullaniciMail { get; private set; }
        public int UserLogin(string email, string password)
        {
            Yonetici user = GetUserByEmail(email);

            if (user != null && IsPasswordValid(user, password))
            {
                return user.Id;
            }

            return -1; // Kullanıcı bulunamadı veya şifre uyuşmuyor
        }

        public Yonetici GetUserByEmail(string email)
        {
            return _yoneticiDbSet.SingleOrDefault(x => x.KullaniciMail == email);
        }

        public bool IsPasswordValid(Yonetici user, string password)
        {
            return user.KullaniciSifre == sha256_hash(password);
        }

        public string sha256_hash(string sifre)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(sifre)).Select(l => l.ToString("X2")));
            }
        }
    }



}

