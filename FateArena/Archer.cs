using System;

namespace FateArena
{

    public class Archer : Karakter
    {
        private int okSayisi;

        public int OkSayisi
        {
            get { return okSayisi; }
            set { okSayisi = Math.Max(0, value); } 
        }


       
        public Archer(string ad)
            : base(ad, can: 95, guc: 28, savunma: 8)
        {
            this.OkSayisi = 12;
        }


       
        public override int HasarHesapla(double zarSonucu)
        {
            if (OkSayisi > 0)
            {
                OkSayisi--; 
                
                int okHasari = Guc + (int)(zarSonucu * 2.0);
                return okHasari;
            }
            else
            {
               
                return (int)(zarSonucu * 0.4) + 1;
            }
        }



        public override string Bilgi()
        {
            return base.Bilgi()
                 + $"\n  │  Ok      : {OkSayisi} adet"
                 + $"\n  └─ Tür    : 🏹 Archer (Dengeli)";
        }
    }
}

