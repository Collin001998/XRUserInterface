using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class MinigameManager : MonoBehaviour
{
    [SerializeField]private GameObject Star;
    [SerializeField] private Transform eyeTransform;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rayDistance = 1f;
    
    [SerializeField] private List<Material> selectedMaterial;
    private bool GameStarted = false;
    
    private GameObject hoveredStar;
    [SerializeField]private List<GameObject> Stars;
    
    [SerializeField] Game_LineRenderer _lineRenderer;

    [SerializeField] private UnityEvent _event;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (GameStarted)
        {
            RaycastHit hit;
            Vector3 direction = eyeTransform.TransformDirection(Vector3.forward) * rayDistance;
            
            if (Physics.Raycast(eyeTransform.position, direction, out hit, Mathf.Infinity, layerMask)){

                if (hoveredStar != hit.collider.gameObject)
                {
                    hoveredStar = hit.collider.gameObject;
                    if (!hit.collider.gameObject.GetComponent<Game_Star>().connected)
                    {
                        hit.collider.gameObject.GetComponent<Game_Star>().connected = true;
                        hoveredStar.GetComponent<MeshRenderer>().SetMaterials(selectedMaterial);
                        _lineRenderer.AddPoint(hit.collider.transform.position);
                        WinCheck();
                    }
                }
                else
                {
                    
                }
                
            }
        }
    }

    public void SpawnStars(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 RandomLoc = RandomVectorInCollider(this.GetComponent<Collider>());
            Stars.Add(Instantiate(Star, RandomLoc, new Quaternion(0, 0, 0, 0)));
        }

        GameStarted = true;
    }
    public static Vector3 RandomVectorInCollider(Collider collider)
    {
        Bounds bounds = collider.bounds;
        Vector3 location = new Vector3(Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y), Random.Range(bounds.min.z, bounds.max.z));
        return location;
    }

    private bool WinCheck()
    {
        foreach (var star in Stars)
        {
            if (!star.GetComponent<Game_Star>().connected)
            {
                
                return false;
            }
        }
        _event.Invoke();
        foreach (var star in Stars)
        {
            Destroy(star);
        }
        Stars.Clear();
        _lineRenderer.Reset();
        return true;
    }
}
