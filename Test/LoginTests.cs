using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightDemo.Pages;

namespace PlaywrightDemo.Tests
{
    /// <summary>
    /// Pruebas del módulo de Login.
    /// Cubre casos positivos y negativos.
    /// En sistemas financieros esto es crítico: 
    /// controlar quién accede y cómo falla el sistema.
    /// </summary>
    [TestFixture]
    public class LoginTests : PageTest
    {
        private LoginPage _loginPage = null!;

        [SetUp]
        public async Task SetUp()
        {
            _loginPage = new LoginPage(Page);
            await _loginPage.GoToAsync();
        }

        // =====================================================
        // CASO POSITIVO: Login exitoso
        // =====================================================

        [Test]
        [Description("Usuario válido debe redirigir al inventario")]
        public async Task Login_WithValidCredentials_ShouldRedirectToInventory()
        {
            // Arrange
            var username = "standard_user";
            var password = "secret_sauce";

            // Act
            await _loginPage.LoginAsync(username, password);

            // Assert
            var productsPage = new ProductsPage(Page);
            var title = await productsPage.GetPageTitleAsync();
            Assert.That(title, Is.EqualTo("Products"),
                "Debe mostrar 'Products' después del login exitoso");
        }

        // =====================================================
        // CASO NEGATIVO: Contraseña incorrecta
        // =====================================================

        [Test]
        [Description("Contraseña incorrecta debe mostrar mensaje de error")]
        public async Task Login_WithWrongPassword_ShouldShowErrorMessage()
        {
            // Arrange
            var username = "standard_user";
            var password = "wrong_password";

            // Act
            await _loginPage.LoginAsync(username, password);

            // Assert
            var isErrorVisible = await _loginPage.IsErrorVisibleAsync();
            Assert.That(isErrorVisible, Is.True,
                "Debe mostrarse error con contraseña incorrecta");
        }

        // =====================================================
        // CASO NEGATIVO: Campos vacíos
        // =====================================================

        [Test]
        [Description("Campos vacíos deben mostrar mensaje de validación")]
        public async Task Login_WithEmptyFields_ShouldShowErrorMessage()
        {
            // Act
            await _loginPage.LoginAsync("", "");

            // Assert
            var errorMessage = await _loginPage.GetErrorMessageAsync();
            Assert.That(errorMessage, Does.Contain("Username is required"),
                "Debe pedir el usuario cuando el campo está vacío");
        }

        // =====================================================
        // CASO NEGATIVO: Usuario bloqueado
        // =====================================================

        [Test]
        [Description("Usuario bloqueado no debe poder acceder al sistema")]
        public async Task Login_WithLockedUser_ShouldShowLockedMessage()
        {
            // Arrange: simula cuenta suspendida (común en sistemas bancarios)
            var username = "locked_out_user";
            var password = "secret_sauce";

            // Act
            await _loginPage.LoginAsync(username, password);

            // Assert
            var errorMessage = await _loginPage.GetErrorMessageAsync();
            Assert.That(errorMessage, Does.Contain("locked out"),
                "Debe informar que la cuenta está bloqueada");
        }
    }
}