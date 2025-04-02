using System;
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EyeTackingDemo : MonoBehaviour
{
    [SerializeField]
    private float rayDistance = 1f;
    [SerializeField]
    private float rayWith = 0.1f;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Color rayColorDefaultState = Color.yellow;

    [SerializeField]
    private Color rayColorHoverState = Color.red;

    private LineRenderer lineRenderer;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetupRay();
    }

    private void SetupRay()
    {
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = rayWith;
        lineRenderer.endWidth = rayWith;
        lineRenderer.startColor = rayColorDefaultState;
        lineRenderer.endColor = rayColorDefaultState;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, new Vector3(transform.position.x,transform.position.y,transform.position.z + rayDistance));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
