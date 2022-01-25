using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
	public Transform player;
	public GameObject bossman;
	SpriteRenderer SR;



	public bool isFlipped = false;

    private void Start()
    {
		SR = bossman.GetComponent<SpriteRenderer>();

	}

    public void LookAtPlayer()
	{
		//Vector3 flipped = transform.localScale;
		//flipped.z *= -1f;

		if (transform.position.x > player.position.x)
		{
			
			//transform.localScale = flipped;
			//transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
			SR.flipX = true;

		}
		else if (transform.position.x < player.position.x)
		{
			
			//transform.localScale = flipped;
			//transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
			bossman.GetComponent<SpriteRenderer>().flipX = false;

		}
	}

}
