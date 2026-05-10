using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    public Camera Camera;
    //public RoundManager roundManagerScript;

    [Header("Camera Targets")]
    //public Transform cheeseCuttingTarget;

    [Header("UI Buttons")]
    [SerializeField] private Button lookCounterButton;
    [SerializeField] private List<Button> lookCheeseButtons;

    [Header("Camera Movement")]
    private float movementDuration = 0.12f;

    private Coroutine activeMovement;

    public void SetView(Transform target, bool isCounterView)
    {
        MoveToTarget(target);

        lookCounterButton.interactable = !isCounterView;

        foreach (Button button in lookCheeseButtons)
        {
            button.interactable = isCounterView;
        }
    }

    /*
    public void CounterView()
    {
        MoveToTarget(counterTarget);
        foreach (Button button in lookCheeseButtons)
        {
            button.interactable = true;
        }

        // Disable counter button
        lookCounterButton.interactable = false;
    }

    public void CheeseView(Transform target)
    {

        roundManagerScript.ChangeCameraView(RoundManager.CameraView.Counter);
        MoveToTarget(target);

        // Enable counter button
        lookCounterButton.interactable = true;

        // Disable all cheese buttons
        foreach (Button button in lookCheeseButtons)
        {
            button.interactable = false;
        }

        roundManagerScript.ChangeCameraView(RoundManager.CameraView.Counter);
    }
    */

    private void MoveToTarget(Transform target)
    {
        if (activeMovement != null)
        {
            StopCoroutine(activeMovement);
        }

        activeMovement = StartCoroutine(
            SmoothMove(target)
        );
    }

    private IEnumerator SmoothMove(Transform target)
    {
        Vector3 startPosition = Camera.transform.position;
        Quaternion startRotation = Camera.transform.rotation;

        Vector3 targetPosition = target.position;
        Quaternion targetRotation = target.rotation;

        float elapsed = 0f;

        while (elapsed < movementDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.SmoothStep(
                0f,
                1f,
                elapsed / movementDuration
            );

            Camera.transform.position =
                Vector3.Lerp(
                    startPosition,
                    targetPosition,
                    t
                );

            Camera.transform.rotation =
                Quaternion.Slerp(
                    startRotation,
                    targetRotation,
                    t
                );

            yield return null;
        }

        Camera.transform.position = targetPosition;
        Camera.transform.rotation = targetRotation;
    }
}