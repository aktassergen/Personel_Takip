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

            string dogumText = DogumTtextBox.Text; // TextBox'tan gelen tarih giri�i

            bool success = DateTime.TryParseExact(dogumText, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate);

            string tcNo = TCtextBox.Text;

            // Veritaban�nda bu TcNo'ya sahip ba�ka bir kay�t var m� kontrol et
            bool isTcNoExists = personelService.GetAll().Any(p => p.TcNo == tcNo);

            if (success && !isTcNoExists)
            {
                // Ba�ar�l� bir d�n���m ger�ekle�ti, parsedDate art�k do�ru tarih de�erini i�erir.
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
                MessageBox.Show("ekleme ba�ar�l� �leriye bas�n�z ");
            }
            else
            {
                // Ba�ar�s�z bir d�n���m oldu, kullan�c� uygun formatta bir tarih giri�i yapmad�.
                MessageBox.Show("Ge�ersiz tarih format�. L�tfen dd/MM/yyyy format�nda bir tarih girin. Veya Bu TcNo ile zaten bir kay�t mevcut ");
            }


        }
    }
}