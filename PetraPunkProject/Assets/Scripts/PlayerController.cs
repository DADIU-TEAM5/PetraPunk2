﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float progress;


    public float Speed;

    [Header("Controller Parameters")]
    public float FlatSpeed;
    public float SlopeSpeed;
    public float FlatAcceleration;
    public float SlopeAcceleration;
    public float OffSlopeDecay;
    public float ObstacleSpeedLoss;
    public float MinSpeed;
    public float HorizontalSpeed;
    public float SlopeHorizontalSpeed;
    public float slopeMultiplier = 5;

    public float invulnerableSecs = 1;


    public float DashLength = 2;
    public float DashCD = 1;
    public float DashDuration = 0.5f;

    public float airMultiplier = 1;
    public float airHorizontalSpeed;


    [Header("Other stuff")]

    bool standAndDie;
    Vector3 graphicTargetPos;
    public Transform graphics;

    Vector3 colliderDampVelocity = Vector3.zero;
    float colliderDampSpeed = 0.1f;

    public Animator anim;


    Transform deathCamPos;
    Vector3 currentPosition;
    Vector3 lastPosition;
    bool fallAndDie;

    AnimationCurve jumpCurve;
    float maxAirTime;
    float airTimer;
    float maxHeight;
    float height =0;
    bool inAir = false;
    bool falling;
    float groundHeight;
    Vector3 airHitPoint;


    public FloatVariable dashCDtimer;
    float dashTime;
    Vector3 dashStartPos;
    bool isDashing = false;
    float dashDirection;
    public IntVariable swipe4Dash;
    public GameEvent dashAudio;
    public GameEvent cooldownOverAudio;

    public GameEvent PitfallAudioEvent;
    public BoolVariable dashCooldownActiveVar;

    public CameraMovement camScript;

    public FloatVariable gyroTilt;
    public FloatVariable playerSpeed;
    public BoolVariable audioSlope;

    public FloatVariable life;

    public FloatVariable SteeringInput;

    float hitCooldown;

    bool limitLeft, limitRight;

    RaycastHit[] collisions;

    public Transform colliderAndGraphics;


    public bool OnSlope = false;


    float input;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        fallAndDie = false;

        hitCooldown = invulnerableSecs;

        Speed = MinSpeed;

        dashCooldownActiveVar.Value = false;
    }
    private void Awake()
    {
        progress = transform.position.z;
    }

    // Update is called once per frame

    
    void Update()
    {
        graphics.position = graphicTargetPos + Vector3.down;

        

            if (!standAndDie)
            {

                progress = transform.position.z;

                if (hitCooldown < invulnerableSecs)
                {
                    hitCooldown += Time.deltaTime;
                }

                if (!isDashing)
                {
                    input = SteeringInput.Value;
                    input += Input.GetAxis("Horizontal");
                }
                else
                    input = 0;

                limitMovementInput();

                Move();


                if (dashCooldownActiveVar.Value == false)
                {

                    //if (Input.GetKeyDown(KeyCode.Q))
                    //{
                    //    dashCooldownActiveVar.Value = true;
                    //    swipe4Dash.Value = -1;
                    //}
                    //else if (Input.GetKeyDown(KeyCode.E))
                    //{
                    //    dashCooldownActiveVar.Value = true;
                    //    swipe4Dash.Value = 1;
                    //}

                    //if (swipe4Dash.Value != 0)
                    //{
                    //    Dash(swipe4Dash.Value);
                    //    dashCooldownActiveVar.Value = true;

                    //    swipe4Dash.Value = 0;
                    //}
                }


                //ApplyDashMovement();

                ApplyJump();

                if (falling && height < 1f)
                {
                    print(height);
                    height = 0;
                    inAir = false;
                }
                lastPosition = currentPosition;
                currentPosition = transform.position;
            }
            else if (fallAndDie)
            {
            print("fallen");
                FallAndDie();
            }
        else
        {
            print("I AM DEAD MUTTHAFUKKA");
        }
        
        
        


        anim.SetBool("OnSlope", OnSlope);
        anim.SetBool("InAir", inAir);
       // print("Anim value"+ ((Speed - MinSpeed) / (SlopeSpeed - MinSpeed)));
        anim.SetFloat("Blend", (Speed - MinSpeed) / (SlopeSpeed - MinSpeed));

        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetTrigger("Die");
        }
        

        //audio setup
        audioSlope.Value = OnSlope;
        playerSpeed.Value = Speed;
        gyroTilt.Value = input;
    }

    void limitMovementInput()
    {
        if (limitRight  && input < 0)
        {
            input = 0;
        }
        if (limitLeft && input > 0)
        {
            input = 0;
        }
    }


    void Move()
    {
        if (OnSlope)
        {
            if(Speed < SlopeSpeed)
            {
                Speed += SlopeAcceleration *Time.deltaTime;

                if(Speed> SlopeSpeed)
                {
                    Speed = SlopeSpeed;
                }
            }
            
        }
        else
        {
            if(Speed< FlatSpeed)
            {
                Speed += FlatAcceleration * Time.deltaTime;
                if (Speed > FlatSpeed)
                {
                    Speed = FlatSpeed;
                }

            }
            else if(Speed > FlatSpeed)
            {
                Speed -= OffSlopeDecay * Time.deltaTime;
                if (Speed < FlatSpeed)
                {
                    Speed = FlatSpeed;
                }

            }
            
        }

        if (OnSlope)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime * slopeMultiplier);
            transform.Translate(Vector3.right * input * Time.deltaTime * SlopeHorizontalSpeed);
        }
        else if (inAir)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime * airMultiplier);
            transform.Translate(Vector3.right * input * Time.deltaTime * airHorizontalSpeed);
        }
        else
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            transform.Translate(Vector3.right * input * Time.deltaTime * HorizontalSpeed);
        }




        int layerMask = 1 << 9;
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 100, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {

            //print(inAir);
            


            if (inAir && hit.point.y > colliderAndGraphics.position.y)
            {
                inAir = false;
                height = 0;
            }

            if (!inAir)
            {
                graphicTargetPos = hit.point + (Vector3.up * height) + Vector3.up;
                airHitPoint = hit.point;
            }
            else
            {
                airHitPoint.x = transform.position.x;
                airHitPoint.z = transform.position.z;

                graphicTargetPos = airHitPoint + (Vector3.up * height) + Vector3.up;
            }

            
            if(!inAir && hit.collider.gameObject.CompareTag("PitFall"))
            {
                PitfallAudioEvent.Raise();

                fallAndDie = true;
                standAndDie = true;

                deathCamPos= hit.collider.GetComponent<PitFall>().CamPosition;
            }

            graphicTargetPos = new Vector3(transform.position.x, graphicTargetPos.y, transform.position.z);

            //colliderAndGraphics.position = Vector3.SmoothDamp(colliderAndGraphics.position, graphicTargetPos, ref colliderDampVelocity, colliderDampSpeed);
            //colliderAndGraphics.GetComponent<Rigidbody>().position = Vector3.SmoothDamp(colliderAndGraphics.position, graphicTargetPos, ref colliderDampVelocity, colliderDampSpeed);
            colliderAndGraphics.position = graphicTargetPos;
            
            

            //groundHeight = (hit.point + Vector3.up).y;

            if (!inAir)
                OnSlope = hit.transform.CompareTag("Slope");
            else
                OnSlope = false;

        }


        

        // print("speed: "+Speed + " speed on slope: " + Speed * slopeMultiplier);

    }

    
    public void Die()
    {
        anim.SetTrigger("Die");
        standAndDie = true;
        
    }

    public void GetHit(Vector3 direction)
    {
        endDash();
        camScript.ShakeCam();
        direction.y = 0;
        if (limitLeft && direction.x > 0)
        {
            direction.x = direction.x * -1;
        }
        if (limitRight && direction.x < 0)
        {
            direction.x = direction.x * -1;
        }

        transform.Translate(direction);

        if( Speed > MinSpeed)
        {
            Speed -= ObstacleSpeedLoss;
            if( Speed< MinSpeed)
            {
                Speed = MinSpeed;
            }

        }
        if (hitCooldown >= invulnerableSecs)
        {
            life.Value--;

            hitCooldown = 0;
        }
    }

    public void RotatePlayer(Vector3 normal)
    {
        print("i was called");
        colliderAndGraphics.rotation = Quaternion.LookRotation(normal);
    }


    public float GetSpeed()
    {
        return Speed;
    }


    public void Dash(float direction)
    {
        if (limitLeft && direction > 0)
        {
            direction = 0;
        }
        if (limitRight && direction < 0)
        {
            direction = 0;
        }

        if (dashCDtimer.Value <= 0)
        {
            dashAudio.Raise();

            dashCDtimer.Value = DashCD;

            dashDirection = DashLength * direction;
                //print("DASH!");
            dashStartPos = transform.position;
            isDashing = true;
            
        }
        else
        {
           // print("dash on CD");
        }
        
    }
    void ApplyDashMovement()
    {

        //print(dashCDtimer);
        if (dashCDtimer.Value > 0 && !isDashing)
        {
            dashCDtimer.Value -= Time.deltaTime;

            dashCooldownActiveVar.Value = true;
        }

        if (dashCDtimer.Value <= 0 && dashCooldownActiveVar.Value == true)
        {
            //Debug.Log("if " + dashCooldownActiveVar.Value);
            //Debug.Log("if " + dashCDtimer.Value);

            cooldownOverAudio.Raise();
            dashCooldownActiveVar.Value = false;
        } //else if(dashCDtimer.Value > 0 && dashCooldownActiveVar.Value == false)
       // {
           // Debug.Log("else " + dashCooldownActiveVar.Value);
           // Debug.Log("else " + dashCDtimer.Value);
       // }

        if (isDashing)
        {
            dashTime += Time.deltaTime;

            dashStartPos.z = transform.position.z;
            
            transform.position = Vector3.Lerp(dashStartPos, dashStartPos + (Vector3.right * dashDirection), dashTime / DashDuration);

            if (dashTime >= DashDuration)
            {
                endDash();
            }
        }
    }
    void endDash()
    {
        //print("dashEnded");
        isDashing = false;
        dashTime = 0;
        

    }


    public void HitWall(float direction, float xHitPos)
    {
        endDash();

        if(direction < 0)
        {
            limitLeft = true;
        }
        if(direction > 0)
        {
            limitRight = true;
        }

        Vector3 temp = transform.position;

        temp.x = xHitPos;
        transform.position = temp;

    }
    public void leaveWall()
    {
        if (limitLeft )
            limitLeft = false;
        if (limitRight)
            limitRight = false;
    }

    public void Jump(float height, float airTime,AnimationCurve curve)
    {
        maxAirTime = airTime;
        maxHeight = height;
        jumpCurve = curve;

        airTimer = 0;

        inAir = true;
        falling = false;
    }

    void ApplyJump()
    {
        if (inAir)
        {
            if (airTimer < maxAirTime)
            {
                airTimer += Time.deltaTime;

                if (airTimer >= maxAirTime)
                {
                    inAir = false;
                    height = 0;
                }
            }

            if (airTimer>(maxAirTime/2))
            {
                falling = true;
            }

            /*
            if (!falling)
                height = Mathf.Lerp(0, maxHeight, (airTimer / maxAirTime)*2);
            else
                height = Mathf.Lerp( maxHeight,0, (airTimer / maxAirTime)*2-(maxAirTime/2));
            */
            height = Mathf.Lerp(0, maxHeight, jumpCurve.Evaluate(Mathf.Sin((airTimer / maxAirTime ) * Mathf.PI*2)));

            //print(jumpCurve.Evaluate( airTimer / maxAirTime));
           
        }
       

        //print("AIR TIMER"+ airTimer);
        //print("height " + height);

        
    }


    float fallSpeed = 9.8f;
    bool initCam;

    float DeathTimer = 0;

    void FallAndDie()
    {
        DeathTimer += Time.deltaTime;
        if (!initCam)
        {
            camScript.MoveToDeathCam(deathCamPos);
            initCam = true;
            
        }

        fallSpeed += 9.8f*Time.deltaTime;


        graphics.transform.position = transform.position;

        transform.Translate(( currentPosition -lastPosition) );
        transform.Translate(Vector3.down *Time.deltaTime* fallSpeed);
        if(DeathTimer >= 1)
        {
            life.Value = 0;
        }
    }

}
