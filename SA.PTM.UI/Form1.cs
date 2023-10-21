using Microsoft.VisualBasic.ApplicationServices;
using SA.PTM.BLL.Services;
using SA.PTM.DAL.Concrete;
using SA.PTM.DAL.Context;
using SA.PTM.Entity.Concrete;
using System;
using System.Globalization;

namespace SA.PTM.UI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        private void PersonelEklebutton_Click(object sender, EventArgs e)
        {
            IPersonelService personelService = new PersonelManager(new DAL.Concrete.BaseRepo<Entity.Concrete.Personel>(new PersonelTakipDbContext()));

            string dogumText = DogumTtextBox.Text; // TextBox'tan gelen tarih giriþi

            bool success = DateTime.TryParseExact(dogumText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate);

            string tcNo = TCtextBox.Text;

            // Veritabanýnda bu TcNo'ya sahip baþka bir kayýt var mý kontrol et
            bool isTcNoExists = personelService.GetAll().Any(p => p.TcNo == tcNo);

            if (success && !isTcNoExists)
            {
                // Baþarýlý bir dönüþüm gerçekleþti, parsedDate artýk doðru tarih deðerini içerir.
                // parsedDate'i kullanabilirsiniz.
                personelService.Insert(new Personel()
                {
                    Ad = AdtextBox.Text,
                    Anne = AnnetextBox.Text,
                    Baba = BabatextBox.Text,
                    Departman = DepartmantextBox.Text,
                    DogumTarihi = parsedDate,
                    MedeniDurumu = MedeniDtextBox.Text,
                    DogumYeri = DogumYtextBox.Text,
                    TcNo = TCtextBox.Text,
                    Rol = RoltextBox.Text,
                    YasadigiSehir = SehirtextBox.Text,
                    Soyad = SoyadtextBox.Text,
                });
                MessageBox.Show("ekleme baþarýlý Ýleriye basýnýz ");
            }
            else
            {
                // Baþarýsýz bir dönüþüm oldu, kullanýcý uygun formatta bir tarih giriþi yapmadý.
                MessageBox.Show("Geçersiz tarih formatý. Lütfen dd/MM/yyyy formatýnda bir tarih girin. Veya Bu TcNo ile zaten bir kayýt mevcut ");
            }


        }
    }
}