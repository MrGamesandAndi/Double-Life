using Localisation;
using System;

[Serializable]
public class PurchasedFurniture
{
    public LocalisedString furnitureName;
    public int amount;

    public PurchasedFurniture(LocalisedString furnitureName, int amount)
    {
        this.furnitureName = furnitureName;
        this.amount = amount;
    }
}
