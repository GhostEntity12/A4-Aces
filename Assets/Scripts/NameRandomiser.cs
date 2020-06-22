using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameRandomiser : MonoBehaviour
{
    TextMeshPro whiteboardText;
    // Start is called before the first frame update
    void Start()
    {
        int a = Random.Range(0, 9);
        whiteboardText = GetComponent<TextMeshPro>();
        whiteboardText.text = "Happy birthday" + (
            a == 0 ? "\nJames!!" :
            a == 1 ? "\nNoah!!" :
            a == 2 ? "\nMarcus!!" :
            a == 3 ? "\nAaron!!" :
            a == 4 ? "\nAbbey!!" :
            a == 5 ? "\nAlicia!!" :
            a == 6 ? "\nDeclan!!" :
            a == 7 ? "\nOnii!!" :
            a == 8 ? "\nRegina!!" :
            "!!");
    }
}
