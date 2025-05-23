using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;

public class MoveMagnifier : MonoBehaviour
{
    [SerializeField] private GameObject Magnifier;
    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float _smoothingFactor = 20f;
    [SerializeField] private float _movingthreshold;

    private bool Active = true;
    private Vector3 _lastGazePosition;
    private Vector3 _newGazePosition;
    private Vector3 _smoothingVector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    private void FixedUpdate()
    {
        

        if (Active)
        {
            RaycastHit hit;

            Vector3 direction = transform.TransformDirection(Vector3.forward) * rayDistance;
            
            if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask)){
                Magnifier.SetActive(true);
                _lastGazePosition = Magnifier.transform.position;
                _newGazePosition = hit.point;

                if ((_newGazePosition - _lastGazePosition).magnitude > _movingthreshold)
                {
                    //smooth position
                    _smoothingVector = Vector3.Lerp(_smoothingVector, hit.point, Time.deltaTime * _smoothingFactor);
                    //Magnifier.transform.position = hit.point;
                    Magnifier.transform.position = _smoothingVector;
                    Magnifier.transform.rotation = hit.transform.rotation;
                }
                
            }
            else if (Magnifier.activeSelf)
            {
                Magnifier.SetActive(false);
            }
        }
        
        if (OVRInput.Get(OVRInput.Button.One))
        {
            if (Active)
            {
                Active = false;
                Magnifier.SetActive(false);
            }
            else
            {
                Active = true;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
