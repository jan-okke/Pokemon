namespace Pokemon
{
    class Battle
    {
        public List<Pokemon> PlayerPokemons;
        public List<Pokemon> EnemyPokemons;
        public Weather CurrentWeather;
        public Terrain CurrentTerrain;
        public int Turn;
        public BattleType Type;
        public Battle(List<Pokemon> playerPokemons, List<Pokemon> enemyPokemons, Weather weather, Terrain terrain)
        {
            PlayerPokemons = playerPokemons;
            EnemyPokemons = enemyPokemons;
            CurrentWeather = weather;
            CurrentTerrain = terrain;
            Turn = 0;
        }
    }
}