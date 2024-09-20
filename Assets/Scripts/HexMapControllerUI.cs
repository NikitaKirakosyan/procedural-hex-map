using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HexMapControllerUI : MonoBehaviour
{
    [SerializeField] private Color[] _colors;
    [Header("UI")] 
    [SerializeField] private ToggleGroup _toggleGroup;
    [SerializeField] private ColorToggle _colorTogglePrefab;

    private Color _activeColor;
    private ColorToggle[] _colorToggles;
    

    private void Awake()
    {
        CreateColorToggles();
        SelectColor(0);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput();
        }
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hit))
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(Camera.main.transform.position, hit.point);
        }
    }
#endif


    private void CreateColorToggles()
    {
        _colorToggles = new ColorToggle[_colors.Length];
        
        for(var i = 0; i < _colors.Length; i++)
        {
            var colorToggle = Instantiate(_colorTogglePrefab, _toggleGroup.transform);
            colorToggle.Setup(i, SelectColor, _colors[i], i == 0, _toggleGroup);
            _colorToggles[i] = colorToggle;
        }
    }
    
    private void HandleInput()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out var hit))
        {
            HexGrid.Instance.ColorCell(hit.point, _activeColor);
        }
    }
    
    private void SelectColor(int index)
    {
        _activeColor = _colors[index];
    }
}
