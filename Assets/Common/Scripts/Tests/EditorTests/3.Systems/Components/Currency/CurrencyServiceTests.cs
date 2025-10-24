using System;
using NUnit.Framework;

public class CurrencyTests
{
    CurrencyComponent _currency;
    int _startAmount = 0, _testAmount = 12000;

    [SetUp]
    public void SetUp() {
        _currency = new CurrencyComponent(null );
        _currency.Initilize(0, null);
        _currency.Add(_testAmount);
    }   

    [Test]
    public void Added() {
        Assert.AreEqual(_currency.Currency.TotalCopper, _testAmount);        
    }

    [Test]
    public void Removed() {
        _currency.Remove(_testAmount);
        Assert.Less(_currency.Currency.TotalCopper, _testAmount);
    }

    [Test]
    public void ConvertedCorrectly() {
        _currency.Add(345);

        Tuple<int,int,int> converted = Tuple.Create(1,23,45);
        Assert.AreEqual(_currency.Currency.Copper, converted.Item1);          
        Assert.AreEqual(_currency.Currency.Silver, converted.Item2);
        Assert.AreEqual(_currency.Currency.Gold, converted.Item3);          
    }
}