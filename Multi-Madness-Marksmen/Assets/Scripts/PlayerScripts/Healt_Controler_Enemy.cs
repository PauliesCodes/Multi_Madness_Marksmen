using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt_Controler_Enemy : MonoBehaviour
{
    public float health = 100f;

    public GameObject playerBody;
    public GameObject player;
    public Rigidbody cap;
    public Rigidbody enemy;
    public AudioSource dieSound;
    public ParticleSystem explosionEffect;
    public bool TakeDamage(float amount){

        bool did_it_die = false;

        health -= amount;

        Debug.Log("I have "+health+" hp");

        if(health <= 0f){

            did_it_die = true; 

            playerBody.SetActive(false);

            explosionEffect.Play();
            dieSound.Play();

            Debug.Log("Ive been kiled");

            enemy.isKinematic = true;

            cap.constraints = RigidbodyConstraints.None;

            Invoke("destrit", 2f);

        }
        return did_it_die;
    }


    void destrit(){

        Destroy(player);

    }

}
