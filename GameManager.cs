using BoardCreator;
using PlayerCreator;

namespace GameManager;

public class NewGame {
    public void Start(int Fields, int BonusFieldsPercentage) {
        
        Board board = new(Fields, BonusFieldsPercentage);
        bool finish = false;
        while (finish != true) {
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
        while (win != true) {
            if (current > Player.Players.Count - 1) {
                 current = 0;
            }
            
            Player CurrentPlayer = Player.Players[current];
            
            List<string> AllPlayers = Player.GetAllPlayers();
            List<string> PlayersExceptYou = AllPlayers.FindAll(pl0 => pl0 != CurrentPlayer.Name);
            
            Console.ForegroundColor = CurrentPlayer.Color;
            Console.WriteLine($"{CurrentPlayer.Name}'s turn!");
            Console.WriteLine($"Type: {CurrentPlayer.Type}; Points: {CurrentPlayer.Points}; Helath: {CurrentPlayer.Health}; Attack: {CurrentPlayer.Abilities["Attack"]}; Shield: {CurrentPlayer.Abilities["Shield"]}; {CurrentPlayer.SuperAbilities[1]}: {CurrentPlayer.SuperAbilities[2]};");
            Console.WriteLine();
            Random r = new();
            int rn = r.Next(0, 6);
            Console.WriteLine($"{CurrentPlayer.Name} draws {rn}!");
            CurrentPlayer.Position += rn;
            if (CurrentPlayer.Position > board.Fields) {
                CurrentPlayer.Position -= board.Fields;
            }
            Console.WriteLine($"Current position: Field {CurrentPlayer.Position}");
            if (board.BonusFields.Contains(CurrentPlayer.Position)) {
                Console.WriteLine("Bonus Field!");
                
                Dictionary<string, double> PlayerAbilities = CurrentPlayer.Abilities;

                Random q = new();
                int w = q.Next(1, 4);
                if (CurrentPlayer.Abilities["Shield"] > 25) {
                    
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
                Player.UpdatePlayer addPts = CurrentPlayer.UpdatePoints;
                addPts(pp, "add");
                Console.WriteLine($"Collected {pp} points! You have {CurrentPlayer.Points} points now!");
            }

            List<string> PlayersInRange = Player.GetPlayersByPosition(CurrentPlayer.Position - 5, CurrentPlayer.Position + 5);
            PlayersInRange.RemoveAll(pl => pl == CurrentPlayer.Name);
            Console.WriteLine();
            if (PlayersInRange.Count == 0) {
                Console.WriteLine("Cannot attack any player.");
            } else if (PlayersInRange.Count == 1) {
                Player AttackedPlayer = Player.Players.Find(pl1 => pl1.Name == PlayersInRange[0]);
                Player.UpdatePlayer update = AttackedPlayer.UpdateHealth;
                double attack = CurrentPlayer.Abilities["Attack"];
                if (CurrentPlayer.Abilities["Attack"] < 0) {
                    attack = 0;
                }
                if (CurrentPlayer.SuperAbilities.Contains("AttackMultiply")) {
                    if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) {
                        attack *= Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    }
                }
                if (CurrentPlayer.SuperAbilities.Contains("SuperAttack")) {
                    if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) {
                        attack += Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    }
                }
                update(attack, "remove");
                Console.WriteLine($"Player {AttackedPlayer.Name} has been attacked (has {AttackedPlayer.Health} HP left)!");
            } else {
                Console.Write("Which player do you want to attack? ");
                foreach (var l in PlayersInRange) {
                    Console.Write($"{l} ");
                }
                string? choice = Console.ReadLine();
                string target;
                if (PlayersInRange.Contains(choice)) {
                    target = choice;
                } else {
                    Random x = new();
                    int xx = x.Next(0, PlayersInRange.Count);
                    target = PlayersInRange[xx];
                }
                Player AttackedPlayer = Player.Players.Find(pl1 => pl1.Name == target);
                Player.UpdatePlayer update = AttackedPlayer.UpdateHealth;
                double attack = CurrentPlayer.Abilities["Attack"];
                if (CurrentPlayer.Abilities["Attack"] < 0) {
                    attack = 0;
                }
                if (CurrentPlayer.SuperAbilities.Contains("AttackMultiply")) {
                    if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) {
                        attack *= Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    }
                }
                if (CurrentPlayer.SuperAbilities.Contains("SuperAttack")) {
                    if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) {
                        attack += Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    }
                }
                update(attack, "remove");
                Console.WriteLine($"Player {AttackedPlayer.Name} has been attacked (has {AttackedPlayer.Health} HP left)!");
            }
            if (Player.Players.Count == 1) {
                win = true;
                winner = Player.Players[0].Name;
                Console.WriteLine("--------------------");
                Console.ReadKey();
                current += 1;
                break;
            }
            
            if (CurrentPlayer.SuperAbilities.Contains("Cast")) {
                if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) {
                    Console.Write("Who do you want to cast spell on? ");
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
                
                    Player Spelled = Player.Players.Find(pl2 => pl2.Name == targetSpell);
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
            
            if (CurrentPlayer.SuperAbilities.Contains("Heal")) {
                if (Convert.ToDouble(CurrentPlayer.SuperAbilities[2]) > 0) {
                    Player.UpdatePlayer heal = CurrentPlayer.UpdateHealth;
                    heal(Convert.ToDouble(CurrentPlayer.SuperAbilities[2]), "add");
                    Console.WriteLine($"You healed yourself. Now you have {CurrentPlayer.Health} HP.");
                }
            }

            if (CurrentPlayer.SuperAbilities.Contains("Shoot")) {
                if (PlayersExceptYou.Count == 1) {
                    Player AttackedPlayer3 = Player.Players.Find(pl3 => pl3.Name == PlayersExceptYou[0]);
                    Player.UpdatePlayer update3 = AttackedPlayer3.UpdateHealth;
                    double attack3 = Convert.ToDouble(CurrentPlayer.SuperAbilities[2]);
                    if (attack3 < 0) {
                        attack3 = 0;
                    }
                    update3(attack3, "remove");
                    Console.WriteLine($"Player {AttackedPlayer3.Name} has been shot (has {AttackedPlayer3.Health} HP left)!");
                } else {
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
                    update3(attack3, "remove");
                    Console.WriteLine($"Player {AttackedPlayer3.Name} has been shot (has {AttackedPlayer3.Health} HP left)!");
                }
                if (Player.Players.Count == 1) {
                    win = true;
                    winner = Player.Players[0].Name;
                    Console.WriteLine("--------------------");
                    Console.ReadKey();
                    current += 1;
                    break;
                }
            }
            
            if (Player.Players.Count == 1) {
                win = true;
                winner = Player.Players[0].Name;
            }
            Console.WriteLine("--------------------");
            Console.ReadKey();
            current += 1;
        }

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("--------------------");
        Console.WriteLine($"{winner} wins the game! Score: {Player.Players[0].Points}");
        Console.WriteLine("--------------------");

        Console.ReadKey();
    }
}