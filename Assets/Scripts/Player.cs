using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public Vector3 moveInput;
    public Animator animator;
    public SpriteRenderer CharcterSR;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSpeed * Time.deltaTime;
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
        if(moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                CharcterSR.transform.localScale = new Vector3(1,1,1);
            }else CharcterSR.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
