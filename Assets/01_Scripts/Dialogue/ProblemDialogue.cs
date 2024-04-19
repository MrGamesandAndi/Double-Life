using Buildings.Apartments;
using General;
using Needs;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class ProblemDialogue : MonoBehaviour
    {

        [SerializeField] List<Sprite> _emotes;
        [SerializeField] SpriteRenderer _emoteWindow;

        public void ChangeEmote(NeedType type)
        {
            switch (type)
            {
                case NeedType.Happy:
                    _emoteWindow.sprite = _emotes[0];
                    GetComponent<WindowManager>().state = NeedType.Happy;
                    break;
                case NeedType.BuyFurniture:
                    _emoteWindow.sprite = _emotes[4];
                    GetComponent<WindowManager>().state = NeedType.BuyFurniture;
                    break;
                case NeedType.MakeFriend:
                    _emoteWindow.sprite = _emotes[2];
                    GetComponent<WindowManager>().state = NeedType.MakeFriend;
                    break;
                case NeedType.TalkToFriend:
                    _emoteWindow.sprite = _emotes[2];
                    GetComponent<WindowManager>().state = NeedType.TalkToFriend;
                    break;
                case NeedType.ConfessLove:
                    _emoteWindow.sprite = _emotes[3];
                    GetComponent<WindowManager>().state = NeedType.ConfessLove;
                    break;
                case NeedType.HaveFight:
                    _emoteWindow.sprite = _emotes[4];
                    GetComponent<WindowManager>().state = NeedType.HaveFight;
                    break;
                case NeedType.Sickness:
                    _emoteWindow.sprite = _emotes[4];
                    GetComponent<WindowManager>().state = NeedType.Sickness;
                    break;
                case NeedType.HaveDate:
                    _emoteWindow.sprite = _emotes[3];
                    GetComponent<WindowManager>().state = NeedType.HaveDate;
                    break;
                case NeedType.Hunger:
                    _emoteWindow.sprite = _emotes[4];
                    GetComponent<WindowManager>().state = NeedType.Hunger;
                    break;
                case NeedType.HaveDepression:
                    _emoteWindow.sprite = _emotes[5];
                    GetComponent<WindowManager>().state = NeedType.HaveDepression;
                    break;
                case NeedType.BreakUp:
                    _emoteWindow.sprite = _emotes[5];
                    GetComponent<WindowManager>().state = NeedType.BreakUp;
                    break;
                default:
                    _emoteWindow.sprite = _emotes[0];
                    GetComponent<WindowManager>().state = NeedType.Happy;
                    Debug.Log("Reached default");
                    break;
            }
        }
    }
}