using UnityEngine;
using TMPro;

public class UIView : MonoBehaviour
{
    [Header("UI Text Fields")]
    [SerializeField] private TextMeshProUGUI resistanceText;
    [SerializeField] private TextMeshProUGUI currentText;
    [SerializeField] private TextMeshProUGUI voltageDCText;
    [SerializeField] private TextMeshProUGUI voltageACText;

    [Header("3D Display")]
    [SerializeField] private TextMeshPro displayScreen;
    //Вызывается из контроллера при смене режима или при старте
    public void UpdateUI(MultimeterData data)
    {
        // Отображение сопротивления 
        if (resistanceText != null)
            resistanceText.text = $"Ω {data.Resistance:F2}";

        // Обнуляем все показания
        if (currentText != null)
            currentText.text = "A 0";
        if (voltageDCText != null)
            voltageDCText.text = "V 0";
        if (voltageACText != null)
            voltageACText.text = "~ 0";

        float displayValue = 0f;//Значение на дисплеи по умолчанию 
        //Выводим значение на дисплей в зависимость от переключеного режима
        switch (data.CurrentMode)
        {
            case MultimeterMode.OFF:
                displayValue = 0f;
                break;

            case MultimeterMode.Resistance:
                displayValue = data.Resistance;
                break;

            case MultimeterMode.Current:
                displayValue = data.Current;
                if (currentText != null)
                    currentText.text = $"A {data.Current:F2}";
                break;

            case MultimeterMode.VoltageDC:
                displayValue = data.VoltageDC;
                if (voltageDCText != null)
                    voltageDCText.text = $"V {data.VoltageDC:F2}";
                break;

            case MultimeterMode.VoltageAC:
                displayValue = data.VoltageAC;
                if (voltageACText != null)
                    voltageACText.text = $"~ {data.VoltageAC:F2}";
                break;
        }

        // Обновляем дисплей 
        if (displayScreen != null)
        {
            displayScreen.text = displayValue.ToString("F2");
        }

        Debug.Log($"UI Обновлен: режим={data.CurrentMode}, значение={displayValue}");
    }
}