using UnityEngine;
using UnityEngine.UI;

public class RatController : MonoBehaviour
{
    public CustomerOrder order;
    [SerializeField] private Slider patienceSlider;
    [SerializeField] private Image fillColor;
    [SerializeField] private Color highPatience;
    [SerializeField] private Color mediumPatience;
    [SerializeField] private Color lowPatience;

    [SerializeField] private StarManager starManager;

    private Vector3 targetPosition;
    public bool isMoving;
    public float patience;
    public float maxPatience;
    private bool patienceActive;

    public float moveSpeed = 5f;
    public int initialSpend;
    public int tipMin;
    public int tipMax;
    public string preferredFlavour;



    private void Update()
    {
        //Vector3 direction = (targetPosition - transform.position).normalized;

        //if (!moving) return;

        //if (direction != Vector3.zero)
        //{
        //    transform.forward = direction;
        //}

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            isMoving = false;
        }

        if (!patienceActive)
            return;
        patience -= Time.deltaTime;

        if (patience <= 0)
        {
            patience = 0;

            //todo
            starManager.RemoveStars(1);
        }

        // Update slider
        UpdatePatienceUI();
    }

    public void MoveTo(Vector3 position)
    {
        targetPosition = position;
        isMoving = true;
    }

    public void PausePatience()
    {
        patienceActive = false;
    }

    public void ResumePatience()
    {
        patienceActive = true;
    }

    public void GenerateOrder(string flavour, float weight)
    {
        order = new CustomerOrder
        {
            flavour = flavour,
            requestedWeight = weight
        };
    }

    private void UpdatePatienceUI()
    {
        

        float patiencePercent = patience / maxPatience;

        patienceSlider.value = patiencePercent;

        if (patienceSlider.value > 0.6f)
        {
            fillColor.color = highPatience;
        }

        else if (patienceSlider.value > 0.3f)
        {
            fillColor.color = mediumPatience;
        }
        
        else
        {
            fillColor.color = lowPatience;
        }

    }

}
