using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace CharacterCreator
{
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField] TabGroup _tabGroup; //Reference to a Tab Group.
        [SerializeField] Image _background; //Background image of the tab.
        [SerializeField] UnityEvent onTabSelected;
        [SerializeField] UnityEvent onTabDeselected;

        public Image background
        {
            get 
            { 
                return _background; 
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _tabGroup.OnTabSelected(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tabGroup.OnTabEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tabGroup.OnTabExit(this);
        }

        private void Start()
        {
            _background = GetComponentInChildren<Image>();
            _tabGroup.Subscribe(this);
        }
        public void Select()
        {
            if (onTabSelected != null)
            {
                onTabSelected.Invoke();
            }
        }
        public void Deselect()
        {
            if (onTabDeselected != null)
            {
                onTabDeselected.Invoke();
            }
        }
    }
}