using System;

namespace FateArena
{

    public class Warrior : Karakter
    {

        public int KalkanBonusu { get; private set; }


        public Warrior(string ad)
            : base(ad, can: 120, guc: 22, savunma: 12)
            
        {
            this.KalkanBonusu = 8; 
        }


        public override int HasarHesapla(double zarSonucu)
        {

            int temelHasar = Guc + (int)(zarSonucu * 0.8);
            return temelHasar;
        }


        public override string Bilgi()
        {
            return base.Bilgi()                                    
                 + $"\n  │  Kalkan  : +{KalkanBonusu} savunma bonusu"
                 + $"\n  └─ Tür    : ⚔️  Warrior (Tank)";
        }
    }
}
