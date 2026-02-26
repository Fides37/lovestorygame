using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingDelete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") //when player collides with this collider
        {
            SceneManager.LoadScene("DeletingYou");//change scene to deletingyou
        }
    }


}
