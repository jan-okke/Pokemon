namespace Pokemon
{
    class Item
    {
        public string Name;
        public BagLocation Location;
        public BattleTrigger Trigger;
        public Item(string name, BagLocation bagLocation, BattleTrigger trigger)
        {
            Name = name;
            Location = bagLocation;
            Trigger = trigger;
        }
    }
}