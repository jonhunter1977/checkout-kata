using System.Collections.Generic;

namespace Checkout.Interfaces
{
    public interface IOfferService
    {
        double CalculateDiscountFromOffers(List<IItem> scannedItems);
        double CalculateDiscountFromOffers();
    }
}