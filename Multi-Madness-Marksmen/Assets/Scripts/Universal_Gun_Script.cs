using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universal_Gun_Script : MonoBehaviour
{
[Header("Settings")]
    public float normalDamage = 10f;
    public float headDemage = 30f;
    public float range = 100f;
    public float dispersion = 1f;
    public float aimDispersionMultiplier = 0.5f;
    public float impactForce = 30f;
    public float fireRate = 8f;
    public float reloadTime = 2f;
    public float magazineSize = 30f;
    public float recoilForce = 1.0f;
    public float recoilDuration = 0.1f;
    public bool fullAuto = true;
    
    [Header("Keybinds")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode fireMode = KeyCode.F;
    public KeyCode reloadKey = KeyCode.R;
    public KeyCode aimKey = KeyCode.Mouse1;

    [Header("Components")]

    public GameObject impactEffect;
    public ParticleSystem MuzzleFlash;
    public GameObject shootingDirection;
    public GameObject recoilScript;
    public AudioSource soundEffect;

    private float nextTimeToFire = 0f;

    private double reloadTimer = 0.0;

    private bool reloading = false;

    private float currentAmmo;

    static bool semiFire = true;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
        if(fullAuto){
            if(Input.GetKeyDown(fireMode)){
                semiFire = !semiFire;
                Debug.Log("semiFire is on "+semiFire);
            }
        }

        if(Input.GetKeyDown(reloadKey) && !reloading){

            Debug.Log("Reloading....");

            reloading = true;

            currentAmmo = magazineSize;

            reloadTimer = reloadTime;

        }

        if(Input.GetKeyDown(aimKey)){ // Hrac ztaměřil, zbran se mu dá k hlave a bude střílet pořesněji

            dispersion = dispersion * aimDispersionMultiplier;
            Debug.Log("Im Aiming now");
        }
        if(Input.GetKeyUp(aimKey)){

            dispersion = dispersion / aimDispersionMultiplier;
            Debug.Log("Im Aiming no more :/");
        }

        if(reloading){

            reloadTimer -= Time.deltaTime;

            if(reloadTimer <= 0){

                reloading = false;
                Debug.Log("Im done reloading");
            }

        }
        else // bylo by lepší přepsat aby se prvně ptalo na to jestli chceme stříoet až pak na to jkesrli jksou naboje OPTRIMALIZACEE
        {
            if(currentAmmo > 0){
                 /// Tento spoefdek optiflůaalmsdaskmdkoasdjokfdflaskhjnflksdhn optimalizovsat idk jak ted ale určo to jde
                 /// plsky oopravit díkyyyyy
                if(semiFire){
                    if (Input.GetKeyDown(shootKey) && Time.time >= nextTimeToFire){ //otázka jestli už možu vystzřelit je tlacitko a zarovne je cas :D
                        nextTimeToFire = Time.time + 1f/fireRate; // Zde jako moc se muže pufat, firerate je v výstřelech za s
                        Shoot(); //Zavolání fce Shooot

                        soundEffect.Play(); // Beng Beng saund zahrání
                        currentAmmo--;
                        Debug.Log(currentAmmo);
                    }
                } 
                else 
                {
                    if (Input.GetKey(shootKey) && Time.time >= nextTimeToFire){ //otázka jestli už možu vystzřelit je tlacitko a zarovne je cas :D
                        nextTimeToFire = Time.time + 1f/fireRate; // Zde jako moc se muže pufat, firerate je v výstřelech za s
                        Shoot(); //Zavolání fce Shooot

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



        Vector3 dispersion_vector = new Vector3(Random.Range(0, dispersion), Random.Range(0, dispersion), Random.Range(0, dispersion));

        if (Physics.Raycast(shootingDirection.transform.position, shootingDirection.transform.forward + dispersion_vector, out hit, range)){

            Healt_Controler target = hit.transform.GetComponent<Healt_Controler>();

            Rigidbody position = hit.transform.GetComponent<Rigidbody>();

            if(target != null){

                if(hit.rigidbody != null){

                hit.rigidbody.AddForce(-hit.normal * impactForce);

                float heightDiference = hit.point.y - position.transform.position.y; //zjištění jestli dostal čočku xd

                if (heightDiference > 0.5f) {

                    //Byla zasažena hlava
                    got_kill = target.TakeDamage(headDemage);
                    Debug.Log("Hlava" + headDemage);
                } else {
                    //Cokoliv jine na těle zasazene xd
                    got_kill = target.TakeDamage(normalDamage);
                    Debug.Log("Telo" + normalDamage);
                }
            }
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); // Efeeeekt particle na místo výstřelu
            Destroy(impactGO, 1f);

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

    
}
