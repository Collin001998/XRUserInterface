using System.Linq;
using Meta.XR.ImmersiveDebugger.UserInterface;
using UnityEngine;

public class MoveUI : MonoBehaviour
{
    public bool iniciated = true;
    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private Transform eyeTransform;
    [SerializeField] private GameObject UIRoot;

    //[SerializeField] private GameObject Magnifier;
    [SerializeField] private MoveMagnifier moveMagnifier;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startMoveUI()
    {
        iniciated = true;
    }
    void FixedUpdate()
    {
        if (iniciated)
        {
            moveMagnifier.ToggleMagnifier(false);
            Vector3 direction = eyeTransform.TransformDirection(Vector3.forward) * rayDistance;
            UIRoot.transform.position = eyeTransform.position + direction;
            UIRoot.transform.position = new Vector3(UIRoot.transform.position.x, UIRoot.transform.position.y, 0.4f);

            if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 1f)
            {
                iniciated = false;
                moveMagnifier.ToggleMagnifier(true);
            }
        }
    }
}
