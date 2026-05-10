using UnityEngine;

public class RatController : MonoBehaviour
{
    public CustomerOrder order;

    private Vector3 targetPosition;
    public bool isMoving;
    public float patience;
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
            //OnPatienceExpired();
        }
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

}
