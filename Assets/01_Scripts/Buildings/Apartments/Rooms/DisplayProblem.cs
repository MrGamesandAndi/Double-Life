using General;
using Needs;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings.Apartments.Rooms
{
    public class DisplayProblem : MonoBehaviour
    {
        [SerializeField] List<DialogueTrigger> _problems;

        private void Start()
        {
            _problems = GameManager.Instance.Problems;
        }

        private void OnMouseDown()
        {
            RoomManager.Instance.HideTabs();

            switch (GameManager.Instance.currentLoadedDouble.CurrentState)
            {
                case NeedType.Happy:
                    _problems[5].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case NeedType.BuyFurniture:
                    _problems[4].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case NeedType.MakeFriend:
                    _problems[3].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case NeedType.TalkToFriend:
                    _problems[10].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case NeedType.ConfessLove:
                    _problems[0].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case NeedType.HaveFight:
                    _problems[2].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case NeedType.Sickness:
                    _problems[9].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case NeedType.HaveDate:
                    _problems[1].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case NeedType.Hunger:
                    _problems[6].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case NeedType.HaveDepression:
                    _problems[7].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case NeedType.BreakUp:
                    _problems[8].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}