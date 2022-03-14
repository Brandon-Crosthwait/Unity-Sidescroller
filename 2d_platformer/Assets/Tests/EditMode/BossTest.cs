using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BossTests
{
    BossHelper bHelper;
    float playerPosition;
    float bossPosition;
    bool bossIsToTheRight = true;
    

    [SetUp]
    public void Setup()
    {
        bHelper = new BossHelper();
        
    }

    [Test]
    public void CheckBossIsToRightOfPlayer()
    {
        playerPosition = -20;
        bossPosition = 40;
        bHelper.checkPosition(ref playerPosition, ref bossPosition, ref bossIsToTheRight);
        Assert.AreEqual(true, bossIsToTheRight);
    }

    [Test]
    public void CheckBossIsToLeftOfPlayer()
    {
        playerPosition = 40;
        bossPosition =-20;
        bHelper.checkPosition(ref playerPosition, ref bossPosition, ref bossIsToTheRight);
        Assert.AreEqual(false, bossIsToTheRight);
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
