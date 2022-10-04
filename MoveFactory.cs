namespace Pokemon
{
    class MoveFactory
    {
        public static Move Tackle = new("Tackle", 40, 100, 0, PokemonType.Normal, MoveCategory.Physical, 30);
        public static Move Leer = new("Leer", 0, 100, 0, PokemonType.Normal, MoveCategory.Status, 40);
        public static Move Ember = new("Ember", 40, 100, 0, PokemonType.Fire, MoveCategory.Special, 30);
        public static Move Pound = new("Pound", 40, 100, 0, PokemonType.Normal, MoveCategory.Physical, 40);
        public static Move Growl = new("Growl", 0, 100, 0, PokemonType.Normal, MoveCategory.Status, 40);
        public static Move WaterGun = new("Water Gun", 40, 100, 0, PokemonType.Water, MoveCategory.Special, 40);
        public static List<Move> MoveList = new() {Tackle, Leer, Ember, Pound, Growl, WaterGun};
    }
}