using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public GameObject platform;
    Color color;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        platform.GetComponent<Collider2D>().enabled = false; //at start, disable pink platform colliders

        spriteRenderer = platform.GetComponent<SpriteRenderer>();// get sprite renderer for platforms

        color = new Color(200, 200, 200, 255);//the color is grey
        
        
        //spriteRenderer.color = color;

    }

    void Update()
    {
        //Debug.Log(color);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Blue")//when Blue collides with this object
        {
            platform.GetComponent<Collider2D>().enabled = true;//turn the pink colliders on
            spriteRenderer.color = new Color(1, 0, 0, 1);//change color to red
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Blue") //when Blue exits this collider
        {
            platform.GetComponent<Collider2D>().enabled = true; //keep the colliders for platforms on
        }
    }
    




}
