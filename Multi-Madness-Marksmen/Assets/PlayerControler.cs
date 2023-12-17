using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

public class PlayerControler : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject wepponAK;
    //public GameObject wepponAKHolder;
    //public GameObject wepponSniper;
    //public GameObject wepponSniperHolder;
    //public GameObject wepponShootgun;
    //public GameObject wepponShootgunHolder;

    public Gun_Switch gunSwitchScript;
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

    public Camera mainCamera;
    public Camera topCamera;

    //public float health = 100f;

    public string[] deathNotes = new string[] {"You done well, you can rest now", "He got you good", "Are you stupid or something ?", "SkILL ISUE", "Just HIT them! its not that hard","Are you blind?","L", "Get better :D", "Cmoon kill them", "Trash!", "Unbelivable", "WHY ARE YOU STILL DIINGGGG?!"};


    public int selectedGun = 0;
    public int actualGun;
    public int kills = 0;
    private int ammo;

    public bool isDeath = true;
    private bool isInMenu = false;
    bool did_it_die;
    public bool singlePlayer = true;

    public float timeOfGame;

    public TextMeshProUGUI timerText;
    private bool isCounting = false;
    public int maxPlayerScore;
    public string DateOfScore = "";

private Coroutine countdownCoroutine;

    void Start()
    {
        gunSwitchScript = GetComponent<Gun_Switch>();

        mainCamera.enabled = true;

        topCamera.enabled = false;

        maxPlayerScore = 0;// sem se to hodí

        // Získání komponenty Transform aktuálního objektu
        Transform myTransform = transform;

        // Zjištění počtu všech potomků pomocí rekurzivní funkce
        int totalChildCount = CountAllChildren(myTransform);

        // Vypsání počtu všech potomků do konzole
        Debug.Log("Celkový počet potomků: " + totalChildCount);

        actualGun = selectedGun;

        killCountText.text = "0";

        healt_Controler.healUp();

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

        if(Input.GetKey(KeyCode.Tab)){

            mainCamera.enabled = false;
            topCamera.enabled = true;
            deleteAllDamageTexts();
            gunSwitchScript.onPlayerEnterMinimap();

        }else{

            mainCamera.enabled = true;
            topCamera.enabled = false;

        }
        if(Input.GetKeyUp(KeyCode.Tab)){

            gunSwitchScript.onPlayerLeaveMinimap();

        }

        Menu();
/*
        if(singlePlayer){

            if(Input.GetKeyDown(KeyCode.E)){


                float a = UnityEngine.Random.Range(spawnPointA.position.x, spawnPointB.position.x);

                float b = UnityEngine.Random.Range(spawnPointA.position.z, spawnPointB.position.z);

                Instantiate(enemy, new Vector3(a,10,b), transform.rotation);

            }

        }*/

        killsOut = gunSwitchScript.currentScore();
        
        if(killsOut != kills){// dal kill dalo by se říct

            kills = killsOut; // přepíše se počet kilů

            if(kills == 1){
                StartCountdown();
                Debug.Log("Timer on");
            }

            //healt_Controler.makeItHarder(); // jen když bych chtěl hp a taky je ubírat

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
    /*public void die(){ // i want too :)

        explosionEffect.Play();
        dieSound.Play();

        deleteAllDamageTexts();
        //Zde se před vynulováním porovná data z databáze a s kills, podle toho jeslti bude kills větší tak se tatro hodnota zapíše do databáze

        kills = 0;
        killCountText.text = "0";

        gunSwitchScript

        disableControls();

        writeDeathNote();

        isDeath = true;

        player.isKinematic = true;

        escMenuVisual.SetActive(true);
        playerObject.SetActive(false);

        playerUI.SetActive(false);

    } */

    public void respawn(){ // You live only once, dont use it note. mby you can try to be with her in next life. I know its now over, but ......

        menuMessage.color = Color.white;

        float a = UnityEngine.Random.Range(spawnPointA.position.x, spawnPointB.position.x);

        float b = UnityEngine.Random.Range(spawnPointA.position.z, spawnPointB.position.z);

        playerMovement.transform.position = new Vector3(a,spawnPointA.position.y, b);

        playerMovement.transform.rotation = spawnPointA.rotation;

        isDeath = false;

        playerObject.SetActive(true);

        enableControls();
        
        string magazineSize = gunSwitchScript.onPlayerRespawn().ToString();

        currnetAmmo.text = magazineSize;

        Debug.Log(currnetAmmo.text + " tolik ted mám");

        maxAmmo.text = magazineSize;

        //maxAmmo.ForceMeshUpdate();

        Debug.Log(maxAmmo.text + " tolik je max");

        displayWepponStats();

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
                    gunSwitchScript.onResumeGuns();
                    enableControls();
                    escMenuVisual.SetActive(false);
                    settingsMenuVisual.SetActive(false);
                    playerUI.SetActive(true);

                    Debug.Log("Odchazím z menu");


                } else {

                    isInMenu = true;
                    disableControls();
                    gunSwitchScript.onPauseGuns();
                    menuMessage.text = "Paused, just for you <3 Meow";
                    escMenuVisual.SetActive(true);
                    playerUI.SetActive(false);

                    Debug.Log("Ted jsem v menu");

                }
            }
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
    
    private void displayWepponStats(){

        maxAmmo.text = gunSwitchScript.maxAmmo().ToString();
        currnetAmmo.text = gunSwitchScript.currentAmmo().ToString();

    }


    private void displayCurrentAmmo(int gunSelected){

        int Ammo = gunSwitchScript.currentAmmo();

        if(ammo != Ammo){

            ammo = Ammo;

            currnetAmmo.text = ammo.ToString();

        }

    }

/*
    private void changeCatLook(int gunSelected){ // Ted není potřeba kočka není vidět

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

    }*/   

    public void backToGame(){

        if(isDeath){

            respawn();

        }
        else {

            isInMenu = false;
            gunSwitchScript.onResumeGuns();
            enableControls();
            escMenuVisual.SetActive(false);
            settingsMenuVisual.SetActive(false);
            playerUI.SetActive(true);

        }
    }

    public void startSettings(){

        kills = 0;
        killCountText.text = "0";

        disableControls();

        menuMessage.text = "Let's kill them !";

        isDeath = true;

        player.isKinematic = true;

        escMenuVisual.SetActive(true);
        playerObject.SetActive(false);

        playerUI.SetActive(false);

    }


    void StartCountdown()
    {            
        isCounting = true;
        countdownCoroutine = StartCoroutine(Countdown());
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
        if(countdownCoroutine != null){
            isCounting = false;
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
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

        deleteAllDamageTexts();

        ResetCountdown();

        explosionEffect.Play();
        dieSound.Play();

        writePlayerScore();

        if(kills > PlayerInfo.score && PlayerInfo.isLogged){

            menuMessage.color = new Color(1f, 0.847f, 0f);

            menuMessage.text = "New highscore: "+kills+"!!";

            PlayerInfo.score = kills;

            SaveScore();
        }

        kills = 0;
        killCountText.text = "0";

        gunSwitchScript.onPlayerDeathGuns();

        disableControls();

        isDeath = true;

        player.isKinematic = true;

        escMenuVisual.SetActive(true);
        playerObject.SetActive(false);

        playerUI.SetActive(false);

    } 

    private void writePlayerScore(){

        menuMessage.color = Color.white;

        menuMessage.text = "Your score : "+kills;

    }
    public void deleteAllDamageTexts()
    {
        // Získá všechny objekty s názvem "TextBox" ve scén

        GameObject[] textBoxCopies = GameObject.FindGameObjectsWithTag("destroyText");

        // Projde všechny získané objekty a odstraní kopie vzorového TextBoxu
        if(textBoxCopies != null){
            foreach (GameObject textBoxCopy in textBoxCopies)
            {
                Destroy(textBoxCopy);
            }
        }

    }
    private string serverURL = "https://mmm.9e.cz/TestPHP/";

    public void SaveScore()
    {
        DateTime date = DateTime.Now;

        DateOfScore = date.ToString();

        Debug.Log("Time : "+DateOfScore);

        PlayerInfo.dateOfScore = DateOfScore;

        Debug.Log(PlayerInfo.dateOfScore);

        StartCoroutine(SaveScoreRequest());
    }
    IEnumerator SaveScoreRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", PlayerInfo.playerName);
        form.AddField("score", PlayerInfo.score);
        form.AddField("gun", gunSwitchScript.gunOut());
        form.AddField("timeofscore", PlayerInfo.dateOfScore);

        using (UnityWebRequest www = UnityWebRequest.Post(serverURL + "savescoretest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if(www.downloadHandler.text == "ok"){

                    Debug.Log(www.downloadHandler.text);
                }
                else{
                    
                Debug.Log("Database conection error, please check your internet conection");

                }

            }
            else
            {
                Debug.Log("Database conection error, please check your internet conection");
            }
        }
    }
}