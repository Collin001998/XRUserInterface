using UnityEngine;

public class MoveMagnifier : MonoBehaviour
{
    [SerializeField] private GameObject Magnifier;
    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private LayerMask layerMask;

    private bool Active = true;

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
                Magnifier.transform.position = hit.point;
                Magnifier.transform.rotation = hit.transform.rotation;
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
