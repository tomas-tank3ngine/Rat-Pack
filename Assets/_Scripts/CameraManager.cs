using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    public Camera Camera;

    [Header("Camera Targets")]
    public GameObject cheeseCuttingTarget;
    public GameObject counterTarget;

    [Header("UI Buttons")]
    [SerializeField] private Button lookCounterButton;
    [SerializeField] private List<Button> lookCheeseButtons;

    [Header("Camera Movement")]
    [SerializeField] private float rotationDuration = 0.15f;

    private Coroutine activeRotation;

    public void LookCounter()
    {
        RotateTo(counterTarget.transform.rotation);

        // Enable all cheese buttons
        foreach (Button button in lookCheeseButtons)
        {
            button.interactable = true;
        }

        // Disable counter button
        lookCounterButton.interactable = false;
    }

    public void LookCheeseCut()
    {
        RotateTo(cheeseCuttingTarget.transform.rotation);

        // Enable counter button
        lookCounterButton.interactable = true;

        // Disable all cheese buttons
        foreach (Button button in lookCheeseButtons)
        {
            button.interactable = false;
        }
    }

    private void RotateTo(Quaternion targetRotation)
    {
        if (activeRotation != null)
        {
            StopCoroutine(activeRotation);
        }

        activeRotation = StartCoroutine(
            SmoothRotate(targetRotation)
        );
    }

    private IEnumerator SmoothRotate(Quaternion targetRotation)
    {
        Quaternion startRotation = Camera.transform.rotation;

        float elapsed = 0f;

        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / rotationDuration;

            Camera.transform.rotation =
                Quaternion.Slerp(
                    startRotation,
                    targetRotation,
                    t
                );

            yield return null;
        }

        Camera.transform.rotation = targetRotation;
    }
}