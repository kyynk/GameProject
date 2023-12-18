using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
	public Transform target;
	public float speed = 200f;
	public float nextWaypointDistance = 3f;

	public Transform EnemyGFX;

	Path path;
	int currentWaypoint = 0;
	bool reachedEndOfPath = false;

	Seeker seeker;
	Rigidbody2D rb;


	void Start()
	{
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();

		InvokeRepeating("UpdatePath", 0f, .5f);

	}

	void UpdatePath()
	{
		if (seeker.IsDone())
			seeker.StartPath(rb.position, target.position, OnPathComplete);
	}

	void OnPathComplete(Path P)
	{
		if(!P.error)
		{
			path = P;
			currentWaypoint = 0;
		}
	}

	void FixedUpdate()
	{
		if (path == null)
			return;

		if (currentWaypoint >= path.vectorPath.Count)
		{
			reachedEndOfPath = true;
			return;
		}
		else
		{
			reachedEndOfPath = false;
		}

		Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
		Vector2 force = direction * speed * Time.deltaTime;

		rb.AddForce(force);

		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

		if (distance < nextWaypointDistance)
		{
			currentWaypoint++;
		}

		if (rb.velocity.x >= 0.01f)
	{
			EnemyGFX.localScale = new Vector3(-0.2296841f, 0.1828911f, 1f);
		}
		else if (rb.velocity.x <= -0.01f)
		{
			EnemyGFX.localScale = new Vector3(0.2296841f, 0.1828911f, 1f);
		}
	}
}
