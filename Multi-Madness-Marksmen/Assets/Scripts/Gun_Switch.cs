using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Gun_Switch : MonoBehaviour
{
    public GameObject wepponAK;
    public GameObject wepponAKHolder;
    public GameObject wepponSniper;
    public GameObject wepponSniperHolder;
    public GameObject wepponShootgun;
    public GameObject wepponShootgunHolder;


    public GameObject wepponsStartPos;

    public static int selectedGun = 0;
    public static int currentGun = 0;

    GameObject[] gunHolders; 
    GameObject[] guns;

    void Start()
    {
        
        gunHolders = new GameObject[] {wepponAKHolder , wepponSniperHolder, wepponShootgunHolder};
        guns = new GameObject[] {wepponAK, wepponSniper, wepponShootgun};

    }

    public void onPlayerEnterMinimap(){

        removeGun(currentGun);

    }
    public void onPlayerLeaveMinimap(){

        takeGunBack(currentGun);

    }
    public int onPlayerRespawn(){ // use selected Gun

        Debug.Log("You are respawning with "+selectedGun);

        playGun(selectedGun);

        equipGun(selectedGun); // Player will be equiped with gun he selected

        currentGun = selectedGun; // Make selected gun current gun

        return guns[selectedGun].GetComponent<Universal_Gun_Script>().magazineSize;

    }
    public void onPlayerDeathGuns(){ // Use current gun

        resetGun(currentGun); // Gun will be reset to starting state

        removeGun(currentGun); // Gun will be removed from game scene (set non active)
                                    
    }

    public void onPauseGuns(){

        pauseGun(currentGun); // Gun will be paused (used when player enters esc menu)

    }

    public void onResumeGuns(){

        playGun(currentGun); // Gun will be un paused (used when player leave esc menu)

    }

    public int currentAmmo(){

        return guns[currentGun].GetComponent<Universal_Gun_Script>().currentAmmo;

    }

    public int maxAmmo(){

        return guns[currentGun].GetComponent<Universal_Gun_Script>().magazineSize;

    }

    public int gunOut(){

        return currentGun;

    }

    public int currentScore(){

        return  guns[currentGun].GetComponent<Universal_Gun_Script>().killCount;

    }
    int resetGun(int gunId){

        guns[gunId].GetComponent<Universal_Gun_Script>().zoom.fieldOfView = guns[gunId].GetComponent<Universal_Gun_Script>().standartFOV;
        guns[gunId].GetComponent<Universal_Gun_Script>().gunCam.nearClipPlane = 0.01f;

        if(gunId == 1){ // When it is sniper, the scope view will be set to false

            guns[gunId].GetComponent<Universal_Gun_Script>().scope.SetActive(false);

        }

        gunHolders[gunId].transform.position = wepponsStartPos.transform.position; 
        gunHolders[gunId].transform.rotation = wepponsStartPos.transform.rotation;

        guns[gunId].transform.position = wepponsStartPos.transform.position; 
        guns[gunId].transform.rotation = wepponsStartPos.transform.rotation;

        guns[gunId].GetComponent<Universal_Gun_Script>().currentAmmo = guns[gunId].GetComponent<Universal_Gun_Script>().magazineSize;
        guns[gunId].GetComponent<Universal_Gun_Script>().reloading = false;
        guns[gunId].GetComponent<Universal_Gun_Script>().reloadText.text = "";
        guns[gunId].GetComponent<Universal_Gun_Script>().youNeedToReload.text = "";

        guns[gunId].GetComponent<Universal_Gun_Script>().killCount = 0;

        playGun(gunId);

        return guns[gunId].GetComponent<Universal_Gun_Script>().killCount;

    }

    void equipGun(int gunId){

        gunHolders[gunId].SetActive(true);

    }
    void removeGun(int gunId){

        gunHolders[gunId].SetActive(false);

    }

    void takeGunBack(int gunId){

        gunHolders[gunId].SetActive(true);

    }
    void pauseGun(int gunId){

        guns[gunId].GetComponent<Universal_Gun_Script>().isEnabled = false;

    }

    void playGun(int gunId){

        guns[gunId].GetComponent<Universal_Gun_Script>().isEnabled = true;

    }

    public void akSelect(){ // akButton click

        selectedGun = 0;

    }

    public void srSelect(){ // srButton click

        selectedGun = 1;

    }    

    public void sgSelect(){ // sgButton click

        selectedGun = 2;

    }
}
