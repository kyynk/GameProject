using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_move : MonoBehaviour
{
	public float speed;
	public float startWaitTime;
	private float waitTime;
	public Transform movePos;
	public Transform leftDownPos;
	public Transform rightUpPos;

	void Start()
	{
		
		waitTime = startWaitTime;
		movePos.position = GetRandomPos();
	}

	void Update()
	{
		

		transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);

		if (Vector2.Distance(transform.position, movePos.position) < 0.1f) ;
		{
			if (waitTime <= 0)
			{
				movePos.position = GetRandomPos();
				waitTime = startWaitTime;
			}
			else
			{
				waitTime -= Time.deltaTime;
			}
		}
	}

	Vector2 GetRandomPos()
	{
		Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
		return rndPos;
	}
}
