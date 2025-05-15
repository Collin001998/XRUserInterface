using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MinigameManager : MonoBehaviour
{
    [SerializeField]private GameObject Star;

    private bool GameStarted = false;
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
        
    }

    public void SpawnStars(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 RandomLoc = RandomVectorInCollider(this.GetComponent<Collider>());
            Instantiate(Star, RandomLoc, new Quaternion(0, 0, 0, 0));
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
}
