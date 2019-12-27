using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This Script Checks If carrot is faild to hit the floor
/// It also has a base Suicide Script to be called to delete the object with crumble effect
/// </summary>
public class Carrot : MonoBehaviour
{

    public int IndexID;
    public GameObject parts;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > 5)  // same speed check as the player
        {
            Destroy(this.gameObject);
        }
    }

    public void separateAndSuicide()
    {
        GetComponent<CapsuleCollider>().enabled = false;  // turns on the collision of the base carrot
        parts.SetActive(true);  // turns on the broken carrot
        parts.transform.parent = null;  // unparents the carrot so its not deleted with the object
        Destroy(transform.gameObject);  // delete the object
    }
}
