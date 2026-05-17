using System;

namespace FateArena
{

    public class Mage : Karakter
    {

        private int mana;

        public int Mana
        {
            get { return mana; }
            set { mana = Math.Max(0, Math.Min(100, value)); }

        }



        public Mage(string ad)
            : base(ad, can: 75, guc: 35, savunma: 4)
        {
            this.Mana = 100; 
        }



        public override int HasarHesapla(double zarSonucu)
        {
            if (Mana >= 15)
            {
                Mana -= 15; 
                int buyuHasari = Guc + (int)(zarSonucu * 1.5);
                return buyuHasari;
            }
            else
            {
                return (int)(zarSonucu * 0.5) + 2;
            }
        }

        public override string Bilgi()
        {
            string manaBari = "[" + new string('▓', mana / 10) 
                                  + new string('░', 10 - mana / 10) + "]";
            return base.Bilgi()
                 + $"\n  │  Mana    : {manaBari} {Mana}/100"
                 + $"\n  └─ Tür    : 🔮 Mage (Glass Cannon)";
        }
    }
}
