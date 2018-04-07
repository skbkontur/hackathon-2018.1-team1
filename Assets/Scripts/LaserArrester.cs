﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserArrester : MonoBehaviour {

    private Transform leftArrester;
    private Transform rightArrester;
    private LineRenderer lr;
    private BoxCollider2D boxCollider;

    public bool isStatic = false;

    public float laserShootSeconds = 1f;
    public float laserShootTimeoutSeconds = 0.5f;

    // Use this for initialization
    void Start()
    {
        leftArrester = transform.GetChild(0);
        rightArrester = transform.GetChild(1);

        lr = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (!isStatic)
        {
            StartCoroutine(ShootTimer());
        } else
        {
            lr.enabled = true;
            boxCollider.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        var positions = new Vector3[] { leftArrester.transform.position, rightArrester.transform.position };

        lr.SetPositions(positions);
	}

    private void ShootLaser ()
    {
        lr.enabled = true;
        boxCollider.enabled = true;
        StartCoroutine(DisableLaser());
    }

    private IEnumerator DisableLaser()
    {
        yield return new WaitForSeconds(laserShootTimeoutSeconds);
        lr.enabled = false;
        boxCollider.enabled = false;
    }

    private IEnumerator ShootTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(laserShootSeconds);

            ShootLaser();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("LASER COLLIDE");

        //var health = collision.collider.GetComponent<Health>();

        //health.TakeDamage();

    }
}