using Microsoft.Playwright;

namespace PlaywrightDemo.Pages
{
    /// <summary>
    /// Página de Productos/Inventario de SauceDemo.
    /// Acciones disponibles después de iniciar sesión.
    /// </summary>
    public class ProductsPage
    {
        private readonly IPage _page;

        // --- LOCATORS ---
        private readonly string _pageTitle = ".title";
        private readonly string _productItems = ".inventory_item";
        private readonly string _btnAddToCart = "[data-test='add-to-cart-sauce-labs-backpack']";
        private readonly string _cartBadge = ".shopping_cart_badge";
        private readonly string _sortDropdown = "[data-test='product_sort_container']";

        public ProductsPage(IPage page)
        {
            _page = page;
        }

        /// <summary>
        /// Retorna el título de la página actual.
        /// Confirma que el login fue exitoso.
        /// </summary>
        public async Task<string> GetPageTitleAsync()
        {
            return await _page.InnerTextAsync(_pageTitle);
        }

        /// <summary>
        /// Cuenta cuántos productos están listados.
        /// Valida que el catálogo carga correctamente.
        /// </summary>
        public async Task<int> GetProductCountAsync()
        {
            var items = await _page.QuerySelectorAllAsync(_productItems);
            return items.Count;
        }

        /// <summary>
        /// Agrega el Backpack al carrito.
        /// </summary>
        public async Task AddBackpackToCartAsync()
        {
            await _page.ClickAsync(_btnAddToCart);
        }

        /// <summary>
        /// Retorna el número del badge del carrito.
        /// Verifica que el producto fue agregado.
        /// </summary>
        public async Task<string> GetCartBadgeCountAsync()
        {
            return await _page.InnerTextAsync(_cartBadge);
        }

        /// <summary>
        /// Ordena productos. Opciones: "az", "za", "lohi", "hilo"
        /// </summary>
        public async Task SortProductsAsync(string sortOption)
        {
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await _page.SelectOptionAsync(_sortDropdown, sortOption);
        }
    }
}