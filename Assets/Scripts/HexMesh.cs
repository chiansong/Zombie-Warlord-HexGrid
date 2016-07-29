using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangle;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangle = new List<int>();
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);

        triangle.Add(vertexIndex);
        triangle.Add(vertexIndex + 1);
        triangle.Add(vertexIndex + 2);
    }

    public void Triangulate(HexCell[] cells)
    {
        hexMesh.Clear();
        vertices.Clear();
        triangle.Clear();

        for(int i = 0; i < cells.Length; i++)
        {
            Triangulate(cells[i]);
        }

        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangle.ToArray();
        hexMesh.RecalculateNormals();
    }

    void Triangulate(HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;

        for (int i = 0; i < 6; ++i)
        {
            AddTriangle(center,
                center + HexMetrics.corners[i],
                center + HexMetrics.corners[i + 1]);
        }
    }
}
