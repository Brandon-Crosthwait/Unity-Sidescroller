using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HealthTests
{
    HealthHelper hHelper;
    float health;
    float maxHealth;
    float amount;
    float damage;

    [SetUp]
    public void Setup()
    {
        hHelper = new HealthHelper();
        health = 1f;
        maxHealth = 3f;
        amount = 2f;
        damage = 1f;
    }

    [Test]
    public void HealthEqualsMaxAtGameStart()
    {
        health = 1f;
        hHelper.initializeStartingHealth(ref health, maxHealth);
        Assert.AreEqual(health, maxHealth);
    }

    [Test]
    public void IncreaseHealthByOne()
    {
        health = 1f;
        hHelper.IncreaseHealth(ref health, maxHealth);
        Assert.AreEqual(2f, health);
    }

    [Test]
    public void IncreaseHealthNotMoreThanMax()
    {
        health = 3f;
        hHelper.IncreaseHealth(ref health, maxHealth);
        Assert.AreEqual(3f, health);
    }

    [Test]
    public void IncreaseHealthByAmount()
    {
        health = 1f;
        hHelper.IncreaseHealth(ref health, amount, maxHealth);
        Assert.AreEqual(3f, health);
    }

    [Test]
    public void IncreaseHealthByAmountNotMoreThanMax()
    {
        health = 2f;
        hHelper.IncreaseHealth(ref health, amount, maxHealth);
        Assert.AreEqual(3f, health);
    }

    [Test]
    public void TakeDamage()
    {
        health = 3f;
        hHelper.takeDamage(ref health, damage, maxHealth);
        Assert.AreEqual(2f, health);
    }

    [Test]
    public void HealthCantBeNegativeFromDamage()
    {
        health = 0f;
        hHelper.takeDamage(ref health, damage, maxHealth);
        Assert.AreEqual(0f, health);
    }

    /*
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator HealthTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
    */
}
