using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private MeshCollider _meshCollider;
    
    private Mesh _mesh;
    private readonly List<Vector3> _vertices = new ();
    private readonly List<int> _triangles = new ();
    private readonly List<Color> _colors = new ();


    private void Awake()
    {
        _meshFilter.mesh = _mesh = new Mesh();
        _mesh.name = "HexMesh";
    }


    public void Triangulate(HexCell[] cells)
    {
        _mesh.Clear();
        _vertices.Clear();
        _triangles.Clear();
        _colors.Clear();

        foreach(var cell in cells)
        {
            Triangulate(cell);
        }

        _mesh.vertices = _vertices.ToArray();
        _mesh.triangles = _triangles.ToArray();
        _mesh.colors = _colors.ToArray();
        _mesh.RecalculateNormals();
        
        _meshCollider.sharedMesh = _mesh;
    }


    private void Triangulate(HexCell cell)
    {
        var center = cell.transform.localPosition;
        for(var i = 0; i < 6; i++)
        {
            var v2 = center + HexMetrics.Corners[i];
            var v3 = center + HexMetrics.Corners[i + 1];
            AddTriangle(center, v2, v3);
            AddTriangleColor(cell.Color);
        }
    }

    private void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        var vertexIndex = _vertices.Count;
        
        _vertices.Add(v1);
        _vertices.Add(v2);
        _vertices.Add(v3);
        
        _triangles.Add(vertexIndex);
        _triangles.Add(vertexIndex + 1);
        _triangles.Add(vertexIndex + 2);
    }

    private void AddTriangleColor(Color color)
    {
        for(var i = 0; i < 3; i++)
        {
            _colors.Add(color);
        }
    }
}
