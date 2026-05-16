using UnityEngine;
using UnityEngine.UI;


public class StarManager : MonoBehaviour
{
    [SerializeField] private GameObject StarUi;

    [Range(0f, 5f)]
    public float stars = 3f;

    public void AddStars(float amount)
    {
        stars += amount;

        stars =
            Mathf.Clamp(stars, 0f, 5f);

        Debug.Log("Stars now: " + stars);

        StarUi.GetComponent<Slider>().value += stars;
    }

    public void RemoveStars(float amount)
    {
        stars -= amount;
        stars =
            Mathf.Clamp(stars, 0f, 5f);
        Debug.Log("Stars now: " + stars);
        StarUi.GetComponent<Slider>().value -= stars;
    }
}