using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header ("Patrol Areas")]
    [SerializeField] private Transform leftPatrolArea;
    [SerializeField] private Transform rightPatrolArea;

    [Header("Platform")]
    [SerializeField] private Transform platform;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    private void Awake()
    {
        initScale = platform.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (platform.position.x >= leftPatrolArea.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (platform.position.x <= rightPatrolArea.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        //Face direction
        platform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        //Move in direction
        platform.position = new Vector3(platform.position.x + Time.deltaTime * _direction * speed, platform.position.y, platform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

}