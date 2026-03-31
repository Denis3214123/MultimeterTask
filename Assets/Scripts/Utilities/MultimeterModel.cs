using UnityEngine;

public class MultimeterModel
{
    public MultimeterData Data { get; private set; }

    public MultimeterModel()
    {
        Data = new MultimeterData();
        CalculateValues();
    }
    // Метод для смены режима мультиметра
    public void SetMode(MultimeterMode mode)
    {
        Data.CurrentMode = mode;
        CalculateValues();
    }
    //Метод для расчета всех значений по закону Ома
    private void CalculateValues()
    {     
        Data.Current = Mathf.Sqrt(Data.Power / Data.Resistance);     // P = I² * R  → I = √(P / R)
        Data.VoltageDC = Mathf.Sqrt(Data.Power * Data.Resistance);        // P = V² / R  → V = √(P * R)     
        Data.VoltageAC = 0.01f;

        Debug.Log($"Model Calculate: R={Data.Resistance}, P={Data.Power}, " +
          $"I={Data.Current:F2}, V={Data.VoltageDC:F-2}, Mode={Data.CurrentMode}");
    }
}