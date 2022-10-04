using PokemonExceptions;

namespace Pokemon
{
    class Program
    {
        public static void Main(string[] args)
        {
            try {
            Pokemon Charmander = Business.NewPokemon("Charmander", 5, new List<string>() {"Tackle", "Leer", "Ember"}, "Oran Berry");
            Pokemon Squritle = Business.NewPokemon("Squirtle", 5, new List<string>() {"Pound", "Growl", "Water Gun"});
            InterActiveConsole(new() {Squritle}, new() {Charmander});
            }
            catch (InvalidLevelException e)
            {
                System.Console.WriteLine($"{e.Message}");
            }
            catch (InvalidSpeciesException e)
            {
                System.Console.WriteLine($"{e.Message}");
            }
            catch (InvalidItemException e)
            {
                System.Console.WriteLine($"{e.Message}");
            }
            catch (InvalidMoveException e)
            {
                System.Console.WriteLine($"{e.Message}");
            }
        }
        private static void InterActiveConsole(List<Pokemon> pokemons, List<Pokemon> otherPkmn)
        {
            Battle? battle = null;
            while (true)
            {
                System.Console.Write(">");
                string? nextText = Console.ReadLine();
                if (nextText != null) {
                    if (nextText == "exit") break;
                    string[] splitted = nextText.Split(new[] {" "}, StringSplitOptions.None);
                    if (splitted[0] == "show" && splitted[1] == "pokemons" || splitted[1] == "party")
                    {
                        System.Console.WriteLine(Business.ShowPokemonParty(pokemons));
                    }
                    if (splitted[0] == "start" && splitted[1] == "battle")
                    {
                        battle = Business.StartTrainerBattle(pokemons, new Trainer("Gary", TrainerCategory.Rival, otherPkmn));
                    }
                    if (splitted[0] == "show" && splitted[1] == "moves")
                    {
                        foreach (Pokemon pokemon in pokemons)
                        {
                            if (pokemon.Name == splitted[2]) System.Console.WriteLine(Business.ShowAvailableMoves(pokemon));
                        }
                    }
                    if (splitted[0] == "show" && splitted[1] == "battle")
                    {
                        if (battle != null)
                        System.Console.WriteLine(Business.ShowBattleInfo(battle));
                    }
                    if (splitted[0] == "battle" && splitted[1] == "use")
                    {
                        if (battle != null)
                        try {
                            System.Console.WriteLine(Business.UseMoveInBattle(battle, nextText.Split(new [] {"use "}, StringSplitOptions.None)[1]));
                        }
                        catch (InvalidMoveException e)
                        {
                            System.Console.WriteLine(e.Message);
                        }
                    }
                }
            }
        }
    }
}