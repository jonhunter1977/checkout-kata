using System.Collections.Generic;

namespace Checkout.Interfaces
{
    public interface IOffer
    {
        bool CanOfferBeAppliedToRemainingScannedItems(List<IItem> scannedItems);
        void RemoveItemsThatWereInOfferFromScannedItems(List<IItem> scannedItems);
        double CalculateDiscount(List<IItem> scannedItems);
    }
}