using System.Linq;
using System.Collections.Generic;
using Checkout.Interfaces;

namespace Checkout.Offers
{
    public class QuantityDiscountOffer : IOffer
    {
        private readonly string _itemSku;
        private readonly int _qualifyingQuantity;
        private readonly double _discountToApply;

        public QuantityDiscountOffer(string itemSku, int qualifyingQuantity, double discountToApply)
        {
            _itemSku = itemSku;
            _qualifyingQuantity = qualifyingQuantity;
            _discountToApply = discountToApply;
        }
        public double CalculateDiscount(List<IItem> scannedItems)
        {
            return scannedItems.Where(item => item.getItemCode() == _itemSku).Count() >= _qualifyingQuantity ? _discountToApply : 0;
        }

        public bool CanOfferBeAppliedToRemainingScannedItems(List<IItem> scannedItems)
        {
            return scannedItems.Where(item => item.getItemCode() == _itemSku).Count() >= _qualifyingQuantity;
        }

        public void RemoveItemsThatWereInOfferFromScannedItems(List<IItem> scannedItems)
        {
            var itemsRemoved = 0;
            for (int i = scannedItems.Count - 1; i >= 0; i--)
            {
                if (scannedItems[i].getItemCode() == _itemSku)
                {
                    scannedItems.RemoveAt(i);
                    itemsRemoved++;
                }

                if (itemsRemoved == _qualifyingQuantity) return;
            }
        }
    }
}