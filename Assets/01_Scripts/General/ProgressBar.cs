using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] int _minimum;
    [SerializeField] int _maximum;
    [SerializeField] float _current;
    [SerializeField] Image _mask;
    [SerializeField] Image _fill;
    [SerializeField] Color _color;

    public int Minimum { get => _minimum; set => _minimum = value; }
    public int Maximum { get => _maximum; set => _maximum = value; }
    public float Current { get => _current; set => _current = value; }

    private void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float currentOffset = Current - Minimum;
        float maximumOffset=Maximum - Minimum;
        float fillAmount = currentOffset / maximumOffset;
        _mask.fillAmount = fillAmount;
        _fill.color = _color;
    }
}
