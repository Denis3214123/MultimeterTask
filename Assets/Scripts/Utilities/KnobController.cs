using UnityEngine;
using System;

public class KnobController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float rotationSpeed = 200f;// Скорость вращения регулятора
    [SerializeField] private Material highlightMaterial;// Материал для подсветки при наведении
    [SerializeField] private float sensitivityMultiplier = 100f;// Множитель чувствительности прокрутки

    [Header("Mode Mapping (clockwise order)")]
    [SerializeField]
    private MultimeterMode[] modeOrder = new MultimeterMode[]//Режимы
    {
        MultimeterMode.OFF, 
        MultimeterMode.VoltageDC,
        MultimeterMode.VoltageAC,
        MultimeterMode.Current,
        MultimeterMode.Resistance
    };

    public event Action<MultimeterMode> OnKnobTurned; // Событие которое вызывается при повороте регулятора
    private bool isActive = false;   // Флаг активации регулятора 
    private Material originalMaterial; // Матерьял по умолчанию
    private Renderer knobRenderer; //Подстветка
    private float currentAngle;// Текущий угол поворота
    private MultimeterMode currentMode;// Текущий режим
    private float anglePerMode; //Угол режима

    void Start()
    {
        knobRenderer = GetComponent<Renderer>(); // Получаем компонент рендерера для подсветки

        if (knobRenderer != null)
        {
            originalMaterial = knobRenderer.material;// Сохранение оригинального матерьяла
        }
        
        anglePerMode = 360f / (modeOrder.Length - 1);// Рассчитываем угол для каждого режима
        //Начальный угол 
        currentAngle = 0f;
        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
        //Установка off
        currentMode = modeOrder[0];
        OnKnobTurned?.Invoke(currentMode);
    }
    //Наведение мыши на регулятор
    void OnMouseEnter()
    {
        isActive = true;

        if (knobRenderer != null && highlightMaterial != null)
        {
            knobRenderer.material = highlightMaterial;
        }

        Debug.Log("Knob активен");
    }
    //Мышь вне регулятора
    void OnMouseExit()
    {
        isActive = false;

        if (knobRenderer != null && originalMaterial != null)
        {
            knobRenderer.material = originalMaterial;
        }

        Debug.Log("Knob неактивен");
    }

    void Update()
    {
        if (isActive)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");//Прокрутка колесика мыши
            if (scroll != 0)
            {
                // Изменяем угол поворота в зависимости от направления прокрутки scroll > 0 = прокрутка вправо, scroll < 0 = прокрутка влево
                currentAngle += scroll * rotationSpeed * Time.deltaTime * sensitivityMultiplier;
                // Нормализуем угол в диапазон 0-360°
                if (currentAngle >= 360f)
                {
                    currentAngle -= 360f;
                }
                else if (currentAngle < 0f)
                {
                    currentAngle += 360f;
                }
                // Вращаем объект регулятора
                transform.localRotation = Quaternion.Euler(0, 0, currentAngle);

                // Обновляем режим по углу
                UpdateModeFromAngle();

                Debug.Log($"Knob повернут: угол={currentAngle:F1}°, режим={currentMode}");
            }
        }
    }
    // Определяет режим по текущему углу поворота
    private void UpdateModeFromAngle()
    {
        MultimeterMode newMode;

    
        if (currentAngle < 15f || currentAngle > 345f)//Off
        {
            newMode = MultimeterMode.OFF;
        }
        else
        {
            // Определяем индекс рабочего режима 
            int index = Mathf.FloorToInt(currentAngle / anglePerMode);
            index = Mathf.Clamp(index, 0, modeOrder.Length - 2);    
            newMode = modeOrder[index + 1];
        }

        if (newMode != currentMode)
        {
            currentMode = newMode;
            OnKnobTurned?.Invoke(currentMode);
        }
    }
    //Получения текущего режима
    public MultimeterMode GetCurrentMode()
    {
        return currentMode;
    }
}