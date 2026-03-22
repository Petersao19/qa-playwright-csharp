using Microsoft.Playwright;

namespace PlaywrightDemo.Pages
{
    /// <summary>
    /// Página de Login de SauceDemo.
    /// Patrón Page Object Model (POM): cada página tiene su propia clase
    /// con sus locators y acciones. Reutilizable y fácil de mantener.
    /// </summary>
    public class LoginPage
    {
        private readonly IPage _page;

        // --- LOCATORS ---
        // Si el sitio cambia un selector, solo se edita aquí.
        private readonly string _txtUsername = "#user-name";
        private readonly string _txtPassword = "#password";
        private readonly string _btnLogin    = "#login-button";
        private readonly string _lblError    = "[data-test='error']";

        public LoginPage(IPage page)
        {
            _page = page;
        }

        /// <summary>
        /// Navega a la página de login.
        /// </summary>
        public async Task GoToAsync()
        {
            await _page.GotoAsync("https://www.saucedemo.com/");
        }

        /// <summary>
        /// Realiza el login con usuario y contraseña dados.
        /// </summary>
        public async Task LoginAsync(string username, string password)
        {
            await _page.FillAsync(_txtUsername, username);
            await _page.FillAsync(_txtPassword, password);
            await _page.ClickAsync(_btnLogin);
        }

        /// <summary>
        /// Retorna el texto del mensaje de error si el login falla.
        /// </summary>
        public async Task<string> GetErrorMessageAsync()
        {
            return await _page.InnerTextAsync(_lblError);
        }

        /// <summary>
        /// Verifica si el mensaje de error está visible.
        /// </summary>
        public async Task<bool> IsErrorVisibleAsync()
        {
            return await _page.IsVisibleAsync(_lblError);
        }
    }
}