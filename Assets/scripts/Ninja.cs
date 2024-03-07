using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool ninjaSelected;
    public static bool dragSelectedNinaAllowed, mouseOverNinja;
    private Vector2 MousePos;
    private float dragOffsetX, dragOffsetY;
    public GameObject board;


    private Vector2 StartingTrans;
    // Start is called before the first frame update
    void Start()
    {
       
        StartingTrans = gameObject.transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        ninjaSelected = false;
        dragSelectedNinaAllowed = false;
        mouseOverNinja = false;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxSelection>())
        {
            spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
            ninjaSelected = true;
        }
        if (collision.gameObject.name == "Left" && collision.gameObject.name == "Right")
        {

            resetPosition();

        }
        if (collision.gameObject.name == "Left" && collision.gameObject.name != "Right")
        {

            board.GetComponent<multiSelect>().ninjasInLeft +=1;

            
        }
        if (collision.gameObject.name == "Right" && collision.gameObject.name != "Left")
        {
            board.GetComponent<multiSelect>().ninjasInRight +=1;
           
        }

        if (collision.gameObject.name == "Bottom")
        {

            board.GetComponent<multiSelect>().ninjasInBottom += 1;
        }
      
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<BoxSelection>() && Input.GetMouseButton(0))
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            ninjaSelected = false;
        }

        if (collision.gameObject.name == "Bottom")
        {

            board.GetComponent<multiSelect>().ninjasInBottom -= 1;
        }

      

        if (collision.gameObject.name == "Left" )
        {

            board.GetComponent<multiSelect>().ninjasInLeft -= 1;

        }
        if (collision.gameObject.name == "Right" )
        {
            board.GetComponent<multiSelect>().ninjasInRight -= 1;

        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if (board.GetComponent<multiSelect>().correctAns == true)
        {
            resetPosition();
        }
       
        if (Input.GetMouseButtonDown(0))
        {
            dragOffsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            dragOffsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

        }

        if (Input.GetMouseButton(0))
        {
            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }

        if(ninjaSelected && dragSelectedNinaAllowed)
        {
            transform.position = new Vector2(MousePos.x - dragOffsetX, MousePos.y - dragOffsetY);

        }

        if(Input.GetMouseButtonDown(1))
        {
            ninjaSelected = false;
            dragSelectedNinaAllowed = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }

        if (Input.GetMouseButtonDown(0) && ninjaSelected && mouseOverNinja ==false)
        {
            ninjaSelected = false;
            dragSelectedNinaAllowed = false;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
    }


    private void OnMouseDown()
    {
        mouseOverNinja = true;
    }


    private void OnMouseUp()
    {
        mouseOverNinja = false;
        dragSelectedNinaAllowed = false;
    }

    private void OnMouseDrag()
    {
        dragSelectedNinaAllowed = true;
        if (!ninjaSelected)
        {
            dragSelectedNinaAllowed = false;
        }

        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(MousePos.x - dragOffsetX, MousePos.y - dragOffsetY);
    }


    public void resetPosition()
    {
        ninjaSelected = false;
        OnMouseUp();
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        gameObject.transform.position = StartingTrans;

        //board.GetComponent<multiSelect>().correctAns = false;
    }
}
