using General;
using Relationships;
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
            switch (GameManager.Instance.currentLoadedDouble.CurrentState)
            {
                case DoubleState.Happy:
                    _problems[5].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Buy:
                    _problems[4].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case DoubleState.MakeFriend:
                    _problems[3].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Confession:
                    _problems[0].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Angry:
                    _problems[2].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Sick:
                    _problems[9].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Date:
                    _problems[1].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Hungry:
                    _problems[6].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Sad:
                    _problems[7].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                case DoubleState.BreakUp:
                    _problems[7].InitiateDialogue();
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}