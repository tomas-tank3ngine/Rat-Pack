using UnityEngine;

public static class SatisfactionSystem
{
    public static float CalculateStarDelta(
    CustomerOrder order,
    float deliveredWeight,
    string deliveredFlavour
)
    {
        float weightError = Mathf.Abs(order.requestedWeight - deliveredWeight);

        float weightScore = 1f - Mathf.Clamp01(weightError / 10f);

        int flavourDiff = GetFlavourDifference(order.flavour, deliveredFlavour);

        float flavourScore = 1f - Mathf.Clamp01(flavourDiff / 5f);

        float W = 0.7f;
        float F = 0.3f;

        float finalScore =
            (weightScore * W) +
            (flavourScore * F);

        float adjusted = finalScore * finalScore;

        return (adjusted - 0.5f);
    }
    public static float CalculateStars(
        CustomerOrder order,
        float deliveredWeight,
        string deliveredFlavour
    )
    {
        float weightDiff =
            Mathf.Abs(order.requestedWeight - deliveredWeight);

        int flavourDiff =
            GetFlavourDifference(order.flavour, deliveredFlavour);

        float starChange = 0f;

        // Weight influence (you can tune this)
        if (weightDiff == 0)
            starChange += 0.5f;
        else if (weightDiff <= 1)
            starChange += 0.25f;
        else if (weightDiff <= 2)
            starChange += 0f;
        else if (weightDiff <= 3)
            starChange -= 0.25f;
        else
            starChange -= 0.5f;

        // Flavour adjustment
        if (flavourDiff == 1)
            starChange += 0.25f;
        else if (flavourDiff == 3)
            starChange -= 0.25f;
        else if (flavourDiff >= 4)
            starChange -= 0.5f;

        return starChange;
    }

    private static int GetFlavourDifference(
        string a,
        string b
    )
    {
        // simple placeholder system for now
        if (a == b) return 1;
        return 3;
    }
}