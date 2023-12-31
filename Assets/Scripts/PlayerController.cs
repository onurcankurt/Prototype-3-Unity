using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody playerRb;

    public float jumpForce;
    public float gravityModifier;

    public bool isOnGround = true;
    public bool gameOver = false;
    public bool doubleJumped = false;

    private Animator playerAnim;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private AudioSource playerAudio;

    private float score = 0;


    private MoveLeft moveLeftScript;

    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        playerAnim = GetComponent<Animator>();

        playerAudio = GetComponent<AudioSource>();

        moveLeftScript = GameObject.Find("Background").GetComponent<MoveLeft>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJumped = false;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !gameOver && !doubleJumped){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJumped = true;
        }

        if(moveLeftScript.dashMode && !gameOver)
        {
            score = score + Time.deltaTime * 20;
            Debug.Log("Score :" + ((int)score));
        }
        else if (!moveLeftScript.dashMode && !gameOver)
        {
            score = score + Time.deltaTime;
            Debug.Log("Score :" + ((int)score));
        }
        
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            dirtParticle.Play();
        } else if (collision.collider.CompareTag("Obstacle"))
        {
            explosionParticle.Play();
            gameOver = true;
            Debug.Log("Game Over!   Score: " + ((int)score));

            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
