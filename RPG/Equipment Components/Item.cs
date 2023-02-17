namespace RPG
{
    public abstract class Item
    {
        public Item(string name, int requiredLevel, EquipmentSlot slot)
        {
            this.Name = name;
            this.RequiredLevel = requiredLevel;
            this.Slot = slot;
        }

        public string Name { get; private set; } = "[No name assigned]";
        public int RequiredLevel { get; private set; }
        public EquipmentSlot Slot { get; private set; }
    }
}
