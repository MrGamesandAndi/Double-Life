using AudioSystem;
using UnityEngine;

public class SoundFeedback : MonoBehaviour
{
    [SerializeField] AudioClip _clickSound;
    [SerializeField] AudioClip _placeSound;
    [SerializeField] AudioClip _removeSound;
    [SerializeField] AudioClip _wrongPlacementSound;

    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
            case SoundType.Click:
                AudioManager.Instance.PlaySfx(_clickSound);
                break;
            case SoundType.Place:
                AudioManager.Instance.PlaySfx(_placeSound);
                break;
            case SoundType.Remove:
                AudioManager.Instance.PlaySfx(_removeSound);
                break;
            case SoundType.WrongPlacement:
                AudioManager.Instance.PlaySfx(_wrongPlacementSound, 10f);
                break;
            default:
                break;
        }
    }
}

public enum SoundType
{
    Click,
    Place,
    Remove,
    WrongPlacement
}
