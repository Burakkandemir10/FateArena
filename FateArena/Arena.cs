using System;
using System.Collections.Generic;

namespace FateArena
{

    public class Arena
    {

        private static int toplamSavas = 0;
        public  static int ToplamSavas { get => toplamSavas; }


        private List<Karakter> karakterler;

 

        public Arena(List<Karakter> karakterListesi)
        {

            karakterListesi.Sort((a, b) => a.SiraNumarasi.CompareTo(b.SiraNumarasi));
            this.karakterler = karakterListesi;

            toplamSavas++;
        }

        public void SavasBaslat()
        {
            BaslikYaz($"⚔️  FATE ARENA — SAVAŞ #{toplamSavas}  ⚔️");

            Console.WriteLine("\n  Savaşa girecek karakterler:\n");
            foreach (Karakter k in karakterler)
            {

                Console.WriteLine(k.Bilgi());
                Console.WriteLine();
            }

            AyracYaz();

            int tur = 1;
            int aktifIndex = 0;

            while (HayattaOlanlariGetir().Count >= 2)
            {
                List<Karakter> hayattalar = HayattaOlanlariGetir();


                Karakter aktif = hayattalar[aktifIndex % hayattalar.Count];
                aktifIndex++;

                if (!aktif.HayattaMi()) continue;

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\n  ═══ TUR {tur} ═══  Sıra: {aktif.Ad}");
                Console.ResetColor();

                Karakter hedef = HedefBelirle(aktif, hayattalar);
                if (hedef == null) break;

                SaldiriTurYonet(aktif, hedef);

                TurSonuGoster();

                tur++;

                if (tur > 50)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  ⏰ 50 tur doldu! Kader bu savaşı berabere bitirdi.");
                    Console.ResetColor();
                    return;
                }
            }

            SonucGoster();
        }


        private void SaldiriTurYonet(Karakter saldiran, Karakter hedef)
        {
            Console.WriteLine();

            ZarSonucu saldiranZar = Zar.At();
            ZarGoster(saldiran.Ad, saldiranZar);

            switch (saldiranZar.Tur)
            {
                case ZarSonucuTuru.Miss:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"\n  💨 MISS! {saldiran.Ad}'ın saldırısı boşa gitti.");
                    Console.ResetColor();
                    break;

                case ZarSonucuTuru.Hit:
                    NormalVurusYonet(saldiran, hedef, saldiranZar);
                    break;

                case ZarSonucuTuru.Critical:
                    KritikVurusYonet(saldiran, hedef, saldiranZar);
                    break;
            }
        }


        private void NormalVurusYonet(Karakter saldiran, Karakter hedef, ZarSonucu zarSonucu)
        {
            int hasar = saldiran.HasarHesapla(zarSonucu.Deger);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  ⚡ HIT! {saldiran.Ad} saldırıyor → {hasar} olası hasar");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  [{hedef.Ad}] Ne yapıyorsun?");
            Console.WriteLine("  [1] 🛡️  Blok dene (Zar at, 5+ ise blok başarılı)");
            Console.WriteLine("  [2] ⚔️  Saldırıyı kabul et ve hasarı al");
            Console.ResetColor();
            Console.Write("  Seçim (1/2): ");

            string secim = Console.ReadLine()?.Trim();

            if (secim == "1")
            {
                ZarSonucu blokZar = Zar.BlokAt();
                ZarGoster(hedef.Ad + " (BLOK)", blokZar);

                if (blokZar.Tam >= 5)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n  🛡️  BLOK BAŞARILI! {hedef.Ad} saldırıyı savuşturdu!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n  ❌ Blok başarısız! {hedef.Ad} saldırıyı durduramadı!");
                    Console.ResetColor();
                    int alinanHasar = hedef.HasarAl(hasar);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  💥 {hedef.Ad} {alinanHasar} hasar aldı! "
                                    + $"(Kalan Can: {hedef.Can}/{hedef.MaxCan})");
                    Console.ResetColor();
                }
            }
            else
            {
                int alinanHasar = hedef.HasarAl(hasar);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  💥 {hedef.Ad} {alinanHasar} hasar aldı! "
                                + $"(Kalan Can: {hedef.Can}/{hedef.MaxCan})");
                Console.ResetColor();
            }
        }


        private void KritikVurusYonet(Karakter saldiran, Karakter hedef, ZarSonucu zarSonucu)
        {
            int temelHasar   = saldiran.HasarHesapla(zarSonucu.Deger);
            int kritikHasar  = (int)(temelHasar * 1.5); 

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n  💥 KRİTİK VURUŞ! {saldiran.Ad} tam isabetle vurdu!");
            Console.WriteLine($"  ⚠️  Bu vuruş savuşturulamayacak kadar güçlü!");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n  [{hedef.Ad}] Ne yapıyorsun?");
            Console.WriteLine("  [1] 🛡️  Blok dene");
            Console.WriteLine("  [2] ⚔️  Hasarı al");
            Console.ResetColor();
            Console.Write("  Seçim (1/2): ");

            string secim = Console.ReadLine()?.Trim();

            if (secim == "1")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  🔥 Bu vuruş savuşturulamayacak kadar güçlü! "
                                + "Blok işe yaramadı!");
                Console.ResetColor();
            }

            int alinanHasar = hedef.HasarAl(kritikHasar);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"  💥 {hedef.Ad} {alinanHasar} KRİTİK hasar aldı! "
                            + $"(Kalan Can: {hedef.Can}/{hedef.MaxCan})");
            Console.ResetColor();
        }

        private List<Karakter> HayattaOlanlariGetir()
        {
            List<Karakter> hayattalar = new List<Karakter>();
            foreach (Karakter k in karakterler)
            {
                if (k.HayattaMi())
                    hayattalar.Add(k);
            }
            return hayattalar;
        }

        private Karakter HedefBelirle(Karakter aktif, List<Karakter> hayattalar)
        {
            foreach (Karakter k in hayattalar)
            {
                if (k != aktif) return k;
            }
            return null;
        }

        private void TurSonuGoster()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  ── Güncel Durumlar ──");
            foreach (Karakter k in karakterler)
            {
                string durum = k.HayattaMi() ? $"Can: {k.Can}/{k.MaxCan}" : "☠️  HAYATTA DEĞİL";
                Console.WriteLine($"  {k.Ad}: {durum}");
            }
            Console.ResetColor();
        }

        private void SonucGoster()
        {
            AyracYaz();
            List<Karakter> hayattalar = HayattaOlanlariGetir();

            if (hayattalar.Count == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  🏆 KAZANAN: {hayattalar[0].Ad}!");
                Console.WriteLine($"  Kalan Can: {hayattalar[0].Can}/{hayattalar[0].MaxCan}");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                foreach (Karakter k in karakterler)
                {
                    if (!k.HayattaMi())
                        Console.WriteLine($"  ☠️  {k.Ad} savaşta hayatını kaybetti.");
                }
                Console.ResetColor();
            }
            AyracYaz();
        }

        private void ZarGoster(string kimAtti, ZarSonucu zar)
        {
            ConsoleColor renk = zar.Tur switch
            {
                ZarSonucuTuru.Miss     => ConsoleColor.DarkGray,
                ZarSonucuTuru.Hit      => ConsoleColor.Yellow,
                ZarSonucuTuru.Critical => ConsoleColor.Magenta,
                _                      => ConsoleColor.White
            };

            string turAdi = zar.Tur switch
            {
                ZarSonucuTuru.Miss     => "MISS (1-3)",
                ZarSonucuTuru.Hit      => "HIT (4-7)",
                ZarSonucuTuru.Critical => "CRİTİCAL (8-10) 🔥",
                _                      => ""
            };

            Console.ForegroundColor = renk;
            Console.WriteLine($"\n  🎲 {kimAtti} zar attı: "
                            + $"{zar.Deger:F1} → {zar.Tam} [{turAdi}]");
            Console.ResetColor();
        }

        private void AyracYaz()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n  ══════════════════════════════════════");
            Console.ResetColor();
        }

        private void BaslikYaz(string baslik)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n  ╔══════════════════════════════════════╗");
            Console.WriteLine($"  ║  {baslik,-38}║");
            Console.WriteLine("  ╚══════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}
