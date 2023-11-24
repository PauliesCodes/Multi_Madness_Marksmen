using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt_Controler_Enemy : MonoBehaviour
{
    public float health = 100f;

    public GameObject playerBody;
    public bool TakeDamage(float amount){

        bool did_it_die = false;

        health -= amount;

        Debug.Log("I have "+health+" hp");

        if(health <= 0f){

            did_it_die = true; 

            playerBody.SetActive(false);

            Debug.Log("Ive been kiled");

        }
        return did_it_die;
    }


}
