using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 10f;//speed
    public Vector2 maxVelocity = new Vector2(3, 5);//x and y axis
    public bool standing;
    public float jumpSpeed = 15f;
    //private int jumpTimer;
    //private int jumpCounter;
    //public float jumpLimit = 100f;
    private PlayerController controller; //= GetComponent<PlayerController>();//look on obj for script that has type player controller;
    private Animator animator;

    private new Rigidbody2D rigidbody2D;
    // Update is called once per frame
    void Start()
    {
        controller = GetComponent<PlayerController>();//look on obj for script that has type player controller
        animator = GetComponent<Animator>();
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if (!standing)
        {
            var absVelX = Mathf.Abs(rigidbody2D.velocity.x);
            var absVelY = Mathf.Abs(rigidbody2D.velocity.y);
        }
    }
    void Update()
    {
        var forceX = 0f;
        var forceY = 0f;

        rigidbody2D = GetComponent<Rigidbody2D>();
        var absVelX = Mathf.Abs(rigidbody2D.velocity.x);//speed of player currently
        var absVelY = Mathf.Abs(rigidbody2D.velocity.y);
        if (absVelY < .2f) //check if standing or jump
        {
            standing = true;
        }
        else
        {
            standing = false;
        }
        if (controller.moving.x != 0)
        {
            if (absVelX < maxVelocity.x)
            {
                forceX = standing ? speed * controller.moving.x : (speed * controller.moving.x);//if standing return speed, if not
                transform.localScale = new Vector3(forceX > 0 ? 1 : -1, 1, 1);//change the way character face, check if moving left or right
            }
            animator.SetInteger("AnimState", 1); //change to player walking Animation transition
        }
        else
        {
            animator.SetInteger("AnimState", 0); //change to player Animation Idle
        }

        if (controller.moving.y > 0)
        {
            if (absVelY < maxVelocity.y)
                forceY = jumpSpeed * controller.moving.y; //moving upwards
            animator.SetInteger("AnimState", 0); //idle
        }
        else if (absVelY > 0)
        {
            animator.SetInteger("AnimState", 0); //set animator to falling
        }
        else if(controller.moving.y == 0 && controller.moving.x == 0)
        {
            animator.SetInteger("AnimState", 3);
        }
        rigidbody2D.AddForce(new Vector2(forceX, forceY));
    }
    /* void Update () {
         //force applied to player gets clear after each frame
         rigidbody2D = GetComponent<Rigidbody2D>();
         var xForce = 0f;
         var yForce = 0f;
         var absVelx = Mathf.Abs(rigidbody2D.velocity.x);  //abs of rididbody2d x value
         var absVely = Mathf.Abs(rigidbody2D.velocity.y);  //abs of rididbody2d y value
         //need to calculate if player moving faster than max velocity
         if (absVely < .2f) //test if player is standing
         {
             standing = true;
         }
         else
         {
             standing = false;
         }

         if (Input.GetKey("right")) //move right conditiion
         {
             if (absVelx < maxVelocity.x)
                 xForce = speed;
             transform.localScale = new Vector3(1, 1, 1);
         }
         else if (Input.GetKey("left")) //move left condition
         {
             if (absVelx < maxVelocity.x)
                 xForce = -speed;
             transform.localScale = new Vector3(-1, 1, 1);
         }

         if (Input.GetKey("up")) //move up condition
         {
             if (absVely < maxVelocity.y) //&& absVely <= jumpLimit)
             {
                 yForce = jumpSpeed;
             }
            // jumpLimit = 4f;
         }
         rigidbody2D.AddForce(new Vector2(xForce, yForce)); //make player move
     }//end update method*/
}//end player class
