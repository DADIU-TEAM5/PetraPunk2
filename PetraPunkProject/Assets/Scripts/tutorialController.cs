using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tutorialController : MonoBehaviour
{
    public LevelGenerator lg;
    public GameObject player;
    public GameObject phoneUI;
    public GameObject newPhoneUI;
    public GameObject fadeObject;

    public FloatVariable health;
    public SceneGenrator tutorialGenerator;

    public Animator animator;
    public Animator newAnimator;



    // Use Enum or int instead
    private bool isTurningRight = false;
    private bool isTurningLeft = false;
    private bool isDashing = false;
    private bool isJumping = false;
    private bool isFinal = false;
    private int dashCount;

    public float animationSpeed ;
    public float maxTurn;
    public float dodgeTime = 10;

    public GameObject[] tutorialSegments;
    public LevelSegment nextSegment;
    

    private void Awake()
    {
        fadeObject.SetActive(false);
        nextSegment.Segment = tutorialSegments[0];
        tutorialGenerator.SegementDifficulty[0] = 100;
        tutorialGenerator.SegementDifficulty[1] = 100;
        tutorialGenerator.SegementDifficulty[2] = 100;
        tutorialGenerator.SegementDifficulty[3] = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.speed = animationSpeed;
        //newAnimator.speed = animationSpeed;
        health.Value = 99;
        
    }

    // Update is called once per frame
    void Update()
    {

        // Part 1 - start turning left - Automatic
        if (player.transform.position.x != 0 && !isTurningRight)
        {
            isTurningRight = true;
            animator.SetBool("startTutorial", true);
            newAnimator.SetBool("startTutorial", true);
        }

        // Part 2 - start turning right
        if (player.transform.position.x > maxTurn && isTurningRight)
        {
            //Debug.Log("position: " + player.transform.position.x);
            isTurningRight = false;
            isTurningLeft = true;
            animator.SetBool("turnedRight", true);
            newAnimator.SetBool("turnedRight", true);
        }

        // Part 3 - start dodging pipes
        if (player.transform.position.x < -maxTurn && isTurningLeft)
        {
            //Debug.Log("position: " + player.transform.position.x);
            isTurningLeft = false;
           
            Destroy(phoneUI);
            Destroy(newPhoneUI);
            nextSegment.Segment = tutorialSegments[2];
            StartCoroutine(timeToJump());
        }


        // Part 4 - Start Jumping
        if (isJumping)
        {
            //Debug.Log("We Jumping");
            nextSegment.Segment = tutorialSegments[3];
        }

        // Part Final
        if (isFinal)
        {
            //Debug.Log("Final part initiated");
           
            // Amazing fix
            fadeObject.SetActive(true);
        }

    }

    IEnumerator timeToJump()
    {
        print(Time.time);
        yield return new WaitForSeconds(dodgeTime);
        print(Time.time);
        isJumping = true;
    }

    public void JumpDone()
    {
        //Debug.Log("Jump is has been completed");
        lg.IsEndless = false;

        isJumping = false;
        isFinal = true;

        tutorialGenerator.SegementDifficulty[0] = 0;
        tutorialGenerator.SegementDifficulty[1] = 1;
        tutorialGenerator.SegementDifficulty[2] = 1;
        tutorialGenerator.SegementDifficulty[3] = 666;


    }

    // Could be changed to reset to specifik part of tutorial
    public void reloadLevel()
    {
        StartCoroutine(load());
        
    }

    IEnumerator load()
    {
        yield return new WaitForSeconds(2);
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }
}
