using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //Reset moveDelta
        moveDelta = Vector3.zero;

        //Get Axis
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0);

        //Swap player direction for going left or right
        if(moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }

        //Make sure we can move in this direction
        //By casting a box there first
        //If the box returns null, free to move
        hit = Physics2D.BoxCast(transform.position,
                                boxCollider.size,
                                0,
                                new Vector2(moveDelta.x, 0),
                                Mathf.Abs(moveDelta.x * Time.deltaTime),
                                LayerMask.GetMask("Actor", "Blocking"));


        //Movement
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

        //Make sure we can move in this direction
        //By casting a box there first
        //If the box returns null, free to move
        hit = Physics2D.BoxCast(transform.position,
                                boxCollider.size,
                                0,
                                new Vector2(0,moveDelta.y),
                                Mathf.Abs(moveDelta.y * Time.deltaTime),
                                LayerMask.GetMask("Actor", "Blocking"));


        //Movement
        if (hit.collider == null)
        {
            transform.Translate(0,moveDelta.y * Time.deltaTime,0);
        }



        
    }
}
