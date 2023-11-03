using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt_Controler : MonoBehaviour
{

    public float health = 100f;

    public bool TakeDamage(float amount){

        bool did_it_die = false;

        health -= amount;

        if(health <= 0f){

            did_it_die = true; // Pokud hráč umřel tak se hráči co ho zabil vrátí bool s informací jesrtli zabil nebo ne
    
            Die();
            //Zde se napíše to co se stane s hráčem po smrti
            //Zablokuje se movement
            //Hodí se death screan s výběrem zbraní
            //Resetuje se aktuální K/D stat
        }
        return did_it_die;
    }

    void Die(){

        Destroy(gameObject);
    }


}
