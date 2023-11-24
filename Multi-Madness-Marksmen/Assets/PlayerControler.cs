using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject wepponAK;
    public GameObject wepponAKHolder;
    public GameObject wepponSniper;
    public GameObject wepponSniperHolder;
    public GameObject wepponShootgun;
    public GameObject wepponShootgunHolder;
    public GameObject playerCam;
    public GameObject playerMovement;
    public GameObject escMenuVisual;
    public GameObject playerObject;
    private GameObject[] weppons;
    private GameObject[] wepponHolders;
    public Rigidbody player;


    public KeyCode escape = KeyCode.Escape;


    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI menuMessage;

    //public float health = 100f;

    public string[] deathNotes = new string[] {"You must be from ITA :(", "He got you good", "Are you stupid or something ?", "SkILL ISUE", "Just HIT them! its not that hard","Are you blind?","L", "Get better :D", "Cmoon kill them", "Trash!", "Unbelivable", "WHY ARE YOU STILL DIINGGGG?!"};


    public int selectedGun = 0;
    public int actualGun;
    public int kills = 0;

    public bool isDeath = false;
    private bool isInMenu = false;
    bool did_it_die;

    void Start()
    {
        weppons = new GameObject[] {wepponAK, wepponSniper, wepponShootgun};

        wepponHolders = new GameObject[] {wepponAKHolder, wepponSniperHolder, wepponShootgunHolder};

        actualGun = selectedGun;

        killCountText.text = "0";
    }

    int killsOut;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            die();
        }

        Menu();

        killsOut = getKillCount(actualGun);
        
        if(killsOut != kills){// dal kill dalo by se říct

            kills = killsOut; // přepíše se počet kilů


            //Zještě bude nahoře proměná do které se zaoplíéše po připojení max score z databáze, poté se bude s hodnotou kills porvnávat a jakmile bude vyšší než  ta v databázi tak se přepíše

            //NEbo, se až po hráčově smrti připíše do databáíze což bude ase better


            killCountText.text = kills.ToString();

        }

/*
        if(wepponAKHolder.activeSelf){
            if(wepponAK.GetComponent<Universal_Gun_Script>().killCount > kills){
                kills = wepponAK.GetComponent<Universal_Gun_Script>().killCount;
                killCountText.text = kills.ToString();
                if(!wepponAK.GetComponent<Universal_Gun_Script>().wasAiming){
                    Debug.Log("NOSCOPE XD");
                }
            }
        } else if(wepponSniperHolder.activeSelf){
            kills = wepponSniper.GetComponent<Universal_Gun_Script>().killCount;
            killCountText.text = kills.ToString();
        }else if(wepponShootgunHolder.activeSelf){
            kills = wepponShootgun.GetComponent<Universal_Gun_Script>().killCount;
            killCountText.text = kills.ToString();
        }*/
    }
    /*public bool TakeDamage(float amount){

        did_it_die = false;

        health -= amount;

        Debug.Log("I have "+health+" hp");

        if(health <= 0f){

            did_it_die = true; // Pokud hráč umřel tak se hráči co ho zabil vrátí bool s informací jesrtli zabil nebo ne

            Debug.Log("Ive been kiled");

            die();
            //Zde se napíše to co se stane s hráčem po smrti
            //Zablokuje se movement
            //Hodí se death screan s výběrem zbraní
            //Resetuje se aktuální K/D stat
        }
        return did_it_die;
    }*/
    public void die(){ // i want too :)


        //Zde se před vynulováním porovná data z databáze a s kills, podle toho jeslti bude kills větší tak se tatro hodnota zapíše do databáze

        kills = 0;
        killCountText.text = "0";

        resetKillCount();

        removeGuns();

        disableControls();

        writeDeathNote();

        isDeath = true;

        player.isKinematic = true;

        escMenuVisual.SetActive(true);
        playerObject.SetActive(false);


    } 

    public void respawn(){ // You live only once, dont use it

        isDeath = false;

        playerObject.SetActive(true);

        enableControls();

        reloadGuns();

        actualGun = selectedGun;

        equipGun(selectedGun);

        escMenuVisual.SetActive(false);

        player.isKinematic = false;

    }
    private void Menu(){

        if(Input.GetKeyDown(escape)){

            if(isDeath){

                respawn();

            }else {
                if(isInMenu){

                    isInMenu = false;
                    equipGun(actualGun);
                    enableControls();
                    escMenuVisual.SetActive(false);

                } else {

                    isInMenu = true;
                    disableControls();
                    disableGuns();
                    escMenuVisual.SetActive(true);

                }
            }
        }
    }

    private void equipGun(int gunId){


        disableGuns();

        switch(gunId){

            case 0:{//AK

                wepponAK.GetComponent<Universal_Gun_Script>().isEnabled = true; // Dá aktivní zbran pod zadaným ID
                wepponAKHolder.SetActive(true);

                break;
            }
            case 1:{//SNIPER

                wepponSniper.GetComponent<Universal_Gun_Script>().isEnabled = true;// Dá aktivní zbran pod zadaným ID
                wepponSniperHolder.SetActive(true);

                break;
            }
            case 2:{//SHOTGUN

                wepponShootgun.GetComponent<Universal_Gun_Script>().isEnabled = true;// Dá aktivní zbran pod zadaným ID
                wepponShootgunHolder.SetActive(true);

                break;
            }

        }
        

    }

    private void disableGuns(){
            foreach(GameObject gun in weppons){//Začne to tím že to zneaktivní všechny zbraně

            gun.GetComponent<Universal_Gun_Script>().isEnabled = false;

        }
    }

    private void removeGuns(){
        foreach(GameObject holder in wepponHolders){//Začne to tím že to zneviditlení všechny zbraně

            holder.SetActive(false);

        }
    }   

    private void disableControls(){

        playerCam.GetComponent<PlayerCam>().isEnabled = false;
        playerMovement.GetComponent<PlayerMovement>().isEnabled = false;

    }
    private void enableControls(){

        playerCam.GetComponent<PlayerCam>().isEnabled = true;
        playerMovement.GetComponent<PlayerMovement>().isEnabled = true;

    }

    private void writeDeathNote(){

        int noteID = Random.Range(0, deathNotes.Length - 1);

        menuMessage.text = deathNotes[noteID];

    }

    public void selectAK(){
        selectedGun = 0;
    }

    public void selectSR(){
        selectedGun = 1;
    }

    public void selectSG(){
        selectedGun = 2;
    }

    private void reloadGuns(){

        foreach(GameObject gun in weppons){//Začne to tím že to zneaktivní všechny zbraně

            gun.GetComponent<Universal_Gun_Script>().currentAmmo = gun.GetComponent<Universal_Gun_Script>().magazineSize;

        }

    }

    private void resetKillCount(){

        foreach(GameObject gun in weppons){//Začne to tím že to zneaktivní všechny zbraně

            gun.GetComponent<Universal_Gun_Script>().killCount = 0;

        }
    }

    private int getKillCount(int gunSelected){

        int kills = 0;

        switch(gunSelected){

            case 0:{
                
                kills = wepponAK.GetComponent<Universal_Gun_Script>().killCount;

                break;
            }

            case 1:{

                kills = wepponSniper.GetComponent<Universal_Gun_Script>().killCount;

                break;
            }

            case 2:{

                kills = wepponShootgun.GetComponent<Universal_Gun_Script>().killCount;

                break;
            }

        }

        return kills;

    }

}


