using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public enum ControlStates //enum for scene states
    {
        IntroScene, //for intro scene
        PlatformScene, //for plaformer scene
        FollowScene, //for the follow scene
        DeleteScene, //for the delete scene
        MazeScene
    }
    public ControlStates controlStates; //declare the enum as a variable for later referencing


    public Rigidbody2D rigidBody; //reference to rigidbody in inspector
    public float speed;//variable for speed
    public float jumpForce; //variable for jump force
    public bool isGrounded; //bool for when the player can move
    public bool isClimbing;// bool for when player can climb

    public float wallJumpDirection;
    public Vector2 wallJumpPower = new Vector2(8f, 16f);

    public Camera cam; //reference to main camera in inspector

    private Vector2 target; //vector2 variable to pull player towards
    private float step;// variable for how many times the player is pulled towards the target
    // Start is called before the first frame update

    public TextMeshProUGUI answerText; // reference for answer text


    void Start()
    {
        
        //rigidBody.bodyType = RigidbodyType2D.Kinematic;
        if (SceneManager.GetActiveScene().name == "Intro")// if the active scene is named "Intro"
        {
            target = new Vector2(0f, 0f); //set target to (0,0)

            controlStates = ControlStates.IntroScene; //the current state is IntroScene
        }
        if (SceneManager.GetActiveScene().name == "Platform") //if the active scene is named "Platform"
        {
            controlStates = ControlStates.PlatformScene; //the current state is PlatformScene
        }
        if (SceneManager.GetActiveScene().name == "Follow")// if the scene is named follow
        {

            controlStates = ControlStates.FollowScene; //change the state to follow
            answerText.enabled = false;// answer text is not shown on screen

        }

    }

    // Update is called once per frame
    void Update()
    {

        CameraMovement(); //calls cameramovement function


        Debug.Log(controlStates);//to check which state the player is in
        if (controlStates == ControlStates.IntroScene)//if the current state is IntroScene
        {
            GetComponent<Rigidbody2D>().velocity = TwoMovement();
            TwoMovement();
        }
        if (controlStates == ControlStates.PlatformScene) //if the current state is PlatformScene
        {
            GetComponent<Rigidbody2D>().velocity = DynamicMovement();//rigidbody is set to dynamicmovement function
            DynamicMovement();//calls the dynamicmovement function  - player is affected by gravity
        }
        if (controlStates == ControlStates.FollowScene) //when 
        {
            GetComponent<Rigidbody2D>().velocity = FollowMovement();
            FollowMovement();
            
        }

    }

    private void OnTriggerStay2D(Collider2D collision)// if the player's collider is intersecting with...
    {

        if (collision.gameObject.name == "GreenCollider") // a collider called green collider
        {
            step = Time.deltaTime * 4; //step = every quarter of a frame
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, step);// move the player towards (0,0) every quarter of a frame
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision) //if player is within this collider
    {
        if (collision.gameObject.name == "Answer")// named answer
        {
            answerText.enabled = true;// answer text shows up on screen
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)// if object enters this collider
    {
        if (collision.gameObject.CompareTag("Wall"))// with wall tag
        {
            
            isClimbing = true;// is climbing is true, player can climb
        }
    }

    private void OnCollisionStay2D(Collision2D collision) //if object stays in this collider
    {
        if (collision.gameObject.CompareTag("Grounded"))// with grounded tag
        {
            isGrounded = true;// is grounded is true, can move
        }
    }

    private void OnCollisionExit2D(Collision2D collision) //when the object exits a collider
    {
        if (collision.gameObject.CompareTag("Grounded")) //if collider has grounded tag/ if object exits this collider
        {
            isGrounded = false; //is grounded is false, can't move
        }
        if (collision.gameObject.CompareTag("Wall")) //if collider has wall tag/ if object exits this collider
        {
            isClimbing = false; //is climbing is false, can't climb
        }
        
    }


    private void CameraMovement()//camera movement function, camera follows player
    {
        cam.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);// camera position follows coordinates of player
    }

    public Vector2 MazeMovement()//dynamic movement, player can move <- & -> & jump, affected by gravity
    {
        Vector2 velo = rigidBody.velocity;//set temp variable velo to rigidbody's veloctiy

        
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("moving");
            velo.y += speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("moving1");
            velo.y = -speed;
        }
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
            velo.y = 0;
        }

        return velo; //return vector data
    }

    public Vector2 DynamicMovement()//dynamic movement, player can move <- & -> & jump, affected by gravity
    {
        Vector2 velo = rigidBody.velocity;//set temp variable velo to rigidbody's veloctiy



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
        if (Input.GetKeyDown(KeyCode.W) && isGrounded == true)//if press W and velocity is between 0.1 and 0
        {
            velo.y = jumpForce;// jump up
        }
        if (Input.GetKey(KeyCode.W) && isClimbing == true)// if press w and is climbing is true
        {
            velo.y = speed;// player can move up like an elevator
        }
       
        return velo; //return vector data
    }


    public Vector2 TwoMovement() //player can move in two directions <- left and -> right
    {
        Vector2 velo = rigidBody.velocity;// set temp var velo to rigidbody's velocity

        if (Input.GetKey(KeyCode.D))//if press D
        {
            velo.x = speed;// move -> right
        }
        else if (Input.GetKey(KeyCode.A)) //if press A
        {
            velo.x = -speed;// move <- left
        }
        else
        {
            velo.x = 0;
        }
        return velo; //return vector data

    }

    public Vector2 FollowMovement()//dynamic movement, player can move <- & -> & jump, affected by gravity
    {
        Vector2 velo = rigidBody.velocity;//set temp variable velo to rigidbody's veloctiy


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
