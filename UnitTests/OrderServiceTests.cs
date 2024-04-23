using DotnetUnitTesting;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    internal class OrderServiceTests
    {
        UserAccount? userJohn;
        Mock<IDiscountUtility>? mockDiscount;
        private Product? product1;
        private Product? product2;
        [SetUp]
        public void SetUp() 
        {
            userJohn = new UserAccount("John", "Smith", "1990/10/10");

            product1 = new Product(1, "Milk", 20, 1);
            product2 = new Product(2, "Apple", 10, 1);

            userJohn.ShoppingCart.AddProductToCart(product1);
            userJohn.ShoppingCart.AddProductToCart(product2);

            mockDiscount = new Mock<IDiscountUtility>();
            mockDiscount.Setup(x => x.CalculateDiscount(userJohn)).Returns(3);
        }

        [Test]
        public void GetOrderPriceForValidUserTest()
        {
            OrderService service = new OrderService(mockDiscount!.Object);

            //Act
            var price = service.GetOrderPrice(userJohn);

            //Assert
            Assert.That(price, Is.EqualTo(27));
        }

        [Test]
        public void GetOrderPriceForValidUserAndOneProductTest()
        {
            userJohn!.ShoppingCart.RemoveProductFromCart(product1);
            OrderService service = new OrderService(mockDiscount!.Object);

            //Act
            var price = service.GetOrderPrice(userJohn);

            //Assert
            Assert.That(price, Is.EqualTo(7));
        }

        [Test]
        public void GetOrderPriceForNewUserTest()
        {
            var user = new UserAccount("Kenny", "Stoch", "1990/10/10");
            user.ShoppingCart.AddProductToCart(product1);
            OrderService service = new OrderService(mockDiscount!.Object);

            //Act
            var price = service.GetOrderPrice(user);

            //Assert
            Assert.That(price, Is.EqualTo(20));
        }
    }
}
