using TMPro;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public static HexGrid Instance { get; private set; }
    
    [SerializeField] private HexMesh _hexMesh;
    [SerializeField] private HexCell _cellPrefab;
    [Header("UI")] 
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _cellLabelPrefab;
    [Header("Size")] 
    [SerializeField] private int _width = 6;
    [SerializeField] private int _height = 6;

    private HexCell[] _cells;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _cells = new HexCell[_width * _height];

        var cellIndex = 0;
        for(var z = 0; z < _height; z++)
        {
            for(var x = 0; x < _width; x++)
            {
                var cellInstance = CreateCell(x, z);
                _cells[cellIndex] = cellInstance;
                cellIndex++;
            }
        }
    }

    private void Start()
    {
        _hexMesh.Triangulate(_cells);
    }
    
    
    public void ColorCell(Vector3 position, Color color)
    {
        position = transform.InverseTransformPoint(position);
        var coordinates = HexCoordinates.FromPosition(position);
        var index = coordinates.X + coordinates.Z * _width + coordinates.Z / 2;
        var cell = _cells[index];
        cell.Color = color;
        _hexMesh.Triangulate(_cells);
    }


    private HexCell CreateCell(int x, int z)
    {
        Vector3 cellPosition;
        cellPosition.x = (x + z * 0.5f - z / 2) * (HexMetrics.InnerRadius * 2f);
        cellPosition.y = 0;
        cellPosition.z = z * (HexMetrics.InnerRadius * 1.73f);

        var cellInstance = Instantiate(_cellPrefab, transform, false);
        cellInstance.transform.localPosition = cellPosition;
        cellInstance.SetCoordinates(x, z);

        var cellLabel = Instantiate(_cellLabelPrefab, _canvas.transform, false);
        cellLabel.rectTransform.anchoredPosition = new Vector2(cellPosition.x, cellPosition.z);
        cellLabel.text = cellInstance.Coordinates.ToString();

        return cellInstance;
    }
}