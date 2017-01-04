using NUnit.Framework;
using System.Collections.Generic;
using Checkout.Interfaces;

namespace Checkout.Tests
{
    [TestFixture]
    public class OfferServiceTests
    {
        private OfferService _offerService;

        [Test]
        public void If_There_Are_No_Items_To_Calculate_The_Discount_Is_0()
        {
            _offerService = new OfferService();
            var discountAmount = _offerService.CalculateDiscountFromOffers(new List<IItem>());

            Assert.AreEqual(0, discountAmount);
        }

        [Test]
        public void If_I_Have_3_Of_A_The_Discount_Is_20()
        {
            _offerService = new OfferService();
            var discountAmount = _offerService.CalculateDiscountFromOffers(new List<IItem> {
                new Item("A"),
                new Item("A"),
                new Item("A")
            });

            Assert.AreEqual(20, discountAmount);
        }

        [Test]
        public void If_I_Have_2_Of_B_The_Discount_Is_15()
        {
            _offerService = new OfferService();
            var discountAmount = _offerService.CalculateDiscountFromOffers(new List<IItem> {
                new Item("B"),
                new Item("B")
            });

            Assert.AreEqual(15, discountAmount);
        }

        [Test]
        public void If_I_Have_3_Of_A_And_2_Of_B_The_Discount_Is_35()
        {
            _offerService = new OfferService();
            var discountAmount = _offerService.CalculateDiscountFromOffers(new List<IItem> {
                new Item("A"),
                new Item("A"),
                new Item("A"),
                new Item("B"),
                new Item("B")
            });

            Assert.AreEqual(35, discountAmount);
        }

        [Test]
        public void If_I_Have_6_Of_A_The_Discount_Is_40()
        {
            _offerService = new OfferService();
            var discountAmount = _offerService.CalculateDiscountFromOffers(new List<IItem> {
                new Item("A"),
                new Item("A"),
                new Item("A"),
                new Item("A"),
                new Item("A"),
                new Item("A")
            });

            Assert.AreEqual(40, discountAmount);
        }
    }
}