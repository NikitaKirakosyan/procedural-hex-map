using UnityEngine;

public class HexCell : MonoBehaviour
{
    public HexCoordinates Coordinates { get; private set; }
    public Color Color { get; set; }


    public void SetCoordinates(int x, int z)
    {
        Coordinates = HexCoordinates.GetOffsetCoordinates(x, z);
    }
}
