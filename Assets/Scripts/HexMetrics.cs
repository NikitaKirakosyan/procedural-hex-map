using UnityEngine;

public static class HexMetrics
{
    public const float OuterRadius = 10;
    public const float InnerRadius = OuterRadius * 0.866025404f;

    public static Vector3[] Corners =
    {
        new (0, 0, OuterRadius),
        new (InnerRadius, 0, OuterRadius * 0.5f),
        new (InnerRadius, 0, OuterRadius * -0.5f),
        new (0, 0, -OuterRadius),
        new (-InnerRadius, 0, OuterRadius * -0.5f),
        new (-InnerRadius, 0, OuterRadius * 0.5f),
        new Vector3(0f, 0f, OuterRadius)
    };
}
