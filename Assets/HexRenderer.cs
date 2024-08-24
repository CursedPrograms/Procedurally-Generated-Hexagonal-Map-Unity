using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class HexRenderer : MonoBehaviour
{
    Mesh m_mesh;
    MeshFilter m_meshFilter;
    MeshRenderer m_meshRenderer;

    List<Face> m_faces;

    public Material material;

    private void Awake()
    {
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        m_mesh = new Mesh();
        m_mesh.name = "Hex";

        m_meshFilter.mesh = m_mesh;
        m_meshRenderer.material = material;
    }

    public struct Face
    {
        public List<Vector3> vertices { get; private set; }
        public List<int> triangles { get; private set; }
        public List<Vector2> uvs { get; private set; }

        public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
        {
            this.vertices = vertices;
            this.triangles = triangles;
            this.uvs = uvs;
        }
    }

    private void OnEnable()
    {
        DrawMesh();

    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            DrawMesh();
        }
    }

    void DrawMesh()
    {
        DrawFaces();
        CombineFaces();
    }

    void DrawFaces()
    {
        m_faces = new List<Face>();
        for(int point = 0; point < 6; point++)
        {

        }
    }

    void CombineFaces()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < vertices.Count; i++)
        {
            vertices.AddRange(m_faces[i].vertices);
            uvs.AddRange(m_faces[i].uvs);

            int Offset = (4 * i);
            foreach (int triangle in m_faces[i].triangles)
            {
                tris.Add(triangle + Offset);
            }
        }

        m_mesh.vertices = vertices.ToArray();
        m_mesh.triangles = tris.ToArray();
        m_mesh.uv = uvs.ToArray();
        m_mesh.RecalculateNormals();
    }

    private Face CreateFace(float innerRad, float outerRad, float HeightA, float HeightB, int point, bool reverse = false)
    {
        Vector3 PointA = GetPoint(innerRad, HeightB, point);//
        return new Face();
    }

    protected Vector3 GetPoint(float size, float height, int index)
    {
        float angleDeg = 60 * index;
        float angleRad = Mathf.PI / 180f * angleDeg;
        return new Vector3((size*Mathf.Cos(angleRad)),height,size*Mathf.Sin(angleRad));
    }
}
