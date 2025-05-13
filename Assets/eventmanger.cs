using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eventmanger : MonoBehaviour
{
    [SerializeField] OVRCameraRig _ovrCameraRig;
    private bool _hasHeightAdjusted = false;

    [Serializable] public enum ProfileType { Offset, Default, Narrowed};

    [SerializeField] private GameObject profileSelector;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    public ProfileType profileType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //add local method CamerarigUpdatedAnchors to the camera rig c# event
        if (_ovrCameraRig != null)
        {
            _ovrCameraRig.UpdatedAnchors += CamerarigOnUpdatedAnchors;
        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (_ovrCameraRig != null)
        {
            _ovrCameraRig.UpdatedAnchors -= CamerarigOnUpdatedAnchors;
        }
    }

    public void SetUIProfile(int profileIndex)
    {
        switch (profileIndex)
        {
            case 0: profileType = ProfileType.Offset; break;
            case 1: profileType = ProfileType.Default; break;
            case 2: profileType = ProfileType.Narrowed; break;
        }

        if (profileType == ProfileType.Default)
        {
            profileSelector.SetActive(false);
            mainMenu.SetActive(true);
        }
        Debug.Log("pressed a button " + profileType);
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    
    void CamerarigOnUpdatedAnchors(OVRCameraRig obj)
    {
        if (_hasHeightAdjusted) return;
        if (obj.centerEyeAnchor.transform.position.y < 0.2) return;
        
        transform.position = new Vector3(transform.position.x, obj.centerEyeAnchor.transform.position.y, transform.position.z);
        _hasHeightAdjusted = true;
        
        
    }
}
