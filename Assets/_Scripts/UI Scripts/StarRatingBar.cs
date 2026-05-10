using UnityEngine;
using UnityEngine.UI;

public class StarRatingBar : MonoBehaviour
{
    public Slider slider;

    public void SetDefaultRating(float rating)
    {
        slider.maxValue = 5f;
        slider.value = rating;
    }

    public void SetCurrentRating(float rating)
    {
        slider.value = rating;
    }
}
