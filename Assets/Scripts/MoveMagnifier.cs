using Meta.XR.ImmersiveDebugger.UserInterface.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using Slider = UnityEngine.UI.Slider;

public class MoveMagnifier : MonoBehaviour
{
    [SerializeField] private GameObject testcube;
    [SerializeField] private RayOrigin interactionRayOrigin;
    
    [SerializeField] private GameObject Magnifier;
    [SerializeField] private GameObject MagnifierZoom;
    [SerializeField] private Camera magnifierCamera;
    [SerializeField] private Transform eyeTransform;
    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerMask2;
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
            MagnifierZoom.GetComponent<Renderer>().material.SetFloat("_Radius", OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger));
            // Debug.Log(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger));
            
            Vector3 direction = eyeTransform.TransformDirection(Vector3.forward) * rayDistance;
            
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0)
            {
                interactionRayOrigin.Magnifier = true;
                RaycastHit hit2;
                if (Physics.Raycast(eyeTransform.position, direction, out hit2, Mathf.Infinity, layerMask2)){

                    if (hit2.collider.gameObject)
                    {
                        Vector2 uv = hit2.textureCoord;
                        Debug.Log("hit:" + uv);
                        Vector3 screenPos = new Vector3(
                            uv.x * magnifierCamera.pixelWidth,
                            uv.y * magnifierCamera.pixelHeight,
                            0
                        );
                        
                        Ray renderCamRay = magnifierCamera.ScreenPointToRay(screenPos);
                        if (Physics.Raycast(renderCamRay, out RaycastHit renderHit,Mathf.Infinity, layerMask))
                        {
                            Debug.Log("Render camera ray hit: " + renderHit.collider.gameObject.name);
                            testcube.transform.position = renderHit.point; //new Vector3(renderHit.point.x, renderHit.point.y, magnifierCamera.transform.position.z);
                        }
                        //testcube.transform.position = screenPos;
                    }
                
                }
                return;
            }
            else
            {
                interactionRayOrigin.Magnifier = false;
            }
            RaycastHit hit;

            
            
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
        
        // if (OVRInput.Get(OVRInput.Button.One))
        // {
        //     if (Active)
        //     {
        //         Active = false;
        //         Magnifier.SetActive(false);
        //     }
        //     else
        //     {
        //         Active = true;
        //     }
        // }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
