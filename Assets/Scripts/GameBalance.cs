using UnityEngine;

public static class GameBalance
{
    private static int difficulty = 0;

    public static void AddDifficulty(int addingDifficulty)
    {
        difficulty += addingDifficulty;
        PlayerPrefs.SetInt("Difficulty", difficulty);
    }

    public static float GetRoundDuration()
    {
        return 40 + difficulty * 5;
    }

    public static float GetCannonCooldown()
    {
        return Mathf.Max(1.4f - 0.05f * difficulty, 0.4f) + Random.Range(0, Mathf.Min(difficulty, 8) * 0.1f);
    }

    public static int GetScoreAim()
    {
        return 4 + difficulty * 2;
    }

    public static Vector3[] GetCannonsPosition()
    {
        float[] x;

        int[] z = { 6 + 3 * Random.Range(0, 5), 6 + 3 * Random.Range(0, 5) };
        if (difficulty > 4)
        {
            x = new float[2];
            x[0] = Random.Range(1.0f, z[0] / 3.0f);
            x[1] = -Random.Range(1.0f, z[1] / 3.0f);
            return new Vector3[] { new Vector3(x[0], 0, z[0]), new Vector3(x[1], 0, z[1]) };
        }
        else
        {
            x = new float[1];
            x[0] = Random.Range(-z[0] / 3.0f, z[0] / 3.0f);
            return new Vector3[] { new Vector3(x[0], 0, z[0]) };
        }
    }

    public static int GetLevelCost()
    {
        return 2 + 2 * difficulty;
    }
}
