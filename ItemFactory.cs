namespace Pokemon
{
    class ItemFactory
    {
        public static Item OranBerry = new("Oran Berry", BagLocation.Berries, BattleTrigger.AfterDefending);
        public static List<Item> ItemList = new() {OranBerry};
    }
}