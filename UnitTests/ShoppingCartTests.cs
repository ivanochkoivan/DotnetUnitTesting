using DotnetUnitTesting;
using NUnit.Framework;

namespace UnitTests
{
    internal class ShoppingCartTests
    {
        private Product? product1;
        private Product? product2;
        private ShoppingCart? shoppingCart;

        [SetUp]
        public void SetUp() {
            product1 = new Product(1, "Milk", 20, 1);
            product2 = new Product(2, "Apple", 10, 1);
            shoppingCart = new ShoppingCart(new List<Product> { product1, product2 });
        }

        [Test]
        public void UserCanAddProductsToTheShoppingCartTest() 
        { 
            
            ShoppingCart cart = new ShoppingCart();

            //Act
            cart.AddProductToCart(product1);
            cart.AddProductToCart(product2);

            //Assert
            Assert.That(cart.GetProductById(product1!.Id), Is.EqualTo(product1));
            Assert.That(cart.GetProductById(product2!.Id), Is.EqualTo(product2));
        }

        [Test]
        public void UserCanRemoveProductsFromTheShoppingCartTest() 
        {
            //Act
            shoppingCart!.RemoveProductFromCart(product1);
            shoppingCart.RemoveProductFromCart(product2);

            //Assert
            Assert.That(shoppingCart.Products.Count, Is.EqualTo(0));         
        }

        [Test]
        public void UserCanRemovePartOfProductsFromTheShoppingCartTest()
        {
            //Act
            shoppingCart!.RemoveProductFromCart(product1);

            //Assert
            Assert.That(shoppingCart.Products.Count, Is.EqualTo(1));
            Assert.That(shoppingCart.GetProductById(product2!.Id), Is.EqualTo(product2));
        }

        [Test]
        public void UserCanGetTheTotalPriceOfTheShoppingCartTest()
        {
            //Act
            var price = shoppingCart!.GetCartTotalPrice();

            //Assert
            Assert.That(price, Is.EqualTo(30));
        }

        [Test]
        public void UserCanGetTheTotalPriceOfTheShoppingCartWhereQuantity2Test()
        {
            shoppingCart!.AddProductToCart(product1!);
            //Act
            var price = shoppingCart!.GetCartTotalPrice();

            //Assert
            Assert.That(price, Is.EqualTo(50));
        }
    }
}
