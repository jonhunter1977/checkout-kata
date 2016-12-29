using NUnit.Framework;

namespace Checkout.Tests
{
    [TestFixture]
    public class ItemTests
    {
        [Test]
        public void Item_Code_Should_Be_Correct()
        {
            Item item = new Item("A TEST CODE");
            Assert.AreEqual(item.getItemCode(), "A TEST CODE");
        }

    }
}