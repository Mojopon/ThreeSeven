using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameText : MonoBehaviour, IGameText {

    public Text textObject;

    public void UpdateText(string text)
    {
        if (textObject == null)
        {
            Debug.Log("text object is null! check if the object is attached to the script");
            return;
        }

        if (textObject.enabled == false)
        {
            textObject.enabled = true;
        }

        textObject.text = text;
    }


    public void Disable()
    {
        textObject.enabled = false;
    }
}
