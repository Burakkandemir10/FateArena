using System;
using System.Collections.Generic;

namespace FateArena
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "⚔️  FateArena — Fantezi Savaş Oyunu";

            KapakGoster();

            bool devamEt = true;

            while (devamEt)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n  Kaç karakter savaşa girecek?");
                Console.WriteLine("  (2 - 4 arası bir sayı gir)");
                Console.ResetColor();
                Console.Write("  Karakter sayısı: ");

                int karakterSayisi = 2; 
                if (int.TryParse(Console.ReadLine(), out int girilen))
                {
             
                    karakterSayisi = Math.Clamp(girilen, 2, 4);
                }


                List<Karakter> karakterler = new List<Karakter>();

                Console.WriteLine();
                for (int i = 1; i <= karakterSayisi; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"  --- {i}. Karakter -----------------");
                    Console.ResetColor();

                    Karakter yeniKarakter = KarakterSec(i);
                    karakterler.Add(yeniKarakter);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  ✅ {yeniKarakter.Ad} savaş alanına girdi! "
                                    + $"(Sıra #{yeniKarakter.SiraNumarasi})");
                    Console.ResetColor();
                }

                Console.WriteLine("\n  Karakterler oluşturulma sırasına göre hamle yapacak.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  Başlamak için Enter'a bas...");
                Console.ResetColor();
                Console.ReadLine();

                Arena arena = new Arena(karakterler);
                arena.SavasBaslat();


                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"\n  📊 Toplam Savaş    : {Arena.ToplamSavas}");
                Console.WriteLine($"  📊 Toplam Karakter : {Karakter.ToplamKarakter}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n  🔄 Yeni savaş? (E/H): ");
                Console.ResetColor();

                string cevap = Console.ReadLine()?.Trim().ToUpper();
                devamEt = (cevap == "E");
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n  ⚔️  FateArena'dan ayrılıyorsunuz...");
            Console.WriteLine($"  Toplam {Arena.ToplamSavas} savaş yapıldı. Güle güle!");
            Console.ResetColor();
            Console.ReadKey();
        }



        static Karakter KarakterSec(int siraNo)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  Karakter türü:");
            Console.WriteLine("  [1] ⚔️  Warrior  — Yüksek can, iyi savunma, kalkan bonusu");
            Console.WriteLine("  [2] 🔮 Mage     — Kırılgan ama çok güçlü, mana sistemi");
            Console.WriteLine("  [3] 🏹 Archer   — Dengeli, zara en çok bağımlı, kritik ok");
            Console.ResetColor();

            Console.Write("  Seçim (1/2/3): ");
            string tur = Console.ReadLine()?.Trim();

            Console.Write("  Karakter adı: ");
            string ad = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(ad)) ad = $"Kahraman_{siraNo}";



            switch (tur)
            {
                case "1":
 
                    return new Warrior(ad);

                case "2":
                    return new Mage(ad);

                case "3":
                    return new Archer(ad);

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  Geçersiz seçim — Warrior olarak devam edildi.");
                    Console.ResetColor();
                    return new Warrior(ad);
            }
        }

        static void KapakGoster()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine();
            Console.WriteLine("     ⚔️       F A T E  A R E N A      ⚔️");
            Console.WriteLine("-----------Fantezi Zar Savaş Oyunu-----------");
            Console.WriteLine();
            Console.WriteLine("🎲 Zar Sistemi:");
            Console.WriteLine("1-3  → MISS   : Vuruş ıskalandı");
            Console.WriteLine("4-7  → HIT    : Normal hasar");
            Console.WriteLine("8-10 → CRİTİCAL: x1.5 bloklanamazz");
            Console.WriteLine("Blok: HIT gelince zar at, 5+ ise kaçar");
            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
