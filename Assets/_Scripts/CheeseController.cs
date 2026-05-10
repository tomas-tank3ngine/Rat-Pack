using UnityEngine;
using UnityEngine.UI;

public class CheeseController : MonoBehaviour
{

    [SerializeField] private RoundManager roundManager;
    [Header("Cheese Wheels")]
    [SerializeField]
    private CheeseWheel[] cheeseWheels;

    [Header("UI")]
    [SerializeField]
    private Slider cutSlider;

    private CheeseWheel activeCheese;

    [Header("Scoring")]
    [SerializeField] private StarManager starManager;
    //[SerializeField] private float weightPerSlice = 10f;


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
        RatController rat = roundManager.currentRat?.GetComponent<RatController>();
        Debug.Log("CONFIRM CUT PRESSED");
        if (activeCheese == null)
        {
            Debug.LogError("activeCheese is NULL");
            return;
        }

        if (rat == null)
        {
            Debug.LogError("currentRat is NULL");
            return;
        }

        int amount =
            Mathf.RoundToInt(cutSlider.value);

        float deliveredWeight =
            amount * activeCheese.Profile.weightPerSlice;

        float starDelta =
            SatisfactionSystem.CalculateStars(
                rat.order,
                deliveredWeight,
                activeCheese.Profile.cheeseFlavour.ToString()
            );

        starManager.AddStars(starDelta);

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

    //public float GetDeliveredWeight(int cutAmount) { return cutAmount * weightPerSlice; }

    public void ServeCurrentCut()
    {
        RatController rat = roundManager.currentRat?.GetComponent<RatController>();
        if (activeCheese == null || rat == null)
            return;

        int amount = Mathf.RoundToInt(cutSlider.value);

        float deliveredWeight = amount * activeCheese.Profile.weightPerSlice;
        float delta = SatisfactionSystem.CalculateStars(
            rat.order,
            deliveredWeight,
            activeCheese.Profile.cheeseFlavour.ToString());

        starManager.AddStars(delta);

        activeCheese.CutSlices(amount);
    }
}