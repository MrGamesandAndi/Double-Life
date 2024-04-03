using AudioSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    [SerializeField] AudioClip _mouseEnterSFX;
    [SerializeField] AudioClip _mouseExitSFX;
    [SerializeField] AudioClip _mouseClickDownSFX;
    [SerializeField] AudioClip _mouseClickUpSFX;

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySfx(_mouseEnterSFX);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySfx(_mouseExitSFX);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySfx(_mouseClickDownSFX);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySfx(_mouseClickUpSFX);
    }
}
