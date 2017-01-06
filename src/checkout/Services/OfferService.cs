using System.Collections.Generic;
using Checkout.Interfaces;

namespace Checkout
{
    public class OfferService : IOfferService
    {
        private readonly List<IOffer> _offers;

        public OfferService(List<IOffer> offers)
        {
            _offers = offers;
        }

        public double CalculateDiscountFromOffers(List<IItem> scannedItems)
        {
            var totalDiscount = 0.00;

            foreach (IOffer offer in _offers)
            {
                while (offer.CanOfferBeAppliedToRemainingScannedItems(scannedItems))
                {
                    totalDiscount += offer.CalculateDiscount(scannedItems);
                    offer.RemoveItemsThatWereInOfferFromScannedItems(scannedItems);
                }
            }

            return totalDiscount;
        }
    }
}