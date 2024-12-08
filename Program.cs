using GameManager;

namespace Program;

class Program {
    public static void Main(string[] args) { // główna metoda

        NewGame game = new(); // nowa gra
        
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
        /*
         * linie 14 - 21;
         * jeśli użytkownik wprowadzi błędne dane, to ustawienia planszy będą domyślne (100 pól i 10% pól bonnusowych)
         */
        
        game.Start(finalFld, finalPrc); // rozpoczęcie gry

    }
}