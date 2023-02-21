using Apartments;
using General;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class ProblemDialogue : MonoBehaviour
    {

        [SerializeField] List<Sprite> _emotes;
        [SerializeField] SpriteRenderer _emoteWindow;

        public void ChangeEmote(DoubleState type)
        {
            switch (type)
            {
                case DoubleState.Happy:
                    _emoteWindow.sprite = _emotes[0];
                    GetComponent<WindowManager>().state = DoubleState.Happy;
                    break;
                case DoubleState.Buy:
                    _emoteWindow.sprite = _emotes[1];
                    GetComponent<WindowManager>().state = DoubleState.Buy;
                    break;
                case DoubleState.MakeFriend:
                    _emoteWindow.sprite = _emotes[2];
                    GetComponent<WindowManager>().state = DoubleState.MakeFriend;
                    break;
                case DoubleState.Confession:
                    _emoteWindow.sprite = _emotes[3];
                    GetComponent<WindowManager>().state = DoubleState.Confession;
                    break;
                case DoubleState.Angry:
                    _emoteWindow.sprite = _emotes[4];
                    GetComponent<WindowManager>().state = DoubleState.Angry;
                    break;
                case DoubleState.Sick:
                    _emoteWindow.sprite = _emotes[5];
                    GetComponent<WindowManager>().state = DoubleState.Sick;
                    break;
                case DoubleState.Date:
                    _emoteWindow.sprite = _emotes[6];
                    GetComponent<WindowManager>().state = DoubleState.Date;
                    break;
                case DoubleState.Hungry:
                    _emoteWindow.sprite = _emotes[7];
                    GetComponent<WindowManager>().state = DoubleState.Hungry;
                    break;
                case DoubleState.Sad:
                    _emoteWindow.sprite = _emotes[8];
                    GetComponent<WindowManager>().state = DoubleState.Sad;
                    break;
                default:
                    _emoteWindow.sprite = _emotes[0];
                    GetComponent<WindowManager>().state = DoubleState.Happy;
                    Debug.Log("Reached default");
                    break;
            }
        }
    }
}