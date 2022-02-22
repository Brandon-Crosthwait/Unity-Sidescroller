using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnvironmentDamageTests
{
    private EnvironmentDamageHelper edHelper;
    private GameObject player;

    [SetUp]
    public void SetUp()
    {
        edHelper = new EnvironmentDamageHelper();
        player = new GameObject();
        player.AddComponent<Health>();
        player.AddComponent<BoxCollider2D>();
        player.tag = "Player";
    }

    [Test]
    public void wereEventsTriggered()
    {
        player.GetComponent<Health>().setStartingHealth(3f);
        player.GetComponent<Health>().initializeStartingHealth();
        edHelper.playerTakesDamage(1f, player.GetComponent<Collider2D>());
        Assert.IsTrue(edHelper.wereEventsTriggered);
    }
}
