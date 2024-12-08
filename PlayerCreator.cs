namespace PlayerCreator;

class Player { 
    public string Name;
    public int Position;
    public string Type;
    public int Points;
    public int Health;
    public Dictionary<string, double> Abilities;
    public List<object> SuperAbilities;
    public static List<Player> Players { get; set; } = new();

    public Player(string Name) {
        this.Name = Name;
        Position = 1;
        Points = 0;
        Health = 100;
        
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
                    "Cast", "Spell Casting", 3.0
                };
                break;
            case 3:
                Type = "Healer";
                Abilities = new() {
                    { "Attack", 2.0 },
                    { "Shield", 4.0 }
                };
                SuperAbilities = new() {
                    "Heal", "Healing", 5.0 
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

    public delegate void UpdatePlayer(int number, string addOrRemove);

    public void UpdatePoints(int number, string addOrRemove) {
        if (addOrRemove == "add") {
            Points += number;
        } else if (addOrRemove == "remove") {
            Points -= number;
        } else {
            Console.WriteLine($"\n ---------- \n Error! Cannot execute action \" {addOrRemove} \" on Player! \n ---------- \n");
        }
    }
    public void UpdateHealth(int number, string addOrRemove) {
        if (addOrRemove == "add") {
            Health += number;
        } else if (addOrRemove == "remove") {
            Health -= number;
        } else {
            Console.WriteLine($"\n ---------- \n Error! Cannot execute action \" {addOrRemove} \" on Player! \n ---------- \n");
        }

        if (Health <= 0) {
            Players.RemoveAll(player => player.Name == this.Name);
            Console.WriteLine($"Player {this.Name} died! {Players.Count} players left.");
        }
    }

    public static List<string> GetPlayersByPosition(int min, int max) {
        List<string> names = new();
        foreach (var x in Players) {
            if (x.Position >= min && x.Position <= max) {
                names.Add(x.Name);
            }
        }

        return names;
    }
}
