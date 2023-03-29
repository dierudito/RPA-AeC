using Moreno.RPA_AeC.Infra.Rpa.Base;
using Moreno.RPA_AeC.Infra.Rpa.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Moreno.RPA_AeC.Infra.Rpa.WebControls;

[ExcludeFromCodeCoverage]
public class SeleniumRpa : ISeleniumRpa
{
    private readonly IWebDriver _driver;

    public SeleniumRpa(IWebDriver driver)
    {
        _driver = new ChromeDriver();
    }

    public async Task NavegarParaUrl(string url)
    {
        _driver.Navigate().GoToUrl(url);
    }
    public async Task<bool> AtribuirValor(TipoElementoEnum tipoElemento, string chaveElemento, string valor, int indexArray = -1)
    {
        try
        {
            switch (tipoElemento)
            {
                case TipoElementoEnum.Id:
                    _driver.FindElement(By.Id(chaveElemento)).SendKeys(valor);
                    break;
                case TipoElementoEnum.Name:
                    var elementosName = _driver.FindElements(By.Name(chaveElemento));
                    AtribuirValorNosElementos(elementosName, valor, indexArray);
                    break;
                case TipoElementoEnum.XPath:
                    _driver.FindElement(By.XPath(chaveElemento)).SendKeys(valor);
                    break;
                case TipoElementoEnum.TagName:
                    var elementosTagName = _driver.FindElements(By.TagName(chaveElemento));
                    AtribuirValorNosElementos(elementosTagName, valor, indexArray);
                    break;
                default:
                    return false;
            }
        }
        catch (Exception)
        {
            return AtribuirValorNoElementosPorJavaScript(tipoElemento, chaveElemento, valor, indexArray);
        }

        return true;
    }

    public async Task<bool> EnviarComando(TipoElementoEnum tipoElemento, string chaveElemento, TipoComandoEnum tipoComando, int indexArray = -1)
    {
        try
        {
            switch (tipoElemento)
            {
                case TipoElementoEnum.Id:
                    var elementoId = _driver.FindElement(By.Id(chaveElemento));
                    EnviarComando(elementoId, tipoComando);
                    break;
                case TipoElementoEnum.Name:
                    var elementosName = _driver.FindElements(By.Name(chaveElemento));
                    EnviarComandoParaOsElementos(elementosName, tipoComando);
                    break;
                case TipoElementoEnum.XPath:
                    var elementoXpath = _driver.FindElement(By.XPath(chaveElemento));
                    EnviarComando(elementoXpath, tipoComando);
                    break;
                case TipoElementoEnum.TagName:
                    var elementosTagName = _driver.FindElements(By.TagName(chaveElemento));
                    EnviarComandoParaOsElementos(elementosTagName, tipoComando);
                    break;
                default:
                    return false;
            }
        }
        catch (Exception)
        {
            return EnviarComandoPorJavaScript(tipoElemento, chaveElemento, tipoComando, indexArray);
        }

        return true;
    }

    public async Task<string> ObterConteudoDoAtributo(IWebElement webElement, TipoAtributoEnum tipoAtributo)
    {
        return tipoAtributo switch
        {
            TipoAtributoEnum.value => webElement.GetAttribute("value"),
            TipoAtributoEnum.text => webElement.Text,
            _ => string.Empty,
        };
    }

    public async Task<string> ObterConteudoUnicoDoAtributo(TipoElementoEnum tipoElemento, string chaveElemento, TipoAtributoEnum tipoAtributo)
    {
        IWebElement? elemento = null;
        switch (tipoElemento)
        {
            case TipoElementoEnum.Id:
                elemento = _driver.FindElement(By.Id(chaveElemento));
                break;
            case TipoElementoEnum.Name:
                var elementoName = _driver.FindElements(By.Name(chaveElemento));
                if (elementoName.Any()) elemento = elementoName[0];
                break;
            case TipoElementoEnum.XPath:
                var elementoXPath = _driver.FindElements(By.XPath(chaveElemento));
                if (elementoXPath.Any()) elemento = elementoXPath[0];
                break;
            case TipoElementoEnum.TagName:
                var elementoTagName = _driver.FindElements(By.TagName(chaveElemento));
                if (elementoTagName.Any()) elemento = elementoTagName[0];
                break;
            default:
                break;
        }

        if (elemento == null) return string.Empty;
        return await ObterConteudoDoAtributo(elemento, tipoAtributo);
    }

    public async Task<ReadOnlyCollection<IWebElement>?> ObterListaDeElementos(TipoElementoEnum tipoElemento, string chaveElemento)
    {
        return tipoElemento switch
        {
            TipoElementoEnum.Id => _driver.FindElements(By.Id(chaveElemento)),
            TipoElementoEnum.Name => _driver.FindElements(By.Name(chaveElemento)),
            TipoElementoEnum.XPath => _driver.FindElements(By.XPath(chaveElemento)),
            TipoElementoEnum.TagName => _driver.FindElements(By.TagName(chaveElemento)),
            _ => null,
        };
    }

    public async Task<IWebElement?> ObterElemento(IWebElement webElement, TipoElementoEnum tipoElemento, string chaveElemento, int indexArray)
        => tipoElemento switch
        {
            TipoElementoEnum.Id => webElement.FindElement(By.Id(chaveElemento)),
            TipoElementoEnum.Name => webElement.FindElements(By.Name(chaveElemento))[indexArray],
            TipoElementoEnum.XPath => webElement.FindElements(By.XPath(chaveElemento))[indexArray],
            TipoElementoEnum.TagName => webElement.FindElements(By.TagName(chaveElemento))[indexArray],
            _ => null,
        };

    private void EnviarComandoParaOsElementos(ReadOnlyCollection<IWebElement> elementos, TipoComandoEnum tipoComando)
    {
        foreach (var elemento in elementos)
        {
            EnviarComando(elemento, tipoComando);
        }
    }
    private void EnviarComando(IWebElement elemento, TipoComandoEnum tipoComando)
    {
        switch (tipoComando)
        {
            case TipoComandoEnum.Click:
                elemento.Click();
                break;
            case TipoComandoEnum.Submit:
                elemento.Submit();
                break;
        }
    }

    private void AtribuirValorNosElementos(ReadOnlyCollection<IWebElement> elementos, string valor, int indexArray)
    {
        if (indexArray == -1)
            foreach (var elemento in elementos)
            {
                elemento.SendKeys(valor);
            }
        else
            elementos[indexArray].SendKeys(valor);
    }

    private bool AtribuirValorNoElementosPorJavaScript(TipoElementoEnum tipoElemento, string chaveElemento, string valor, int indexArray = -1)
    {
        var executor = (IJavaScriptExecutor)_driver;

        switch (tipoElemento)
        {
            case TipoElementoEnum.Id:
                executor.ExecuteScript($"document.getElementById(\"{chaveElemento}\").setAttribute('value','{valor}');");
                break;
            case TipoElementoEnum.Name:
                if (indexArray == -1)
                {
                    executor.ExecuteScript($"var elements = document.getElementsByName(\"{chaveElemento}\");" +
                        $"var tamanho = elements.length;" +
                        "for (var i = 0; i < tamanho; i++){ " +
                            $"elements[i].setAttribute('value','{valor}');" +
                        "}");
                }
                else
                    executor.ExecuteScript($"document.getElementsByName(\"{chaveElemento}\")[{indexArray}]" +
                        $".setAttribute('value','{valor}')");
                break;
            case TipoElementoEnum.XPath:
                executor.ExecuteScript($"document.evaluate(\"{chaveElemento}\", document, null, " +
                    $"XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.setAttribute(\"value\", \"{valor}\");");
                break;
            case TipoElementoEnum.TagName:
                if (indexArray == -1)
                {
                    executor.ExecuteScript($"var elements = document.getElementsByTagName(\"{chaveElemento}\");" +
                        $"var tamanho = elements.length;" +
                        "for (var i = 0; i < tamanho; i++){ " +
                            $"elements[i].setAttribute('value','{valor}');" +
                        "}");
                }
                else
                    executor.ExecuteScript($"document.getElementsByTagName(\"{chaveElemento}\")[{indexArray}]" +
                        $".setAttribute('value','{valor}')");
                break;
            default:
                return false;
        }
        return true;
    }

    private bool EnviarComandoPorJavaScript(TipoElementoEnum tipoElemento, string chaveElemento, TipoComandoEnum tipoComando, int indexArray = -1)
    {
        var executor = (IJavaScriptExecutor)_driver;
        var comandoScript = tipoComando switch
        {
            TipoComandoEnum.Click => "click",
            TipoComandoEnum.Submit => "submit",
            _ => string.Empty,
        };
        switch (tipoElemento)
        {
            case TipoElementoEnum.Id:
                executor.ExecuteScript($"document.getElementById(\"{chaveElemento}\").{comandoScript}();");
                break;
            case TipoElementoEnum.Name:
                if (indexArray == -1)
                {
                    executor.ExecuteScript($"var elements = document.getElementsByName(\"{chaveElemento}\");" +
                        $"var tamanho = elements.length;" +
                        "for (var i = 0; i < tamanho; i++){ " +
                            $"elements[i].{comandoScript}();" +
                        "}");
                }
                else
                    executor.ExecuteScript($"document.getElementsByName(\"{chaveElemento}\")[{indexArray}].{comandoScript}();");
                break;
            case TipoElementoEnum.XPath:
                executor.ExecuteScript($"document.evaluate(\"{chaveElemento}\", document, null, " +
                    $"XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.{comandoScript}();");
                break;
            case TipoElementoEnum.TagName:
                if (indexArray == -1)
                {
                    executor.ExecuteScript($"var elements = document.getElementsByTagName(\"{chaveElemento}\");" +
                        $"var tamanho = elements.length;" +
                        "for (var i = 0; i < tamanho; i++){ " +
                            $"elements[i].{comandoScript}();" +
                        "}");
                }
                else
                    executor.ExecuteScript($"document.getElementsByTagName(\"{chaveElemento}\")[{indexArray}].{comandoScript}();");
                break;
            default:
                return false;
        }

        return true;
    }

    public void Dispose()
    {
        _driver.Dispose();
    }
}