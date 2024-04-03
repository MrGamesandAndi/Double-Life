using Buildings.Apartments.Rooms;
using Localisation;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] GameObject _dialogueParent;
    [SerializeField] TMP_Text _dialogueText;
    [SerializeField] Button _option1Button;
    [SerializeField] Button _option2Button;
    [SerializeField] float _typingSpeed = 0.05f;

    List<DialogueString> _dialogueList;
    int _currentDialogueIndex = 0;
    bool _optionSelected = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _dialogueParent.SetActive(false);
    }

    public void DialogueStart(List<DialogueString> textToPrint)
    {
        _dialogueParent.SetActive(true);
        _dialogueList = textToPrint;
        _currentDialogueIndex = 0;
        DisableButtons();
        StartCoroutine(PrintDialogue());
    }

    private IEnumerator PrintDialogue()
    {
        while (_currentDialogueIndex<_dialogueList.Count)
        {
            DialogueString line = _dialogueList[_currentDialogueIndex];
            line.startDialogueEvent?.Invoke();
            yield return StartCoroutine(TypeText(line.text));

            if (line.isQuestion)
            {
                _option1Button.interactable = true;
                _option2Button.interactable = true;
                _option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1.Value;
                _option2Button.GetComponentInChildren<TMP_Text>().text = line.answerOption2.Value;
                _option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));
                _option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump));
                yield return new WaitUntil(() => _optionSelected);
            }

            line.endDialogueEvent?.Invoke();
            _optionSelected = false;
        }

        DialogueStop();
    }

    private void DialogueStop()
    {
        RoomManager.Instance.ShowTabs();
        StopAllCoroutines();
        _dialogueText.text = "";
        _dialogueParent.SetActive(false);
    }

    private IEnumerator TypeText(LocalisedString text)
    {
        _dialogueText.text = "";

        foreach (char letter in text.Value.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }

        if (!_dialogueList[_currentDialogueIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (_dialogueList[_currentDialogueIndex].isEnd)
        {
            DialogueStop();
        }

        _currentDialogueIndex++;
    }

    private void HandleOptionSelected(int indexJump)
    {
        _optionSelected = true;
        DisableButtons();
        _currentDialogueIndex = indexJump;
    }

    private void DisableButtons()
    {
        _option1Button.interactable = false;
        _option2Button.interactable = false;
        _option1Button.GetComponentInChildren<TMP_Text>().text = "";
        _option2Button.GetComponentInChildren<TMP_Text>().text = "";
    }
}
