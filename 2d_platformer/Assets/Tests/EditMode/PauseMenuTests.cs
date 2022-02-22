using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PauseMenuTests
{
    PauseMenuHelper PMH = new PauseMenuHelper();
    float updatedTime;
    float startingPausedTime;

    [SetUp]
    public void Setup()
    {
        startingPausedTime = 0f;
    }


    [Test]
    public void PauseMenuCorrectTimeScale()
    {
        updatedTime = 1f;
        PMH.setTimeScale(updatedTime, startingPausedTime);
        Assert.AreEqual(updatedTime, 1); //TO-DO
    }





    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    /*[UnityTest]
    public IEnumerator PauseMenuTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }*/
}
