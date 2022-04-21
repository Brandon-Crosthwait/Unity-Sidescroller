using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    GameObject player;
    PlayerMovementHelper playerMovementHelper;
    Animator animator;
    bool canMove;
    bool checkPointUnlocked;
    Rigidbody2D rb;
    float gravityScale;


    [SetUp]
    public void Setup()
    {
        canMove = true;
        checkPointUnlocked = false;
        gravityScale = 5.0f;
        playerMovementHelper = new PlayerMovementHelper();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    [Test]
    public void MovementCanBeSetTrue()
    {
        canMove = false;
        playerMovementHelper.SetCanMove(ref canMove, true);
        Assert.IsTrue(canMove);
    }

    [Test]
    public void MovementCanBeSetFalse()
    {
        canMove = true;
        playerMovementHelper.SetCanMove(ref canMove, false);
        Assert.IsFalse(canMove);
    }

    [Test]
    public void PlayerWasHit()
    {
        playerMovementHelper.GetHit(animator);
        Assert.IsTrue(animator.GetBool("isHit"));
    }

    [Test]
    public void UnlockCheckpoint()
    {
        playerMovementHelper.SetCheckpointActive(ref checkPointUnlocked);
        Assert.IsTrue(checkPointUnlocked);
    }

    [Test]
    public void IsDeadAfterDeath()
    {
        playerMovementHelper.PlayerDeath(animator, rb, ref canMove);
        Assert.IsTrue(animator.GetBool("isDead"));
    }

    [Test]
    public void IsNotJumpingAfterDeath()
    {
        playerMovementHelper.PlayerDeath(animator, rb, ref canMove);
        Assert.IsFalse(animator.GetBool("isJumping"));
    }

    [Test]
    public void NoGravityAfterDeath()
    {
        playerMovementHelper.PlayerDeath(animator, rb, ref canMove);
        Assert.AreEqual(rb.gravityScale, 0.0f);
    }

    [Test]
    public void CannotMoveAfterDeath()
    {
        canMove = true;
        playerMovementHelper.PlayerDeath(animator, rb, ref canMove);
        Assert.IsFalse(canMove);
    }

    [Test]
    public void CanMoveAfterRespawn()
    {
        canMove = false;
        playerMovementHelper.Respawn(animator, rb, ref canMove, gravityScale);
        Assert.IsTrue(canMove);
    }

    [Test]
    public void IsNotDeadAfterRespawn()
    {
        playerMovementHelper.Respawn(animator, rb, ref canMove, gravityScale);
        Assert.IsFalse(animator.GetBool("isDead"));
    }

    [Test]
    public void AppearAfterRespawn()
    {
        playerMovementHelper.Respawn(animator, rb, ref canMove, gravityScale);
        Assert.IsTrue(animator.GetBool("Appear"));
    }

    [Test]
    public void PlayerEffectedByGravityAfterRespawn()
    {
        playerMovementHelper.Respawn(animator, rb, ref canMove, gravityScale);
        Assert.AreEqual(rb.gravityScale, gravityScale);
    }
}
