using General;
using Needs;
using Population;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugNeedsPanel : MonoBehaviour
{
    [SerializeField] List<GameObject> _needHunger;
    [SerializeField] List<GameObject> _needMakeFriend;
    [SerializeField] List<GameObject> _needBuyFurniture;
    [SerializeField] List<GameObject> _needSickness;
    [SerializeField] List<GameObject> _needHaveDepression;
    [SerializeField] List<GameObject> _needHaveFight;
    [SerializeField] List<GameObject> _needHaveDate;
    [SerializeField] List<GameObject> _needConfessLove;
    [SerializeField] List<GameObject> _needTalkFriend;
    [SerializeField] List<GameObject> _needBreakUp;


    NeedsManager _needManager;

    private void Start()
    {
        _needManager = PopulationManager.Instance.GetAIByID(GameManager.Instance.currentLoadedDouble.Id);
    }

    private void Update()
    {
        _needHunger[0].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.Hunger).RingAmount.ToString();
        _needHunger[1].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.Hunger).CoreAmount.ToString();

        _needMakeFriend[0].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.MakeFriend).RingAmount.ToString();
        _needMakeFriend[1].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.MakeFriend).CoreAmount.ToString();

        _needBuyFurniture[0].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.BuyFurniture).RingAmount.ToString();
        _needBuyFurniture[1].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.BuyFurniture).CoreAmount.ToString();

        _needSickness[0].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.Sickness).RingAmount.ToString();
        _needSickness[1].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.Sickness).CoreAmount.ToString();

        _needHaveDepression[0].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.HaveDepression).RingAmount.ToString();
        _needHaveDepression[1].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.HaveDepression).CoreAmount.ToString();

        _needHaveFight[0].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.HaveFight).RingAmount.ToString();
        _needHaveFight[1].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.HaveFight).CoreAmount.ToString();

        _needHaveDate[0].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.HaveDate).RingAmount.ToString();
        _needHaveDate[1].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.HaveDate).CoreAmount.ToString();

        _needConfessLove[0].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.ConfessLove).RingAmount.ToString();
        _needConfessLove[1].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.ConfessLove).CoreAmount.ToString();

        _needTalkFriend[0].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.TalkToFriend).RingAmount.ToString();
        _needTalkFriend[1].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.TalkToFriend).CoreAmount.ToString();

        _needBreakUp[0].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.BreakUp).RingAmount.ToString();
        _needBreakUp[1].GetComponent<TextMeshProUGUI>().text = _needManager.GetNeed(NeedType.BreakUp).CoreAmount.ToString();
    }

    public void ActivateNeed(int need)
    {
        PopulationManager.Instance.GetAIByID(GameManager.Instance.currentLoadedDouble.Id).ActivateNeed((NeedType)need);
    }
}
