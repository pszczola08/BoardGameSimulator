namespace PlayerCreator;

class Player { 
    public string Name;
    public int Position;
    public string Type;
    public Dictionary<string, double> Abilities;
    public Dictionary<string, double> SuperAbilities;
    public static List<Player> Players { get; set; } = new();

    public Player(string Name) {
        this.Name = Name;
        Position = 1;
        Random rand = new();
        int Rnd = rand.Next(1, 5);
        switch (Rnd) {
            case 1:
            default:
                Type = "Warrior";
                Abilities = new() {
                    { "Attack", 4.0 },
                    { "Hp", 5.0 }
                };
                SuperAbilities = new() {
                    { "MulitiplyAttack", 1.3 }
                };
                break;
            case 2:
                Type = "Mage";
                Abilities = new() {
                    { "Attack", 2.0 },
                    { "Hp", 4.0 }
                };
                SuperAbilities = new() {
                    { "Force", 3.0  }
                };
                break;
            case 3:
                Type = "Healer";
                Abilities = new() {
                    { "Attack", 2.0 },
                    { "Hp", 5.0 }
                };
                SuperAbilities = new() {
                    { "Heal", 4.0 }
                };
                break;
            case 4:
                Type = "Dwarf";
                Abilities = new() {
                    { "Attack", 3.0 },
                    { "Hp", 5.0 }
                };
                SuperAbilities = new() {
                    { "DoubleAttack", 6.0 }
                };
                break;
            case 5:
                Type = "Elf";
                Abilities = new() {
                    { "Attack", 3.0 },
                    { "Hp" , 4.0 }
                };
                SuperAbilities = new() {
                    { "Shoot", 3.0 }
                };
                break;
        }
        Console.WriteLine($"Player {this.Name} ({Type}) was added!");
    }
}
