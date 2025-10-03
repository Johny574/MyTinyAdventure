
using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;

public class SerializeTests {
    private SerializedMockTest _mock;
    private int testAmount = 5;


    [SetUp]
    public void SetUp() {
        _mock = new(testAmount);
    }


    [Test]
    public void MockSerialized() { 
        Serializer.SaveFile(_mock, "Test", SaveSlot.AutoSave);
        SerializedMockTest obj = Serializer.LoadFile<SerializedMockTest>("Test", SaveSlot.AutoSave);
        Assert.That(obj.amount == testAmount);
    }

  
}

[Serializable]
public class SerializedMockTest {
    public int amount;

    public SerializedMockTest(int amount) {
        this.amount = amount;
    }
}