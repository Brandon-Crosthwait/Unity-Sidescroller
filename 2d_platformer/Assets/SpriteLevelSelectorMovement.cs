using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteLevelSelectorMovement : MonoBehaviour
{

    // Animator assigned to the player
    private Animator animator;

    private bool isTriggered = false;
    private bool isStartPosReached = false;
    private bool isMoving = false;

    private float charMoveSpeed = 3f; //set sprite movespeed

    string previousLevel;

    float step;

    public GameObject levelSelectStartPosition; //starting checkpoint
    public GameObject spriteLevelSelector; //character model
    private GameObject currentPosition; //current character position
    private Vector3 desiredPosition; //where you want the character to go position


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); //initialize animator
        animator.SetFloat("Speed", charMoveSpeed); //set animator speed
        //set all booleans to false
        isStartPosReached = false;
        isTriggered = false;
        isMoving = false;

        previousLevel = PlayerPrefs.GetString("PreviousLevel");

        foreach(GameObject GO in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if(GO.tag == "Level Selection Box")
            {
                if(GO.name == previousLevel)
                {
                    Debug.Log("Success" + previousLevel + "is a match to" + GO.tag);
                    spriteLevelSelector.transform.position = GO.transform.position;
                    isStartPosReached = true;
                }
            }
        }

        desiredPosition = new Vector3(0f, 0f, 0f);
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
        if(isTriggered == true && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            if(currentPosition.transform.Find("W") != null)
            {
                animator.SetFloat("Speed", charMoveSpeed);
                isTriggered = false;
                isMoving = true;
                desiredPosition = new Vector3(spriteLevelSelector.transform.position.x, spriteLevelSelector.transform.position.y + 100f, spriteLevelSelector.transform.position.z);
            }
        }
        

        if (isTriggered == true && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            if (currentPosition.transform.Find("A") != null)
            {
                animator.SetFloat("Speed", charMoveSpeed);
                isTriggered = false;
                isMoving = true;
                desiredPosition = new Vector3(spriteLevelSelector.transform.position.x -100f, spriteLevelSelector.transform.position.y, spriteLevelSelector.transform.position.z);
            }
        }

        if (isTriggered == true && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            if (currentPosition.transform.Find("S") != null)
            {
                animator.SetFloat("Speed", charMoveSpeed);
                isTriggered = false;
                isMoving = true;
                desiredPosition = new Vector3(spriteLevelSelector.transform.position.x, spriteLevelSelector.transform.position.y -100f, spriteLevelSelector.transform.position.z);
            }
        }

        if (isTriggered == true && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            if (currentPosition.transform.Find("D") != null)
            {
                animator.SetFloat("Speed", charMoveSpeed);
                isTriggered = false;
                isMoving = true;
                desiredPosition = new Vector3(spriteLevelSelector.transform.position.x + 100f, spriteLevelSelector.transform.position.y, spriteLevelSelector.transform.position.z);
            }
        }
        /********************************************************************************************/

        if (isMoving == true && isTriggered == false)
        {
            spriteLevelSelector.transform.position = Vector3.MoveTowards(spriteLevelSelector.transform.position, desiredPosition, step);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetFloat("Speed", 0f);
        isTriggered = true;
        isMoving = false;
        currentPosition = collision.gameObject;
        GameObject levelSelectPoint = collision.gameObject;
        for (int i = 0; i < transform.childCount; i++)
        {
            levelSelectPoint.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
        GameObject levelSelectPoint = collision.gameObject;
        for (int i = 0; i < transform.childCount; i++)
        {
            levelSelectPoint.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
