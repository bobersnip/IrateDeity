using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void QuitApplication()
    {
        Debug.Log("Quitting the game.");
        Application.Quit();
    }
}
