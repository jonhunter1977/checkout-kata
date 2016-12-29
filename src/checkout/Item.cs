using Checkout.Interfaces;

namespace Checkout
{
    public class Item : IItem
    {
        private string _itemCode;
        private bool _hasBeenPriced;

        public Item(string itemCode) {
            _itemCode = itemCode;
            _hasBeenPriced = false;
        }

        public string getItemCode()
        {
            return _itemCode;
        }

        public bool getPricingStatus ()
        {
            return _hasBeenPriced;
        }

    }
}