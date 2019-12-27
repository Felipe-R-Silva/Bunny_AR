using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is not part of the project at all
/// It was created to check the behaviour of List<T>.removeAtIndex(index);
/// The final conclusion is that it colapses the list from right to left maintaining the index with the next element of the list
/// </summary>
public class ListTest : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> allObjects;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(prefab, new Vector3(transform.position.x + allObjects.Count*0.2f, transform.position.y, transform.position.z), transform.rotation);
            temp.name = temp.name + "(" + i + ")";
            allObjects.Add(temp);
        }

    }
    public void deleteElement(int index)
    {
        Destroy(allObjects[index]);
        removeAtIndex(index);
        for (int i = 0; i < allObjects.Count; i++)
        {
            print(allObjects[i].name + " " + i);
        }
    }
    public void removeAtIndex(int index)
    {
        allObjects.RemoveAt(index);
    }
}
