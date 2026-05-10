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
        UpdateVisual(remainingSlices);
    }

    public void PreviewCut(int cutAmount)
    {
        int previewSlices =
            remainingSlices - cutAmount;

        previewSlices =
            Mathf.Clamp(
                previewSlices,
                0,
                maxSlices
            );

        UpdateVisual(previewSlices);
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

        UpdateVisual(remainingSlices);
    }

    private void UpdateVisual(int slicesRemaining)
    {
        if (cheeseModels.Length == 0)
        {
            Debug.LogError(
                "No cheese models assigned!"
            );

            return;
        }

        for (int i = 0; i < cheeseModels.Length; i++)
        {
            if (cheeseModels[i] != null)
            {
                cheeseModels[i].SetActive(false);
            }
        }

        int index =
            maxSlices - slicesRemaining;

        index = Mathf.Clamp(
            index,
            0,
            cheeseModels.Length - 1
        );

        if (cheeseModels[index] != null)
        {
            cheeseModels[index].SetActive(true);
        }
    }
}