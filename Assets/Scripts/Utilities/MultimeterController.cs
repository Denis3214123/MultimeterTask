using UnityEngine;

public class MultimeterController : MonoBehaviour
{
    [SerializeField] private UIView uiView;
    [SerializeField] private KnobController knob; // Добавляем ссылку на регулятор

    private MultimeterModel model;

    void Start()
    {
        model = new MultimeterModel();

        // Подписываемся на событие поворота регулятора
        if (knob != null)
        {
            knob.OnKnobTurned += OnKnobTurned;
            Debug.Log("Knob подключен к контроллеру");
        }
        else
        {
            Debug.LogError("Knob не назначен в GameController!");
        }

        // Обновляем UI
        if (uiView != null)
        {
            uiView.UpdateUI(model.Data);
        }
    }

    // Обработчик поворота регулятора
    private void OnKnobTurned(MultimeterMode newMode)
    {
        Debug.Log($"Контроллер: смена режима на {newMode}");

        if (model != null)
        {
            model.SetMode(newMode);

            if (uiView != null)
            {
                uiView.UpdateUI(model.Data);
            }
        }
    }

    void OnDestroy()
    {
        // Отписываемся от события при уничтожении
        if (knob != null)
        {
            knob.OnKnobTurned -= OnKnobTurned;
        }
    }
}