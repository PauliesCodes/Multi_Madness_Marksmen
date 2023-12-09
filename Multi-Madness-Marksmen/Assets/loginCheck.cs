using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loginCheck : MonoBehaviour
{
    public GameObject questionWindow;

    public void isItLogged(){

        if(PlayerInfo.isLogged){

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        else{

            questionWindow.SetActive(true);

        }

    }

    public void clickedOk(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

}
