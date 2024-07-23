using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SetTransparencySortingAxis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         // Set the transparency sorting axis to be y-axis based
        Vector3 sortingAxis = new Vector3(0, 1, 0); 
        GraphicsSettings.transparencySortMode = TransparencySortMode.CustomAxis;
        GraphicsSettings.transparencySortAxis = sortingAxis;

        // Print messages to the console to confirm the script is working
        Debug.Log("Transparency Sort Mode set to: " + GraphicsSettings.transparencySortMode);
        Debug.Log("Transparency Sort Axis set to: " + GraphicsSettings.transparencySortAxis);
    }

}
