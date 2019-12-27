using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This Script contains the function to start the AR scene called by the menu Button
/// </summary>
public class LoadLevel : MonoBehaviour
{
    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
}
