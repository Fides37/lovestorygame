using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingFollow : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") //when collider collides with Player
       {
            SceneManager.LoadScene("Follow");// load follow scene


        }
        

        
    }


}
