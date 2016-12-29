namespace Checkout.Interfaces 
{
    public interface IItemRepository
    {
        bool checkItemExists(string itemSku);
        double getItemPrice(string itemSku);
    }
}