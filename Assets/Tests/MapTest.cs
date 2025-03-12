using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using HopBotNamespace;

public class MapTest
{
    private Map map;
    private int rows = 10;
    private int cols = 10;

    [SetUp]
    public void SetUp()
    {
        map = new Map(rows, cols);
    }
    // A Test behaves as an ordinary method
    [Test]
    public void TestMapInitialization()
    {
        Assert.AreEqual(rows, map.GetNumRows());
        Assert.AreEqual(cols, map.GetNumCols());
    }

    [Test]
    public void TestValidPath()
    {
        map.GenerateMap("easy");
        bool startTileFound = false;
        bool endTileFound = false;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] is SafeTile safeTile)
                {
                    if (safeTile.GetIsStart())
                    {
                        startTileFound = true;
                    }
                    if (safeTile.GetIsEnd())
                    {
                        endTileFound = true;
                    }
                }
            }
        }
        Assert.IsTrue(startTileFound, "No start tile generated");
        Assert.IsTrue(endTileFound, "No end tile generated");
    }

    [Test]
    public void TestValidTiles()
    {
        map.GenerateTiles(0.2, 0.8, 0.9, 6);

        bool hasSafeTile = false;
        bool hasBreakableTile = false;
        bool hasDangerousTile = false;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] is SafeTile)
                    hasSafeTile = true;
                else if (map[i, j] is BreakableTile)
                    hasBreakableTile = true;
                else if (map[i, j] is DangerousTile)
                    hasDangerousTile = true;
            }
        }
        Assert.IsTrue(hasSafeTile, "No safe tiles generated");
        Assert.IsTrue(hasBreakableTile, "No breakable tiles generated");
        Assert.IsTrue(hasDangerousTile, "No dangerous tiles generated");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestMapGeneration()
    {   
        map.GenerateMap("easy");
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return new WaitForSeconds(0.5f);

        Assert.IsNotNull(map);
        Assert.AreEqual(rows, map.GetNumRows());
        Assert.AreEqual(cols, map.GetNumCols());

        bool hasTile = false;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i, j] is SafeTile)
                {
                    hasTile = true;
                    break;
                }
            }
        }
        Assert.IsTrue(hasTile);
    }
}
