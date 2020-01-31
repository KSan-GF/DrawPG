﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    private float walkSpeedDiag;
    public float diviseurDiagMouvement;

    private bool moving;


    private Animator anim;
    public Vector2 lastMove;

    private Rigidbody2D myRigidBody;

    private static bool playerExists;

    public float attackSpeed;
    private float attackSpeedCounter;

    private float hurtEffectTimeCounter;
    public float hurtEffectTime;

    private SpriteRenderer spriteRenderer;

    
    // Start is called before the first frame update
    void Start()
    {
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        } else
        {
            Destroy(gameObject);


        }

        walkSpeedDiag = walkSpeed * diviseurDiagMouvement;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()

    {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();


        moving = false;

        if(attackSpeedCounter > 0)
        {
            attackSpeedCounter -= Time.deltaTime;
            if (attackSpeedCounter <= 0)
            {
                anim.SetBool("Attacking", false);
            }
            //attackState = false;
        }

        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) >0.5f)
        {
            //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * walkSpeed * Time.deltaTime,0f,0f));
            myRigidBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * walkSpeed, myRigidBody.velocity.y);
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            moving = true;
            
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
        {
            //transform.Translate(new Vector3(0f,Input.GetAxisRaw("Vertical") * walkSpeed * Time.deltaTime, 0f));
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxisRaw("Vertical") * walkSpeed);
            moving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            
        }

        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.5f)
        {
            myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
        }

        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.5f)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x , 0f);
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("Moving", moving);
        anim.SetFloat("PositionX", lastMove.x);
        anim.SetFloat("PositionY", lastMove.y);

        if (Input.GetKeyDown(KeyCode.J))
        {
            //attackState = true;
            attackSpeedCounter = attackSpeed;
            anim.SetBool("Attacking", true);
            
            

        }

        if( Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
        {
            if( walkSpeed == walkSpeedDiag)
            {

            }
            else
            {
                walkSpeed = walkSpeedDiag;
            }
        }

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.5f || Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.5f)
        {
            walkSpeed = walkSpeedDiag * (1/diviseurDiagMouvement);
        }

        if (hurtEffectTimeCounter > 0.66f * hurtEffectTime )
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        } else if (hurtEffectTimeCounter > 0.33f * hurtEffectTime)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        } else if (hurtEffectTimeCounter > 0f )
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        } else
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }


        hurtEffectTimeCounter -= Time.deltaTime;

    }

    public void playerHurtEffect()
    {
        hurtEffectTimeCounter = hurtEffectTime;
    }
}