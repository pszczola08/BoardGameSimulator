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
            }

            Console.WriteLine("\n");
            Console.ReadKey();
            
            current += 1;
        }

    }
}