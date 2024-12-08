using GameManager;

namespace Program;

class Program {
    public static void Main(string[] args) {

        NewGame game = new();
        
        Console.Write("Number of fields in the board: ");
        string? fields = Console.ReadLine();
        Console.Write("Percentage of bonus fields in the board: ");
        string? percentage = Console.ReadLine();
        int finalFld = 100;
        int finalPrc = 10;
        if (int.TryParse(fields, out int fieldsConverted)) {
            finalFld = fieldsConverted;
        }
        if (int.TryParse(percentage, out int percentageConverted)) {
            finalPrc = percentageConverted;
        }
        
        game.Start(finalFld, finalPrc);

    }
}