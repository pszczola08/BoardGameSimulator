namespace PlayerCreator;

class Player { 
    public string Name;
    public int Position; // aktualna pozycja gracza
    public string Type; // typ gracza
    public double Points;
    private double _Health; // prywatne pole zdrowie (jak robiłem publiczne, to nie mogłem zrobić customowego get i set)

    public double Health {
        get { return Math.Round(_Health, 1); } // zaokrąglanie zdrowia
        set { _Health = Math.Round(_Health, 1); } // i tu też
    }
    public ConsoleColor Color; // fajny kolorek
    public Dictionary<string, double> Abilities; // umiejętności
    public List<object> SuperAbilities; // lepsze umiejętności
    public static List<Player> Players { get; set; } = new(); // statyczne pole, czyli nie jest polem obiektu tylko jedno dla klasy całej; jest to lista graczy

    public Player(string Name) {
        this.Name = Name;
        Position = 1;
        Points = 0;
        _Health = 100;
        
        Random col = new Random();
        Array colors = Enum.GetValues(typeof(ConsoleColor)); // pobiera wszystkie istniejące kolory konsoli
        Color = (ConsoleColor)colors.GetValue(col.Next(colors.Length)); // ustawia kolor
        
        Random rand = new();
        int Rnd = rand.Next(1, 6);
        switch (Rnd) {
            case 1:
                Type = "Warrior";
                Abilities = new() {
                    { "Attack", 4.0 },
                    { "Shield", 5.0 }
                };
                SuperAbilities = new() {
                    "AttackMultiply", "Attack Multiplier", 1.2
                };
                break;
            case 2:
                Type = "Mage";
                Abilities = new() {
                    { "Attack", 2.0 },
                    { "Shield", 4.0 }
                };
                SuperAbilities = new() {
                    "Cast", "Spell Casting", 0.1
                };
                break;
            case 3:
                Type = "Healer";
                Abilities = new() {
                    { "Attack", 2.0 },
                    { "Shield", 4.0 }
                };
                SuperAbilities = new() {
                    "Heal", "Healing", 0.1 
                };
                break;
            case 4:
                Type = "Elf";
                Abilities = new() {
                    { "Attack", 3.0 },
                    { "Shield", 4.0 }
                };
                SuperAbilities = new() {
                    "Shoot", "Shooting", 3.0 
                };
                break;
            default:
                Type = "Dwarf";
                Abilities = new() {
                    { "Attack", 3.0 },
                    { "Shield", 5.0 }
                };
                SuperAbilities = new() {
                    "SuperAttack", "Super Attack", 2.0
                };
                break;
        }
        Console.WriteLine($"Player {this.Name} ({Type}) was added!");
    }

    public delegate void UpdatePlayer(double number, string addOrRemove); // definicja delegata (nienawidzę delegatów)

    public void UpdatePoints(double number, string addOrRemove) { // metoda uaktualniająca punkty
        if (addOrRemove == "add") {
            Points += number;
        } else if (addOrRemove == "remove") {
            Points -= number;
        } else {
            Console.WriteLine($"\n ---------- \n Error! Cannot execute action \" {addOrRemove} \" on Player! \n ---------- \n");
        }
    }
    public void UpdateHealth(double number, string addOrRemove) { // metoda aktualizująca zdrowie
        if (addOrRemove == "add") {
            _Health += number;
        } else if (addOrRemove == "remove") {
            _Health -= number * ((100 - Abilities["Shield"]) / 100);
        } else {
            Console.WriteLine($"\n ---------- \n Error! Cannot execute action \" {addOrRemove} \" on Player! \n ---------- \n");
        }

        if (Health <= 0) {
            Players.RemoveAll(player => player.Name == this.Name);
            Console.WriteLine($"Player {this.Name} died! {Players.Count} players left.");
        }
    }

    public static List<string> GetPlayersByPosition(int min, int max) { // statyczna metoda (czyli dla klasy, a nie obiektu), która pobiera graczy w danym zakresie pól
        List<string> names = new();
        foreach (var x in Players) {
            if (x.Position >= min && x.Position <= max) {
                names.Add(x.Name);
            }
        }

        return names;
    }

    public static List<string> GetAllPlayers() { // metoda pobierająca wszystkich graczy
        List<string> names = new();
        foreach (var x in Players) {
            names.Add(x.Name);
        }

        return names;
    }
}
