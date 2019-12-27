using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is not part of the project at all
/// This a test Script that makes the bunny look at all time at the carrot
/// The final conclusion is that the bunny shoudl look at the X and Z axis and maintains its Y
/// 
/// This is a working script in its own and can be added to any object to look at other by draging the other to the target
/// </summary>
public class lookatk : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
    }
}
