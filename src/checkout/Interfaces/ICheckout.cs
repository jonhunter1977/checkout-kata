namespace Checkout.Interfaces
{
    public interface ICheckout 
    {
        void scanItem(IItem item);
        double getTotalCost();
    }
}