using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
/// <summary>
/// This code handles the Inputs of the User 
/// -Screen touch 
/// -Spawning objects
/// -UI Tweaking 
/// </summary>
public class ARTapToPlaceObjects : MonoBehaviour
{
    private enum enumMarkers { bunnyMarker=0, carrotMarker=1 };  // to decide what marker to spawn
    int chosenEnum;  // current selected marker
    public List<GameObject> objectToPlace;  // possible objects to Spawn
    public List<GameObject> placedObjects;  // history of spawned carrots
    public GameObject placementIndicator;  // reference to the marker obj
    public GameObject MasterOfAll;  // referece to the global manager
    private ARRaycastManager arOrigin;  // AR- Important for seting placement
    private Pose placementPose;  // AR- Position and rotation of point
    private bool placementPoseIsValid = false;  // check marker visibility
    void Start()
    {
        // finds reference to AR Raycast Manager
        arOrigin = FindObjectOfType<ARRaycastManager>();
        chosenEnum = 0;  // makes shure It starts with bunny spawner and not carrot
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdatePlacementPose();  // set marker placement and rotation of the marker to be the same as phone 
        UpdatePlacementIndicator();  // Hide and unhide marker

        // detects screan Input
        if (placementPoseIsValid && Input.touchCount >0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
           
            // check If Bunny Is instantiated and decides betwin Instantiating the bunny or the carrots
            if (MasterOfAll.GetComponent<GeneralData>().BunnyBasicNodeChase == false)
            {
                Instantiate(objectToPlace[chosenEnum], placementIndicator.transform.position, placementIndicator.transform.rotation);  // place bunny
                MasterOfAll.GetComponent<GeneralData>().BunnyBasicNodeChase = true;
                placementIndicator.GetComponent<placemntIndicatorElements>().childs[chosenEnum].SetActive(false);
                chosenEnum = (int)enumMarkers.carrotMarker;
                placementIndicator.GetComponent<placemntIndicatorElements>().childs[chosenEnum].SetActive(true);
            }
            else
            {
                placedObjects.Add(placeObject(objectToPlace[chosenEnum], placedObjects));  // place carrot
            }
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);  // raycast for the marker

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }

    }
    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

        }
        else
        {
            placementIndicator.SetActive(false);
        }
       
    }
    private GameObject placeObject (GameObject prefab,List<GameObject>objectHistory)
    {
        GameObject temp = Instantiate(prefab, placementIndicator.transform.position, placementIndicator.transform.rotation);
        temp.GetComponent<Carrot>().IndexID = objectHistory.Count;
        objectHistory.Add(temp);
        return temp;
    }
}
