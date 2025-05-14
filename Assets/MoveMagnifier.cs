using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using Slider = UnityEngine.UI.Slider;

public class MoveMagnifier : MonoBehaviour
{
    [SerializeField] private GameObject Magnifier;
    [SerializeField] private Transform eyeTransform;
    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float _smoothingFactor = 20f;
    [SerializeField] private float _movingthreshold;
    
    [SerializeField] private Slider settingsSlider;

    private float magnificationStrenth = 1f;
    private bool Active = true;
    private Vector3 _lastGazePosition;
    private Vector3 _newGazePosition;
    private Vector3 _smoothingVector;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ChangeStrenth()
    {
        
        //float strenth = UnityEngine.Random.Range(0f, 1f);
        float strenth = Mathf.Lerp(40f, 0f, settingsSlider.value);
        Magnifier.GetComponentInChildren<Camera>().fieldOfView = strenth;
    }
    public void ChangeStrenth2(float strenth)
    {
        
        //float strenth = UnityEngine.Random.Range(0f, 1f);
        Magnifier.GetComponentInChildren<Camera>().fieldOfView = Mathf.Lerp(40f, 0f, strenth);
    }
    public void ToggleMagnifier(bool toggle)
    {
        Active = toggle;
        Magnifier.SetActive(toggle);
    }
    private void FixedUpdate()
    {
        

        if (Active)
        {
            RaycastHit hit;

            Vector3 direction = eyeTransform.TransformDirection(Vector3.forward) * rayDistance;
            
            if (Physics.Raycast(eyeTransform.position, direction, out hit, Mathf.Infinity, layerMask)){
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
