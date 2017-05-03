using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillGenerator : MonoBehaviour
{
    public float SegmentLength = 5;
    public int SegmentResolution = 32;
    public int MeshCount = 1;
    public int VisisbleMeshes = 4;
    public MeshFilter SegmentPrefab;
    private Vector3[] _vertexArray;
    private List<MeshFilter> _freeMeshFilters = new List<MeshFilter>();
    private List<Segment> _usedSegments = new List<Segment>();

    void Awake()
    {
        _vertexArray = new Vector3[SegmentResolution * 2];

        int iterations = _vertexArray.Length / 2 - 1;
        var triangles = new int[(_vertexArray.Length - 2) * 3];

        for (int i = 0; i < iterations; i++)
        {
            int i2 = i * 6;
            int i3 = i * 2;

            triangles[i2] = i3 + 2;
            triangles[i2 + 1] = i3 + 1;
            triangles[i2 + 2] = i3 + 0;

            triangles[i2 + 3] = i3 + 2;
            triangles[i2 + 4] = i3 + 3;
            triangles[i2 + 5] = i3 + 1;
        }

        var colors = new Color[_vertexArray.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = new Color(255, 255, 255, 255);
        }

        for (int i = 0; i < MeshCount; i++)
        {
            MeshFilter filter = Instantiate(SegmentPrefab);

            Mesh mesh = filter.mesh;
            mesh.Clear();

            mesh.vertices = _vertexArray;
            mesh.triangles = triangles;
            mesh.colors = colors;

            filter.gameObject.SetActive(false);
            _freeMeshFilters.Add(filter);
        }
    }

    private float GetHeight(float position)
    {
        return ((Mathf.Sin(position) + 1.5f + Mathf.Sin(position * 1.75f) + 1f) / 2f) - (position / 10);
    }

    public void GenerateSegment(int index, ref Mesh mesh)
    {
        float startPosition = index * SegmentLength;
        float step = SegmentLength / (SegmentResolution - 1);

        for (int i = 0; i < SegmentResolution; i++)
        {
            float xPos = step * i;

            float yPostTop = GetHeight(startPosition + xPos);
            _vertexArray[i * 2] = new Vector3(xPos, yPostTop, 0);

            _vertexArray[i * 2 + 1] = new Vector3(xPos, yPostTop - 5, 0);
        }

        mesh.vertices = _vertexArray;

        mesh.RecalculateBounds();
    }

    private bool IsSegmentInSight(int index)
    {
        Vector3 worldLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 worldRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float x1 = index * SegmentLength;
        float x2 = x1 + SegmentLength;

        return x1 <= worldRight.x && x2 >= worldLeft.x;
    }

    private bool IsSegmentVisible(int index)
    {
        return SegmentCurrentlyVisibleListIndex(index) != -1;
    }

    private int SegmentCurrentlyVisibleListIndex(int index)
    {
        for (int i = 0; i < _usedSegments.Count; i++)
        {
            if (_usedSegments[i].Index == index)
            {
                return i;
            }
        }

        return -1;
    }

    private void EnsureSegmentVisible(int index)
    {
        if (!IsSegmentVisible(index))
        {
            int meshIndex = _freeMeshFilters.Count - 1;
            MeshFilter filter = _freeMeshFilters[meshIndex];
            _freeMeshFilters.RemoveAt(meshIndex);

            Mesh mesh = filter.mesh;
            GenerateSegment(index, ref mesh);

            filter.transform.position = new Vector3(index * SegmentLength, 0, 0);
            filter.gameObject.SetActive(true);

            var segment = new Segment();
            segment.Index = index;
            segment.MeshFilter = filter;

            _usedSegments.Add(segment);
        }
    }

    private void EnsureSegmentNotVisible(int index)
    {
        if (IsSegmentVisible(index))
        {
            int listIndex = SegmentCurrentlyVisibleListIndex(index);
            Segment segment = _usedSegments[listIndex];
            _usedSegments.RemoveAt(listIndex);

            MeshFilter filter = segment.MeshFilter;
            filter.gameObject.SetActive(false);

            _freeMeshFilters.Add(filter);
        }
    }

    void Update()
    {
        Vector3 worldCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        int currentSegment = (int)(worldCenter.x / SegmentLength);

        for (int i = 0; i < _usedSegments.Count;)
        {
            int segmentIndex = _usedSegments[i].Index;
            if (!IsSegmentInSight(segmentIndex))
            {
                EnsureSegmentNotVisible(segmentIndex);
            }
            else
            {
                i++;
            }
        }

        for (int i = currentSegment - VisisbleMeshes / 2; i < currentSegment + VisisbleMeshes / 2; i++)
        {
            if (IsSegmentInSight(i))
            {
                EnsureSegmentVisible(i);
            }
        }
    }

    private struct Segment
    {
        public int Index { get; set; }
        public MeshFilter MeshFilter { get; set; }
    }
}