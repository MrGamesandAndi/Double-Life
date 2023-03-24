using UnityEngine;

namespace General
{
    public class DisplayProblem : MonoBehaviour
    {
        private void OnMouseDown()
        {
            switch (GameManager.Instance.currentLoadedDouble.CurrentState)
            {
                case DoubleState.Happy:
                    RoomManager.Instance.DialogueRunner.StartDialogue($"HappySpeech{Random.Range(1, 10)}");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Buy:
                    RoomManager.Instance.DialogueRunner.StartDialogue($"Furniture1");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.MakeFriend:
                    RoomManager.Instance.DialogueRunner.StartDialogue($"Friend1");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Confession:
                    RoomManager.Instance.DialogueRunner.StartDialogue($"Love2");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Angry:
                    RoomManager.Instance.DialogueRunner.StartDialogue($"Fight1");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Sick:
                    RoomManager.Instance.DialogueRunner.StartDialogue($"Sickness{Random.Range(1, 3)}");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Date:
                    RoomManager.Instance.DialogueRunner.StartDialogue($"Love1");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Hungry:
                    RoomManager.Instance.DialogueRunner.StartDialogue($"Hunger{Random.Range(1, 5)}");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.Sad:
                    RoomManager.Instance.DialogueRunner.StartDialogue($"Sad{Random.Range(2, 4)}");
                    gameObject.SetActive(false);
                    break;
                case DoubleState.BreakUp:
                    RoomManager.Instance.DialogueRunner.StartDialogue($"RelFailed2");
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }          
        }
    }
}