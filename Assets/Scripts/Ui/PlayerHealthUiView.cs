using System.Globalization;
using TMPro;
using UnityEngine;

public class PlayerHealthUiView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthValueText;

    public void UpdateHealthValueText(float value)
    {
        _healthValueText.text = value.ToString(CultureInfo.InvariantCulture);
    }
}
