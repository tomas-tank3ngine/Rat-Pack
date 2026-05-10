using UnityEngine;

public class StarManager : MonoBehaviour
{
    [Range(0f, 5f)]
    public float stars = 3f;

    public void AddStars(float amount)
    {
        stars += amount;

        stars =
            Mathf.Clamp(stars, 0f, 5f);

        Debug.Log("Stars now: " + stars);
    }
}