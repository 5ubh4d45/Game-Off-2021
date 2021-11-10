using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.DialogueSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Required Compoments")]

    public Rigidbody2D rb;
    public Camera cam;

    private Player player;


    //bool for checking left or right
    private bool isFacingRight;
    public bool IsfacingRight => isFacingRight;

    private Vector2 _movementDir;
    private Vector2 _mousePos;



    void Start()
    {
        player = GetComponent<Player>();
        cam = Camera.main;
    }


    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {

        if (DialogueManager.Instance.IsOpen) return;
        MovePlayer();
    }


    private void ProcessInputs()
    {

        //Gethering input for Player MOvement X & Y axis
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _movementDir = new Vector2(moveX, moveY).normalized;

        //Gathering mousepointer position
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void MovePlayer()
    {

        //chnaging positions according to input
        rb.MovePosition(rb.position + _movementDir * player.MoveSpeed * Time.fixedDeltaTime);

        //Finding Angle of rotation from player to mouse
        Vector2 lookDir = _mousePos - rb.position;

        // checks if player is left or right of the mouse position
        if (lookDir.x > 0)
        {

            isFacingRight = true;
        }
        else
        {

            isFacingRight = false;
        }

    }


}