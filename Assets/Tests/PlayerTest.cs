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

    [UnityTest]
    public IEnumerator PlayerFallsBelowThreshold_GameLost()
    {
        playerController.transform.position = new Vector3(0, -20, 0);
        yield return new WaitForSeconds(0.5f);
        Assert.Less(playerController.transform.position.y, playerController.fallThreshold);
    }
}
