using Checkout.Interfaces;

namespace Checkout
{
    public class Item : IItem
    {
        private string _itemCode;

        public Item(string itemCode) {
            _itemCode = itemCode;
        }

        public string getItemCode()
        {
            return _itemCode;
        }
    }
}