using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAmmoUiView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoValueText;

    public void UpdateAmmoValueText(float value)
    {
        _ammoValueText.text = value.ToString(CultureInfo.InvariantCulture);
    }
}
