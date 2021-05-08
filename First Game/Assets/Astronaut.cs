using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Astronaut : MonoBehaviour
{
    public float speed = 20f;
    public bool jump = false;
    public float JumpForce = 600f;
    public float climbSpeed;
    private Rigidbody2D myrb;
    public Animator animator;
    public Text mytext;
    public GameObject ReplayButton;
    private bool onLadder;
    public Text justthewriting;

    public GameObject endingScreen;
    int score;
    
    private void Start()
    {
        myrb = GetComponent<Rigidbody2D>();
        ReplayButton.SetActive(false);
        Time.timeScale = 1;

        animator.SetBool("Idle", true);
        animator.SetBool("Jump", false);
        animator.SetBool("Run", false);
        animator.SetBool("LadderUp", false);
    }


    public void Update()
    {
        var movement = Input.GetAxis("Horizontal") * speed;

        transform.position += new Vector3(movement * Time.deltaTime, 0f, 0f);

        animator.SetBool("Run", movement != 0);
        animator.SetBool("Idle", movement == 0);

        Jump();

        Flip(movement);


        if (onLadder && Input.GetKey(KeyCode.UpArrow))
        {
            myrb.velocity = new Vector2(myrb.velocity.x, climbSpeed);
            animator.SetBool("LadderUp", true);
        }
        else if(onLadder && Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool("LadderUp", false);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triiger Exit");
        if (other.gameObject.CompareTag("coins"))
        {
            Destroy(other.gameObject);
            score++;
            mytext.text = score.ToString();

        }
        if (other.gameObject.CompareTag("Ladder"))
        {
            onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            onLadder = false;
            animator.SetBool("LadderUp", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            Time.timeScale = 0;
            ReplayButton.SetActive(true);
        }
        if (collision.gameObject.tag == "Death")
        {
            Time.timeScale = 0;
            ReplayButton.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            jump = false;
            animator.SetBool("Jump", false);
        }
        if (collision.gameObject.tag == "endGame")
        {
            endingScreen.SetActive(true);
            Time.timeScale = 0;
        }
      
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !jump)
        {
            jump = true;

            myrb.AddForce(new Vector2(0, JumpForce));

            animator.SetBool("Jump", true);
        }
    }

    private void Flip(float movement)
    {
        if (movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        else if (movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
}


