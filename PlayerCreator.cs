namespace PlayerCreator;

class Player { 
    public string Name;
    public int Position;
    public string Type;
    public Dictionary<string, double> Abilities;
    public List<object> SuperAbilities;
    public static List<Player> Players { get; set; } = new();

    public Player(string Name) {
        this.Name = Name;
        Position = 1;
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
                    "AttackMultiply", "Attack Multiply", 1.2
                };
                break;
            case 2:
                Type = "Mage";
                Abilities = new() {
                    { "Attack", 2.0 },
                    { "Shield", 4.0 }
                };
                SuperAbilities = new() {
                    "Cast", "Cast", 3.0 
                };
                break;
            case 3:
                Type = "Healer";
                Abilities = new() {
                    { "Attack", 2.0 },
                    { "Shield", 4.0 }
                };
                SuperAbilities = new() {
                    "Heal", "Heal", 5.0 
                };
                break;
            case 4:
                Type = "Elf";
                Abilities = new() {
                    { "Attack", 3.0 },
                    { "Shield", 4.0 }
                };
                SuperAbilities = new() {
                    "Shoot", "Shoot", 3.0 
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
}