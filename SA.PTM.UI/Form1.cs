using Microsoft.VisualBasic.ApplicationServices;
using SA.PTM.BLL.Services;
using SA.PTM.DAL.Concrete;
using SA.PTM.DAL.Context;
using SA.PTM.Entity.Concrete;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SA.PTM.BLL.Managers;

namespace SA.PTM.UI
{
    public partial class Form1 : Form
    {

        IPersonelService personelService = new PersonelManager(new DAL.Concrete.BaseRepo<Entity.Concrete.Personel>(new PersonelTakipDbContext()));
        IOzlukBilgisiService ozlukBilgisiService = new OzlukBilgisiManager(new DAL.Concrete.BaseRepo<Entity.Concrete.OzlukBilgisi>(new PersonelTakipDbContext()));
        IAvansService avansService = new AvansManager(new DAL.Concrete.BaseRepo<Entity.Concrete.Avans>(new PersonelTakipDbContext()));
        IBordroService bordroService = new BordroManager(new DAL.Concrete.BaseRepo<Entity.Concrete.Bordro>(new PersonelTakipDbContext()));
        IEgitimBilgisiService egitimBilgisiService = new EgitimBilgisiManager(new DAL.Concrete.BaseRepo<Entity.Concrete.EgitimBilgisi>(new PersonelTakipDbContext()));
        IIzinBilgisiService izinBilgisiService = new IzinBilgisiManager(new DAL.Concrete.BaseRepo<Entity.Concrete.IzinBilgisi>(new PersonelTakipDbContext()));
        IMaasBilgisiService maasBilgisiService = new MaasBilgisiManager(new DAL.Concrete.BaseRepo<Entity.Concrete.MaasBilgisi>(new PersonelTakipDbContext()));
        IOkulService okulService = new OkulManeger(new DAL.Concrete.BaseRepo<Entity.Concrete.Okul>(new PersonelTakipDbContext()));
        ISertifikaService SertifikaService = new SertifikaManager(new DAL.Concrete.BaseRepo<Entity.Concrete.Sertifika>(new PersonelTakipDbContext()));
        ISirketIciEgitimService sirketIciEgitimService = new SirketIciEgitimManeger(new DAL.Concrete.BaseRepo<Entity.Concrete.SirketIciEgitim>(new PersonelTakipDbContext()));
        IYoneticiService yoneticiService = new YoneticiManager(new DAL.Concrete.BaseRepo<Entity.Concrete.Yonetici>(new PersonelTakipDbContext()));

        public Form1()
        {
            InitializeComponent();

        }

        private void PersonelEklebutton_Click(object sender, EventArgs e)
        {


            string kullaniciMail = KullaniciAdiTextBox.Text;
            string kullaniciSifre = SifreTextBox.Text;
            int yoneticiId = KullaniciId(kullaniciMail, kullaniciSifre);
            DateTime dogumTarihi = DogumTarihidateTimePicker1.Value;
            string tcNo = TCtextBox.Text;
            bool tcNoBenzersiz = IsTcNoUnique(tcNo);
            if (yoneticiId != -1 && tcNoBenzersiz)
            {
                
                Personel personel = new Personel();

                    personel.YoneticiId = yoneticiId;
                    personel.Ad = AdtextBox.Text;
                    personel.Anne = AnnetextBox.Text;
                    personel.Baba = BabatextBox.Text;
                    personel.Departman = DepartmantextBox.Text;
                    personel.DogumTarihi = dogumTarihi;
                    personel.MedeniDurumu = MedeniDtextBox.Text;
                    personel.DogumYeri = DogumYtextBox.Text;
                    personel.TcNo = tcNo;
                    personel.Rol = RoltextBox.Text;
                    personel.YasadigiSehir = SehirtextBox.Text;
                    personel.Soyad = SoyadtextBox.Text;  

            personelService.Insert(personel);
            personelService.SaveChanges();
            MessageBox.Show("Ekleme ba�ar�l�.");

            }
            else
            {
                MessageBox.Show("Ekleme Ba�ar�s�z . mevcut TcNo zaten olabili");
            }


        }

        private void BilgileriGoruntulebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCGiristextBox10.Text;
            IEnumerable<Personel> personeller = personelService.GetAll().Where(p => p.TcNo == tcNo);

            if (personeller.Any())
            {
                Personel personel = personeller.FirstOrDefault(); // �lk ki�iyi al�yoruz, gerekirse uygun ki�iyi se�melisiniz

                AdtextBox.Text = personel.Ad;
                SoyadtextBox.Text = personel.Soyad;
                AnnetextBox.Text = personel.Anne;
                BabatextBox.Text = personel.Baba;
                TCtextBox.Text = personel.TcNo;
                DepartmantextBox.Text = personel.Departman;
                DogumTarihidateTimePicker1.Value = personel.DogumTarihi;
                MedeniDtextBox.Text = personel.MedeniDurumu;
                DogumYtextBox.Text = personel.DogumYeri;
                RoltextBox.Text = personel.Rol;
                SehirtextBox.Text = personel.YasadigiSehir;

                if (personel.OzlukBilgisi != null)
                {
                    �kamettextBox.Text = personel.OzlukBilgisi.IkametAdresi;
                }
                else
                {
                    �kamettextBox.Text = "�kamet adresi bulunamad�.";
                }
            }
            else
            {
                MessageBox.Show("Personel bulunamad�.");
            }
        }

        private void PersonelGuncellebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCGiristextBox10.Text; // G�ncellenmesi gereken personelin TC kimlik numaras�
            Personel personel = personelService.PersonelGiris(tcNo);

            DateTime dogumTarihi = DogumTarihidateTimePicker1.Value;

            if (personel != null)
            {
                personel.Ad = AdtextBox.Text;
                personel.Anne = AnnetextBox.Text;
                personel.Baba = BabatextBox.Text;
                personel.Departman = DepartmantextBox.Text;
                personel.DogumTarihi = dogumTarihi;
                personel.MedeniDurumu = MedeniDtextBox.Text;
                personel.DogumYeri = DogumYtextBox.Text;
                personel.TcNo = TCtextBox.Text;
                personel.Rol = RoltextBox.Text;
                personel.YasadigiSehir = SehirtextBox.Text;
                personel.Soyad = SoyadtextBox.Text;


                // Di�er alanlar� da g�ncelleyin...

                //personelService.Update(personel);
                personelService.SaveChanges();
                MessageBox.Show("G�ncelleme ba�ar�l�.");
            }
            else
            {
                MessageBox.Show("Personel bulunamad�. G�ncelleme i�lemi yap�lamad�.");
            }

        }

        private void PersonelSilbutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCGiristextBox10.Text; // Silinmesi gereken personelin TC kimlik numaras�
            Personel personel = personelService.PersonelGiris(tcNo);

            if (personel != null)
            {
                // Personel veritaban�nda bulundu, silme i�lemi yapabilirsiniz.
                personelService.Delete(personel);
                personelService.SaveChanges();
                MessageBox.Show("Silme ba�ar�l�.");
            }
            else
            {
                MessageBox.Show("Personel bulunamad�. Silme i�lemi yap�lamad�.");
            }
        }

        private void �kametEklebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCtextBox.Text;
            string yeniIkametAdresi = �kamettextBox.Text;

            // Personeli TcNo'ya g�re al
            Personel personel = personelService.PersonelGiris(tcNo);

            if (personel != null)
            {
                // Ki�iye ait �zl�k bilgilerini al
                OzlukBilgisi ozlukBilgisi = ozlukBilgisiService.GetAll().FirstOrDefault(p => p.PersonelId == personel.Id);

                if (ozlukBilgisi != null)
                {
                    // Ki�iye ait mevcut ikametgah bilgisini al
                    string mevcutIkametAdresi = ozlukBilgisi.IkametAdresi;

                    if (string.IsNullOrEmpty(mevcutIkametAdresi))
                    {
                        // Mevcut ikametgah bilgisi yok, yeni ikametgah ekleyebilirsiniz
                        ozlukBilgisi.IkametAdresi = yeniIkametAdresi;
                        ozlukBilgisiService.Update(ozlukBilgisi);
                        MessageBox.Show("�kametgah bilgisi ba�ar�yla eklendi.");
                    }
                    else
                    {
                        // Mevcut ikametgah bilgisi var, kullan�c�ya g�ncelleme yapmas� gerekti�ini s�yle
                        DialogResult result = MessageBox.Show("Bu ki�inin zaten bir ikametgah adresi var. Mevcut ikametgah� g�ncellemek ister misiniz?", "�kametgah G�ncelleme", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            // Mevcut ikametgah� g�ncelle
                            ozlukBilgisi.IkametAdresi = yeniIkametAdresi;
                            ozlukBilgisiService.Update(ozlukBilgisi);
                            MessageBox.Show("�kametgah bilgisi g�ncellendi.");
                        }
                    }
                }
                else
                {
                    // �zl�k bilgisi bulunamad�, �nce �zl�k bilgilerini eklemeniz gerekebilir
                    MessageBox.Show("Bu personelin �zl�k bilgileri bulunamad�. �nce �zl�k bilgilerini eklemelisiniz.");
                }
            }
            else
            {
                // Personel bulunamad�, kullan�c�ya uygun bir mesaj g�sterilebilir
                MessageBox.Show("Belirtilen TC kimlik numaras�na sahip personel bulunamad�.");
            }
        }

        private void IkametGuncellebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCtextBox.Text;

            // Veritaban�nda bu TcNo'ya sahip ba�ka bir kay�t var m� kontrol et
            bool isTcNoExists = personelService.GetAll().Any(p => p.TcNo == tcNo);

            if (isTcNoExists)
            {
                // TC'ye g�re ilgili personeli bul
                Personel personel = personelService.PersonelGiris(tcNo);

                // �lgili personelin Id'sini al
                int personelId = personel.Id;

                // �kamet adresini TextBox'tan al
                string yeniIkametAdresi = �kamettextBox.Text;

                // �kamet bilgisini veritaban�ndan al
                OzlukBilgisi eskiIkametBilgisi = ozlukBilgisiService.GetAll().FirstOrDefault(o => o.PersonelId == personelId);

                if (eskiIkametBilgisi != null)
                {
                    // Eski ikamet bilgisini g�ncelle
                    eskiIkametBilgisi.IkametAdresi = yeniIkametAdresi;

                    // G�ncelleme i�lemini ger�ekle�tir
                    ozlukBilgisiService.Update(eskiIkametBilgisi);

                    MessageBox.Show("�kamet bilgisi ba�ar�yla g�ncellendi.");
                }
                else
                {
                    // Eski ikamet bilgisi bulunamad�ysa, yeni bir ikamet bilgisi olu�turun ve ekleyin
                    OzlukBilgisi yeniIkametBilgisi = new OzlukBilgisi
                    {
                        PersonelId = personelId,
                        IkametAdresi = yeniIkametAdresi
                    };

                    // Yeni ikamet bilgisini ekleyin
                    ozlukBilgisiService.Insert(yeniIkametBilgisi);

                    MessageBox.Show("�kamet bilgisi ba�ar�yla eklenmi�tir.");
                }
            }
            else
            {
                // Ki�i TC'ye sahip de�ilse kullan�c�ya uygun bir mesaj g�sterilebilir.
                MessageBox.Show("TC'ye sahip personel bulunamad�. L�tfen ge�erli bir TC girin.");
            }
        }

        private void SaglikRaporuEklebutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Dosyalar�|*.pdf|T�m Dosyalar|*.*";
            string tcNo = TCtextBox.Text;
            Personel personel = personelService.PersonelGiris(tcNo);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Dosya yolu olarak PDF dosyas�n� saklay�n
                string pdfDosyaYolu = openFileDialog.FileName;


                // Mevcut PersonelId'ye ait �zl�k bilgisi varsa g�ncelleyin, yoksa yeni bir �zl�k bilgisi olu�turun
                OzlukBilgisi ozluk = ozlukBilgisiService.GetById(personel.Id);
                if (ozluk == null)
                {
                    ozluk = new OzlukBilgisi();
                    ozluk.PersonelId = personel.Id;
                }

                // Di�er �zellikleri ayarlay�n
                // ...

                // PDF dosyas�n� y�kleyin
                ozluk.SaglikRaporu = pdfDosyaYolu;

                // Veritaban�na kaydedin veya g�ncelleyin
                if (ozluk.Id > 0)
                {
                    ozlukBilgisiService.Update(ozluk);
                }
                else
                {
                    ozlukBilgisiService.Insert(ozluk);
                }

                // CheckBox'� i�aretle (se�ili olarak ayarla)
                SaglikRaporucheckBox.Checked = true;
                MessageBox.Show("Sa�l�k raporu kaydedildi.");
            }
        }

        private void SaglikRapGoruntulebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCtextBox.Text; // TcNo giri�i
            Personel personel = personelService.PersonelGiris(tcNo);

            if (personel != null)
            {
                List<OzlukBilgisi> ozlukBilgileri = ozlukBilgisiService.GetAll().Where(p => p.PersonelId == personel.Id).ToList();

                if (ozlukBilgileri.Count > 0)
                {
                    // �lgili ki�iye ait �zl�k bilgileri var, burada g�r�nt�leyebilirsiniz
                    foreach (OzlukBilgisi ozluk in ozlukBilgileri)
                    {
                        // Dosyay� g�r�nt�le
                        if (!string.IsNullOrEmpty(ozluk.SaglikRaporu) && ozluk.PersonelId == personel.Id)
                        {
                            Process.Start(ozluk.SaglikRaporu);
                            // Di�er dosyalar� da g�r�nt�leyebilirsiniz
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ki�iye ait �zl�k bilgisi bulunamad�.");
                }
            }
            else
            {
                MessageBox.Show("Ki�i bulunamad�.");
            }
        }

        private void MaasEklebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCtextBox.Text;

            Personel personel = personelService.PersonelGiris(tcNo);

            if (personel != null)
            {
                int personelId = personel.Id;

                if (decimal.TryParse(MaastextBox.Text, out decimal yeniMaas))
                {
                    DateTime odemeTarihi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    MaasBilgisi eskiMaasBilgisi = maasBilgisiService.GetAll().FirstOrDefault(o => o.PersonelId == personelId);

                    if (eskiMaasBilgisi != null)
                    {
                        eskiMaasBilgisi.MaasMiktar = yeniMaas;
                        eskiMaasBilgisi.OdemeTarihi = odemeTarihi;
                        maasBilgisiService.Update(eskiMaasBilgisi);

                        MessageBox.Show("Maa� bilgisi ba�ar�yla g�ncellendi.");
                    }
                    else
                    {
                        MaasBilgisi yeniMaasBilgisi = new MaasBilgisi
                        {
                            PersonelId = personelId,
                            MaasMiktar = yeniMaas,
                            OdemeTarihi = odemeTarihi
                        };

                        maasBilgisiService.Insert(yeniMaasBilgisi);

                        MessageBox.Show("Maa� bilgisi ba�ar�yla eklenmi�tir.");
                    }
                }
                else
                {
                    MessageBox.Show("Ge�erli bir maa� miktar� giriniz.");
                }
            }
            else
            {
                MessageBox.Show("TC'ye sahip personel bulunamad�. L�tfen ge�erli bir TC girin.");
            }
        }

        private void KullaniciTanimlaButonu_Click(object sender, EventArgs e)
        {
            string kullaniciMail = KullaniciAdiTextBox.Text;
            string kullaniciSifre = SifreTextBox.Text;

            // Yeni bir y�netici olu�turun
            Yonetici yeniYonetici = new Yonetici
            {
                KullaniciMail = kullaniciMail,
                KullaniciSifre = kullaniciSifre
            };

            try
            {
                // Y�neticiyi veritaban�na ekleyin
                yoneticiService.Insert(yeniYonetici);
                MessageBox.Show("Y�netici eklendi.");

                // Ekledikten sonra gerekli i�lemleri yapabilirsiniz
            }
            catch (Exception ex)
            {
                MessageBox.Show("Y�netici eklenirken hata olu�tu: " + ex.Message);
            }
        }

        private void GirisYapBtn_Click(object sender, EventArgs e)
        {
            string kullaniciMail = KullaniciAdiTextBox.Text;
            string kullaniciSifre = SifreTextBox.Text;

            int yoneticiId = KullaniciId(kullaniciMail, kullaniciSifre);

            if (yoneticiId != -1)
            {
                MessageBox.Show("Giri� ba�ar�l�. Y�netici ID: " + yoneticiId);

                // Giri� yapt�ktan sonra yap�lacak i�lemleri burada ger�ekle�tirebilirsiniz.
            }
            else
            {
                MessageBox.Show("Giri� ba�ar�s�z. Kullan�c� ad� veya �ifre hatal�.");
            }
        }



        public int KullaniciId(string kullaniciMail, string kullaniciSifre)
        {
            try
            {
                using (var dbContext = new PersonelTakipDbContext())
                {
                    var user = dbContext.Yoneticiler.FirstOrDefault(u => u.KullaniciMail == kullaniciMail && u.KullaniciSifre == kullaniciSifre);

                    if (user != null)
                    {
                        // Kullan�c� do�ruland�
                        return user.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giri� yap�l�rken hata olu�tu: " + ex.Message);
            }

            // Kullan�c� bulunamad� veya hata meydana geldi�i durumda -1 d�nd�r�lebilir.
            return -1;
        }

        private bool IsTcNoUnique(string tcNo)
        {
            // Veritaban�nda ayn� TcNo'ya sahip ba�ka bir personel var m� kontrol edin.
            using (var dbContext = new PersonelTakipDbContext())
            {
                var existingPersonel = dbContext.Personeller.FirstOrDefault(p => p.TcNo == tcNo);
                return existingPersonel == null;
            }
        }
    }
}