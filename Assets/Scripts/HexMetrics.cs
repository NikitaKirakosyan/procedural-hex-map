using UnityEngine;

public static class HexMetrics
{
    public const float OuterRadius = 10;
    public const float InnerRadius = OuterRadius * 0.866025404f;
    public const float SolidFactor = 0.75f;
    public const float BlendFactor = 1f - SolidFactor;

    private static Vector3[] Corners =
    {
        new (0, 0, OuterRadius),
        new (InnerRadius, 0, OuterRadius * 0.5f),
        new (InnerRadius, 0, OuterRadius * -0.5f),
        new (0, 0, -OuterRadius),
        new (-InnerRadius, 0, OuterRadius * -0.5f),
        new (-InnerRadius, 0, OuterRadius * 0.5f),
        new (0f, 0f, OuterRadius)
    };


    public static Vector3 GetFirstCorner(HexDirection direction)
    {
        return Corners[(int)direction];
    }

    public static Vector3 GetSecondCorner(HexDirection direction)
    {
        return Corners[(int)direction + 1];
    }

    public static Vector3 GetFirstSolidCorner(HexDirection direction)
    {
        return Corners[(int)direction] * SolidFactor;
    }

    public static Vector3 GetSecondSolidCorner(HexDirection direction)
    {
        return Corners[(int)direction + 1] * SolidFactor;
    }

    public static Vector3 GetBridge(HexDirection direction)
    {
        return (GetFirstCorner(direction) + GetSecondCorner(direction)) * BlendFactor;
    }
}