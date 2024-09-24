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


    private void TriangulateConnection(HexDirection direction, HexCell cell, Vector3 v1, Vector3 v2)
    {
        var neighbor = cell.GetNeighbor(direction);
        if(neighbor == null)
        {
            return;
        }

        var bridge = HexMetrics.GetBridge(direction);
        var v3 = v1 + bridge;
        var v4 = v2 + bridge;

        AddQuad(v1, v2, v3, v4);
        AddQuadColor(cell.Color, neighbor.Color);

        var nextNeighbor = cell.GetNeighbor(direction.Next());
        if(direction <= HexDirection.E && nextNeighbor != null)
        {
            AddTriangle(v2, v4, v2 + HexMetrics.GetBridge(direction.Next()));
            AddTriangleColor(cell.Color, neighbor.Color, nextNeighbor.Color);
        }
    }

    private void Triangulate(HexDirection direction, HexCell cell)
    {
        var center = cell.transform.localPosition;
        var v1 = center + HexMetrics.GetFirstSolidCorner(direction);
        var v2 = center + HexMetrics.GetSecondSolidCorner(direction);

        AddTriangle(center, v1, v2);
        AddTriangleColor(cell.Color);

        if(direction <= HexDirection.SE)
        {
            TriangulateConnection(direction, cell, v1, v2);
        }
    }

    private void Triangulate(HexCell cell)
    {
        for(var direction = HexDirection.NE; direction <= HexDirection.NW; direction++)
        {
            Triangulate(direction, cell);
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

    private void AddTriangleColor(Color c1, Color c2, Color c3)
    {
        _colors.Add(c1);
        _colors.Add(c2);
        _colors.Add(c3);
    }

    private void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        var vertexIndex = _vertices.Count;
        _vertices.Add(v1);
        _vertices.Add(v2);
        _vertices.Add(v3);
        _vertices.Add(v4);
        
        _triangles.Add(vertexIndex);
        _triangles.Add(vertexIndex + 2);
        _triangles.Add(vertexIndex + 1);
        _triangles.Add(vertexIndex + 1);
        _triangles.Add(vertexIndex + 2);
        _triangles.Add(vertexIndex + 3);
    }

    private void AddQuadColor(Color c1, Color c2, Color c3, Color c4)
    {
        _colors.Add(c1);
        _colors.Add(c2);
        _colors.Add(c3);
        _colors.Add(c4);
    }
    
    private void AddQuadColor (Color c1, Color c2) 
    {
        _colors.Add(c1);
        _colors.Add(c1);
        _colors.Add(c2);
        _colors.Add(c2);
    }
}
