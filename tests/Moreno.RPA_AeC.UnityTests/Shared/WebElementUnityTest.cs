using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Moreno.RPA_AeC.UnityTests.Shared;

public class WebElementUnityTest : IWebElement
{
    private readonly Faker _faker;
    public WebElementUnityTest()
    {
        _faker = new Faker();
    }
    public string TagName => _faker.Random.AlphaNumeric(5);

    public string Text => _faker.Random.AlphaNumeric(5);

    public bool Enabled => _faker.Random.Bool();

    public bool Selected => _faker.Random.Bool();

    public Point Location => Point.Empty;

    public Size Size => Size.Empty;

    public bool Displayed => _faker.Random.Bool();

    public void Clear()
    {
    }

    public void Click()
    {
    }

    public IWebElement FindElement(By by)
    {
        return this;
    }

    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
        return null;
    }

    public string GetAttribute(string attributeName)
    {
        return _faker.Random.AlphaNumeric(5);
    }

    public string GetCssValue(string propertyName)
    {
        return _faker.Random.AlphaNumeric(5);
    }

    public string GetDomAttribute(string attributeName)
    {
        return _faker.Random.AlphaNumeric(5);
    }

    public string GetDomProperty(string propertyName)
    {
        return _faker.Random.AlphaNumeric(5);
    }

    public ISearchContext GetShadowRoot()
    {
        return null;
    }

    public void SendKeys(string text)
    {
    }

    public void Submit()
    {
    }
}
