using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrondPatrol : MonoBehaviour
{

    //Movement speed for enemy that can be altered in Unity
    public float enemyMovementSpeed;
    //Casts a ray according to length specified in unity along with what direction it is looking (Raycast(arg, this argument is the direction, arg))
    public float raycastLength;
    //Is the enemy moving left
    public bool isMovingLeft;
    //Checks if contact has been made with the ray
    public Transform contactChecker;

    //Sets enemy position
    void Start()
    {
        SetEnemyPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * enemyMovementSpeed * Time.deltaTime);
    }

    private void SetEnemyPosition()
    {
        if(isMovingLeft == true)
            {
                //euler is used to flip the object
                transform.eulerAngles = new Vector2(0, -180);
                isMovingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 0);
                isMovingLeft = true;
            }
    }

    //Checks contact for the ray and sets enemy position
    private void FixedUpdate()
    {
        RaycastHit2D contactCheck = Physics2D.Raycast(contactChecker.position, Vector2.left, raycastLength);

        if(contactCheck == true)
        {
                SetEnemyPosition();
        }
    }
}
