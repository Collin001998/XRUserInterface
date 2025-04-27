using UnityEngine;

public class eventmanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Logpress()
    {
        Debug.Log("pressed a button");
    }
}
