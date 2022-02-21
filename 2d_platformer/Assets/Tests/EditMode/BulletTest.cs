using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BulletTests
{
    BulletHelper bHelper;
    bool isFacingRight = true;
    float localScale;
    

    [SetUp]
    public void Setup()
    {
        bHelper = new BulletHelper();
        
    }

    [Test]
    public void CheckPlayerFacingRight()
    {
        localScale = 1f;
        bHelper.checkDirection(ref localScale, ref isFacingRight);
        Assert.AreEqual(localScale == 1f, isFacingRight);
    }

    [Test]
    public void CheckPlayerFacingLeft()
    {
        localScale = -1f;
        bHelper.checkDirection(ref localScale, ref isFacingRight);
        Assert.AreEqual(localScale == -1f, isFacingRight == false);
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
