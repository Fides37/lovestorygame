using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

public class BlueBehavior : PlayerController
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Intro")// if the active scene is named "Intro"
        {
            controlStates = ControlStates.IntroScene; //the current state is IntroScene
        }
        if (SceneManager.GetActiveScene().name == "Platform") //if the active scene is named "Platform"
        {
            controlStates = ControlStates.PlatformScene; //the current state is PlatformScene
        }
        if (SceneManager.GetActiveScene().name == "Follow")// if the scene is follow
        {
            controlStates = ControlStates.FollowScene;// change state to follow
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (controlStates == ControlStates.PlatformScene) //if state is platform scene
        {
            GetComponent<Rigidbody2D>().velocity = BluePlatformMove(); //set rigid body velocity to blueplatform function
            BluePlatformMove();//call this function every frame
        }
        if (controlStates == ControlStates.FollowScene)//if state is follow scene
        {
            GetComponent<Rigidbody2D>().velocity = BlueFollowMove();//set rigid body velocity to follow scene
            BlueFollowMove();// call this function every frame
        }
        

    }
 

    private void OnCollisionStay2D(Collision2D collision)// if this object collides with a collider
    {
        if (collision.gameObject.CompareTag("Grounded")) //with the grounded tag
        {
            isGrounded = true;// object can move
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //if this object exits a collider
    {
        if (collision.gameObject.CompareTag("Grounded")) //with grounded tag
        {
            isGrounded = false;// object cant move
        }
        if (collision.gameObject.CompareTag("Wall"))// with wall tag
        {
            isClimbing = false;// cant climb
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)//if this object collides with a collider for the first time
    {
        if (collision.gameObject.name == "NoCheatingZone") //with an object named nocheatingzone
        {
            gameObject.transform.position = new Vector2(-41, -19);//set their position to where player cant interact with this object
        }
        if (collision.gameObject.CompareTag("Wall"))// with wall tag
        {
            isClimbing = true;// this object can climb
        }
    }

    public Vector2 BluePlatformMove()//dynamic movement, player can move <- & -> & jump, affected by gravity
    {
        Vector2 velo = rigidBody.velocity;//set temp variable velo to rigidbody's veloctiy

        //Debug.Log(rigidBody.velocity);


        if (Input.GetKey(KeyCode.RightArrow))//if press D
        {
            velo.x = speed; // player moves -> right
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) //if press A
        {
            velo.x = -speed; //player moves <- left
        }
        else //if not pressing anything
        {
            velo.x = 0; //do not move
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)//if press W and velocity is between 0.1 and 0
        {
            velo.y = jumpForce;// jump up
        }
        if (Input.GetKey(KeyCode.UpArrow) && isClimbing == true) //if press w and is climbing is true
        {
            velo.y = speed;// this object goes up like an elevator
        }
        return velo; //return vector data
    }

    public Vector2 BlueFollowMove()//dynamic movement, player can move <- & -> & jump, affected by gravity
    {
        Vector2 velo = rigidBody.velocity;//set temp variable velo to rigidbody's veloctiy

        //Debug.Log(rigidBody.velocity);


        if (Input.GetKey(KeyCode.D))//if press D
        {
            velo.x = speed; // player moves -> right
        }
        else if (Input.GetKey(KeyCode.A)) //if press A
        {
            velo.x = -speed; //player moves <- left
        }
        else //if not pressing anything
        {
            velo.x = 0; //do not move
        }
        return velo; //return vector data
    }


}
