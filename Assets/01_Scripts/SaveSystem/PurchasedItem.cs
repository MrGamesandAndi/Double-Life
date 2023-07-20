using Localisation;

namespace SaveSystem
{
    [System.Serializable]
    public class PurchasedItem
    {
        public LocalisedString itemName;
        public int amount;

        public PurchasedItem(LocalisedString itemName, int amount)
        {
            this.itemName = itemName;
            this.amount = amount;
        }
    }
}