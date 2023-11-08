using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 15;
    private float leftBound = -15;

    private float lowSpeed;
    private float highSpeed;
    public bool dashMode = false;

    private PlayerController playerControllerScript;
    void Start()
    {
        lowSpeed = speed;
        highSpeed = speed * 2;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        } 
        if(gameObject.CompareTag("Obstacle") && transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }

        if(Input.GetKey(KeyCode.LeftShift) && playerControllerScript.isOnGround) {
            speed = highSpeed;
            dashMode = true;
        }else {
            speed = lowSpeed;
            dashMode = false;
        }
    }
}
