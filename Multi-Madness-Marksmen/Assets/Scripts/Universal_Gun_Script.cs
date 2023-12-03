using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Universal_Gun_Script : MonoBehaviour
{
[Header("Settings")]
    public float normalDamageLow = 8f;
    public float normalDamageHigh = 16f;
    public float headDemageLow = 20f;
    public float headDemageHigh = 40f;
    public float range = 100f;
    public float normalDispersion = 0.5f;
    public float aimDispersion = 0.1f;
    public float impactForce = 30f;
    public float fireRate = 8f;
    public float reloadTime = 2f;
    public float magazineSize = 30f;
    //public float recoilForce = 1.0f;
    //public float recoilDuration = 0.1f;
    public bool fullAuto = true;
    public bool shootGun = false;
    public float shootgunRounds = 10f;
    public float standartFOV = 60f;
    public float aimFOV = 40f;
    public bool isEnabled = true;
    
    [Header("Keybinds")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode fireMode = KeyCode.F;
    public KeyCode reloadKey = KeyCode.R;
    public KeyCode aimKey = KeyCode.Mouse1;

    [Header("Components")]

    public TrailRenderer bulletTrail;
    public GameObject impactEffect;
    public ParticleSystem MuzzleFlash;
    public GameObject shootingDirection;
    public AudioSource soundEffect;
    public AudioSource hitSound;
    public Camera gunCam;
    Recoil_Script recoil;

    [Header("Animations")]
    //public Animation reloadAnim;
    public Animator reloadAnim;

    [Header("Aim Settings")]
    private Vector3 originalPos;
    public Vector3 aimPos;
    public float aimSpeed = 8f;
    private bool isAiming;
    public bool sniperScope;
    public GameObject scope;
    public GameObject sniperRifel;
    public Camera zoom;
    public Transform damageSpawnPoint;
    public GameObject damageText;
    public GameObject damageTextHead;
    public Canvas targetCanvas;
    public float positionThreshold;
    private float nextTimeToFire = 0f;

    private double reloadTimer = 0.0;

    private bool reloading = false;

    public float currentAmmo;

    private float dispersion;

    public int killCount = 0;
    static bool semiFire = true;

    private bool allreadyKilled = false;


    public float rotationSpeed = 180f; // Adjust this value to control the rotation speed

    private float targetRotation;   

    public GameObject wepponHolder;

    public TextMeshProUGUI noScopeText;
    public Transform spawnPointA;
    public Transform spawnPointB;
    public GameObject enemy;

    private void Aim(){

        if(Input.GetKey(aimKey) && !reloading){ // Hrac ztaměřil, zbran se mu dá k hlave a bude střílet pořesněji

            dispersion = aimDispersion;
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPos, Time.deltaTime * aimSpeed);
            isAiming = true;
            gunCam.fieldOfView = Mathf.Lerp(gunCam.fieldOfView, aimFOV, Time.deltaTime * aimSpeed);
            if(sniperScope && Vector3.Distance(transform.localPosition, aimPos) < positionThreshold){
                scope.SetActive(true);
                gunCam.nearClipPlane = 20f;
                //sniperRifel.SetActive(false);
                zoom.fieldOfView = Mathf.Lerp(gunCam.fieldOfView, aimFOV, Time.deltaTime * aimSpeed);
            }
        } else {

            dispersion = normalDispersion;
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * aimSpeed);
            isAiming = false;
            gunCam.fieldOfView = Mathf.Lerp(gunCam.fieldOfView, standartFOV, Time.deltaTime * aimSpeed);
            if(sniperScope && Vector3.Distance(transform.localPosition, aimPos) > positionThreshold){
                scope.SetActive(false);
                gunCam.nearClipPlane = 0.01f;
                //sniperRifel.SetActive(true);
                zoom.fieldOfView = Mathf.Lerp(gunCam.fieldOfView, standartFOV, Time.deltaTime * aimSpeed);
            }
        }

    }
    void Start()
    {
        gunCam.nearClipPlane = 0.01f;

        gunCam.fieldOfView = standartFOV;

        zoom.fieldOfView = standartFOV;

        currentAmmo = magazineSize;

        recoil = GetComponent<Recoil_Script>();

        targetRotation = transform.eulerAngles.y + 360f;

    }

    void Update()
    {
        if(isEnabled){
             Aim();

        if(Input.GetKeyDown(reloadKey) && !reloading){

            Debug.Log("Reloading....");

            reloadAnim.SetTrigger("ReloadTrigger");

            reloading = true;

            reloadTimer = reloadTime;

        }        

        if(reloading){

            reloadTimer -= Time.deltaTime;

            if(reloadTimer <= 0){

                reloading = false;
                currentAmmo = magazineSize;
                Debug.Log("Im done reloading");
            }

        }
        else // bylo by lepší přepsat aby se prvně ptalo na to jestli chceme stříoet až pak na to jkesrli jksou naboje OPTRIMALIZACEE
        {
            if(fullAuto){

                if(Input.GetKeyDown(fireMode)){
                    semiFire = !semiFire;
                    Debug.Log("semiFire is on "+semiFire);
                }

                //Zde ptání na střelbu při full auto na on
                if(semiFire){
                    if(Input.GetKeyDown(shootKey) && currentAmmo > 0 && Time.time >= nextTimeToFire){

                        nextTimeToFire = Time.time + 1f/fireRate; // Zde jako moc se muže pufat, firerate je v výstřelech za s
                        Shoot(); //Zavolání fce Shooot
                        allreadyKilled = false;
                        //Zde Recoil Script, ale ten nemám :DS/"SLDAS/§nkmůlasdf§sdfnklSYFNKLDX"!
                        recoil.ApplyRecoil(isAiming);

                        soundEffect.Play(); // Beng Beng saund zahrání
                        currentAmmo--;
                        Debug.Log(currentAmmo);
                    }
                } else {
                    if(Input.GetKey(shootKey) && currentAmmo > 0 && Time.time >= nextTimeToFire){

                        nextTimeToFire = Time.time + 1f/fireRate; // Zde jako moc se muže pufat, firerate je v výstřelech za s
                        Shoot(); //Zavolání fce Shooot
                        allreadyKilled = false;
                        //Zde Recoil Script, ale ten nemám :DS/"SLDAS/§nkmůlasdf§sdfnklSYFNKLDX"!
                        recoil.ApplyRecoil(isAiming);

                        soundEffect.Play(); // Beng Beng saund zahrání
                        currentAmmo--;
                        Debug.Log(currentAmmo);
                    }
                }

            } else if(shootGun){
                //Zde ptání na střelnu když je brokovnice mode
                    if(Input.GetKeyDown(shootKey) && currentAmmo > 0 && Time.time >= nextTimeToFire){

                        nextTimeToFire = Time.time + 1f/fireRate; // Zde jako moc se muže pufat, firerate je v výstřelech za s

                        for(int i = 0; i < shootgunRounds; i++){
                            Shoot(); //Zavolání fce Shooot
                        }
                        //Zde Recoil Script, ale ten nemám :DS/"SLDAS/§nkmůlasdf§sdfnklSYFNKLDX"!
                        allreadyKilled = false;
                        recoil.ApplyRecoil(isAiming);

                        soundEffect.Play(); // Beng Beng saund zahrání
                        currentAmmo--;
                        Debug.Log(currentAmmo);
                    }
            } else {
                if(Input.GetKeyDown(shootKey) && currentAmmo > 0 && Time.time >= nextTimeToFire){

                        nextTimeToFire = Time.time + 1f/fireRate; // Zde jako moc se muže pufat, firerate je v výstřelech za s
                        Shoot(); //Zavolání fce Shooot
                        allreadyKilled = false;
                        //Zde Recoil Script, ale ten už mám xdasdasdasdasdasdads :DS/"SLDAS/§nkmůlasdf§sdfnklSYFNKLDX"!
                        recoil.ApplyRecoil(isAiming);

                        soundEffect.Play(); // Beng Beng saund zahrání
                        currentAmmo--;
                        Debug.Log(currentAmmo);
                    }
            }
        }
        }
    }

    void Shoot (){
        RaycastHit hit;

        bool got_kill;

        MuzzleFlash.Play();

        int layer = 90; // Nějhkaé random číslo vrstvy jelikož jich mít tolik rozjofně nebudu tak to uděšlám takoto asi to jde i jinak ale ted idk casenm se muze opravit

        Vector3 dispersion_vector = new Vector3(UnityEngine.Random.Range(-dispersion, dispersion), UnityEngine.Random.Range(-dispersion, dispersion), UnityEngine.Random.Range(-dispersion, dispersion));

        if (Physics.Raycast(shootingDirection.transform.position, shootingDirection.transform.forward + dispersion_vector, out hit, range)){ // Or dispersion vectopr

            TrailRenderer trail = Instantiate(bulletTrail, shootingDirection.transform.forward + dispersion_vector, Quaternion.identity);

            Healt_Controler target = hit.transform.GetComponent<Healt_Controler>(); // Něco je zde špatně idk co
            // po hitu se nedá player controler, je to shit protože jak je kravina prázdný game object tak to dělá neplechu, zkus dát jako hlavní kravinu model toho hráče tomu dej PlayerControler atd, u toho modelu máš rovnou riugiutbody a capsule colider to by mohlo fachat a taky by to asi vyřešilo problém s sekavýám movementem ale to se uvidí, ok díky jí jdu spat gn xdddddd

            Healt_Controler_Enemy enemyTarget = hit.transform.GetComponent<Healt_Controler_Enemy>();

            Rigidbody position = hit.transform.GetComponent<Rigidbody>();

            Debug.Log(position);

            layer = hit.collider.gameObject.layer;

            StartCoroutine(SpawnTrail(trail, hit, layer));

            if(target != null || enemyTarget != null){//Enemytarget pak odstran ted to je jen na test

                //hit.rigidbody.AddForce(-hit.normal * impactForce);

                hitSound.Play();

                float heightDiference = hit.point.y - position.transform.position.y; //zjištění jestli dostal čočku xd

                if (heightDiference > 0.5f) {
                    //Byla zasažena hlava

                    float damage = UnityEngine.Random.Range(headDemageLow, headDemageHigh);

                    got_kill = enemyTarget.TakeDamage(damage); // pak přepiš na target
                    SpawnTextBox(Convert.ToInt32(damage), true);

                    Debug.Log("Hlava" + headDemageLow);
                } else {
                    //Cokoliv jine na těle zasazene xd

                    float damage = UnityEngine.Random.Range(normalDamageLow, normalDamageHigh);

                    got_kill = enemyTarget.TakeDamage(damage);//pak přepiš na target toto je test
                    SpawnTextBox(Convert.ToInt32(damage), false);
                    Debug.Log("Telo" + damage);
                }
                if(got_kill && !allreadyKilled){

                    killCount++;
                    allreadyKilled = true;

                    float a = UnityEngine.Random.Range(spawnPointA.position.x, spawnPointB.position.x);

                    float b = UnityEngine.Random.Range(spawnPointA.position.z, spawnPointB.position.z);

                    Instantiate(enemy, new Vector3(a,10,b), transform.rotation);

                    Debug.Log(killCount);
                    if(!isAiming && sniperScope){
                        Debug.Log("NO Scope");

                        SetText("!NOSCOPE!");

                        StartCoroutine(DisappearAfterDelay(1f));

                    }
                }
            } else {
                Debug.Log("Its not target");
            }
            GameObject impactGO;

            if(layer != 8){
                impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); // Efeeeekt particle na místo výstřelu
                Destroy(impactGO, 1f);
           }
            
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit, int layer)
    {

        float time = 0;

        Vector3 startPosition = shootingDirection.transform.position;

        while(time < 1){
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime * Time.time * 3f;

            yield return null;
        }

        Trail.transform.position = Hit.point;
        Destroy(Trail.gameObject, Trail.time);
    }
    void SpawnTextBox(int hitAmount, bool head)
    {
        GameObject textBoxInstance;
        // Vytvořte instanci textového boxu na zadaném místě
        if(head){

            textBoxInstance = Instantiate(damageTextHead, targetCanvas.transform);

        }
        else {

            textBoxInstance = Instantiate(damageText, targetCanvas.transform);

        }
        

        // Připojte textový obsah k textbox

        textBoxInstance.GetComponent<TextMeshProUGUI>().text = hitAmount.ToString();
        
        textBoxInstance.transform.position = damageSpawnPoint.position;

        // Spusťte proces pohybu a odstranění po určité době
        StartCoroutine(MoveAndDestroy(textBoxInstance));
    }

    IEnumerator MoveAndDestroy(GameObject textBox)
    {
        float duration = .5f; // Doba pohybu a trvání
        float startTime = Time.time;

        int textSpeed;

        while (Time.time - startTime < duration)
        {
            textSpeed = UnityEngine.Random.Range(70, 100);

            // Pohybujte textboxem doprava nahoru
            textBox.transform.Translate(Vector3.right * Time.deltaTime * textSpeed);
            textBox.transform.Translate(Vector3.up * Time.deltaTime * textSpeed);

            yield return null; // Počkejte na další frame
        }

        // Zničte textbox po dokončení pohybu
        Destroy(textBox);
    }
    void SetText(string text)
    {
        noScopeText.text = text;
    }
    IEnumerator DisappearAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Set text to empty string to make it disappear
        SetText("");
    }
}

/* // Pokus o recoil asi změním ted není čas, wepon swing better to do xd

    void Recoil (){

        float timer = 0f;

        while (timer < recoilDuration)
    {
        fpsCam.transform.Rotate(Vector3.up * recoilForce * Time.deltaTime);
        
        timer += Time.deltaTime;
        yield return null;
    }*/ 

    

