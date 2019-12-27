using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the Script that attaches to the Carrot parts 
/// It interacts with the Carrot.cs Script
/// It sets a timer to self destruction using Coorotine
/// It alows hard coded Time and Random time
/// </summary>
public class suicide : MonoBehaviour
{
    public float time;
    public bool randomTime;
    public Vector2 RandRange;
    void Start()
    {
        if (randomTime)
        {
            time = Random.Range(RandRange.x, RandRange.y);
        }
        // Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(suicideTimer(time));
    }

    IEnumerator suicideTimer(float t)
    {
        yield return new WaitForSeconds(t);
        // Do stuff
        Destroy(this.gameObject);
    }
}
