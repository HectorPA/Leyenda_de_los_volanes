using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Camera mainCam;

    public Vector3 velocity;
    Animator playerAnim;

    bool isAttacking = false;
    bool isWalking = false;

    //float inputH;
    float inputV;

    float hp = 10f;

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
        //Debug.Log("Script del Player detectado");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("FPS: " + (1 / Time.deltaTime));
        SetAnimations();
        GetActions();
        SetMouse();
        //MovePlayer();
    }

    void SetMouse()
    {        
            Ray rayoDeCamara = mainCam.ScreenPointToRay(Input.mousePosition);
            Plane planoSuelo = new Plane(Vector3.up, Vector3.zero);
            float longitudDeRayo;

        if (planoSuelo.Raycast(rayoDeCamara, out longitudDeRayo))
        {
            Vector3 direccionCursor = rayoDeCamara.GetPoint(longitudDeRayo);

            if (inputV != 0)
            {
                transform.LookAt(new Vector3(direccionCursor.x, transform.position.y, direccionCursor.z));
            }
            else
            {
                return;
            }
        }
    }

    void GetActions()
    {
        inputV = Input.GetAxis("Vertical");

        if (Input.GetMouseButton(0))
        {
            if (isWalking)
            {
                isWalking = false;
                //Debug.Log("Caminando: " + isWalking);
            }

            isAttacking = true;
            //Debug.Log("Atacando: " + isAttacking);
        }
        else
        {
            isAttacking = false;
            //Debug.Log("Atacando: " + isAttacking);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            isWalking = true;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            isWalking = false;
        }

        if (isAttacking)
        {
            playerAnim.Play("Attack");
        }

        if (isWalking)
        {
            //Debug.Log("Caminando: " + isWalking);
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        //Debug.Log("Caminando: " + isWalking);
        //inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        //Debug.Log("Input V: " + inputV);

        float currentRotation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;

        float moveZ = inputV * velocity.z * Time.deltaTime;
        //float moveX = inputH * velocity.x * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.z += moveZ * Mathf.Cos(currentRotation);
        pos.x += moveZ * Mathf.Sin(currentRotation);
        transform.position = pos;

        //transform.Rotate(new Vector3(0, direction.x, 0));
        //transform.Rotate(new Vector3(0, moveX, 0));

    }

    void SetAnimations()
    {
        //playerAnim.SetFloat("InputH", inputH);
        playerAnim.SetFloat("InputV", inputV);
        playerAnim.SetBool("IsAttacking", isAttacking);
        playerAnim.SetBool("IsWalking", isWalking);
    }
}
