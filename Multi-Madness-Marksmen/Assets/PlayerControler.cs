using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public GameObject settingsMenuVisual;
    public GameObject playerUI;
    public GameObject playerObject;
    private GameObject[] weppons;
    private GameObject[] wepponHolders;
    public GameObject[] playerParts;
    public GameObject enemy;
    public Rigidbody player;

    public Healt_Controler healt_Controler;

    public KeyCode escape = KeyCode.Escape;

    public Transform spawnPointA;
    public Transform spawnPointB;

    public Material akMaterial;
    public Material srMaterial;
    public Material sgMaterial;

    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI menuMessage;
    public TextMeshProUGUI maxAmmo;
    public TextMeshProUGUI currnetAmmo;

    public ParticleSystem explosionEffect;

    public AudioSource dieSound;

    //public float health = 100f;

    public string[] deathNotes = new string[] {"You done well, you can rest now", "He got you good", "Are you stupid or something ?", "SkILL ISUE", "Just HIT them! its not that hard","Are you blind?","L", "Get better :D", "Cmoon kill them", "Trash!", "Unbelivable", "WHY ARE YOU STILL DIINGGGG?!"};


    public int selectedGun = 0;
    public int actualGun;
    public int kills = 0;
    private int maxAmmo0;
    private int maxAmmo1;
    private int maxAmmo2;
    private int ammo;

    public bool isDeath = false;
    private bool isInMenu = false;
    bool did_it_die;
    public bool singlePlayer = true;

    public float timeOfGame;

    public TextMeshProUGUI timerText;
    private bool isCounting = false;

    public int maxPlayerScore;

    void Start()
    {
        //sem se z databáze uloží max score podle jména
        // do proměné maxScore

        maxPlayerScore = 0;// sem se to hodí



        // Získání komponenty Transform aktuálního objektu
        Transform myTransform = transform;

        // Zjištění počtu všech potomků pomocí rekurzivní funkce
        int totalChildCount = CountAllChildren(myTransform);

        // Vypsání počtu všech potomků do konzole
        Debug.Log("Celkový počet potomků: " + totalChildCount);

        weppons = new GameObject[] {wepponAK, wepponSniper, wepponShootgun};

        wepponHolders = new GameObject[] {wepponAKHolder, wepponSniperHolder, wepponShootgunHolder};

        actualGun = selectedGun;

        killCountText.text = "0";

        displayWepponStats(selectedGun);

        healt_Controler.healUp();

        maxAmmo0 = Convert.ToInt32(wepponAK.GetComponent<Universal_Gun_Script>().magazineSize);
        maxAmmo1 = Convert.ToInt32(wepponSniper.GetComponent<Universal_Gun_Script>().magazineSize);
        maxAmmo2 = Convert.ToInt32(wepponShootgun.GetComponent<Universal_Gun_Script>().magazineSize);

        startSettings();

    }
int CountAllChildren(Transform parent)
    {
        int count = 0;

        // Projít všechny přímé potomky
        for (int i = 0; i < parent.childCount; i++)
        {
            // Zvýšit počet o jedna pro aktuální potomka
            count++;

            // Rekurzivně získat počet potomků potomka
            count += CountAllChildren(parent.GetChild(i));
        }

        return count;
    }
    int killsOut;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            dieByTimer();
        }

        Menu();

        if(singlePlayer){

            if(Input.GetKeyDown(KeyCode.E)){


                float a = UnityEngine.Random.Range(spawnPointA.position.x, spawnPointB.position.x);

                float b = UnityEngine.Random.Range(spawnPointA.position.z, spawnPointB.position.z);

                Instantiate(enemy, new Vector3(a,10,b), transform.rotation);

            }

        }

        killsOut = getKillCount(actualGun);
        
        if(killsOut != kills){// dal kill dalo by se říct

            kills = killsOut; // přepíše se počet kilů

            if(kills == 1){
                StartCountdown();
                Debug.Log("Timer on");
            }

            healt_Controler.makeItHarder();

            //Zještě bude nahoře proměná do které se zaoplíéše po připojení max score z databáze, poté se bude s hodnotou kills porvnávat a jakmile bude vyšší než  ta v databázi tak se přepíše

            //NEbo, se až po hráčově smrti připíše do databáíze což bude ase better


            killCountText.text = kills.ToString();

        }

        displayCurrentAmmo(actualGun);
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

        explosionEffect.Play();
        dieSound.Play();

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

        playerUI.SetActive(false);

    } 

    public void respawn(){ // You live only once, dont use it

        float a = UnityEngine.Random.Range(spawnPointA.position.x, spawnPointB.position.x);

        float b = UnityEngine.Random.Range(spawnPointA.position.z, spawnPointB.position.z);

        playerMovement.transform.position = new Vector3(a,spawnPointA.position.y, b);

        playerMovement.transform.rotation = spawnPointA.rotation;

        isDeath = false;

        playerObject.SetActive(true);

        enableControls();

        reloadGuns();

        actualGun = selectedGun;

        equipGun(selectedGun);

        displayWepponStats(selectedGun);

        changeCatLook(selectedGun);

        healt_Controler.restHealth();

        healt_Controler.healUp();

        escMenuVisual.SetActive(false);
        settingsMenuVisual.SetActive(false);

        player.isKinematic = false;

        playerUI.SetActive(true);

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
                    settingsMenuVisual.SetActive(false);
                    playerUI.SetActive(true);


                } else {

                    isInMenu = true;
                    disableControls();
                    disableGuns();
                    menuMessage.text = "Paused, just for you <3 Meow";
                    escMenuVisual.SetActive(true);
                    playerUI.SetActive(false);

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

        int noteID = UnityEngine.Random.Range(0, deathNotes.Length - 1);

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

    private int getMaxAmmo(int gunSelected){

        int Ammo = 30;

        switch(gunSelected){

            case 0:{
                
                Ammo = Convert.ToInt32(wepponAK.GetComponent<Universal_Gun_Script>().magazineSize);

                break;
            }

            case 1:{

                Ammo = Convert.ToInt32(wepponSniper.GetComponent<Universal_Gun_Script>().magazineSize);

                break;
            }

            case 2:{

                Ammo = Convert.ToInt32(wepponShootgun.GetComponent<Universal_Gun_Script>().magazineSize);

                break;
            }
        }

        return Ammo;

    }
    private void displayWepponStats(int gunSelected){

        maxAmmo.text = getMaxAmmo(gunSelected).ToString();
        currnetAmmo.text = getMaxAmmo(gunSelected).ToString();

    }


    private void displayCurrentAmmo(int gunSelected){

        int Ammo = 0;

        switch(gunSelected){

            case 0:{
                
                Ammo = Convert.ToInt32(wepponAK.GetComponent<Universal_Gun_Script>().currentAmmo);

                break;
            }

            case 1:{

                Ammo = Convert.ToInt32(wepponSniper.GetComponent<Universal_Gun_Script>().currentAmmo);

                break;
            }

            case 2:{

                Ammo = Convert.ToInt32(wepponShootgun.GetComponent<Universal_Gun_Script>().currentAmmo);

                break;
            }
        }       

        if(ammo != Ammo){

            ammo = Ammo;

            currnetAmmo.text = ammo.ToString();

        }


    }

    private void changeCatLook(int gunSelected){

        Material playerMaterial = akMaterial;

        switch(gunSelected){

            case 1:{

                playerMaterial = srMaterial;

                break;
            }

            case 2:{

                playerMaterial = sgMaterial;

                break;
            }

        }       
        
        foreach (GameObject objekt in playerParts)
        {
            // Získání komponenty Renderer
            Renderer rend = objekt.GetComponent<Renderer>();

            // Nastavení nového materiálu
            if (rend != null)
            {
                rend.material = playerMaterial;
            }
        }

    }

    public void backToGame(){

        isInMenu = false;
        equipGun(selectedGun);
        enableControls();
        escMenuVisual.SetActive(false);
        settingsMenuVisual.SetActive(false);
        playerUI.SetActive(true);

        if(isDeath){
            respawn();
        }

    }

    public void startSettings(){

        kills = 0;
        killCountText.text = "0";

        resetKillCount();

        removeGuns();

        disableControls();

        menuMessage.text = "Lets kill them !";

        isDeath = true;

        player.isKinematic = true;

        escMenuVisual.SetActive(true);
        playerObject.SetActive(false);

        playerUI.SetActive(false);

    }


    void StartCountdown()
    {            
        isCounting = true;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        while (isCounting && timeOfGame > 0f)
        {
            // Odečte jednu sekundu
            timeOfGame -= 1f;

            // Aktualizuje text na UI
            UpdateTimerUI();

            // Počká jednu sekundu
            yield return new WaitForSeconds(1f);
        }

        // Zde provede akce po skončení časovače
        OnTimerEnd();
    }
    public void StopCountdown()
    {
        isCounting = false;
        StopAllCoroutines();
    }
    public void ResetCountdown()
    {
        timeOfGame = 30f; // Nastavte čas na původní hodnotu
        UpdateTimerUI();
        StopCountdown(); // Zastavte případný běžící časovač
    }
    void UpdateTimerUI()
    {
        // Aktualizuje text na UI podle zbývajícího času
        timerText.text = Mathf.Ceil(timeOfGame).ToString();
    }

    void OnTimerEnd()
    {
        // Zde provede akce po skončení časovače
        Debug.Log("Časovač skončil!");
        dieByTimer();
        timeOfGame = 30;
        timerText.text = Mathf.Ceil(timeOfGame).ToString();
    }


    public void leaveToMenu(){
        
        SceneManager.LoadScene(0);

    }
    public void dieByTimer(){ // i want too :)

        if(kills > maxPlayerScore){

            //Zde se otevře databáze a přepíše se to pokud to bylo větší 


        }


        ResetCountdown();

        explosionEffect.Play();
        dieSound.Play();

        //Zde se před vynulováním porovná data z databáze a s kills, podle toho jeslti bude kills větší tak se tatro hodnota zapíše do databáze
        writePlayerScore();

        kills = 0;
        killCountText.text = "0";

        resetKillCount();

        removeGuns();

        disableControls();

        isDeath = true;

        player.isKinematic = true;

        escMenuVisual.SetActive(true);
        playerObject.SetActive(false);

        playerUI.SetActive(false);

    } 

    private void writePlayerScore(){

        menuMessage.text = "Your score : "+kills;

    }
}