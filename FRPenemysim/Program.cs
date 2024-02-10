using System.Security.Cryptography.X509Certificates;

namespace FRPenemysim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // random türü oluşturma ve düşman türü olasılık listesi


            Random random = new Random();
            string[] zombieKindList = { "rage zombi", "rage zombi", "rage zombi", "klasik zombi", "klasik zombi", "klasik zombi", "klasik zombi", "klasik zombi", "klasik zombi", "mutant zombi" };
            //düşman spawnlama kodları
            while (true)
            {
                Console.Write("kaç tane zombi olacağını girin: ");
                int adet = Convert.ToInt32(Console.ReadLine());
                List<Zombie> zombies = new List<Zombie>();
                for (int i = 0; i < adet; i++)
                {
                    Console.WriteLine("---------------------------------------------------------");
                    
                    int randHealth = random.Next(1, 31);
                    int randAtack = random.Next(1, 11);
                    int randKind = random.Next(0, 10);
                    string zombiePower = zombieKindList[randKind];
                    switch (zombiePower)
                    {
                        case "rage zombi":
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine("[¬º^°]¬");
                            randHealth += 10; 
                            randAtack += 15;
                            break;
                        case "mutant zombi":
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("◄=[¬º*°]¬");
                            randHealth += 20;
                            randAtack += 15;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("[¬º-°]¬");
                            break;
                    }
                    Zombie zombie = new Zombie { health = randHealth, atack = randAtack, power = zombiePower };
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Sağlığı:{zombie.health}");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Saldırı gücü:{zombie.atack}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Türü:{zombie.power}");
                    Console.ForegroundColor = ConsoleColor.White;
                    zombies.Add( zombie );
                    
                }
                
                Console.WriteLine("####################### Savaş Ekranı #######################################");
                bool isBattleContinue = true;
                while (isBattleContinue)
                {
                    Console.Write("Kaç kere saldıracaksın: ");
                    int atackNumber = Convert.ToInt32(Console.ReadLine());
                    Zombie[] afterAtackZombies = Atack(atackNumber, zombies).ToArray();
                    AtackResult(afterAtackZombies);
                    ZombieAttack(afterAtackZombies.ToList());
                    
                    Control(ref isBattleContinue, afterAtackZombies);  
                }
                Console.WriteLine("####################### Mücadele sona erdi #######################################");
            }
            


        }

        public static List<Zombie> Atack(int atackNumber,List<Zombie> zombies)
        {
            for (int i = 0; i < atackNumber; i++)
            {
                Console.Write("Kime saldırıyorsun: ");
                int atackWho = Convert.ToInt32(Console.ReadLine());
                Console.Write("Kaç vurdun: ");
                int hitDamage = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("---------------------------------------------------------");
                Zombie focusZombie = zombies[atackWho-1];
                focusZombie.health -= hitDamage;
                if (focusZombie.health <= 0 )
                {
                    zombies[atackWho - 1].sitituation = "(ÖLDÜ)";
                }
                
            }

            return zombies;
        }
        public static void AtackResult(Zombie[] afterAtackZombies)
        {
           
            foreach (var zombie in afterAtackZombies)
            {
                Console.WriteLine("---------------------------------------------------------");
                if (zombie.sitituation != "(ÖLDÜ)")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Sağlığı:{zombie.health}");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Saldırı gücü:{zombie.atack}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Türü:{zombie.power}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(zombie.sitituation);
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
        }

        public static void Control(ref bool  isBattleContinue, Zombie[] afterAtackZombies)
        {
            bool alldead = true;
            foreach (var zombie in afterAtackZombies)
            {
                if (zombie.sitituation != "(ÖLDÜ)")
                {
                  
                 alldead = false;
                }
            }
            if (alldead) { isBattleContinue = false; }
        }


        public static void ZombieAttack(List<Zombie> zombies)
        {
            
            Random random = new Random();
            string[] rageZombieUlties = {"öfke nöbeti geçirdi", "Çıldırdı", "size odaklandı"};
            string[] mutantZombieUlties = {"kendini patlattı","kusmuk attı","yenilendi"};
            int zombieCount = random.Next(1,zombies.Count);
            int deadCount = 0;
            foreach (var item in zombies)
            {
                if (item.sitituation == "(ÖLDÜ)")
                {
                    deadCount++;
                }
            }
            if (zombieCount-deadCount <= 0)
            {
                zombieCount = 1;
            }
            else
            {
                zombieCount -= deadCount;
            }
            
            for (int i = 0; i < zombieCount; i++)
            {
                int atackKind = random.Next(0, 10);
                int powerKind = random.Next(0, 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("SIRA ZOMBİLERDE");
                int zombiId = random.Next(0, zombies.Count);
                Zombie atackZombie = zombies[zombiId];
                if (atackZombie.sitituation == "(ÖLDÜ)")
                {

                    for (int j = zombiId; j < zombies.Count; j++)
                    {
                        if (zombies[j].sitituation != "(ÖLDÜ)")
                        {
                            atackZombie = zombies[j];
                            zombiId = j;
                        }
                    }
                       
                    
                    
                }
               if ((atackKind < 7 || atackZombie.power == "klasik zombi") && atackZombie.sitituation != "(ÖLDÜ)")
                {
                    Console.Write("lütfen savunma zarı atın ve sonucu yazın: ");
                    int protectDice = Convert.ToInt32(Console.ReadLine());
                    if (protectDice >= 8 && atackZombie.power == "klasik zombi")
                    {
                        Console.WriteLine($"{zombiId + 1}. zombi size {atackZombie.atack / 4} hasar verdi");
                        Console.WriteLine("---------------------------------------------------------");

                    }
                    else if (protectDice >= 6 && atackZombie.power == "klasik zombi")
                    {
                        Console.WriteLine($"{zombiId + 1}. zombi size {atackZombie.atack/2} hasar verdi");
                        Console.WriteLine("---------------------------------------------------------");
                    }

                    else if (protectDice >= 8 && atackZombie.power != "klasik zombi")
                    {
                        Console.WriteLine($"{zombiId + 1}. zombi size {atackZombie.atack/2} hasar verdi");
                        Console.WriteLine("---------------------------------------------------------");
                    }
                    else if (protectDice >=6 && atackZombie.power != "klasik zombi")
                    {
                        Console.WriteLine($"{zombiId + 1}. zombi size {atackZombie.atack/1,5} hasar verdi");
                        Console.WriteLine("---------------------------------------------------------");
                    }
                    else
                    {

                        Console.WriteLine($"{zombiId + 1}. zombi size {atackZombie.atack} hasar verdi");
                        Console.WriteLine("---------------------------------------------------------");
                    }
                    
                }
                else
                {
                    if (atackZombie.power == "rage zombi" && atackZombie.sitituation != "(ÖLDÜ)")  {
                        switch (powerKind)
                        {
                            case 0:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine($"{zombiId + 1}. zombi {rageZombieUlties[powerKind]} ve size {atackZombie.atack + 8} hasar verdi");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("---------------------------------------------------------");
                                break;
                            case 1:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine($"{zombiId + 1}. zombi {rageZombieUlties[powerKind]} ve size {atackZombie.atack} hasar verdi ayrıca artık atağı {atackZombie.atack+8} oldu");
                                Console.ForegroundColor = ConsoleColor.White;
                                atackZombie.atack += 8; 
                                Console.WriteLine("---------------------------------------------------------");
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine($"{zombiId + 1}. zombi {rageZombieUlties[powerKind]} ve size ölene kadar her tur {atackZombie.atack/2} hasar verecek.");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("---------------------------------------------------------");
                                break;

                        }
                    }
                    else if (atackZombie.power =="mutant zombi" && atackZombie.sitituation != "(ÖLDÜ)")
                    {
                        switch (powerKind)
                        {
                            case 0:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine($"{zombiId + 1}. zombi {mutantZombieUlties[powerKind]} ve eğer temizlenmezseniz ya da 6 üzeri zar atamazsanız (herkes zar atsın ve çeviklik ekleyin zara) zombi oldunuz");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("---------------------------------------------------------");
                                break;
                            case 1:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine($"{zombiId + 1}. zombi aranızdan en düşük zar atan kişinin üzerine {mutantZombieUlties[powerKind]} ve eğer o kişi 6 üzeri zar atamazsa (çeviklik ekleyin) zombi oldu");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("---------------------------------------------------------");
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine($"{zombiId + 1}. zombi {mutantZombieUlties[powerKind]} ve canı 15 birim arttı.");
                                Console.ForegroundColor = ConsoleColor.White;
                                atackZombie.health += 15;
                                Console.WriteLine("---------------------------------------------------------");
                                break;

                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}