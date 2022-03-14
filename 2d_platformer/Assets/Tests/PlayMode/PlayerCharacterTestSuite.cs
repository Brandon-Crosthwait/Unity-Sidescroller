using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{

    private GameObject testObject;
    private PlayerMovement pm;

    [SetUp]
    public void Setup()
    {
        testObject = GameObject.Instantiate(new GameObject());
        testObject.AddComponent<PlayerMovement>();
    }

    [Test]
    public void BillTest()
    {
        // Use the Assert class to test conditions
    }

    // A Test behaves as an ordinary method
    [Test]
    public void TestSuiteSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestSuiteWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator Intantiates_GameObject_From_Prefab()
    {
        var playerPrefab = Resources.Load("Prefabs/Setup/Player");

        yield return null;

        var spawnedPlayer = GameObject.FindWithTag("Player");
        var prefabOfSpawnedPlayer = PrefabUtility.GetCorrespondingObjectFromSource(spawnedPlayer);
        Assert.AreEqual(playerPrefab, prefabOfSpawnedPlayer);

        
    }

    [UnityTest]
    public IEnumerator CharacterMovesRight()
    {
       var character = new GameObject();
       character.AddComponent<PlayerMovement>();

       yield return null; 
    }
}
