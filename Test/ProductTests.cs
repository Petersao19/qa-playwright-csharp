using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightDemo.Pages;

namespace PlaywrightDemo.Tests
{
    /// <summary>
    /// Pruebas del módulo de Productos/Inventario
    /// Valida la carga del catálogo, carrito y ordenamiento.
    /// <summary>

    [TestFixture]
    public class ProductTests : PageTest
    {
        private LoginPage _loginPage = null!;
        private ProductsPage _productsPage = null!;

        [SetUp]
        public async Task SetUp()
        {
            _loginPage = new LoginPage(Page);
            _productsPage = new ProductsPage(Page);

            await _loginPage.GoToAsync();
            await _loginPage.LoginAsync("standard_user", "secret_sauce");
        }

        [Test]
        [Description("La página debe cargar 6 productos correctamente")]
        public async Task Products_PageLoad_ShouldDisplaySixProducts()
        {
            var productCount = await _productsPage.GetProductCountAsync();
            Assert.That(productCount, Is.EqualTo(6), "El catálogo debe mostrar exactamente 6 productos");
        }

        [Test]
        [Description("Agregar producto debe actualizar el badge del carrito")]
        public async Task Products_AddToCart_ShouldUpdateCartBadge()
        {
            // Act
            await _productsPage.AddBackpackToCartAsync();
            var cartCount = await _productsPage.GetCartBadgeCountAsync();
            Assert.That(cartCount, Is.EqualTo("1"),"Después de agregar un producto, el badge del carrito debe mostrar '1'");
        }

        [Test]
        [Description("La URL debe contener 'inventory' después del login exitoso")]        
        public async Task Products_PageURL_ShouldContainInventory()
        {
            // Act
            var url = Page.Url;            
            Assert.That(url, Does.Contain("inventory"), "La URL debe contener 'inventory' después del login exitoso");
        }

        [Test]
        [Description("El Título de la página inventario debe ser el correcto")]
        public async Task Products_PageTitle_ShouldBeProducts()
        {
            var title = await _productsPage.GetPageTitleAsync();
            Assert.That(title, Is.EqualTo("Products"), "El título de la página debe ser 'Products'");
        }

    }

}