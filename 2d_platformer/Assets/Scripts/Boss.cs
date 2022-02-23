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

		float playerPosition = player.position.x;  //for unit tests
		float bossPosition = transform.position.x;  //for unit tests
		bool bossIsFartherRight = true;             //for unit tests

		BossHelper bh = new BossHelper(); //instantiate the helper class

		bh.checkPosition(ref playerPosition, ref bossPosition, ref bossIsFartherRight);  //check the player 

		if (bossIsFartherRight)
		{
			
			//transform.localScale = flipped;
			//transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
			SR.flipX = true;

		}
		else if (!bossIsFartherRight)
		{
			
			//transform.localScale = flipped;
			//transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
			bossman.GetComponent<SpriteRenderer>().flipX = false;

		}
	}

}

public class BossHelper
{

	public void checkPosition(ref float playerPosition, ref float bossPosition, ref bool bossIsFartherRight)
    {
		if (playerPosition < bossPosition)
		{
			bossIsFartherRight = true;
		}
		else if (playerPosition > bossPosition)  //basically the same damn thing I'm doing in LookAtPlayer() but adding logic here so it can be tested
		{

			bossIsFartherRight = false;

		}
	}

}
