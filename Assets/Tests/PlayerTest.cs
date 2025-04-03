using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    private GameObject playerObject;
    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private Rigidbody rb;

    [SetUp]
    public void SetUp()
    {
        playerObject = new GameObject();
        rb = playerObject.AddComponent<Rigidbody>();
        playerController = playerObject.AddComponent<PlayerController>();
        playerHealth = playerObject.AddComponent<PlayerHealth>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerObject);
    }

    [Test]
    public void PlayerHealthStart()
    {
        Assert.AreEqual(5, playerHealth.GetHealth());
    }

    [Test]
    public void PlayerDamaged()
    {
        int initialHealth = playerHealth.GetHealth();
        playerHealth.TakeDamage(1, Vector3.zero);
        Assert.AreEqual(initialHealth - 1, playerHealth.GetHealth());
    }

    [Test]
    public void PlayerIsInvincible()
    {
        playerHealth.TakeDamage(1, Vector3.zero);
        Assert.IsTrue(playerHealth.GetIsInvincible());
    }

[UnityTest]
    public IEnumerator PlayerFallsBelowThreshold_GameLost()
    {
        playerController.transform.position = new Vector3(0, -20, 0);
        yield return new WaitForSeconds(0.5f);
        Assert.Less(playerController.transform.position.y, playerController.fallThreshold);
    }

    [UnityTest]
    public IEnumerator PlayerLaunchesWhenGrounded()
    {
        playerController.isGrounded = true;
        yield return null;
        Assert.IsTrue(playerController.isJumping);
    }

    [UnityTest]
    public IEnumerator PlayerStopsFalling()
    {
        playerController.isGrounded = false;
        playerController.OnTriggerEnter(new GameObject().AddComponent<BoxCollider>());
        yield return null;
        Assert.IsTrue(playerController.isGrounded);
    }

    [UnityTest]
    public IEnumerator PlayerJump()
    {
        float initialY = playerController.transform.position.y;
        playerController.LaunchPlayer();
        yield return new WaitForSeconds(0.5f);
        Assert.Greater(playerController.transform.position.y, initialY);
    }
    
    [UnityTest]
    public IEnumerator PlayerBecomesVincible()
    {
        playerHealth.BecomeInvincible(1f);
        yield return new WaitForSeconds(1.5f);
        Assert.IsFalse(playerHealth.GetIsInvincible());
    }
}
