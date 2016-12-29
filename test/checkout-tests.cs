using System;
using NUnit.Framework;
using Moq;
using Checkout.Interfaces;

namespace Checkout.Tests
{
    [TestFixture]
    public class CheckoutTests
    {
        private Checkout _checkout;

        [SetUpAttribute]
        public void Init()
        {
            var mockItemRepository = new Mock<IItemRepository>();

            mockItemRepository.Setup(i => i.checkItemExists("A")).Returns(true);
            mockItemRepository.Setup(i => i.getItemPrice("A")).Returns(50);
            mockItemRepository.Setup(i => i.checkItemExists("B")).Returns(true);
            mockItemRepository.Setup(i => i.getItemPrice("B")).Returns(30);
            mockItemRepository.Setup(i => i.checkItemExists("C")).Returns(true);
            mockItemRepository.Setup(i => i.getItemPrice("C")).Returns(20);
            mockItemRepository.Setup(i => i.checkItemExists("D")).Returns(true);
            mockItemRepository.Setup(i => i.getItemPrice("D")).Returns(15);
            mockItemRepository.Setup(i => i.checkItemExists("X")).Returns(false);

            var itemRepository = mockItemRepository.Object;
            _checkout = new Checkout(itemRepository);
        }

        [Test]
        public void If_I_Scan_An_Unknown_Item_An_Exception_Is_Thrown()
        {
            Item item = new Item("X");
            var ex = Assert.Catch<Exception>(() => _checkout.scanItem(item));
            Assert.AreEqual("The scanned item was not found in the repository", ex.Message);
        }

        [Test]
        public void If_I_Scan_1_Of_Item_A_The_Price_Is_50()
        {
            Item item = new Item("A");
            _checkout.scanItem(item);
            var price = _checkout.getTotalCost();
            Assert.AreEqual(50, price);
        }

        [Test]
        public void If_I_Scan_1_Of_Item_B_The_Price_Is_30()
        {
            Item item = new Item("B");
            _checkout.scanItem(item);
            var price = _checkout.getTotalCost();
            Assert.AreEqual(30, price);
        }

        [Test]
        public void If_I_Scan_1_Of_Item_A_And_1_Of_Item_B_The_Price_Is_80()
        {
            Item item = new Item("A");
            _checkout.scanItem(item);
            item = new Item("B");
            _checkout.scanItem(item);
            var price = _checkout.getTotalCost();
            Assert.AreEqual(80, price);
        }

        [Test]
        public void If_I_Scan_3_Of_Item_A_The_Price_Is_130()
        {
            Item item = new Item("A");
            _checkout.scanItem(item);
            item = new Item("A");
            _checkout.scanItem(item);
            item = new Item("A");
            _checkout.scanItem(item);
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
