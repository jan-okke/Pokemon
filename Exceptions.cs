namespace PokemonExceptions
{
    class InvalidSpeciesException : Exception
    {
        public InvalidSpeciesException(string speciesName)
            :base($"Invalid Species: {speciesName}")
        {}
    }
    class InvalidLevelException : Exception
    {
        public InvalidLevelException(int level)
            :base($"Invalid Level: {level}. Please choose a level between 1 and {Pokemon.Constants.MAXLEVEL}.")
        {}
    }
    class InvalidItemException : Exception
    {
        public InvalidItemException(string item)
            :base($"Invalid Item: {item}")
        {}
    }
    class InvalidMoveException : Exception
    {
        public InvalidMoveException(string moveName)
            :base($"Invalid Move: {moveName}")
        {}
    }
}