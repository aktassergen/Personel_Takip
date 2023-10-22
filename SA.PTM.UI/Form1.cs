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
            MessageBox.Show("Ekleme baþarýlý.");

            }
            else
            {
                MessageBox.Show("Ekleme Baþarýsýz . mevcut TcNo zaten olabili");
            }


        }

        private void BilgileriGoruntulebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCGiristextBox10.Text;
            IEnumerable<Personel> personeller = personelService.GetAll().Where(p => p.TcNo == tcNo);

            if (personeller.Any())
            {
                Personel personel = personeller.FirstOrDefault(); // Ýlk kiþiyi alýyoruz, gerekirse uygun kiþiyi seçmelisiniz

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
                    ÝkamettextBox.Text = personel.OzlukBilgisi.IkametAdresi;
                }
                else
                {
                    ÝkamettextBox.Text = "Ýkamet adresi bulunamadý.";
                }
            }
            else
            {
                MessageBox.Show("Personel bulunamadý.");
            }
        }

        private void PersonelGuncellebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCGiristextBox10.Text; // Güncellenmesi gereken personelin TC kimlik numarasý
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


                // Diðer alanlarý da güncelleyin...

                //personelService.Update(personel);
                personelService.SaveChanges();
                MessageBox.Show("Güncelleme baþarýlý.");
            }
            else
            {
                MessageBox.Show("Personel bulunamadý. Güncelleme iþlemi yapýlamadý.");
            }

        }

        private void PersonelSilbutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCGiristextBox10.Text; // Silinmesi gereken personelin TC kimlik numarasý
            Personel personel = personelService.PersonelGiris(tcNo);

            if (personel != null)
            {
                // Personel veritabanýnda bulundu, silme iþlemi yapabilirsiniz.
                personelService.Delete(personel);
                personelService.SaveChanges();
                MessageBox.Show("Silme baþarýlý.");
            }
            else
            {
                MessageBox.Show("Personel bulunamadý. Silme iþlemi yapýlamadý.");
            }
        }

        private void ÝkametEklebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCtextBox.Text;
            string yeniIkametAdresi = ÝkamettextBox.Text;

            // Personeli TcNo'ya göre al
            Personel personel = personelService.PersonelGiris(tcNo);

            if (personel != null)
            {
                // Kiþiye ait özlük bilgilerini al
                OzlukBilgisi ozlukBilgisi = ozlukBilgisiService.GetAll().FirstOrDefault(p => p.PersonelId == personel.Id);

                if (ozlukBilgisi != null)
                {
                    // Kiþiye ait mevcut ikametgah bilgisini al
                    string mevcutIkametAdresi = ozlukBilgisi.IkametAdresi;

                    if (string.IsNullOrEmpty(mevcutIkametAdresi))
                    {
                        // Mevcut ikametgah bilgisi yok, yeni ikametgah ekleyebilirsiniz
                        ozlukBilgisi.IkametAdresi = yeniIkametAdresi;
                        ozlukBilgisiService.Update(ozlukBilgisi);
                        MessageBox.Show("Ýkametgah bilgisi baþarýyla eklendi.");
                    }
                    else
                    {
                        // Mevcut ikametgah bilgisi var, kullanýcýya güncelleme yapmasý gerektiðini söyle
                        DialogResult result = MessageBox.Show("Bu kiþinin zaten bir ikametgah adresi var. Mevcut ikametgahý güncellemek ister misiniz?", "Ýkametgah Güncelleme", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            // Mevcut ikametgahý güncelle
                            ozlukBilgisi.IkametAdresi = yeniIkametAdresi;
                            ozlukBilgisiService.Update(ozlukBilgisi);
                            MessageBox.Show("Ýkametgah bilgisi güncellendi.");
                        }
                    }
                }
                else
                {
                    // Özlük bilgisi bulunamadý, önce özlük bilgilerini eklemeniz gerekebilir
                    MessageBox.Show("Bu personelin özlük bilgileri bulunamadý. Önce özlük bilgilerini eklemelisiniz.");
                }
            }
            else
            {
                // Personel bulunamadý, kullanýcýya uygun bir mesaj gösterilebilir
                MessageBox.Show("Belirtilen TC kimlik numarasýna sahip personel bulunamadý.");
            }
        }

        private void IkametGuncellebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCtextBox.Text;

            // Veritabanýnda bu TcNo'ya sahip baþka bir kayýt var mý kontrol et
            bool isTcNoExists = personelService.GetAll().Any(p => p.TcNo == tcNo);

            if (isTcNoExists)
            {
                // TC'ye göre ilgili personeli bul
                Personel personel = personelService.PersonelGiris(tcNo);

                // Ýlgili personelin Id'sini al
                int personelId = personel.Id;

                // Ýkamet adresini TextBox'tan al
                string yeniIkametAdresi = ÝkamettextBox.Text;

                // Ýkamet bilgisini veritabanýndan al
                OzlukBilgisi eskiIkametBilgisi = ozlukBilgisiService.GetAll().FirstOrDefault(o => o.PersonelId == personelId);

                if (eskiIkametBilgisi != null)
                {
                    // Eski ikamet bilgisini güncelle
                    eskiIkametBilgisi.IkametAdresi = yeniIkametAdresi;

                    // Güncelleme iþlemini gerçekleþtir
                    ozlukBilgisiService.Update(eskiIkametBilgisi);

                    MessageBox.Show("Ýkamet bilgisi baþarýyla güncellendi.");
                }
                else
                {
                    // Eski ikamet bilgisi bulunamadýysa, yeni bir ikamet bilgisi oluþturun ve ekleyin
                    OzlukBilgisi yeniIkametBilgisi = new OzlukBilgisi
                    {
                        PersonelId = personelId,
                        IkametAdresi = yeniIkametAdresi
                    };

                    // Yeni ikamet bilgisini ekleyin
                    ozlukBilgisiService.Insert(yeniIkametBilgisi);

                    MessageBox.Show("Ýkamet bilgisi baþarýyla eklenmiþtir.");
                }
            }
            else
            {
                // Kiþi TC'ye sahip deðilse kullanýcýya uygun bir mesaj gösterilebilir.
                MessageBox.Show("TC'ye sahip personel bulunamadý. Lütfen geçerli bir TC girin.");
            }
        }

        private void SaglikRaporuEklebutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF Dosyalarý|*.pdf|Tüm Dosyalar|*.*";
            string tcNo = TCtextBox.Text;
            Personel personel = personelService.PersonelGiris(tcNo);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Dosya yolu olarak PDF dosyasýný saklayýn
                string pdfDosyaYolu = openFileDialog.FileName;


                // Mevcut PersonelId'ye ait özlük bilgisi varsa güncelleyin, yoksa yeni bir özlük bilgisi oluþturun
                OzlukBilgisi ozluk = ozlukBilgisiService.GetById(personel.Id);
                if (ozluk == null)
                {
                    ozluk = new OzlukBilgisi();
                    ozluk.PersonelId = personel.Id;
                }

                // Diðer özellikleri ayarlayýn
                // ...

                // PDF dosyasýný yükleyin
                ozluk.SaglikRaporu = pdfDosyaYolu;

                // Veritabanýna kaydedin veya güncelleyin
                if (ozluk.Id > 0)
                {
                    ozlukBilgisiService.Update(ozluk);
                }
                else
                {
                    ozlukBilgisiService.Insert(ozluk);
                }

                // CheckBox'ý iþaretle (seçili olarak ayarla)
                SaglikRaporucheckBox.Checked = true;
                MessageBox.Show("Saðlýk raporu kaydedildi.");
            }
        }

        private void SaglikRapGoruntulebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCtextBox.Text; // TcNo giriþi
            Personel personel = personelService.PersonelGiris(tcNo);

            if (personel != null)
            {
                List<OzlukBilgisi> ozlukBilgileri = ozlukBilgisiService.GetAll().Where(p => p.PersonelId == personel.Id).ToList();

                if (ozlukBilgileri.Count > 0)
                {
                    // Ýlgili kiþiye ait özlük bilgileri var, burada görüntüleyebilirsiniz
                    foreach (OzlukBilgisi ozluk in ozlukBilgileri)
                    {
                        // Dosyayý görüntüle
                        if (!string.IsNullOrEmpty(ozluk.SaglikRaporu) && ozluk.PersonelId == personel.Id)
                        {
                            Process.Start(ozluk.SaglikRaporu);
                            // Diðer dosyalarý da görüntüleyebilirsiniz
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Kiþiye ait özlük bilgisi bulunamadý.");
                }
            }
            else
            {
                MessageBox.Show("Kiþi bulunamadý.");
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

                        MessageBox.Show("Maaþ bilgisi baþarýyla güncellendi.");
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

                        MessageBox.Show("Maaþ bilgisi baþarýyla eklenmiþtir.");
                    }
                }
                else
                {
                    MessageBox.Show("Geçerli bir maaþ miktarý giriniz.");
                }
            }
            else
            {
                MessageBox.Show("TC'ye sahip personel bulunamadý. Lütfen geçerli bir TC girin.");
            }
        }

        private void KullaniciTanimlaButonu_Click(object sender, EventArgs e)
        {
            string kullaniciMail = KullaniciAdiTextBox.Text;
            string kullaniciSifre = SifreTextBox.Text;

            // Yeni bir yönetici oluþturun
            Yonetici yeniYonetici = new Yonetici
            {
                KullaniciMail = kullaniciMail,
                KullaniciSifre = kullaniciSifre
            };

            try
            {
                // Yöneticiyi veritabanýna ekleyin
                yoneticiService.Insert(yeniYonetici);
                MessageBox.Show("Yönetici eklendi.");

                // Ekledikten sonra gerekli iþlemleri yapabilirsiniz
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yönetici eklenirken hata oluþtu: " + ex.Message);
            }
        }

        private void GirisYapBtn_Click(object sender, EventArgs e)
        {
            string kullaniciMail = KullaniciAdiTextBox.Text;
            string kullaniciSifre = SifreTextBox.Text;

            int yoneticiId = KullaniciId(kullaniciMail, kullaniciSifre);

            if (yoneticiId != -1)
            {
                MessageBox.Show("Giriþ baþarýlý. Yönetici ID: " + yoneticiId);

                // Giriþ yaptýktan sonra yapýlacak iþlemleri burada gerçekleþtirebilirsiniz.
            }
            else
            {
                MessageBox.Show("Giriþ baþarýsýz. Kullanýcý adý veya þifre hatalý.");
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
                        // Kullanýcý doðrulandý
                        return user.Id;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriþ yapýlýrken hata oluþtu: " + ex.Message);
            }

            // Kullanýcý bulunamadý veya hata meydana geldiði durumda -1 döndürülebilir.
            return -1;
        }

        private bool IsTcNoUnique(string tcNo)
        {
            // Veritabanýnda ayný TcNo'ya sahip baþka bir personel var mý kontrol edin.
            using (var dbContext = new PersonelTakipDbContext())
            {
                var existingPersonel = dbContext.Personeller.FirstOrDefault(p => p.TcNo == tcNo);
                return existingPersonel == null;
            }
        }
    }
}