using System.Collections;
using UnityEngine;

public class DebugTooltip : MonoBehaviour
{
    [SerializeField] private GameObject _tooltip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // tooltipNotification();
        _tooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tooltipNotification()
    {
        StartCoroutine(WaitAndPrint(5f));
    }
    
    private IEnumerator WaitAndPrint(float waitTime)
    {
        _tooltip.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        _tooltip.SetActive(false);
        print("Coroutine ended: " + Time.time + " seconds");
    }
}
