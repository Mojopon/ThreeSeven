using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingTextRenderer : MonoBehaviour, IFloatingTextRenderer 
{
    public Transform textObject;

    public void Initialize()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(3, 3);
    }

    public void RenderText(Vector2 position, string message)
    {
        Transform t = Instantiate(textObject, new Vector3(position.x, position.y, -2), Quaternion.identity) as Transform;
        t.SetParent(gameObject.transform, false);
        t.GetComponent<Text>().text = message;
        t.GetComponent<IMovableTransform>().MoveTo(new Vector3(position.x, position.y, -2) + new Vector3(0, 3, 0));
    }
}
