using System.Collections.Generic;
using System.Linq;
using Checkout.Interfaces;

namespace Checkout
{
    public class OfferService : IOfferService
    {
        private List<IItem> _scannedItems;

        public OfferService(List<IItem> scannedItems)
        {
            _scannedItems = scannedItems;
        }

        public double CalculateDiscountFromOffers(List<IItem> scannedItems)
        {
            _scannedItems = scannedItems;
            return CalculateDiscountFromOffers();
        }

        public double CalculateDiscountFromOffers()
        {
            var itemADiscount = CalculateQuantityDiscount("A", 3, 20);
            var itemBDiscount = CalculateQuantityDiscount("B", 2, 15);
            return itemADiscount + itemBDiscount;
        }

        private double CalculateQuantityDiscount(string itemSku, int qualifyingQuantity, double discountToApply)
        {
            return _scannedItems.Where(item => item.getItemCode() == itemSku).Count() == qualifyingQuantity ? discountToApply : 0;
        }
    }
}