using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(Rigidbody2D),
        typeof(SpriteRenderer)
    )
    ]
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float magnitude;
    [SerializeField]
    private float drag;
    private Rigidbody2D rigid;

    [SerializeField]
    private bool isGround = true;
    private void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        PlayerMove();
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rigid.AddForce(Vector2.up * Constants.jumpForce * 100);
            isGround = false;
        }
    }
    private void PlayerMove()
    {
        float ad = Input.GetAxis("Horizontal");
        
        if (ad == 0)
        {
            rigid.AddForce(-(Vector2.ClampMagnitude(rigid.velocity, 1) * drag));
        }
        else
        {
            Vector2 vec = new Vector2(x: ad, 0) * speed;
            rigid.AddForce(vec);
            if (rigid.velocity.magnitude > magnitude)
            {
                rigid.velocity = Vector2.ClampMagnitude(rigid.velocity, 1) * magnitude;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
