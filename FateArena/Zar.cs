using System;

namespace FateArena
{

    public enum ZarSonucuTuru
    {
        Miss,     
        Hit,      
        Critical  
    }

    public class ZarSonucu
    {
        
        public double Deger  { get; private set; }  
        public int    Tam    { get; private set; }  
        public ZarSonucuTuru Tur { get; private set; } 

        public ZarSonucu(double deger)
        {

            this.Deger = Math.Min(10.0, Math.Max(1.0, deger)); 
            this.Tam   = (int)Math.Ceiling(this.Deger);         

            if      (Tam <= 3)  this.Tur = ZarSonucuTuru.Miss;
            else if (Tam <= 7)  this.Tur = ZarSonucuTuru.Hit;
            else                this.Tur = ZarSonucuTuru.Critical;
        }
    }

   
    public static class Zar
    {

        private static readonly Random rnd = new Random();


        public static ZarSonucu At()
        {
            double hammadde = rnd.NextDouble() * 10; 
            double deger    = hammadde + 1;          
            return new ZarSonucu(deger);
        }


        public static ZarSonucu BlokAt()
        {
            return At(); 
        }
    }
}
