using UnityEngine;

public class HexCell : MonoBehaviour
{
    [SerializeField] private HexCell[] _neighbors;
    
    public HexCoordinates Coordinates { get; private set; }
    public Color Color { get; set; }


    public void SetCoordinates(int x, int z)
    {
        Coordinates = HexCoordinates.GetOffsetCoordinates(x, z);
    }

    public HexCell GetNeighbor(HexDirection direction)
    {
        return _neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, HexCell cell)
    {
        _neighbors[(int)direction] = cell;
        cell._neighbors[(int)direction.Opposite()] = this;
    }
}
