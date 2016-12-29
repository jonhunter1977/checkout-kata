using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using Checkout.Interfaces;

namespace Checkout.Tests
{
    [TestFixture]
    public class CheckoutTests
    {
        private Checkout _checkout;

        [SetUp]
        public void Init()
        {
            var mockItemRepository = new Mock<IItemRepository>();
            mockItemRepository.Setup(itemRepository => itemRepository.checkItemExists("A")).Returns(true);
            mockItemRepository.Setup(itemRepository => itemRepository.getItemPrice("A")).Returns(50);
            mockItemRepository.Setup(itemRepository => itemRepository.checkItemExists("B")).Returns(true);
            mockItemRepository.Setup(itemRepository => itemRepository.getItemPrice("B")).Returns(30);
            mockItemRepository.Setup(itemRepository => itemRepository.checkItemExists("C")).Returns(true);
            mockItemRepository.Setup(itemRepository => itemRepository.getItemPrice("C")).Returns(20);
            mockItemRepository.Setup(itemRepository => itemRepository.checkItemExists("D")).Returns(true);
            mockItemRepository.Setup(itemRepository => itemRepository.getItemPrice("D")).Returns(15);
            mockItemRepository.Setup(itemRepository => itemRepository.checkItemExists("X")).Returns(false);

            var mockOfferService = new Mock<IOfferService>();
            List<IItem> itemADiscount = new List<IItem> {
                new Item("A"),
                new Item("A"),
                new Item("A")
            };
            mockOfferService.Setup(offerService => offerService.CalculateDiscountFromOffers(
            It.Is<List<IItem>>(x => x.Where(y => y.getItemCode() == "A").Count() == 3))).Returns(20);

            _checkout = new Checkout(mockItemRepository.Object, mockOfferService.Object);
        }

        [Test]
        public void If_I_Scan_An_Unknown_Item_An_Exception_Is_Thrown()
        {
            var ex = Assert.Catch<Exception>(() => _checkout.scanItem(new Item("X")));
            Assert.AreEqual("The scanned item was not found in the repository", ex.Message);
        }

        [Test]
        public void If_I_Scan_1_Of_Item_A_The_Price_Is_50()
        {
            _checkout.scanItem(new Item("A"));
            var price = _checkout.getTotalCost();
            Assert.AreEqual(50, price);
        }

        [Test]
        public void If_I_Scan_1_Of_Item_B_The_Price_Is_30()
        {
            _checkout.scanItem(new Item("B"));
            var price = _checkout.getTotalCost();
            Assert.AreEqual(30, price);
        }

        [Test]
        public void If_I_Scan_1_Of_Item_A_And_1_Of_Item_B_The_Price_Is_80()
        {
            _checkout.scanItem(new Item("A"));
            _checkout.scanItem(new Item("B"));
            var price = _checkout.getTotalCost();
            Assert.AreEqual(80, price);
        }

        [Test]
        public void If_I_Scan_3_Of_Item_A_The_Price_Is_130()
        {
            _checkout.scanItem(new Item("A"));
            _checkout.scanItem(new Item("A"));
            _checkout.scanItem(new Item("A"));
            var price = _checkout.getTotalCost();
            Assert.AreEqual(130, price);
        }

        [Test]
        public void If_I_Scan_1_Of_Item_A_And_1_Of_Item_B_And_1_Of_Item_C_The_Price_Is_100()
        {
            Item item = new Item("A");
            _checkout.scanItem(item);
            item = new Item("B");
            _checkout.scanItem(item);
            item = new Item("C");
            _checkout.scanItem(item);
            var price = _checkout.getTotalCost();
            Assert.AreEqual(100, price);
        }
    }
}
