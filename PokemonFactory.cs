namespace Pokemon
{
    class PokemonFactory
    {
        public static Pokemon Charmander = new("Charmander", 1, new(50, 50, 50, 50, 50, 50), new() {PokemonType.Fire, PokemonType.None});
        public static Pokemon Squirtle = new("Squirtle", 1, new(50, 50, 50, 50, 50, 50), new() {PokemonType.Water, PokemonType.None});
        public static List<Pokemon> PokemonList = new() {Charmander, Squirtle};
    }
}