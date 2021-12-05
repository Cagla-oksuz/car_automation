/*****************************************************************
*
*                        SAKARYA ÜNİVERSİTESİ
*          HAŞİM GÜRDAMAR BİLİŞİM VE BİLGİSAYAR BİLİMLERİ FAKÜLTESİ
*                       BİLGİSAYAR MĞHENDİSLİĞİ
*                 3.NESNE YÖNELİMLİ PROGRAMLAMA ÖDEVİ
* 
*             
*             Öğrenci Adı: Çağla Nur ÖKSÜZ
*             Dersin Alındığı Sınıf: 1-A
*             Öğrenci Numarası: G161210076
* 
* 
* 
* 
* **************************************************************************/



using System;

namespace Odev_3
{

    class ArabaOtomasyonu
    {

        public int HizlanmaMiktari = 0;
        string Motortürü;
        public int TekerlekAcısı = 0;
        public enum ArabaDurumu { Calisiyor, Calismiyor }
        public enum MotorDurumu { Calisiyor, Calismiyor }
        public enum KontakAnahtariDurumu { Acik, Kapali }
        public enum GazPedaliDurumu { Basildi, Basilmadi }
        public enum FrenPedaliDurumu { Basildi, Basilmadi }
        public enum FarlarDurumu { KısaAcik, UzunAcik, Kapali}
        public enum SinyallerinDurumu {  Kapali, SolSinyalAcik, SagSinyalAcik, DortluAcik}

      
        public class ElektronikBeyin
        {
            private readonly KontakAnahtari _kontakAnahtari;
            private readonly Motor _motor;
            private readonly GazPedali _gazPedali;
            private readonly FrenPedali _frenPedali;
            private readonly FarKumandaKolu _farKumandaKolu;
            private readonly SinyalKumandaKolu _sinyalKumandaKolu;

            public string Motortürü { get; private set; }
            public KontakAnahtariDurumu Durum { get; private set; }
         


            public ElektronikBeyin(KontakAnahtari kontakAnahtari, Motor motor)
            {
                
                _kontakAnahtari = kontakAnahtari;
                _motor = motor;

                // Kontağın açılması olayı gerçekleştiğinde KontakAcildi metodu otomatik olarak çalışsın
                _kontakAnahtari.KontakAcildiEventHandler += KontakAcildi;

                // Gaza basılması olayı gerçekleştiğinde GazaBasildi metodu otomatik olarak çalışsın
                _gazPedali.GazaBasildiEventHandler += GazaBasildi;

                // Frene basılması olayı gerçekleştiğinde FreneBasildi metodu otomatik olarak çalışsın
                _frenPedali.FreneBasildiEventHandler += FreneBasildi;
               
                _farKumandaKolu.FarlarKapaliEventHandler += FarlarKapali;

                _farKumandaKolu.KisaFarlarAcikEventHandler += KisaFarlarAcik;

                _farKumandaKolu.UzunFarlarAcikEventHandler += UzunFarlarAcik;

                _sinyalKumandaKolu.SinyallerKapaliEventHandler += Kapali;

                _sinyalKumandaKolu.SagSinyallerAcikEventHandler += SagSinyalAcik;

                _sinyalKumandaKolu.SolSinyallerAcikEventHandler += SolSinyalAcik;

                _sinyalKumandaKolu.DortluSinyalAcikEventHandler += DortluSinyalAcik;


            }

            // Kontak açıldığında bu metot otomatik olarak çalışacaktır.
            private void KontakAcildi(object sender, EventArgs e)
            {
                // Motor zaten çalışıyorsa devam et
                if (_motor.Durum == MotorDurumu.Calisiyor) return;

                // Motoru çalıştır
                _motor.Calistir();
            }

            private void GazaBasildi(object sender, EventArgs e)
            {
                // Motor zaten çalışıyorsa devam et
                if (_motor.Durum == MotorDurumu.Calisiyor)
                {
                    //gaza basıldı
                    if (_gazPedali.Durumu == GazPedaliDurumu.Basildi)
                    {

                        // basildi metodu çağırılır
                        _gazPedali.Basildi();
                        //motor türüne göre hızdaki değişimi hesaplayan metod çağırılır
                        _gazPedali.HizdakiDegişim(Motortürü);
                    }
                }
            }

            private void FreneBasildi(object sender, EventArgs e)
            {
                // Motor zaten çalışıyorsa devam et
                if (_motor.Durum == MotorDurumu.Calisiyor)
                {
                    //frene basıldı
                    if (_frenPedali.Durumu == FrenPedaliDurumu.Basildi) return;
                    {
                        // basildi metodu çağırılır
                        _frenPedali.Basildi();
                        //motor türüne göre hızdaki değişimi hesaplayan metod çağırılır
                        _frenPedali.HizdakiDegişim(Motortürü);
                    }
                }
            }

            private void FarlarKapali(object sender, EventArgs e)
            {

                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    //farı kapatmak için gerekli metod çağırılır
                    _farKumandaKolu.FarlarKapalı();
                }
            }

            private void KisaFarlarAcik(object sender, EventArgs e)
            {
                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    // Kısafarı Açmak için gerekli metod çağırılır
                    _farKumandaKolu.KısaFarAcik();
                }
            }

            private void UzunFarlarAcik(object sender, EventArgs e)
            {

                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    // UzunfarıAçmak için gerekli metod çağırılır
                    _farKumandaKolu.UzunFarAcik();
                }
            }

            private void Kapali(object sender, EventArgs e)
            {

                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    //sinyalleri kapatmak için gerekli metod çağırılır
                    _sinyalKumandaKolu.Kapali();
                }
            }

            private void SolSinyalAcik(object sender, EventArgs e)
            {

                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    // Solsinyal vermek için gerekli metod çağırılır
                    _sinyalKumandaKolu.SolSinyalAcik();
                }
            }

            private void SagSinyalAcik(object sender, EventArgs e)
            {

                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    // sağ sinyal vermek için gerekli metod çağırılır
                    _sinyalKumandaKolu.SagSinyalAcik();
                }
            }

            private void DortluSinyalAcik(object sender, EventArgs e)
            {

                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    // dörtlüleri yakmak için gerekli metod çağırılır
                    _sinyalKumandaKolu.DortluSinyalAcik();
                }
            }
        }



        public abstract class Motor
        {
            public MotorDurumu Durum { get; private set; }
            // public VitesVarMi VitesDurum { get; }
            public int HizlanmaMiktari { get; set; }

            //otomasyon ilk başladığında vitesli veya vitessiz olarak seçildiği var sayılır
            protected Motor()
            {
                // Motorun başlangıç durumu çalışmıyor olarak ayarlanıyor
                Durum = MotorDurumu.Calismiyor;
            }

            public void Calistir()
            {
                // Motor zaten çalışıyorsa devam etme
                if (Durum == MotorDurumu.Calisiyor) return;
                //Motor çalışmıyorsa çalıştırma
                if (Durum == MotorDurumu.Calismiyor)
                {
                    Durum = MotorDurumu.Calisiyor;
                }
                // Bu satır test amaçlı olarak yazılmıştır
                Console.WriteLine("Motor çalıştı.");
            }

        }

        //BenzinliMotor sınıfı Motor sınıfından kalıtım alır
        public class BenzinliMotor : Motor
        {
            public BenzinliMotor()
            {
                HizlanmaMiktari = 10;
            }
        }

        //DizelMotor sınıfı Motor sınıfından kalıtım alır
        public class DizelMotor : Motor
        {
            public DizelMotor()
            {
                HizlanmaMiktari = 8;
            }
        }


        public class KontakAnahtari
        {
            // Kontak anahtarının durumu için salt okunur (read-only) bir özellik yazılıyor
            public KontakAnahtariDurumu Durum { get; private set; }

            // Kontak açıldığında tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler KontakAcildiEventHandler;

            public KontakAnahtari()
            {
                // Kontak anahtarının başlangıç durumu kapalı olarak ayarlanıyor
                Durum = KontakAnahtariDurumu.Kapali;
            }

            public void Ac()
            {
                // Kontak zaten açıksa devam etme
                if (Durum == KontakAnahtariDurumu.Acik) return;

                // Kontağı açık konuma getir
                Durum = KontakAnahtariDurumu.Acik;

                // Bu satır test amaçlı olarak yazılmıştır
                Console.WriteLine("Kontak açıldı.");

                // Kontağın açılması olayını tetikle
                if (KontakAcildiEventHandler == null) return;
                KontakAcildiEventHandler(this, new EventArgs());
            }
        }

        //pedal sınıfını interface olarak tanımladım
        public interface IPedal
        {
            int HizdakiDegişim(string MotorTürü);
        }

        //GazPedali sınıfı IPedal interfaz sınıfından kalıtım alır
        public class GazPedali : IPedal
        {
            //Gaz pedalinın durumunu öğrenmek ve değiştirmek için 
            public GazPedaliDurumu Durumu { get; set; }
            //Motorun durumunu öğrenmek için
            public MotorDurumu Durum { get; }
            //Hız miktarını öğrenmek ve değiştirmek için
            public int HizlanmaMiktari { get; set; }
            // Gaza basıldığında tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler GazaBasildiEventHandler;


            //Ben simulasyon başlamadan önce kullanıcının MotorTürü seçtiğini varsayark bir tasarım yaptım.
            //Kullanıcı motor türünü dizel veya benzinli olarak seçer ve seçimi parametre olarak verilir
            //verilen parametre göre gazabasıldığında hızmiktarındaki değişim hesaplanır
            public int HizdakiDegişim(string MotorTürü)
            {
                //Motor çalışmıyor ise durumunu korur
                if (Durum == MotorDurumu.Calismiyor) return 0;

                //Motor çalışıyor ise motor çeşidine göre hız miktarı artırılır
                if (Durum == MotorDurumu.Calisiyor)
                {
                    //Motor türü dizel ise hızı +10 artırır
                    if (MotorTürü == "Dizel")
                    {//Eğer hizMiktarini 10 artırırsam 220den küçük mü kontrolu yapıyorum
                        if (HizlanmaMiktari + 10 <= 220) HizlanmaMiktari = +10;
                    }
                    //Motor türü dizel ise hızı +8 artırır
                    if (MotorTürü == "Benzinli")
                    {//Eğer hizMiktarini 8 artırırsam 220den küçük mü kontrolu yapıyorum
                        if (HizlanmaMiktari + 8 <= 220) HizlanmaMiktari = +8;
                    }
                }
                //Gaz miktarini döner
                return HizlanmaMiktari;
            }

         
            public void Basildi() 
            {
                //Motor çalışmıyor ise durumunu korur
                if (Durum == MotorDurumu.Calismiyor) return;

                //Motor çalışıyor ise
                if (Durum == MotorDurumu.Calisiyor)
                {
                    //Gaz pedali basıldı durumuna getir
                    Durumu = GazPedaliDurumu.Basildi;

                    // Gaza basılması olayını tetikle
                    if (GazaBasildiEventHandler == null) return;
                    GazaBasildiEventHandler(this, new EventArgs());
                }
            }
        }


        //FrenPedali sınıfı IPedal interfaz sınıfından kalıtım alır
        public class FrenPedali : IPedal
        {
            //Motorun durumunu öğrenmek için
            public MotorDurumu Durum { get; }
            //Hız miktarını öğrenmek ve değiştirmek için
            public int HizlanmaMiktari { get; set; }
            //Fren pedalinın durumunu öğrenmek ve değiştirmek için 
            public FrenPedaliDurumu Durumu { get; set; }
            // Frene basıldığında tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler FreneBasildiEventHandler;



            public int HizdakiDegişim(string MotorTürü)
            {
                //Motor çalışmıyor ise durumunu korur
                if (Durum == MotorDurumu.Calismiyor) return 0;

                //Motor çalışıyor ise motor çeşidine göre hız miktarı artırılır
                if (Durum == MotorDurumu.Calisiyor)
                {
                    //Motor türü dizel ise hızı -10 azaltır
                    if (MotorTürü == "Dizel")
                    {
                        //Hizmiktari 10'dan büyükse hızıMiktarını 10 azaltır
                        if (HizlanmaMiktari > 10) HizlanmaMiktari = -10;
                        //Hizmiktari 10'dan küçükse hızıMiktarını 0'a eşit olur
                        if (HizlanmaMiktari < 10) HizlanmaMiktari = 0;
                    }

                    //Motor türü dizel ise hızı -10 azaltır
                    if (MotorTürü == "Benzinli")
                    {
                        //Hizmiktari 10'dan büyükse hızıMiktarını 10 azaltır
                        if (HizlanmaMiktari > 10) HizlanmaMiktari = -10;
                        //Hizmiktari 10'dan küçükse hızıMiktarını 0'a eşit olur
                        if (HizlanmaMiktari < 10) HizlanmaMiktari = 0;
                    }
                }
                //Gaz miktarini döner
                return HizlanmaMiktari;
            }

          
            public void Basildi()
            {
                //Motor çalışmıyor ise durumunu korur
                if (Durum == MotorDurumu.Calismiyor) return;

                //Motor çalışıyor ise
                if (Durum == MotorDurumu.Calisiyor)
                {
                    //Fren pedali basıldı durumuna getir
                    Durumu = FrenPedaliDurumu.Basildi;

                    // Frene basılması olayını tetikle
                    if (FreneBasildiEventHandler == null) return;
                    FreneBasildiEventHandler(this, new EventArgs());
                }

            }
        }

        public interface ITekerler
        {
            void SagaCevir();
            void SolCevir();
            void DüzCevir();
        }

        //Direksiyon sınıfı ITekerler sınıfından kalıtım alır
        public class Direksiyon : ITekerler
        {
            //Motorun durumunu öğrenmek için
            public MotorDurumu Durum { get; }
            //TekerlekAcısı öğrenmek ve değiştirmek için
            public int TekerlekAcısı { get; private set; }       
            // Sola cevirilme olayını tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler SolaCevirildiEventHandler;
            // Saga cevirilme olayını tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler SagaCevirildiEventHandler;
            //Düz cevirilme olayını tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler DüzCevirildiEventHandler;


            //Kurucu metod
            protected Direksiyon()
            {
                // Tekerleklerin başlangıç açısını sıfır olarak ayarlıyor
                TekerlekAcısı = 0;
            }

          
            
            public void SagaCevir()
            {
                //Motor çalışmıyor ise durumunu korur
                if (Durum == MotorDurumu.Calismiyor) return;


                //Motor çalışıyor ise motor çeşidine göre hız miktarı artırılır
                if (Durum == MotorDurumu.Calisiyor)
                {
                    if (TekerlekAcısı < 40) TekerlekAcısı = TekerlekAcısı + 5;

                    // SagaCevirildi olayını tetikle
                    if (SagaCevirildiEventHandler == null) return;
                    SagaCevirildiEventHandler(this, new EventArgs());
                }
            }

         

            public void SolCevir()
            {
                //Motor çalışmıyor ise durumunu korur
                if (Durum == MotorDurumu.Calismiyor) return;


                //Motor çalışıyor ise motor çeşidine göre hız miktarı artırılır
                if (Durum == MotorDurumu.Calisiyor)
                {
                    if (TekerlekAcısı > -40 ) TekerlekAcısı = TekerlekAcısı - 5;

                    // SolaCevirildi olayını tetikle
                    if (SolaCevirildiEventHandler == null) return;
                    SolaCevirildiEventHandler(this, new EventArgs());
                }
            }
       
            public void DüzCevir()
            {
                //Motor çalışmıyor ise durumunu korur
                if (Durum == MotorDurumu.Calismiyor) return;


                //Motor çalışıyor ise motor çeşidine göre hız miktarı artırılır
                if (Durum == MotorDurumu.Calisiyor)
                {
                    TekerlekAcısı = 0;

                    //DüzCevirildi olayını tetikle
                    if (DüzCevirildiEventHandler == null) return;
                    DüzCevirildiEventHandler(this, new EventArgs());
                }
            }
        }

    

        public class HizGostergesi
        {
            // Kontak anahtarının durumu için salt okunur (read-only) bir özellik yazılıyor
            public KontakAnahtariDurumu Durum { get; }

            // Kontak anahtarının durumu için okunur
            public int HizlanmaMiktari { get; }

            //Kurucu metod
            public HizGostergesi()
            {
                //KontakAnahtarİ açık ise
                if (Durum == KontakAnahtariDurumu.Acik)
                {//hız miktarını yazdırdım
                    Console.Write(" Hız: "+HizlanmaMiktari);
                }
            }
        }

        //interface olarak IFarlar ı tanımladım
        public interface IFarlar
        {
            void KısaFarAcik();
            void UzunFarAcik();
            void FarlarKapalı();
        }

        //IFarlar interfacesinden FarKumanda koluna kalıtım verdim
        public class FarKumandaKolu : IFarlar
        {
            // Kontak anahtarının durumu için salt okunur (read-only) bir özellik yazılıyor
            public KontakAnahtariDurumu Durum { get;  }
            //far durumunu hem okuyor hem yazıyorum
            public FarlarDurumu Durumu1 { get;  set;  }
            //FarlarKapali olayını tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler FarlarKapaliEventHandler;
            //KisaFarlarAcik olayını tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler KisaFarlarAcikEventHandler;
            //UzunFarlarAcik olayını tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler UzunFarlarAcikEventHandler;


            //kurucumetod
            public FarKumandaKolu()
            {
                //farların kapalı durum başlamasının sağlıyorum
                Durumu1 = FarlarDurumu.Kapali;
            }

          
            public void FarlarKapalı()
            {
                // Kontak zaten açıksa devam etme
                if (Durum == KontakAnahtariDurumu.Kapali) return;
                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    // Farlar durumunu kapalı konumuna getir
                    Durumu1 = FarlarDurumu.Kapali;

                    // farlarKapali asılması olayını tetikle
                    if (FarlarKapaliEventHandler == null) return;
                    FarlarKapaliEventHandler(this, new EventArgs());
                }
            }

           
            public void KısaFarAcik()
            {
                // Kontak zaten açıksa devam etme
                if (Durum == KontakAnahtariDurumu.Kapali) return;
                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    // Farları kısa açık durumuna getir
                    Durumu1 = FarlarDurumu.KısaAcik;

                    // KisaFarlarAcik olayını tetikle
                    if (KisaFarlarAcikEventHandler == null) return;
                    KisaFarlarAcikEventHandler(this, new EventArgs());
                }
            }

           
            public void UzunFarAcik()
            {
                // Kontak zaten açıksa devam etme
                if (Durum == KontakAnahtariDurumu.Kapali) return;
                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    // Farları Uzun açık durumuna getir
                    Durumu1 = FarlarDurumu.UzunAcik;


                    // UzunFarlarAcik olayını tetikle
                    if (UzunFarlarAcikEventHandler == null) return;
                    UzunFarlarAcikEventHandler(this, new EventArgs());
                }
            }
        }

        //interface olarak ISinyalLambaları ı tanımladım
        public interface ISinyalLambaları
        {
            void Kapali();
            void SolSinyalAcik();
            void SagSinyalAcik();
            void DortluSinyalAcik();

        }

        //ISinyalLambalarıinterfacesinden SinyalKumandaKoluna kalıtım verdim
        public class SinyalKumandaKolu : ISinyalLambaları
        {
            // Kontak anahtarının durumu için salt okunur (read-only) bir özellik yazılıyor
            public KontakAnahtariDurumu Durum { get; }
            //SinyallerinDurumu okumak ve yazmak için 
            public SinyallerinDurumu Durumu1 { get; private set; }
            //SinyallerKapali olayını tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler SinyallerKapaliEventHandler;
            //SolSinyallerAcik olayını tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler SolSinyallerAcikEventHandler;
            //Düz cevirilme olayını tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler SagSinyallerAcikEventHandler;
            // DortluSinyalAcik olayını tetiklenecek olay için event handler tanımlanıyor
            public event EventHandler DortluSinyalAcikEventHandler;


            public void Kapali()
            {
                // Kontak zaten açıksa devam etme
                if (Durum == KontakAnahtariDurumu.Kapali) return;
                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    //sinyalleri kapali duruma getirir
                    Durumu1 = SinyallerinDurumu.Kapali;

                    // SinyallerKapali olayını tetikle
                    if (SinyallerKapaliEventHandler == null) return;
                    SinyallerKapaliEventHandler(this, new EventArgs());
                }
            }

           
            public void SolSinyalAcik()
            {
                // Kontak zaten açıksa devam etme
                if (Durum == KontakAnahtariDurumu.Kapali) return;
                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {

                    //SolSinyalleri açık duruma getirir
                    Durumu1 = SinyallerinDurumu.SolSinyalAcik;

                 // SolSinyallerAcik olayını tetikle
                    if (SolSinyallerAcikEventHandler == null) return;
                    SolSinyallerAcikEventHandler(this, new EventArgs());
                }

            }

            public void SagSinyalAcik()
            {
                // Kontak zaten açıksa devam etme
                if (Durum == KontakAnahtariDurumu.Kapali) return;
                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    //SagSinyalAcik açık duruma getirir
                    Durumu1 = SinyallerinDurumu.SagSinyalAcik;

                    //SagSinyallerAcik olayını tetikle
                    if (SagSinyallerAcikEventHandler == null) return;
                    SagSinyallerAcikEventHandler(this, new EventArgs());
                }
            }

         
            public void DortluSinyalAcik()
            {
                // Kontak zaten açıksa devam etme
                if (Durum == KontakAnahtariDurumu.Kapali) return;
                // Kontak zaten açıksa devam et
                if (Durum == KontakAnahtariDurumu.Acik)
                {
                    //DortluAcik açık duruma getirir
                    Durumu1 = SinyallerinDurumu.DortluAcik;

                    //DortluSinyalAcik olayını tetikle
                    if (DortluSinyalAcikEventHandler == null) return;
                    DortluSinyalAcikEventHandler(this, new EventArgs());
                }
            }
        }
    }
}
 

