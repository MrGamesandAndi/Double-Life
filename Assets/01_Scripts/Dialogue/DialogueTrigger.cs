using Localisation;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] List<DialogueString> _dialogueStrings = new List<DialogueString>();

    bool _hasSpoken = false;

    public void InitiateDialogue()
    {
        DialogueManager.Instance.DialogueStart(_dialogueStrings);
        _hasSpoken = true;
    }
}

[Serializable]
public class DialogueString
{
    public LocalisedString text;
    public bool isEnd;

    [Header("Branch")]
    public bool isQuestion;
    public LocalisedString answerOption1;
    public LocalisedString answerOption2;
    public int option1IndexJump;
    public int option2IndexJump;

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;
}
