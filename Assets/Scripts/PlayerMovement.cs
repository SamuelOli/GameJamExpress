using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform movePoint;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private LayerMask whatStopsMovement;
    private float moveSpeed;

    private void Start()
    {
        movePoint.parent = null;
        moveSpeed = walkSpeed;
    }

    private void OnEnable()
    {
        PlayerStats.OnSprint += HandleSprinting;
    }

    private void OnDisable()
    {
        PlayerStats.OnSprint -= HandleSprinting;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.2f, whatStopsMovement))
                { 
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f); 
                }
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.2f, whatStopsMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }
    }

    private void HandleSprinting(bool isSprinting)
    {
        if (isSprinting)
            moveSpeed = sprintSpeed;
        else
            moveSpeed = walkSpeed;
    }
}
