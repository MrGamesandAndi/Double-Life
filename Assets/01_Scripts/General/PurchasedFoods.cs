using Localisation;
using System;
using UnityEngine;

[Serializable]
public class PurchasedFoods
{
    public LocalisedString foodName;
    public int amount;

    public PurchasedFoods(LocalisedString foodName, int amount)
    {
        this.foodName = foodName;
        this.amount = amount;
    }
}
