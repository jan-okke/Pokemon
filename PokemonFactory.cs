namespace Pokemon
{
    class PokemonFactory
    {
        public static Pokemon Charmander = new("Charmander", 1, new(39, 52, 43, 60, 50, 65), new() {PokemonType.Fire, PokemonType.None}, 62);
        public static Pokemon Squirtle = new("Squirtle", 1, new(44, 48, 65, 50, 64, 43), new() {PokemonType.Water, PokemonType.None}, 63);
        public static List<Pokemon> PokemonList = new() {Charmander, Squirtle};
    }
}