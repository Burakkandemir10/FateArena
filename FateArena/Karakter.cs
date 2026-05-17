using System;
using System.IO;

namespace FateArena
{
    public abstract class Karakter
    {

        private static int toplamKarakter = 0;
        public  static int ToplamKarakter { get => toplamKarakter; }

        public int SiraNumarasi { get; private set; }


        private string ad;
        private int    can;
        private int    maxCan;    
        private int    guc;
        private int    savunma;


        protected Karakter(string ad, int can, int guc, int savunma)
        {
            toplamKarakter++;               
            this.SiraNumarasi = toplamKarakter; 

            this.Ad      = ad;              
            this.maxCan  = can;             
            this.Can     = can;            
            this.Guc     = guc;
            this.Savunma = savunma;

            LogYaz($"[KARAKTER OLUŞTU] {ad} | Sıra: {SiraNumarasi} | "
                 + $"Can:{can} Güç:{guc} Savunma:{savunma}");
        }

 
        public string Ad
        {
            get => ad;
            private set => ad = string.IsNullOrWhiteSpace(value) ? "İsimsiz" : value;
        }

       
        public int Can
        {
            get { return can; }
            set
            {

                can = Math.Max(0, Math.Min(maxCan, value));
            }
        }

        public int MaxCan { get => maxCan; }

        public int Guc
        {
            get { return guc; }
            set { guc = Math.Max(1, value); }
        }

        public int Savunma
        {
            get { return savunma; }
            set { savunma = Math.Max(0, value); }
        }


        public abstract int HasarHesapla(double zarSonucu);


        public virtual string Bilgi()
        {
            string canBar = CanBariOlustur();
            return $"  ┌─ {Ad} (#{SiraNumarasi}) ─────────────────\n"
                 + $"  │  Can     : {canBar} {Can}/{MaxCan}\n"
                 + $"  │  Güç     : {Guc}\n"
                 + $"  │  Savunma : {Savunma}";
        }


        public bool HayattaMi() => can > 0;


        public int HasarAl(int hasar)
        {
            int netHasar = Math.Max(1, hasar - Savunma);
            Can -= netHasar;  

            if (!HayattaMi())
                LogYaz($"[ÖLÜM] {Ad} savaşta hayatını kaybetti.");

            return netHasar; 
        }


        protected void LogYaz(string mesaj)
        {
            try
            {
                using FileStream   fs = new FileStream(
                    AppContext.BaseDirectory + "FateArena_Log.txt", FileMode.Append);
                using StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] {mesaj}");
            }
            catch { /* Log yazılamazsa oyun durmasın */ }
        }

        private string CanBariOlustur()
        {
            int dolu  = (int)Math.Round((double)can / maxCan * 10);
            int bos   = 10 - dolu;
            return "[" + new string('█', dolu) + new string('░', bos) + "]";
        }
    }
}
