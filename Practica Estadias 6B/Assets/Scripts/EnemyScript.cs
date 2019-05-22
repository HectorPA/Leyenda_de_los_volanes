using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Vector3 velocity;
    Animator anim;

    public Vector3[] points;
    int pointIndex;

    GameObject playerObj;

    bool isAttacking = false;
    bool isWalking = false;
    bool isFollowingPlayer = false;

    int inputH;
    int inputV;

    float hp = 10f;

    void Start()
    {
        anim = GetComponent<Animator>();
        pointIndex = 0;
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        moverEnemigo();
        setAnimations();
    }

    void moverEnemigo()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerObj.transform.position);

        if (distanceToPlayer < 6f && !isFollowingPlayer)
        {
            isFollowingPlayer = true;
        }
        else if (distanceToPlayer > 7f && isFollowingPlayer)
        {
            isFollowingPlayer = false;
        }

        if (distanceToPlayer <= 2f)
        {
            inputV = 0;
            anim.Play("Attack");
            //playerObj.anim.Play("DAMAGED00");
        }
        else
        {
            inputV = 1;
        }

        if (!isFollowingPlayer)
        {
            patrolZone();
        }
        else
        {
            followPlayer();
            //anim.Play("Walk");
        }

        float currentRotation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;

        float moveZ = inputV * velocity.z * Time.deltaTime;
        float moveX = inputH * velocity.x * Time.deltaTime;
        /*
        if (run)
        {
            moveZ *= runMultiply;
        }*/

        Vector3 pos = transform.position;
        pos.z += moveZ * Mathf.Cos(currentRotation);
        pos.x += moveZ * Mathf.Sin(currentRotation);
        transform.position = pos;

        transform.Rotate(new Vector3(0, moveX, 0));

    }

    void patrolZone()
    {
        int nextPoint = pointIndex + 1;
        if (nextPoint >= points.Length)
        {
            nextPoint = 0;
        }
        transform.LookAt(points[nextPoint]);

        float distance = Vector3.Distance(transform.position, points[nextPoint]);
        if (distance < 1f)
        {
            if (pointIndex >= points.Length - 1)
            {
                pointIndex = 0;
            }
            else
            {
                pointIndex++;
            }
        }
    }

    void followPlayer()
    {
        transform.LookAt(playerObj.transform.position);
    }

    void setAnimations()
    {
        anim.SetFloat("InputH", inputH);
        anim.SetFloat("InputV", inputV);
        anim.SetBool("IsWalking", isWalking);
        anim.SetBool("IsAttacking", isAttacking);
        //anim.SetBool("isRunning", run);
        //if (isJumping)
            //anim.Play("JUMP00");
    }
}
