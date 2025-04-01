using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

/*
    File: LogicManagerTest

    Description: Holds all the unit tests for the LogicManagerScript

    Written By: Jaiden Clee [2025]
*/


public class LogicManagerTests
{
    
    private LogicManagerScript logicManager;
    private ShieldCrackManager shieldCrackManager;

    [SetUp]
    public void Setup()
    {
        //create references to scripts (we need a reference for every script LogicManagerScript references)
        GameObject gameObject = new GameObject();
        logicManager = gameObject.AddComponent<LogicManagerScript>();
        shieldCrackManager = gameObject.AddComponent<ShieldCrackManager>();

        // Assign dummy external scripts to LogicManagerScript
        logicManager.shieldCrackManagerScript = shieldCrackManager;

        // Create reference to UI Text object "shieldStrengthText"
        GameObject shieldStrengthTextObject = new GameObject("ShieldStrengthText");
        shieldStrengthTextObject.AddComponent<Text>();
        logicManager.shieldStrengthText = shieldStrengthTextObject.GetComponent<Text>();

        // Create and initialize cracks array with dummy GameObjects (used in ShieldCrackManager)
        shieldCrackManager.cracks = new GameObject[4]; // Assuming 5 cracks
        for (int i = 0; i < shieldCrackManager.cracks.Length; i++)
        {
            GameObject crack = new GameObject("Crack" + i);
            shieldCrackManager.cracks[i] = crack;
        }

        //Set initial values for LogicManagerScript
        logicManager.maxShieldStrength = 5;
        logicManager.shieldStrength = 5;
    }

    [Test]
    public void DecreaseShieldStrength_ReducesShieldByOne()
    {
        logicManager.DecreaseShieldStrength();
        Assert.AreEqual(4, logicManager.shieldStrength, "Shield strength should decrease by 1.");
    }
    
}
