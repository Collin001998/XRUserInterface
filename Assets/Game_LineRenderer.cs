using System.Collections.Generic;
using UnityEngine;

public class Game_LineRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private List<Vector3> _points = new List<Vector3>();

    private int _pointsCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lineRenderer = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void AddPoint(Vector3 point)
    {
        Debug.Log(point);
        _points.Insert(_pointsCount, point);
        _pointsCount++;
        _lineRenderer.positionCount = _points.Count;
        _lineRenderer.SetPositions(_points.ToArray());
    }
    
    public void Reset()
    {
        
        _points.Clear();
        _pointsCount = 0;
        _lineRenderer.positionCount = _points.Count;
        _lineRenderer.SetPositions(_points.ToArray());
    }
}
