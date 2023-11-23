using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt_Controler : MonoBehaviour
{
    public float health = 100f;

    public PlayerControler playerControler;

    public bool TakeDamage(float amount){

        bool did_it_die = false;

        health -= amount;

        Debug.Log("I have "+health+" hp");

        if(health <= 0f){

            did_it_die = true; 

            Debug.Log("Ive been kiled");

            playerControler.die();
        }
        return did_it_die;
    }


}
