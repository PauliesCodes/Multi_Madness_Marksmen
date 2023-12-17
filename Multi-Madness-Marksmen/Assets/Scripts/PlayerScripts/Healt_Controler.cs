using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Healt_Controler : MonoBehaviour
{
    public float baseHealth = 100f; // neměnit !!!
    private float maxHealth;
    private float health;

    public float healAmount = 10f;

    public UnityEngine.UI.Image healtBar;

    public TextMeshProUGUI healtNumber;
    public PlayerControler playerControler;

    void Start(){

        /*

        maxHealth = baseHealth;
        health = maxHealth;
        healtNumber.text = health.ToString();
        healtBar.fillAmount = health /maxHealth;

        */
    }

    /*public bool TakeDamage(float amount){

        bool did_it_die = false;

        health -= amount;
        healtBar.fillAmount = health/maxHealth;
        healtNumber.text = health.ToString();
        Debug.Log("I have "+health+" hp");

        if(health <= 0f){

            did_it_die = true; 

            Debug.Log("Ive been kiled");

            playerControler.die();
        }
        return did_it_die;
    }*/

    void Update(){
/*
        if(Input.GetKeyDown(KeyCode.G)){

            TakeDamage(1);

        }

        if(Input.GetKeyDown(KeyCode.H)){

            heal();

        }*/

    }

    public void heal(){

        /*

        health += healAmount;
        health = Mathf.Clamp(health, 0, maxHealth);

        healtBar.fillAmount = health /maxHealth;
        healtNumber.text = health.ToString();

        */

    }

    public void healUp(){

        /*

        health = maxHealth;
        healtBar.fillAmount = health /maxHealth;
        healtNumber.text = health.ToString();

        */
    }

    public void makeItHarder(){

        /*
        if(maxHealth - 10 > 0){

            maxHealth -= 10;

        } else {

            if(maxHealth - 5 > 0){

                maxHealth -= 5;

            }
            else{

                if(maxHealth - 1 > 0){

                    maxHealth -= 1;

                }

            }
            

        }

        health = maxHealth;
        healtBar.fillAmount = health /maxHealth;
        healtNumber.text = health.ToString();

        */

    }

    public void restHealth(){

        /*

        maxHealth = baseHealth;
        health = maxHealth;
        healtBar.fillAmount = health /maxHealth;
        healtNumber.text = health.ToString();

        */
    }

}
