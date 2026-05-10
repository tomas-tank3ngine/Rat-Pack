using UnityEngine;
using UnityEngine.UI;

public class CheeseController : MonoBehaviour
{
    [Header("Cheese Wheels")]
    [SerializeField]
    private CheeseWheel[] cheeseWheels;

    [Header("UI")]
    [SerializeField]
    private Slider cutSlider;

    private CheeseWheel activeCheese;

    private void Start()
    {
        cutSlider.onValueChanged.AddListener(
            OnSliderChanged
        );

        if (cheeseWheels.Length > 0)
        {
            SetActiveCheese(0);
        }
    }

    public void SetActiveCheese(int index)
    {
        if (index < 0 || index >= cheeseWheels.Length)
        {
            Debug.LogError(
                "Invalid cheese index: " + index
            );

            return;
        }

        activeCheese = cheeseWheels[index];

        RefreshUI();
    }

    public void ConfirmCut()
    {
        if (activeCheese == null)
            return;

        int amount =
            Mathf.RoundToInt(
                cutSlider.value
            );

        activeCheese.CutSlices(amount);

        RefreshUI();
    }

    private void RefreshUI()
    {
        if (activeCheese == null)
            return;

        cutSlider.maxValue =
            activeCheese.RemainingSlices;

        cutSlider.value =
            Mathf.Min(
                cutSlider.value,
                activeCheese.RemainingSlices
            );
    }
    private void OnSliderChanged(float value)
    {
        if (activeCheese == null)
            return;

        int amount =
            Mathf.RoundToInt(value);

        activeCheese.PreviewCut(amount);
    }
}