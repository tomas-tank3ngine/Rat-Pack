using UnityEngine;

public class RatController : MonoBehaviour
{
    private Vector3 targetPosition;
    public bool isMoving;


    public float moveSpeed = 5f;

    public void MoveTo(Vector3 position)
    {
        targetPosition = position;
        isMoving = true;
    }

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
    }
}
