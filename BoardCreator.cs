﻿namespace BoardCreator;

public class Board {
    public int Fields;
    public List<int> BonusFields; // lista z numerami pól bonusowych

    public Board(int Fields, int PercentageOfBonusFields) { // konstruktor
        this.Fields = Fields;
        this.BonusFields = new();

        double BonusFields = Fields * PercentageOfBonusFields * 0.01; // wyliczenie ile pól bonusowych ma być
        for (int i = 1; i <= BonusFields; i++) { // lista dodająca bonusowe pola
            Random rand = new();
            int Rnd = rand.Next(1, this.Fields + 1);
            if (this.BonusFields.Contains(Rnd)) {
                i--;
            } else {
                this.BonusFields.Add(Rnd);
            }
        }

        Console.Write("Bonus Fields:");
        foreach (var j in this.BonusFields) { // wypisanie bonusowych pól
            Console.Write($" {j}");
        }
        Console.WriteLine("\n");
    }
}