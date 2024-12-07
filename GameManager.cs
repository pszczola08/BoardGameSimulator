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
            Console.WriteLine($"{CurrentPlayer.Name}'s turn!");
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
                    Ability = Convert.ToDouble(CurrentPlayer.SuperAbilities[1]);
                }

                Random aa = new();
                double bb = Math.Round(aa.NextDouble() * (1.0 - 0.1) + 0.1, 1);
                double dd;
                if(AbilityName == "Attack" || AbilityName == "Shield") {
                    dd = Math.Round(bb + Ability, 1);
                    CurrentPlayer.Abilities[AbilityName] += bb;
                } else {
                    double cc = Math.Round(Ability + bb, 1);
                    object ccc = cc;
                    dd = cc;
                    CurrentPlayer.SuperAbilities[1] = ccc;
                }
                Console.WriteLine($"Ability {AbilityName} upgraded from {Ability} to {dd}");
            }
            
            
            
            Console.WriteLine("--------------------");
            Console.ReadKey();
            
            current += 1;
        }

    }
}