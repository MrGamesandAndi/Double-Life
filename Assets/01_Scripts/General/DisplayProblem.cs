using UnityEngine;
using Yarn.Unity;

namespace General
{
    public class DisplayProblem : MonoBehaviour
    {
        DialogueRunner _dialogueRunner;

        private void Awake()
        {
            _dialogueRunner= FindObjectOfType<DialogueRunner>();
        }

        private void OnMouseDown()
        {
            int randomIndex = 0;

            switch (GameManager.Instance.currentLoadedDouble.CurrentState)
            {
                case DoubleState.Happy:
                    randomIndex = Random.Range(1, 10);
                    _dialogueRunner.StartDialogue($"HappySpeech{randomIndex}");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Buy:
                    _dialogueRunner.StartDialogue($"Furniture1");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.MakeFriend:
                    _dialogueRunner.StartDialogue($"Friend1");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Confession:
                    _dialogueRunner.StartDialogue($"Love2");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Angry:
                    _dialogueRunner.StartDialogue($"Fight1");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Sick:
                    randomIndex = Random.Range(1, 3);
                    _dialogueRunner.StartDialogue($"Sickness{randomIndex}");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Date:
                    _dialogueRunner.StartDialogue($"Love1");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Hungry:
                    randomIndex = Random.Range(1, 5);
                    _dialogueRunner.StartDialogue($"Hunger{randomIndex}");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Sad:
                    randomIndex = Random.Range(2, 4);
                    _dialogueRunner.StartDialogue($"Sad{randomIndex}");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.BreakUp:
                    _dialogueRunner.StartDialogue($"RelFailed2");
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }          
        }
    }
}