using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SpeedKey.PageObjects;

public class TreinarDigitacaoPO
{
    private IWebDriver _driver;
    private WebDriverWait _wait;
    private Actions _action;
    private By _modalInstrucaoInicial;
    private By _palavras;
    private By _teclado;
    private By _clickToStart;

    public TreinarDigitacaoPO(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
        _action = new Actions(driver);
        
        _modalInstrucaoInicial = By.ClassName("mgtzqhkf5P");
        _palavras = By.ClassName("tt4sGumlutkEo8jwMfdf");
        _teclado = By.ClassName("SvVTND81ohMzRUaa2URo");
        _clickToStart = By.ClassName("YUiIcVk4VfL2Esd2sn4a");
    }

    public TreinarDigitacaoPO digitarListaDePalavras(List<string> palavras)
    {
        foreach (var palavra  in palavras)
        {
            DigitarPalavra(palavra);
        }

        return this;
    }
    
    public List<string> CapturarPalavras()
    {
        var palavras = _wait.Until(a => a.FindElements(_palavras));
        
        return palavras.Select(a => a.Text).ToList();
    }

    public TreinarDigitacaoPO Visitar()
    {
        _driver.Navigate().GoToUrl("https://www.keybr.com/#");

        return this;
    }

    public TreinarDigitacaoPO FecharModalInstrucaoInicial()
    {
        Visitar();
    
        return this;
    }

    

    public bool ExisteModalInstrucaoInicial() =>  _wait.Until(a => a.FindElements(_modalInstrucaoInicial).Count > 0);
    private void DigitarPalavra(string palavra)
    {
        if (string.IsNullOrEmpty(palavra)) return;

        palavra = palavra.Replace("␣", " ");
        _action.SendKeys(palavra).Perform();
    }
}