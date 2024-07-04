using UnityEngine.Assertions;

[System.Serializable]
public class XPLevel
{
    public uint xp = 0;

    // do not serialize xpNeeded
    readonly uint[] xpNeeded; // = new uint[] { 0, 0, 100, 200, 300, 400, 500 }

    public XPLevel(uint[] xpNeeded)
    {
        Assert.IsTrue(xpNeeded.Length >= 2);
        Assert.IsTrue(xpNeeded[0] == 0);
        Assert.IsTrue(xpNeeded[1] == 0);
        this.xpNeeded = xpNeeded;
    }

    public int Level
    {
        get
        {
            for (int i = xpNeeded.Length - 1; i >= 0; i--)
            {
                if (xp >= xpNeeded[i])
                    return i;
            }
            // It is asserted Level >= 1

            // Should never reach here
            return 0;
        }
    }

    public float Progress
    {
        get
        {
            if (Level == xpNeeded.Length - 1)
                return 1.0f;
            return (float)(xp - xpNeeded[Level]) / (xpNeeded[Level + 1] - xpNeeded[Level]);
        }
    }
}
