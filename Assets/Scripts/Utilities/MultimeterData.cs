using System;

[Serializable]
public class MultimeterData
{
    public float Resistance = 1000f;// Сопротивление
    public float Power = 400f;// Мощность
    public float Current;// Сила тока
    public float VoltageDC;// Постоянное напряжение
    public float VoltageAC = 0.01f;// Переменное напряжение
    public MultimeterMode CurrentMode = MultimeterMode.OFF;   // Режим по умолчанию 

}
//Режимы работы мультиметра
public enum MultimeterMode
{
    OFF,
    Resistance,
    Current,
    VoltageDC,
    VoltageAC
}