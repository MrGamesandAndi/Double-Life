namespace Buildings.ShopSystem
{
    public interface IPurchaser
    {
        float GetCurrentFunds();
        bool SpendFundsForFood(FoodItem food);
        bool SpendFundsForFurniture(FurnitureItem furniture);
    }
}
