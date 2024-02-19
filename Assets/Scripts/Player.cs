using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rollBoost = 2f;
    private float rollTime;
    public float RollTime;
    bool rollOnce = false;
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            moveSpeed += rollBoost;
            rollTime = RollTime;
            rollOnce = true;
        }
        if(rollTime <= 0 && rollOnce == true)
        {
            moveSpeed -= rollBoost;
            rollOnce = false;
        }
        else
        {
            rollTime -= Time.deltaTime;
        }
        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                CharcterSR.transform.localScale = new Vector3(1, 1, 1);
            }
            else CharcterSR.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
