using UnityEngine;

public class CheeseWheel : MonoBehaviour
{
    [Header("Cheese State")]
    [SerializeField] private int maxSlices = 32;

    [SerializeField] private int remainingSlices = 32;

    [Header("Visual Models")]
    [SerializeField] private GameObject[] cheeseModels;

    public int RemainingSlices => remainingSlices;

    private void Start()
    {
        UpdateVisual();
    }

    public void CutSlices(int amount)
    {
        remainingSlices -= amount;

        remainingSlices =
            Mathf.Clamp(
                remainingSlices,
                0,
                maxSlices
            );

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (cheeseModels.Length == 0)
        {
            Debug.LogError(
                "No cheese models assigned!"
            );

            return;
        }

        // Disable all models
        for (int i = 0; i < cheeseModels.Length; i++)
        {
            if (cheeseModels[i] != null)
            {
                cheeseModels[i].SetActive(false);
            }
        }

        // Convert remaining slices into visual index
        int index =
            maxSlices - remainingSlices;

        index = Mathf.Clamp(
            index,
            0,
            cheeseModels.Length - 1
        );

        // Enable correct model
        if (cheeseModels[index] != null)
        {
            cheeseModels[index].SetActive(true);
        }
        else
        {
            Debug.LogError(
                "Missing cheese model at index: " +
                index
            );
        }
    }
}