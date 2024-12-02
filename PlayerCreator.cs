namespace PlayerCreator;

class Player { 
    public string Name;
    public int Position;
    public string Type;
    public Dictionary<string, double> Abilities;
    public static List<Player> Players { get; set; } = new();

    public Player(string Name) {
        this.Name = Name;
        Position = 1;
        Random rand = new();
        int Rnd = rand.Next(1, 5);
        switch (Rnd) {
            case 1:
                Type = "Warrior";
                Abilities = new() {
                    { "Attack", 1.1 }
                };
                break;
            case 2:
                Type = "Mag";
                break;
            case 3:
                Type = "Healer";
                Abilities = new() {
                    { "Heal", 5.0 }
                };
                break;
            default:
                Type = "Player";
                break;
        }
        Console.WriteLine($"Player {this.Name} ({Type}) was added!");
    }
}