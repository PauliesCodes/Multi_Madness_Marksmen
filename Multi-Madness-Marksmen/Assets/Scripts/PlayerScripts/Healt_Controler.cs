using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt_Controler : MonoBehaviour
{
    public float health = 100f;

    public bool TakeDamage(float amount){

        bool did_it_die = false;

        health -= amount;

        Debug.Log("I have "+health+" hp");

        if(health <= 0f){

            did_it_die = true; // Pokud hráč umřel tak se hráči co ho zabil vrátí bool s informací jesrtli zabil nebo ne

            Debug.Log("Ive been kiled");

            Die();
            //Zde se napíše to co se stane s hráčem po smrti
            //Zablokuje se movement
            //Hodí se death screan s výběrem zbraní
            //Resetuje se aktuální K/D stat
        }
        return did_it_die;
    }

    void Die(){

        /*Dnes konec, ale pro příštrě zde dát ot
         že jakmile umře hráč ve hře tak mu to disablne 
         controls a ukáže myš, hodí to jednoduchou animaci 
         kamery že se její pozice dá aby směřovala dolú a 
         vystoupá nahoru pak to hodí death screan kde bude 
         výběr zbraní (jedu z 3 si bude moct člověk vybrat)
         a také to napíše nějakou hlášku
         nude možnost odejít kdy to bude v singlu 
         pude do menu nebo v multaku to odpojí a 
         hodí do menu. NIKDY nemazat gameobject
         moc erroru moc bolest hlavy ://///ú
        */
        
    }


}
