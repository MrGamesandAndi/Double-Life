using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    public Transform target;

    /*  
    /!\ Don't forget to make the texture readable
    (Select your texture : in Inspector
    [Texture Import Setting] > Texture Type > Advanced > Read/Write enabled > True  then Apply).
    */
    public Texture2D colorPicker;

    public Rect colorPanelRect = new Rect(0, 0, 200, 200);

    void OnGUI()
    {
        GUI.DrawTexture(colorPanelRect, colorPicker);
        if (GUI.RepeatButton(colorPanelRect, ""))
        {
            Vector2 pickpos = Event.current.mousePosition;
            float aaa = pickpos.x - colorPanelRect.x;

            float bbb = pickpos.y - colorPanelRect.y;

            int aaa2 = (int)(aaa * (colorPicker.width / (colorPanelRect.width + 0.0f)));

            int bbb2 = (int)((colorPanelRect.height - bbb) * (colorPicker.height / (colorPanelRect.height + 0.0f)));

            Color col = colorPicker.GetPixel(aaa2, bbb2);

            target.GetComponent<Renderer>().material.color = col;
        }
    }
}
