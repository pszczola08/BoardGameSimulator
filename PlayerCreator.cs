namespace PlayerCreator;

class Player { 
    public string Name;
    public int Position;
    public string Type;
    public double Points;
    private double _Health;

    public double Health {
        get { return Math.Round(_Health, 1); }
        set { _Health = Math.Round(_Health, 1); }
    }
    public ConsoleColor Color;
    public Dictionary<string, double> Abilities;
    public List<object> SuperAbilities;
    public static List<Player> Players { get; set; } = new();

    public Player(string Name) {
        this.Name = Name;
        Position = 1;
        Points = 0;
        _Health = 100;
        
        Random col = new Random();
        Array colors = Enum.GetValues(typeof(ConsoleColor));
        Color = (ConsoleColor)colors.GetValue(col.Next(colors.Length));
        
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

    public delegate void UpdatePlayer(double number, string addOrRemove);

    public void UpdatePoints(double number, string addOrRemove) {
        if (addOrRemove == "add") {
            Points += number;
        } else if (addOrRemove == "remove") {
            Points -= number;
        } else {
            Console.WriteLine($"\n ---------- \n Error! Cannot execute action \" {addOrRemove} \" on Player! \n ---------- \n");
        }
    }
    public void UpdateHealth(double number, string addOrRemove) {
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

    public static List<string> GetPlayersByPosition(int min, int max) {
        List<string> names = new();
        foreach (var x in Players) {
            if (x.Position >= min && x.Position <= max) {
                names.Add(x.Name);
            }
        }

        return names;
    }

    public static List<string> GetAllPlayers() {
        List<string> names = new();
        foreach (var x in Players) {
            names.Add(x.Name);
        }

        return names;
    }
}
