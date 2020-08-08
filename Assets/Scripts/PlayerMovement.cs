using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float jumpForce = 6.5f;
    public float gravityScale = 1.0f;
    public LayerMask groundLayers;
    public bool facingRight = true;
    private bool isGrounded;

    public Rigidbody2D r2d;
    Collider2D collider;
    Transform t;
    
    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        r2d.freezeRotation = true;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = 0;
        if (Input.GetAxis("Horizontal") < 0)
        {
            moveX = -1;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            moveX = 1;
        }
        else
        {
            moveX = 0;
        }


        bool isJumping = Input.GetKey(KeyCode.Space);

        r2d.velocity = new Vector2(moveX * speed, r2d.velocity.y);

        if (IsGrounded() && isJumping)
        {
            r2d.velocity = Vector2.up * jumpForce;
        }

        //r2d.AddForce(new Vector2(0, -Physics.gravity.y * gravityScale * Time.deltaTime));
        if (!IsGrounded())
        {
            r2d.velocity = new Vector2(r2d.velocity.x, r2d.velocity.y + Physics.gravity.y * gravityScale * Time.deltaTime);
        }
        //This is bad
        if (IsGrounded() && !isJumping)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, 0f);
        }
    }

    private bool IsGrounded()
    {
        float buffer = .01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.down, collider.bounds.extents.y + buffer, groundLayers);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.extents.y + buffer),rayColor);
        return raycastHit.collider != null;
    }
}
