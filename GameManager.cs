using BoardCreator;
using PlayerCreator;

namespace GameManager;

public class NewGame {
    public void Start(int Fields, int BonusFieldsPercentage) { // metoda startująca grę
        
        Board board = new(Fields, BonusFieldsPercentage); // stworzenie nowego obiektu klasy Board
        bool finish = false;
        while (finish != true) { // pętla dodająca graczy
            Console.Write("Player's name: ");
            string? name = Console.ReadLine();
            if (!Player.Players.Any(p => p.Name == name)) {
                Player.Players.Add(new Player(name));
            } else {
                Console.WriteLine("This player already exists!");
            }
            
            Console.Write("Do you want to add another Player? (Y, N) ");
            string? dec = Console.ReadLine();
            if (dec == "N") {
                finish = true;
            }
        }

        Console.WriteLine("--------------------");
        
        bool win = false;
        string winner = "";
        int current = 0;
        while (win != true) { // pętla tur graczy; kluczowy element programu; kończy się w momencie, gdy zostanie jeden gracz
            if (current > Player.Players.Count - 1) { // jeśli zmienna przechowująca indeks obecnego gracza wykracza poza tablicę, jest ustawiana na 0 (dzięki temu po turze gracza ostatniego będzie tura gracza pierwszego)
                 current = 0;
            }
            
            Player CurrentPlayer = Player.Players[current]; // pobiera obecnego gracza
            
            List<string> AllPlayers = Player.GetAllPlayers(); // lista wszystkich imion graczy
            List<string> PlayersExceptYou = AllPlayers.FindAll(pl0 => pl0 != CurrentPlayer.Name); // lista wszystkich imiomn graczy poza samym sobą
            
            Console.ForegroundColor = CurrentPlayer.Color; // zmiana koloru tekstu w konsoli
            Console.WriteLine($"{CurrentPlayer.Name}'s turn!");
            Console.WriteLine($"Type: {CurrentPlayer.Type}; Points: {CurrentPlayer.Points}; Helath: {CurrentPlayer.Health}; Attack: {CurrentPlayer.Abilities["Attack"]}; Shield: {CurrentPlayer.Abilities["Shield"]}; {CurrentPlayer.SuperAbilities[1]}: {CurrentPlayer.SuperAbilities[2]};");
            Console.WriteLine();
            Random r = new();
            int rn = r.Next(0, 6);
            Console.WriteLine($"{CurrentPlayer.Name} draws {rn}!");
            CurrentPlayer.Position += rn;
            if (CurrentPlayer.Position > board.Fields) { // sprawdzenie, czy pozycja wykracza poza wielkość planszy, jeśli tak, to pomniejsza pozycję o wielkość planszy (efekt zapętlenia planszy)
                CurrentPlayer.Position -= board.Fields;
            }
            Console.WriteLine($"Current position: Field {CurrentPlayer.Position}");
            if (board.BonusFields.Contains(CurrentPlayer.Position)) { // sprawdzenie czy dane pole jest bonusowe
                Console.WriteLine("Bonus Field!");
                
                Dictionary<string, double> PlayerAbilities = CurrentPlayer.Abilities; // pobiera umiejętności gracza

                Random q = new();
                int w = q.Next(1, 4);
                if (CurrentPlayer.Abilities["Shield"] > 25) { // jeśli tarcza chroni ponad 25% obrażeń, nie może ona być dalej ulepszana
                    w = 1;
                }
                string AbilityName;
                switch(w) {
                    case 1:
                        AbilityName = "Attack";
                        break;
                    case 2:
                        AbilityName = "Shield";
                        break;
                    default:
                        AbilityName = CurrentPlayer.SuperAbilities[0].ToString();
                        break;
                }
                double Ability;
                if(AbilityName == "Attack" || AbilityName == "Shield") {
                    Ability = CurrentPlayer.Abilities[AbilityName];
                } else {
                    Ability = Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                }

                Random aa = new();
                double bb = Math.Round(aa.NextDouble() * (1.0 - 0.1) + 0.1, 1);
                double dd;
                object FullName;
                if(AbilityName == "Attack" || AbilityName == "Shield") {
                    dd = Math.Round(bb + Ability, 1);
                    CurrentPlayer.Abilities[AbilityName] += bb;
                    FullName = AbilityName;
                } else {
                    double cc = Math.Round(Ability + bb, 1);
                    object ccc = cc;
                    dd = cc;
                    CurrentPlayer.SuperAbilities[2] = ccc;
                    FullName = CurrentPlayer.SuperAbilities[1];
                }
                Console.WriteLine($"Ability {FullName} upgraded from {Ability} to {dd}");
                Random p = new();
                int pp = p.Next(1, 6);
                Player.UpdatePlayer addPts = CurrentPlayer.UpdatePoints; // tu jest ten cały delegat (można to było zrobić 100x łatwiej, ale po co?)
                addPts(pp, "add");
                Console.WriteLine($"Collected {pp} points! You have {CurrentPlayer.Points} points now!");
            }

            List<string> PlayersInRange = Player.GetPlayersByPosition(CurrentPlayer.Position - 5, CurrentPlayer.Position + 5); // pobranie wszystkich graczy (poza sobą oczywiście), którzy znajdują się 5 pól w obie strony od ciebie
            PlayersInRange.RemoveAll(pl => pl == CurrentPlayer.Name); // tutaj usuwam samego siebie (no bo po co miałbym zaatakować siebie?)
            Console.WriteLine();
            if (PlayersInRange.Count == 0) { // to się dzieje, jeśli nie można nikogo zaatakować
                Console.WriteLine("Cannot attack any player.");
            } else if (PlayersInRange.Count == 1) { // a tutaj automatyczny atak, jeśli jest do wyboru tylko jedna osoba
                Player AttackedPlayer = Player.Players.Find(pl1 => pl1.Name == PlayersInRange[0]);
                Player.UpdatePlayer update = AttackedPlayer.UpdateHealth;
                double attack = CurrentPlayer.Abilities["Attack"]; // pobranie ataku gracza
                if (CurrentPlayer.Abilities["Attack"] < 0) { // jeśli gracz ma atak poniżej 0 (może się tak zdarzyć), to wogóle nie zaatakuje (ma go uleczyć?)
                    attack = 0;
                }
                if (CurrentPlayer.SuperAbilities.Contains("AttackMultiply")) { // ewentualnie jak jest wojownikiem i ma zwiększacz ataku razy x to się mnoży
                    if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) {
                        attack *= Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    }
                } // a jak jest goblinem (czy innym karłem albo elfem) to dodatkowy atak ma
                if (CurrentPlayer.SuperAbilities.Contains("SuperAttack")) {
                    if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) { // no i też oczywiście, jeśli jest większy od 0 (nie leczymy przeciwników, to głupie)
                        attack += Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    }
                }
                update(attack, "remove"); // znowu ten delegat (kto projektował to zadanie?)
                Console.WriteLine($"Player {AttackedPlayer.Name} has been attacked (has {AttackedPlayer.Health} HP left)!");
            } else {
                Console.Write("Which player do you want to attack? "); // a jak jest 2 lub więcej przeciwników do wyboru to można wybrać
                foreach (var l in PlayersInRange) {
                    Console.Write($"{l} ");
                }
                string? choice = Console.ReadLine();
                string target;
                if (PlayersInRange.Contains(choice)) {
                    target = choice;
                } else { // chyba że nie umiesz pisać i zrobiłeś literówkę, wtedy C# wybierze za ciebie
                    Random x = new();
                    int xx = x.Next(0, PlayersInRange.Count);
                    target = PlayersInRange[xx];
                }
                Player AttackedPlayer = Player.Players.Find(pl1 => pl1.Name == target);
                Player.UpdatePlayer update = AttackedPlayer.UpdateHealth;
                double attack = CurrentPlayer.Abilities["Attack"];
                if (CurrentPlayer.Abilities["Attack"] < 0) { // no i znowu sprawdzanie, czy atak jest większy od 0
                    attack = 0;
                }
                if (CurrentPlayer.SuperAbilities.Contains("AttackMultiply")) { // i znowu sprawdzenie, czy jest mnożnik ataku
                    if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) {
                        attack *= Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    }
                }
                if (CurrentPlayer.SuperAbilities.Contains("SuperAttack")) { // i czy jest dodatkowy atak
                    if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) { // i czy wynosi on więcej niż 0
                        attack += Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    }
                }
                update(attack, "remove"); // mam dosyć delegatów
                Console.WriteLine($"Player {AttackedPlayer.Name} has been attacked (has {AttackedPlayer.Health} HP left)!");
            }
            if (Player.Players.Count == 1) { // jeśli został 1 gracz to gra się kończy
                win = true;
                winner = Player.Players[0].Name;
                Console.WriteLine("--------------------");
                Console.ReadKey();
                current += 1;
                break; // koniec pętli
            }
            
            if (CurrentPlayer.SuperAbilities.Contains("Cast")) { // jeśli jesteś magiem, to zmniejszysz czyjeś umiejętności (właśnie tak można mieć atak poniżej 0)
                if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) {
                    Console.Write("Who do you want to cast spell on? "); // i znowu jest wybór
                    foreach (var l in PlayersExceptYou) {
                        Console.Write($"{l} ");
                    }
                    string? choiceSpell = Console.ReadLine();
                    string targetSpell;
                    if (PlayersExceptYou.Contains(choiceSpell)) {
                        targetSpell = choiceSpell;
                    } else {
                        Random y = new();
                        int yy = y.Next(0, PlayersExceptYou.Count);
                        targetSpell = PlayersExceptYou[yy];
                    }
                
                    Player Spelled = Player.Players.Find(pl2 => pl2.Name == targetSpell); // i tutaj wybieramy gracza, na którego rzucamy klątwę (ja wybieram użytkowników Linuxa)
                    double range = Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    Random rr = new();
                    int rrr = rr.Next(1, 4);
                    string info;
                    double newVal;
                    if (rrr == 1) {
                        Spelled.Abilities["Attack"] -= range;
                        info = "Attack";
                        newVal = Spelled.Abilities["Attack"];
                    } else if (rrr == 2) {
                        Spelled.Abilities["Shield"] -= range;
                        info = "Shield";
                        newVal = Spelled.Abilities["Shield"];
                    } else {
                        double value = Convert.ToDouble(Spelled.SuperAbilities[2]) - range;
                        object newValue = value;
                        Spelled.SuperAbilities[2] = newValue;
                        info = Convert.ToString(Spelled.SuperAbilities[1]);
                        newVal = Convert.ToDouble(Spelled.SuperAbilities[2]);
                    }
                    Console.WriteLine($"You casted {info} damaging spell on {Spelled.Name}. {Spelled.Name}'s {info} was reduced to {newVal}");
                }
            }
            
            if (CurrentPlayer.SuperAbilities.Contains("Heal")) { // i tutaj można się uleczyć, jeśli jesteś leczycielem
                if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) {
                    Player.UpdatePlayer heal = CurrentPlayer.UpdateHealth;
                    heal(Convert.ToDouble(CurrentPlayer.SuperAbilities[2]), "add");
                    Console.WriteLine($"You healed yourself. Now you have {CurrentPlayer.Health} HP.");
                }
            }

            if (CurrentPlayer.SuperAbilities.Contains("Shoot")) { // a tutaj, jeśli masz umiejętność strzelania (niestety nazwisko Strzelecki nie gwarantuje tej umiejętności), to możesz w kogoś strzelić (bez względu na to, gdzie jest)
                if (PlayersExceptYou.Count == 1) { // no i znowu, jeśli jest jedna osoba, to nie ma wyboru
                    Player AttackedPlayer3 = Player.Players.Find(pl3 => pl3.Name == PlayersExceptYou[0]);
                    Player.UpdatePlayer update3 = AttackedPlayer3.UpdateHealth;
                    double attack3 = Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    if (attack3 < 0) {
                        attack3 = 0;
                    }
                    update3(attack3, "remove"); // nienawidzę delegatów, co to ma być wogóle
                    Console.WriteLine($"Player {AttackedPlayer3.Name} has been shot (has {AttackedPlayer3.Health} HP left)!");
                } else { // a tutaj już jest wybór
                    Console.Write("Which player do you want to attack? ");
                    foreach (var l in PlayersExceptYou) {
                        Console.Write($"{l} ");
                    }
                    string? choice3 = Console.ReadLine();
                    string target3;
                    if (PlayersExceptYou.Contains(choice3)) {
                        target3 = choice3;
                    } else {
                        Random z = new();
                        int zz = z.Next(0, PlayersExceptYou.Count);
                        target3 = PlayersExceptYou[zz];
                    }
                    Player AttackedPlayer3 = Player.Players.Find(pl3 => pl3.Name == target3);
                    Player.UpdatePlayer update3 = AttackedPlayer3.UpdateHealth;
                    double attack3 = Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    if (attack3 < 0) {
                        attack3 = 0;
                    }
                    update3(attack3, "remove"); // ile jeszcze tych delegatów będzie
                    Console.WriteLine($"Player {AttackedPlayer3.Name} has been shot (has {AttackedPlayer3.Health} HP left)!");
                }
                if (Player.Players.Count == 1) { // i znowu, jeśli jedna osoba zostanie to koniec gry
                    win = true;
                    winner = Player.Players[0].Name;
                    Console.WriteLine("--------------------");
                    Console.ReadKey();
                    current += 1;
                    break;
                }
            }
            
            if (Player.Players.Count == 1) { // i tu chyba znowu koniec gry
                win = true;
                winner = Player.Players[0].Name;
            }
            Console.WriteLine("--------------------");
            Console.ReadKey();
            current += 1; // przejście do następnego gracza
        }

        Console.ForegroundColor = ConsoleColor.White; // zmiana koloru w konsoli na biały
        Console.WriteLine("--------------------");
        Console.WriteLine($"{winner} wins the game! Score: {Player.Players[0].Points}"); // ogłoszenie zwycięzcy (emocjonująca chwila)
        Console.WriteLine("--------------------");

        Console.ReadKey();
    }
}