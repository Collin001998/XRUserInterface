using UnityEngine;

public class RayOrigin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject EyeGazeObject;
    [SerializeField] private GameObject MagnifierObject;
    
    [SerializeField] public bool Magnifier;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Magnifier)
        {
            this.transform.position = MagnifierObject.transform.position;
            this.transform.rotation = MagnifierObject.transform.rotation;
        }
        else
        {
            this.transform.position = EyeGazeObject.transform.position;
            this.transform.rotation = EyeGazeObject.transform.rotation;
        }
        
    }
}
