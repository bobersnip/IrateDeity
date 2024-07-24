using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintDex : MonoBehaviour
{
    [SerializeField] GameObject playerCharacter;
    Character stats;
    [SerializeField] TMPro.TextMeshProUGUI textField;

    // Start is called before the first frame update
    void Start()
    {
        stats = playerCharacter.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        textField.text = "DEX " + stats.GetDexterity().ToString();
    }
}
