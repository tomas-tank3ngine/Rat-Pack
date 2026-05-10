using UnityEngine;

public class StarRatingSystem : MonoBehaviour
{
    public float defaultStarRating = 2.5f;

    // 1. Declare the hidden "private" variable
    private float _currentStarRating;

    // 2. Declare the "public" Property that handles the Clamping automatically (sets min and max values)
    public float currentStarRating
    {
        get => _currentStarRating;
        set => _currentStarRating = Mathf.Clamp(value, 0f, 5f);
    }

    public StarRatingBar ratingBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        currentStarRating = defaultStarRating;
        ratingBar.SetDefaultRating(defaultStarRating);
    }

    public void Perfect()
    {
        currentStarRating += 0.5f;
        ratingBar.SetCurrentRating(currentStarRating);
    }

    public void Good()
    {
        currentStarRating += 0.25f;
        ratingBar.SetCurrentRating(currentStarRating);
    }

    public void OK()
    {
        currentStarRating -= 0f;
        ratingBar.SetCurrentRating(currentStarRating);
    }

    public void Bad()
    {
         currentStarRating -= 0.25f;
        ratingBar.SetCurrentRating(currentStarRating);
    }

    public void Terrible()
    {
        currentStarRating -= 0.5f;
        ratingBar.SetCurrentRating(currentStarRating);
    }
}
