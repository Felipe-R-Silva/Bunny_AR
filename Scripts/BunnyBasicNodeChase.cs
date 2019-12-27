using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This Script controlls all functionalities of the bunny
/// -Movment 
/// -animation handlers - Its important to know that all the animation transitions are handled in the animator window
/// -Basic exceptions handlers
/// </summary>
public class BunnyBasicNodeChase : MonoBehaviour
{
    public GameObject NodeGenerator;
    int CurrentIndex;
    public float speed;  // making the speed higher than 8 will trigger the "fall detection" and break this script
    public float targetHitDistance;
    public Animator anim;

    void Start()
    {
        NodeGenerator = GameObject.Find("_Interaction");  // finds were the objects referece hisory is 
        CurrentIndex = 0;  // makes shure character is grabing first position or the list
    // NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects - reference to history of instantiated carrots
    }
    void FixedUpdate()
    {
        // If bunny falls of the plain he will be teleported to the object asigned to folow
        if (GetComponent<Rigidbody>().velocity.magnitude > 8)  // checks the speed to know if he is falling or not  ("fall detection")
        {
            if (NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects == null)
            {
                transform.position = Camera.main.transform.position;
            }
            else
            {
                transform.position = NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects[CurrentIndex].transform.position;
            }
        }
        // deleates empty space for carrots that were deleted and did not free the memory space
        if (NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects[CurrentIndex] == null && NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects.Count > 0)
        {
            NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects.RemoveAt(CurrentIndex);
            anim.SetBool("WalkTime", false);  // turns off walkign animation
        }
        // If the objects list is not empty and the currentelement on the list is not null Trigger behaviour to the bunny 
        if (NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects!= null && NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects[CurrentIndex] != null) {
            anim.SetBool("WalkTime", true);// Turn on walking animation
            anim.SetBool("EatTime", false);// turn off eating animation
            // makes the bunny look at the target carrot
            transform.LookAt(new Vector3(NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects[CurrentIndex].transform.position.x, transform.position.y, NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects[CurrentIndex].transform.position.z));
            float step = speed * Time.deltaTime;  // calculate distance to move setting speed 
            transform.position = Vector3.MoveTowards(transform.position, NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects[CurrentIndex].transform.position, step);  // moves bunny

            // Check if the position of the character and colectable are approximately equal. "targetHitDistance" defines acceptable distance to trigger
            if (Vector3.Distance(transform.position, NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects[CurrentIndex].transform.position) < targetHitDistance)
            {
                anim.SetBool("WalkTime", false);  // End animation
                anim.SetBool("EatTime", true);  // turn on eating animation
                NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects[CurrentIndex].GetComponent<Carrot>().separateAndSuicide();  // turn on carrot self destruction timer
                // Destroy(NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects[CurrentIndex]);
                deleteElement(CurrentIndex);  // removes element from list and world
            }
        }
    }
    // removes element from list and world
    public void deleteElement(int index)
    {
        Destroy(NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects[index]);  // destroy real object
        NodeGenerator.GetComponent<ARTapToPlaceObjects>().placedObjects.RemoveAt(index);  // shift all list back
    }
}
