using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(
        typeof(CharacterController),
        typeof(SpriteRenderer)
    ),
    AddComponentMenu("Mario/Player")
]
public class PlayerManager : MonoBehaviour
{
    #region Component
    private Rigidbody2D rigid;
    #endregion

    #region Variable
    private bool isGround = true;
    #endregion

    #region init
    private void Awake()
    {
        rigid = this.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {

    }
    #endregion

    #region loop
    private void Update()
    {
        PlayerMove(Input.GetAxis("Horizontal"));
    }
    private void LateUpdate()
    {

    }
    #endregion

    #region Event
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if player get hit on ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        //if player get off 
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
    #endregion

    #region Function
    private void PlayerMove(float ad)
    {
        //make player stop when input is nothing.
        if (isGround && rigid.velocity.x != 0)
        {
            //Vector struct declare
            Vector2 vec1 = new Vector2(
                x: -rigid.velocity.x,
                y: 0
            );

            //Add Resistive force TODO : Add Ice Function
            rigid.AddForce(Vector2.ClampMagnitude(vec1, Constants.Physics.Player.drag));
        }

        //When user input jump TODO : Add Run and jump
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector2.up * Constants.Physics.Player.jumpForce);
        }


        if (ad == 0) return;
        //make player move when user input anything TODO : Add run

        //Vector2 변수 선언.
        Vector2 vec2 = new Vector2(
            x: ad,
            y: 0
        );

        //이동방향에 대해 힘을 추가.
        rigid.AddForce(vec2 * Constants.Physics.Player.walkSpeed);

        //Clamp를 통해 최대 속도를 제한.
        rigid.velocity = Vector2.ClampMagnitude(rigid.velocity, Constants.Physics.Player.maxSpeed);
    }
    #endregion
}
