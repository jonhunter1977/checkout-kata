using System;
using System.Collections.Generic;
using System.Linq;
using Checkout.Interfaces;

namespace Checkout
{
    public class Checkout : ICheckout
    {
        private List<IItem> _scannedItems;
        private readonly IItemRepository _itemRepository;
        private readonly IOfferService _offerService;

        public Checkout(IItemRepository itemRepository, IOfferService offerService)
        {
            _scannedItems = new List<IItem>();
            _itemRepository = itemRepository;
            _offerService = offerService;
        }
        public void scanItem(IItem item)
        {
            //Check the item exists in the repository (i.e. there is a price for it)
            if (!_itemRepository.checkItemExists(item.getItemCode()))
            {
                throw new Exception("The scanned item was not found in the repository");
            }

            _scannedItems.Add(item);
        }

        public double getTotalCost()
        {
            var discountFromOffers = _offerService.CalculateDiscountFromOffers(_scannedItems);
            var totalCost = _scannedItems.Sum(x => _itemRepository.getItemPrice(x.getItemCode()));
            return totalCost - discountFromOffers;
        }
    }
}
