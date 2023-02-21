using System.Collections.Generic;
using UnityEngine;

namespace CharacterCreator
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] List<TabButton> _tabButtons; //List of Tab Buttons.
        [SerializeField] Color tabIdleColor; //Sprite of a unselected tab.
        [SerializeField] Color tabHoverColor; //Sprite of a highlighted tab.
        [SerializeField] Color tabActiveColor; //Sprite of a selected tab.
        [SerializeField] TabButton selectedTab; //Tab currently selected.
        [SerializeField] List<GameObject> objectsToSwap; //Pages of content to swap.

        public void Subscribe(TabButton button)
        {
            if (_tabButtons == null)
            {
                _tabButtons = new List<TabButton>();
            }

            _tabButtons.Add(button);
        }

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();

            if (selectedTab == null || button != selectedTab)
            {
                button.background.color = tabHoverColor;
            }
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }

        public void OnTabSelected(TabButton button)
        {
            if (selectedTab != null)
            {
                selectedTab.Deselect();
            }

            selectedTab = button;
            selectedTab.Select();
            ResetTabs();
            button.background.color = tabActiveColor;
            int index = button.transform.GetSiblingIndex();

            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                if (i == index)
                {
                    objectsToSwap[i].SetActive(true);
                }
                else
                {
                    objectsToSwap[i].SetActive(false);
                }
            }
        }

        public void ResetTabs()
        {
            foreach (TabButton button in _tabButtons)
            {
                if (selectedTab != null && button == selectedTab)
                {
                    continue;
                }

                button.background.color = tabIdleColor;
            }
        }
    }
}