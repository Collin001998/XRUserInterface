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

    [SerializeField]
    private GameObject debug;

    private LineRenderer lineRenderer;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetupRay();
    }

    private void SetupRay()
    {
        //lineRenderer.useWorldSpace = false;
        //lineRenderer.positionCount = 2;
        //lineRenderer.startWidth = rayWith;
        //lineRenderer.endWidth = rayWith;
        //lineRenderer.startColor = rayColorDefaultState;
        //lineRenderer.endColor = rayColorDefaultState;
        //lineRenderer.SetPosition(0, transform.position);
        //lineRenderer.SetPosition(1, new Vector3(transform.position.x,transform.position.y,transform.position.z + rayDistance));
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        Vector3 direction = transform.TransformDirection(Vector3.forward) * rayDistance;

        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask)){
            Debug.Log("hit something");
            debug.transform.position = hit.point;
            Debug.Log("Hit Location: " + hit.point);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
