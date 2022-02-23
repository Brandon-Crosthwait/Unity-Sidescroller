using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteLevelSelectorMovement : MonoBehaviour
{
    // Animator assigned to the player
    private Animator animator;

    //booleans
    private bool isTriggered = false;
    private bool isStartPosReached = false;
    private bool isMoving = false;

    //floats
    private float charMoveSpeed = 3f; //set sprite movespeed
    float step; //how far to move sprite in each frame

    //strings
    string previousLevel; //for loading previous scene

    //GameObjects
    public GameObject levelSelectStartPosition; //starting checkpoint
    public GameObject spriteLevelSelector; //character model
    private GameObject currentPosition; //current character position

    //Vectors
    private Vector3 desiredPosition; //where you want the character to go position

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //initialize animator
        animator.SetFloat("Speed", charMoveSpeed); //set animator speed

        //reset starting variables
        isStartPosReached = false;
        isTriggered = false;
        isMoving = false;
        desiredPosition = new Vector3(0f, 0f, 0f);

        previousLevel = PlayerPrefs.GetString("PreviousLevel"); //Retrieved level last played or on
        Debug.Log(PlayerPrefs.GetString("PreviousLevel"));

        //If a level has previously been played, find the correct trigger to start sprite on
        //i.e. if last level was level 1, start sprite on Gameobject that contains variable for level 1
        foreach(GameObject GO in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if(GO.tag == "Level Selection Box")
            {
                if(GO.name == previousLevel)
                {
                    spriteLevelSelector.transform.position = GO.transform.position;
                    isStartPosReached = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Return to Main Menu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(Build.sceneOrder.MainMenu.ToString());
        }
        step = animator.GetFloat("Speed") * Time.deltaTime;

        //Move Character to Starting Position
        if (isTriggered == false && isStartPosReached == false)
        {
            spriteLevelSelector.transform.position = Vector3.MoveTowards(spriteLevelSelector.transform.position, levelSelectStartPosition.transform.position, step);
        }

        //Once Starting Position is Reached, Go to Idle
        if (isTriggered == true && isStartPosReached == false)
        {
            animator.SetFloat("Speed", 0f);
            isStartPosReached = true;
        }

        //Use Space to Select Level or Menu at Current Location
        if(isTriggered == true && Input.GetKeyDown(KeyCode.Space))
        {
            int testNumber = 999; //no possibility of hitting, just setting out of range of any possible scene
            //Check Collider Object to get destination level
            foreach(Transform child in currentPosition.transform)
            {
                int.TryParse(child.name.ToString(), out testNumber);
                if(testNumber != 0 && testNumber != 999)
                {
                    if (testNumber == 99) testNumber = 0; //since all non-number objects return 0, we want to ignore 0 but still scene to main menu(scene 0)
                    SceneManager.LoadScene(testNumber);
                }
            }
        }

        //Determine if player can go any of the following directions and move character if that direction is applicable
        /******************************************************************************************/
        if(isTriggered == true) //if sprite is standing on select point
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentPosition.transform.Find("W") != null) MoveSpriteToNext(0f, 100f); //move sprite up
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentPosition.transform.Find("A") != null) MoveSpriteToNext(-100f, 0f); //move sprite left
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentPosition.transform.Find("S") != null) MoveSpriteToNext(0f, -100f); //move sprite down
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentPosition.transform.Find("D") != null) MoveSpriteToNext(100f, 0f); //move sprite right
            }
        }
        
        /********************************************************************************************/

        //keep the sprite moving a direction until a trigger is reached
        if (isMoving == true && isTriggered == false)
        {
            spriteLevelSelector.transform.position = Vector3.MoveTowards(spriteLevelSelector.transform.position, desiredPosition, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetFloat("Speed", 0f); //set speed to 0
        isTriggered = true; //set is triggered to true to allow user choice in update
        isMoving = false; //set is moving to false to prevent sprite movement until choice
        currentPosition = collision.gameObject; //reset sprite position
        GameObject levelSelectPoint = collision.gameObject; //get current game object for further manipulation
        
        //enable on-screen text for current level info by turning on child object
        for (int i = 0; i < transform.childCount; i++) levelSelectPoint.transform.GetChild(i).gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false; //remove old trigger condition
        GameObject levelSelectPoint = collision.gameObject; //get current game object for further manipulation

        //disable on-screen text for current level info by turning off child object
        for (int i = 0; i < transform.childCount; i++) levelSelectPoint.transform.GetChild(i).gameObject.SetActive(false);
    }

    //helper function to move sprite in desired direction
    private void MoveSpriteToNext(float newX, float newY)
    {
        animator.SetFloat("Speed", charMoveSpeed);
        isTriggered = false;
        isMoving = true;
        desiredPosition = new Vector3(spriteLevelSelector.transform.position.x + newX, spriteLevelSelector.transform.position.y + newY, spriteLevelSelector.transform.position.z);
    }
}
