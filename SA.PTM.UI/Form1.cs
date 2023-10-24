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
            MaascomboBox.Items.Add("Her ayýn 15'i");
            MaascomboBox.Items.Add("Her ayýn 21'i");
            MaascomboBox.SelectedIndex = 0;

            button11.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            button14.Visible = false;
            Personelbutton.Visible = false;
            Ozlukbutton.Visible = false;
            okulbutton.Visible = false;
            Maasbutton.Visible = false;
            Ýzinbutton.Visible = false;
            SirketEgitimbutton.Visible = false;
            Sertifikabutton.Visible = false;
            TCGetirbutton.Visible = false;
        }

        private void PersonelEklebutton_Click(object sender, EventArgs e)
        {


            string kullaniciMail = KullaniciAdiTextBox.Text;
            string kullaniciSifre = SifreTextBox.Text;
            int yoneticiId = yoneticiService.KullaniciId(kullaniciMail, kullaniciSifre);
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
                personel.BaslangicTarihi = IseGirisTarihidateTimePicker.Value;




                EgitimBilgisi egitimBilgisi = new EgitimBilgisi
                {
                    PersonelId = personel.Id,
                };
                personel.EgitimBilgisi = egitimBilgisi;


                OzlukBilgisi ozlukBilgisi = new OzlukBilgisi
                {
                    PersonelId = personel.Id,
                };
                personel.OzlukBilgisi = ozlukBilgisi;

                personelService.Insert(personel);
                personelService.SaveChanges();
                MessageBox.Show("Ekleme baþarýlý.");


            }
            else
            {
                MessageBox.Show("Ekleme Baþarýsýz . mevcut TcNo zaten olabili");
            }


        }
        private void PersonelGuncellebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCGiristextBox11.Text; // Güncellenmesi gereken personelin TC kimlik numarasý
            int personelId = personelService.PersonelGiris(tcNo);

            DateTime dogumTarihi = DogumTarihidateTimePicker1.Value;

            if (personelId != -1)
            {
                Personel personel = personelService.GetById(personelId);
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
                personel.BaslangicTarihi = IseGirisTarihidateTimePicker.Value;

                // Personel bilgilerini güncelleyin
                personelService.Update(personel);
                personelService.SaveChanges();

                MessageBox.Show("Personel güncellendi.");
            }
            else
            {
                MessageBox.Show("Personel bulunamadý. Güncelleme iþlemi yapýlamadý.");
            }

        }

        private void PersonelSilbutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCGiristextBox11.Text; // Silinmesi gereken personelin TC kimlik numarasý
            int personelId = personelService.PersonelGiris(tcNo);

            // Eðer verilen TC kimlik numarasýna sahip bir personel bulunursa
            if (personelId != -1)
            {
                // Personel veritabanýnda bulundu, silme iþlemi yapabilirsiniz.
                Personel personel = personelService.GetById(personelId);
                personelService.Delete(personel);
                personelService.SaveChanges();
                MessageBox.Show("Personel silindi.");
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

            // TC kimlik numarasýna sahip personeli al
            int personelId = personelService.PersonelGiris(tcNo);

            if (personelId != -1)
            {
                // Kiþiye ait özlük bilgilerini al
                OzlukBilgisi? ozlukBilgisi = ozlukBilgisiService.GetAll().FirstOrDefault(p => p.PersonelId == personelId);

                if (ozlukBilgisi != null)
                {
                    // Kiþiye ait mevcut ikametgah bilgisini al
                    string? mevcutIkametAdresi = ozlukBilgisi.IkametAdresi;

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
            string yeniIkametAdresi = ÝkamettextBox.Text;

            // TC kimlik numarasýna sahip personeli al
            int personelId = personelService.PersonelGiris(tcNo);

            if (personelId != -1)
            {
                // Kiþiye ait özlük bilgilerini al
                OzlukBilgisi? ozlukBilgisi = ozlukBilgisiService.GetAll().FirstOrDefault(p => p.PersonelId == personelId);

                if (ozlukBilgisi != null)
                {
                    // Kiþiye ait mevcut ikametgah bilgisini güncelle
                    ozlukBilgisi.IkametAdresi = yeniIkametAdresi;
                    ozlukBilgisiService.Update(ozlukBilgisi);
                    MessageBox.Show("Ýkametgah bilgisi baþarýyla güncellendi.");
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

        private void SaglikRaporuEklebutton_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "PDF Dosyalarý|*.pdf|Tüm Dosyalar|*.*";
            //string tcNo = TCtextBox.Text;
            //Personel personel = personelService.PersonelGiris(tcNo);

            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    // Dosya yolu olarak PDF dosyasýný saklayýn
            //    string pdfDosyaYolu = openFileDialog.FileName;


            //    // Mevcut PersonelId'ye ait özlük bilgisi varsa güncelleyin, yoksa yeni bir özlük bilgisi oluþturun
            //    OzlukBilgisi ozluk = ozlukBilgisiService.GetById(personel.Id);
            //    if (ozluk == null)
            //    {
            //        ozluk = new OzlukBilgisi();
            //        ozluk.PersonelId = personel.Id;
            //    }

            //    // Diðer özellikleri ayarlayýn
            //    // ...

            //    // PDF dosyasýný yükleyin
            //    ozluk.SaglikRaporu = pdfDosyaYolu;

            //    // Veritabanýna kaydedin veya güncelleyin
            //    if (ozluk.Id > 0)
            //    {
            //        ozlukBilgisiService.Update(ozluk);
            //    }
            //    else
            //    {
            //        ozlukBilgisiService.Insert(ozluk);
            //    }

            //    // CheckBox'ý iþaretle (seçili olarak ayarla)
            //    MessageBox.Show("Saðlýk raporu kaydedildi.");
            //}
        }

        private void SaglikRapGoruntulebutton_Click(object sender, EventArgs e)
        {
            //string tcNo = TCtextBox.Text; // TcNo giriþi
            //Personel personel = personelService.PersonelGiris(tcNo);

            //if (personel != null)
            //{
            //    List<OzlukBilgisi> ozlukBilgileri = ozlukBilgisiService.GetAll().Where(p => p.PersonelId == personel.Id).ToList();

            //    if (ozlukBilgileri.Count > 0)
            //    {
            //        // Ýlgili kiþiye ait özlük bilgileri var, burada görüntüleyebilirsiniz
            //        foreach (OzlukBilgisi ozluk in ozlukBilgileri)
            //        {
            //            // Dosyayý görüntüle
            //            if (!string.IsNullOrEmpty(ozluk.SaglikRaporu) && ozluk.PersonelId == personel.Id)
            //            {
            //                Process.Start(ozluk.SaglikRaporu);
            //                // Diðer dosyalarý da görüntüleyebilirsiniz
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Kiþiye ait özlük bilgisi bulunamadý.");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Kiþi bulunamadý.");
            //}
        }

        private void MaasEklebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCtextBox.Text;
            int personelId = personelService.PersonelGiris(tcNo);

            if (personelId != -1)
            {


                if (decimal.TryParse(MaastextBox.Text, out decimal yeniMaas))
                {
                    DateTime odemeTarihi = DateTime.MinValue; // Baþlangýç deðeri atanýyor

                    if (MaascomboBox.SelectedItem.ToString() == "Her ayýn 15'i")
                    {
                        odemeTarihi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                    }
                    else if (MaascomboBox.SelectedItem.ToString() == "Her ayýn 21'i")
                    {
                        odemeTarihi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 21);
                    }

                    MaasBilgisi? eskiMaasBilgisi = maasBilgisiService.GetAll().FirstOrDefault(o => o.PersonelId == personelId);

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

            Yonetici yeniYonetici = new Yonetici
            {
                KullaniciMail = kullaniciMail,
                KullaniciSifre = kullaniciSifre
            };

            try
            {
                yoneticiService.Insert(yeniYonetici);
                MessageBox.Show("Yönetici eklendi.");
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

            int yoneticiId = yoneticiService.KullaniciId(kullaniciMail, kullaniciSifre);

            if (yoneticiId != -1)
            {
                MessageBox.Show("Giriþ baþarýlý. Yönetici ID: " + yoneticiId);
                GirisPanelpanel.Visible = false;
                bilgilerigetirpnl.Visible = true;
                Personelbutton.Visible = true;
                Ozlukbutton.Visible = true;
                okulbutton.Visible = true;
                Maasbutton.Visible = true;
                Ýzinbutton.Visible = true;
                SirketEgitimbutton.Visible = true;
                Sertifikabutton.Visible = true;
                TCGetirbutton.Visible = true;

                // Giriþ yaptýktan sonra yapýlacak iþlemleri burada gerçekleþtirebilirsiniz.
            }
            else
            {
                MessageBox.Show("Giriþ baþarýsýz. Kullanýcý adý veya þifre hatalý.");
            }
        }



     

        private bool IsTcNoUnique(string tcNo)
        {
            using (var dbContext = new PersonelTakipDbContext())
            {
                var existingPersonel = dbContext.Personeller.FirstOrDefault(p => p.TcNo == tcNo);
                return existingPersonel == null;
            }
        }

        private void ÝzinEklebutton_Click_1(object sender, EventArgs e)
        {
            string tcNo = TCGiristextBox11.Text;
            Personel? personel = personelService.GetAll().FirstOrDefault(p => p.TcNo == tcNo);

            if (personel != null)
            {
                DateTime baslangicTarihi = IzinBaslangicdateTimePicker.Value;
                DateTime bitisTarihi = IzinBitisdateTimePicker.Value;

                TimeSpan izinSuresi = bitisTarihi - baslangicTarihi;
                int izinGunSayisi = (int)izinSuresi.TotalDays;

                string izinTuru = izinGunSayisi + " günlük izin";

                IzinBilgisi yeniIzin = new IzinBilgisi
                {
                    PersonelId = personel.Id,
                    IzinBaslangicTarihi = baslangicTarihi,
                    IzinBitisTarihi = bitisTarihi,
                    IzinTuru = izinTuru
                };

                izinBilgisiService.Insert(yeniIzin);


            }
            else
            {
                MessageBox.Show("Personel bulunamadý. Lütfen geçerli bir TC girin.");
            }
        }

        private void DiplomaEklebutton_Click(object sender, EventArgs e)
        {

        }

        private void OkulBilgileriEklebutton_Click(object sender, EventArgs e)
        {


            DateTime mezuniyetTarihi = MezniyetdateTimePicker.Value;
            string tcNo = TCtextBox.Text;
            int personelId = personelService.PersonelGiris(tcNo);

            if (personelId != -1)
            {
                int egitimBilgisiId = egitimBilgisiService.EgitimIdId(personelId);
                if (egitimBilgisiId != -1)
                {
                    Okul okul = new Okul
                    {

                        OkulAdi = OkulAditextBox.Text,
                        MezuniyetDerecesi = MezuniyettextBox.Text,
                        MezuniyetTarihi = MezniyetdateTimePicker.Value,
                        EgitimBilgisiId = egitimBilgisiId,
                        EgitimTipleri = EgitimTipicomboBox.Text

                    };

                    okulService.Insert(okul);

                    MessageBox.Show("Okul bilgisi eklendi.");
                }
                else
                {
                    MessageBox.Show("Kiþiye ait eðitim bilgisi bulunamadý.");
                }
            }
            else
            {
                MessageBox.Show("TcNo'ya sahip kiþi bulunamadý.");
            }

        }

        private void SertifikaEklebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCtextBox.Text;
            int personelId = personelService.PersonelGiris(tcNo);

            if (personelId != -1)
            {
                int egitimBilgisiId = egitimBilgisiService.EgitimIdId(personelId);
                if (egitimBilgisiId != -1)
                {
                    Sertifika sertifika = new Sertifika
                    {

                        SertifikaAdi = SertifikatextBox.Text,
                        SertifikaTarihi = SertifikadateTimePicker.Value,
                        EgitimBilgisiId = egitimBilgisiId,
                        AlinanKurum = AlinanKurumtextBox.Text,



                    };

                    SertifikaService.Insert(sertifika);

                    MessageBox.Show("sertidika bilgisi eklendi.");
                }
                else
                {
                    MessageBox.Show("Kiþiye ait eðitim bilgisi bulunamadý.");
                }
            }
            else
            {
                MessageBox.Show("TcNo'ya sahip kiþi bulunamadý.");
            }
        }

        private void SertifikaGuncellebutton_Click(object sender, EventArgs e)
        {
            string? selectedSertifika = SertifikalistBox.SelectedItem as string;
            if (selectedSertifika != null)
            {
                string[] SertifikaBilgileri = selectedSertifika.Split('-');
                string tcNo = TCtextBox.Text;
                int personelId = personelService.PersonelGiris(tcNo);

                if (personelId != -1)
                {
                    int SertifikaId = int.Parse(selectedSertifika.Split('-')[0]);
                    int egitimBilgisiId = egitimBilgisiService.EgitimIdId(personelId);
                    if (egitimBilgisiId != -1)
                    {

                        Sertifika sertifika = SertifikaService.GetById(SertifikaId);

                        sertifika.SertifikaTarihi = SirketiciEgitimdateTimePicker.Value;
                        sertifika.SertifikaAdi = SertifikatextBox.Text;
                        sertifika.AlinanKurum = AlinanKurumtextBox.Text;


                        SertifikaService.Update(sertifika);
                        SertifikaService.SaveChanges();

                        MessageBox.Show("Okul bilgisi güncellendi.");

                    }
                    else
                    {
                        MessageBox.Show("Kiþiye ait eðitim bilgisi bulunamadý.");
                    }
                }
                else
                {
                    MessageBox.Show("seçim yapýlmadý");
                }
            }
        }

        private void SertifikaSilbutton_Click(object sender, EventArgs e)
        {
            string? selectedSertifika = SertifikalistBox.SelectedItem as string;
            if (selectedSertifika != null)
            {
                string[] SertifikaBilgileri = selectedSertifika.Split('-');
                string tcNo = TCtextBox.Text;
                int personelId = personelService.PersonelGiris(tcNo);

                if (personelId != -1)
                {
                    int SertifikaId = int.Parse(selectedSertifika.Split('-')[0]);
                    int egitimBilgisiId = egitimBilgisiService.EgitimIdId(personelId);
                    if (egitimBilgisiId != -1)
                    {

                        Sertifika sertifika = SertifikaService.GetById(SertifikaId);

                        sertifika.SertifikaTarihi = SirketiciEgitimdateTimePicker.Value;
                        sertifika.SertifikaAdi = SertifikatextBox.Text;
                        sertifika.AlinanKurum = AlinanKurumtextBox.Text;


                        SertifikaService.Delete(sertifika);
                        SertifikaService.SaveChanges();

                        MessageBox.Show("Okul bilgisi güncellendi.");

                    }
                    else
                    {
                        MessageBox.Show("Kiþiye ait eðitim bilgisi bulunamadý.");
                    }
                }
                else
                {
                    MessageBox.Show("seçim yapýlmadý");
                }
            }
        }

        private void SirketIiciEgitimEklebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCtextBox.Text;
            int personelId = personelService.PersonelGiris(tcNo);

            if (personelId != -1)
            {
                int egitimBilgisiId = egitimBilgisiService.EgitimIdId(personelId);
                if (egitimBilgisiId != -1)
                {
                    SirketIciEgitim sirketIciEgitim = new SirketIciEgitim
                    {
                        AlinmaTarihi = SirketiciEgitimdateTimePicker.Value,
                        EgitimAdi = SirketiciegitimtextBox.Text,
                        EgitimBilgisiId = egitimBilgisiId,
                    };

                    sirketIciEgitimService.Insert(sirketIciEgitim);

                    MessageBox.Show("egitim bilgisi eklendi.");
                }
                else
                {
                    MessageBox.Show("Kiþiye ait eðitim bilgisi bulunamadý.");
                }
            }
            else
            {
                MessageBox.Show("TcNo'ya sahip kiþi bulunamadý.");
            }
        }

        private void SirketIiciEgitimGuncellebutton_Click(object sender, EventArgs e)
        {
            string? selectedEgitim = SirketiciegitimlistBox.SelectedItem as string;
            if (selectedEgitim != null)
            {
                string[] okulBilgileri = selectedEgitim.Split('-');
                string tcNo = TCtextBox.Text;
                int personelId = personelService.PersonelGiris(tcNo);

                if (personelId != -1)
                {
                    int egitimId = int.Parse(selectedEgitim.Split('-')[0]);
                    int egitimBilgisiId = egitimBilgisiService.EgitimIdId(personelId);
                    if (egitimBilgisiId != -1)
                    {

                        SirketIciEgitim sirketIciEgitim = sirketIciEgitimService.GetById(egitimId);

                        sirketIciEgitim.AlinmaTarihi = SirketiciEgitimdateTimePicker.Value;
                        sirketIciEgitim.EgitimAdi = SirketiciegitimtextBox.Text;





                        sirketIciEgitimService.Update(sirketIciEgitim);
                        sirketIciEgitimService.SaveChanges();

                        MessageBox.Show("egitim bilgisi güncellendi.");

                    }
                    else
                    {
                        MessageBox.Show("Kiþiye ait eðitim bilgisi bulunamadý.");
                    }
                }
                else
                {
                    MessageBox.Show("seçim yapýlmadý");
                }
            }
        }

        private void SirketIiciEgitimSilbutton_Click(object sender, EventArgs e)
        {
            string? selectedEgitim = SirketiciegitimlistBox.SelectedItem as string;
            if (selectedEgitim != null)
            {
                string[] okulBilgileri = selectedEgitim.Split('-');
                string tcNo = TCtextBox.Text;
                int personelId = personelService.PersonelGiris(tcNo);

                if (personelId != -1)
                {
                    int egitimId = int.Parse(selectedEgitim.Split('-')[0]);
                    int egitimBilgisiId = egitimBilgisiService.EgitimIdId(personelId);
                    if (egitimBilgisiId != -1)
                    {

                        SirketIciEgitim sirketIciEgitim = sirketIciEgitimService.GetById(egitimId);

                        sirketIciEgitim.AlinmaTarihi = SirketiciEgitimdateTimePicker.Value;
                        sirketIciEgitim.EgitimAdi = SirketiciegitimtextBox.Text;

                        sirketIciEgitimService.Delete(sirketIciEgitim);
                        sirketIciEgitimService.SaveChanges();

                        MessageBox.Show("Okul bilgisi güncellendi.");

                    }
                    else
                    {
                        MessageBox.Show("Kiþiye ait eðitim bilgisi bulunamadý.");
                    }
                }
                else
                {
                    MessageBox.Show("seçim yapýlmadý");
                }
            }

        }

        private void OkulBilgileriSilbutton_Click(object sender, EventArgs e)
        {
            string? selectedOkul = OkullistBox.SelectedItem as string;
            if (selectedOkul != null)
            {
                string[] okulBilgileri = selectedOkul.Split(':');
                string tcNo = TCtextBox.Text;
                int personelId = personelService.PersonelGiris(tcNo);

                if (personelId != -1)
                {
                    int okulId = int.Parse(selectedOkul.Split(':')[0]);
                    int egitimBilgisiId = egitimBilgisiService.EgitimIdId(personelId);
                    if (egitimBilgisiId != -1)
                    {

                        Okul okul = okulService.GetById(okulId);
                        okulService.Delete(okul);
                        okulService.SaveChanges();

                        MessageBox.Show("Okul bilgisi Silindi.");

                    }
                    else
                    {
                        MessageBox.Show("Kiþiye ait eðitim bilgisi bulunamadý.");
                    }
                }
                else
                {
                    MessageBox.Show("seçim yapýlmadý");
                }
            }
        }

        private void OkulBilgileriGuncellebutton_Click(object sender, EventArgs e)
        {
            string? selectedOkul = OkullistBox.SelectedItem as string;
            if (selectedOkul != null)
            {
                string[] okulBilgileri = selectedOkul.Split(':');
                string tcNo = TCtextBox.Text;
                int personelId = personelService.PersonelGiris(tcNo);

                if (personelId != -1)
                {
                    int okulId = int.Parse(selectedOkul.Split(':')[0]);
                    int egitimBilgisiId = egitimBilgisiService.EgitimIdId(personelId);
                    if (egitimBilgisiId != -1)
                    {

                        Okul okul = okulService.GetById(okulId);


                        okul.OkulAdi = OkulAditextBox.Text;
                        okul.MezuniyetDerecesi = MezuniyettextBox.Text;
                        okul.MezuniyetTarihi = MezniyetdateTimePicker.Value;
                        okul.EgitimBilgisiId = egitimBilgisiId;
                        okul.EgitimTipleri = EgitimTipicomboBox.Text;



                        okulService.Update(okul);
                        okulService.SaveChanges();

                        MessageBox.Show("Okul bilgisi güncellendi.");

                    }
                    else
                    {
                        MessageBox.Show("Kiþiye ait eðitim bilgisi bulunamadý.");
                    }
                }
                else
                {
                    MessageBox.Show("seçim yapýlmadý");
                }
            }
        }

        private void KullaniciTanýmlabutton_Click(object sender, EventArgs e)
        {

        }

        private void IzinGuncellebutton_Click(object sender, EventArgs e)
        {
            string? selectedizin = IzinlistBox.SelectedItem as string;
            if (selectedizin != null)
            {
                string[] izinBilgileriId = selectedizin.Split('-');
                string tcNo = TCtextBox.Text;
                int personelId = personelService.PersonelGiris(tcNo);

                if (personelId != -1)
                {
                    int izinId = int.Parse(selectedizin.Split('-')[0]);
                    DateTime baslangicTarihi = IzinBaslangicdateTimePicker.Value;
                    DateTime bitisTarihi = IzinBitisdateTimePicker.Value;

                    TimeSpan izinSuresi = bitisTarihi - baslangicTarihi;
                    int izinGunSayisi = (int)izinSuresi.TotalDays;

                    string izinTuru = izinGunSayisi + " günlük izin";


                    IzinBilgisi izin = izinBilgisiService.GetById(izinId);

                    izin.IzinBaslangicTarihi = IzinBaslangicdateTimePicker.Value;
                    izin.IzinBitisTarihi = IzinBitisdateTimePicker.Value;
                    izin.IzinTuru = izinTuru;



                    izinBilgisiService.Update(izin);
                    izinBilgisiService.SaveChanges();

                    MessageBox.Show("izin bilgisi güncellendi.");



                }
                else
                {
                    MessageBox.Show("seçim yapýlmadý");
                }
            }
        }

        private void Izinsilbutton_Click(object sender, EventArgs e)
        {
            string? selectedizin = IzinlistBox.SelectedItem as string;
            if (selectedizin != null)
            {
                string[] izinBilgileriId = selectedizin.Split('-');
                string tcNo = TCtextBox.Text;
                int personelId = personelService.PersonelGiris(tcNo);

                if (personelId != -1)
                {
                    int izinId = int.Parse(selectedizin.Split('-')[0]);
                    DateTime baslangicTarihi = IzinBaslangicdateTimePicker.Value;
                    DateTime bitisTarihi = IzinBitisdateTimePicker.Value;

                    TimeSpan izinSuresi = bitisTarihi - baslangicTarihi;
                    int izinGunSayisi = (int)izinSuresi.TotalDays;

                    string izinTuru = izinGunSayisi + " günlük izin";


                    IzinBilgisi izin = izinBilgisiService.GetById(izinId);

                    izin.IzinBaslangicTarihi = IzinBaslangicdateTimePicker.Value;
                    izin.IzinBitisTarihi = IzinBitisdateTimePicker.Value;
                    izin.IzinTuru = izinTuru;



                    izinBilgisiService.Delete(izin);
                    izinBilgisiService.SaveChanges();

                    MessageBox.Show("izin bilgisi silindi.");



                }
                else
                {
                    MessageBox.Show("seçim yapýlmadý");
                }
            }
        }

        private void IzinBilgileriGoruntulebutton_Click(object sender, EventArgs e)
        {
            string tcNo = TCGiristextBox11.Text;
            IEnumerable<Personel> personeller = personelService.GetAll().Where(p => p.TcNo == tcNo);
            var izinBilgileri = from Personel in personelService.GetAll()
                                join izin in izinBilgisiService.GetAll()
                                on Personel.Id equals izin.PersonelId
                                where Personel.TcNo == tcNo
                                select new
                                {
                                    izinId = izin.Id,
                                    BaslangicTarihi = izin.IzinBaslangicTarihi,
                                    BitisTarihi = izin.IzinBitisTarihi,
                                    IzinTuru = izin.IzinTuru
                                };

            if (izinBilgileri.Any())
            {
                IzinlistBox.Items.Clear(); // ListBox'ý temizleyin

                foreach (var izin in izinBilgileri)
                {
                    string izinBilgisi = $"{izin.izinId} - {izin.BaslangicTarihi?.ToShortDateString()} - {izin.BitisTarihi?.ToShortDateString()} - {izin.IzinTuru}";
                    IzinlistBox.Items.Add(izinBilgisi);
                }
            }
            else
            {
                MessageBox.Show("Personelin izin bilgisi bulunamadý.");
            }
        }
        private void BordroBilgileriGetirbutton_Click(object sender, EventArgs e)
        {

        }

        private void OkulBilgileriGoruntulebutton_Click(object sender, EventArgs e)
        {
            // TC kimlik numarasýný alýn (TCtextBox'ten)
            string tcNo = TCtextBox.Text;

            // Personel hizmetini kullanarak ilgili personeli bulun
            IEnumerable<Personel> personeller = personelService.GetAll().Where(p => p.TcNo == tcNo);

            var OkulBilgileri = from Personel in personelService.GetAll()
                                join egitim in egitimBilgisiService.GetAll() on Personel.Id equals egitim.PersonelId
                                join Okul in okulService.GetAll() on egitim.Id equals Okul.EgitimBilgisiId
                                where Personel.TcNo == tcNo
                                select new
                                {
                                    okulId = Okul.Id,
                                    Okuladi = Okul.OkulAdi,
                                    Derecesi = Okul.MezuniyetDerecesi,
                                    Btarihi = Okul.MezuniyetTarihi,
                                    Tipi = Okul.EgitimTipleri,
                                };


            if (OkulBilgileri.Any())
            {
                OkullistBox.Items.Clear(); // ListBox'ý temizleyin

                foreach (var okul in OkulBilgileri)
                {
                    string OkulBilgisi = $" {okul.okulId}  : {okul.Okuladi}   : {okul.Btarihi.ToShortDateString()} : {okul.Derecesi}  :{okul.Tipi}";
                    OkullistBox.Items.Add(OkulBilgisi);
                }
            }
            else
            {
                MessageBox.Show("Personelin okul bilgisi bulunamadý.");
            }


        }

        private void SirketEgitimBilgileriGetirbtn_Click(object sender, EventArgs e)
        {
            // TC kimlik numarasýný alýn (TCtextBox'ten)
            string tcNo = TCtextBox.Text;

            // Personel hizmetini kullanarak ilgili personeli bulun
            IEnumerable<Personel> personeller = personelService.GetAll().Where(p => p.TcNo == tcNo);

            var SirketEgitimBilgileri = from Personel in personelService.GetAll()
                                        join egitim in egitimBilgisiService.GetAll() on Personel.Id equals egitim.PersonelId
                                        join SirketIciEgitim in sirketIciEgitimService.GetAll() on egitim.Id equals SirketIciEgitim.EgitimBilgisiId
                                        where Personel.TcNo == tcNo
                                        select new
                                        {
                                            EId = SirketIciEgitim.Id,
                                            Atarihi = SirketIciEgitim.AlinmaTarihi,
                                            EAdi = SirketIciEgitim.EgitimAdi,
                                        };


            if (SirketEgitimBilgileri.Any())
            {
                SirketiciegitimlistBox.Items.Clear(); // ListBox'ý temizleyin

                foreach (var egitim in SirketEgitimBilgileri)
                {
                    string OkulBilgisi = $" {egitim.EId}  - {egitim.Atarihi}   -  {egitim.EAdi}  ";
                    SirketiciegitimlistBox.Items.Add(OkulBilgisi);
                }
            }
            else
            {
                MessageBox.Show("Personelin okul bilgisi bulunamadý.");
            }

        }

        private void SertifikaBilgileriGetirbutton_Click(object sender, EventArgs e)
        {
            SertifikalistBox.Items.Clear();
            // TC kimlik numarasýný alýn (TCtextBox'ten)
            string tcNo = TCtextBox.Text;

            // Personel hizmetini kullanarak ilgili personeli bulun
            IEnumerable<Personel> personeller = personelService.GetAll().Where(p => p.TcNo == tcNo);

            var SertifikaBilgileri = from Personel in personelService.GetAll()
                                     join egitim in egitimBilgisiService.GetAll() on Personel.Id equals egitim.PersonelId
                                     join sertifika in SertifikaService.GetAll() on egitim.Id equals sertifika.EgitimBilgisiId
                                     where Personel.TcNo == tcNo
                                     select new
                                     {
                                         SertifikaId = sertifika.Id,
                                         Kurumadi = sertifika.AlinanKurum,
                                         SetifikaAdi = sertifika.SertifikaAdi,
                                         Btarihi = sertifika.SertifikaTarihi,
                                     };


            if (SertifikaBilgileri.Any())
            {
                OkullistBox.Items.Clear(); // ListBox'ý temizleyin

                foreach (var sertifika in SertifikaBilgileri)
                {
                    string OkulBilgisi = $" {sertifika.SertifikaId}  - {sertifika.Kurumadi}   - {sertifika.Btarihi.ToShortDateString()} - {sertifika.SetifikaAdi}";
                    SertifikalistBox.Items.Add(OkulBilgisi);
                }
            }
            else
            {
                MessageBox.Show("Personelin okul bilgisi bulunamadý.");
            }

        }

        private void BilgileriGörüntülebtn_Click(object sender, EventArgs e)
        {
            string tcNo = TCGiristextBox11.Text;
            IEnumerable<Personel> personeller = personelService.GetAll().Where(p => p.TcNo == tcNo);

            var ozlukBilgileri = from Personel in personelService.GetAll()
                                 join ozluk in ozlukBilgisiService.GetAll()
                                 on Personel.Id equals ozluk.PersonelId
                                 where Personel.TcNo == tcNo
                                 select new
                                 {
                                     ÝkamettextBox = ozluk.IkametAdresi
                                 };
            var maasBilgileri = from Personel in personelService.GetAll()
                                join maas in maasBilgisiService.GetAll()
                                on Personel.Id equals maas.PersonelId
                                where Personel.TcNo == tcNo
                                select new
                                {
                                    MaastextBox = maas.MaasMiktar,
                                    MaascomboBox = maas.OdemeTarihi.ToString(),
                                };

            var personel1 = personelService.GetAll().FirstOrDefault(p => p.TcNo == tcNo);
            if (personeller.Any())
            {
                Personel? personel = personeller.FirstOrDefault(); // Ýlk kiþiyi alýyoruz, gerekirse uygun kiþiyi seçmelisiniz

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
                IseGirisTarihidateTimePicker.Text = personel.BaslangicTarihi.ToString();
                Personelpanel1.BringToFront();
                Personelpanel1.Visible = true;
            }
            else
            {
                MessageBox.Show("Personel bulunamadý. Solda bulunan  (Personel) butonundan ekleme yapabilirsiniz tekrardan (Bakýlacak T.C) butonu ile sorgu çekebilirsiniz.");
            }
            if (ozlukBilgileri.Any())
            {
                foreach (var ozluk in ozlukBilgileri)
                {
                    string ozlukBilgisi = ozluk.ÝkamettextBox;
                    ÝkamettextBox.Text = ozlukBilgisi;
                }

            }
            else
            {

            }
            if (maasBilgileri.Any())
            {
                foreach (var maas in maasBilgileri)
                {
                    string maasTarih = maas.MaascomboBox.ToString();
                    string maasBilgi = maas.MaastextBox.ToString();
                    MaastextBox.Text = maasBilgi;
                    OdemeTarihitextBox.Text = maasTarih;

                }
            }
            else
            {

            }

        }
        private void Personelbutton_Click(object sender, EventArgs e)
        {
            Personelpanel1.BringToFront();
            Personelpanel1.Visible = true;
        }

        private void Ozlukbutton_Click(object sender, EventArgs e)
        {
            Ozlukpanel.BringToFront();
            Ozlukpanel.Visible = true;
        }

        private void okulbutton_Click(object sender, EventArgs e)
        {
            Okulpanel.BringToFront();
            Okulpanel.Visible = true;
        }

        private void Maasbutton_Click(object sender, EventArgs e)
        {
            Maaspanel.BringToFront();
            Maaspanel.Visible = true;
        }

        private void Ýzinbutton_Click(object sender, EventArgs e)
        {
            Ýzinpanel.BringToFront();
            Ýzinpanel.Visible = true;
        }

        private void SirketEgitimbutton_Click(object sender, EventArgs e)
        {
            SirketIciEgitimpanel.BringToFront();
            SirketIciEgitimpanel.Visible = true;
        }

        private void Sertifikabutton_Click(object sender, EventArgs e)
        {
            Sertifikapanel.BringToFront();
            Sertifikapanel.Visible = true;
        }

        private void TCGetirbutton_Click(object sender, EventArgs e)
        {
            bilgilerigetirpnl.BringToFront();
            bilgilerigetirpnl.Visible = true;
        }
    }
}


