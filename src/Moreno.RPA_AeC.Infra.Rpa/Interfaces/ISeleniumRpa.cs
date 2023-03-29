using Moreno.RPA_AeC.Infra.Rpa.Base;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Moreno.RPA_AeC.Infra.Rpa.Interfaces;

public interface ISeleniumRpa : IBaseRpa
{
    Task<ReadOnlyCollection<IWebElement>?> ObterListaDeElementos(TipoElementoEnum tipoElemento, string chaveElemento);
    Task<IWebElement?> ObterElemento(IWebElement webElement, TipoElementoEnum tipoElemento, string chaveElemento, int indexArray);
    Task<string> ObterConteudoDoAtributo(IWebElement webElement, TipoAtributoEnum tipoAtributo);
}
