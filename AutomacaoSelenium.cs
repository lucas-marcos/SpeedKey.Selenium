using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SpeedKey.PageObjects;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SpeedKey;

public class AutomacaoSelenium
{
    private IWebDriver _driver;
    private WebDriverWait _wait;

    public AutomacaoSelenium()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless");
        new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

        _driver = new ChromeDriver();
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
        _driver.Manage().Window.Maximize();
    }

    public void Iniciar()
    {
        var pagina = new TreinarDigitacaoPO(_driver, _wait).Visitar();

        if (pagina.ExisteModalInstrucaoInicial())
            pagina.FecharModalInstrucaoInicial();

        var palavras = new List<string>();
        Thread.Sleep(2000);
        do
        {
            palavras = pagina.CapturarPalavras();
            pagina.digitarListaDePalavras(palavras);
        } while (palavras.Count > 0);

        Console.WriteLine();
    }
}