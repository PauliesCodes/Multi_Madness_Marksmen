using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
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
    public bool shootGun = false;
    public float shootgunRounds = 10f;
    
    [Header("Keybinds")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode fireMode = KeyCode.F;
    public KeyCode reloadKey = KeyCode.R;
    public KeyCode aimKey = KeyCode.Mouse1;

    [Header("Components")]

    public GameObject impactEffect;
    public GameObject playerHitEffect;
    public ParticleSystem MuzzleFlash;
    public GameObject shootingDirection;
    public AudioSource soundEffect;
    public Camera gunCam;
    Recoil_Script recoil;

    [Header("Animations")]
    //public Animation reloadAnim;

    [Header("Aim Settings")]
    private Vector3 originalPos;
    public Vector3 aimPos;
    public float aimSpeed = 8f;
    private bool isAiming;

    private float nextTimeToFire = 0f;

    private double reloadTimer = 0.0;

    private bool reloading = false;

    private float currentAmmo;


    private int killCount = 0;
    static bool semiFire = true;

    private void Aim(){

        if(Input.GetKey(aimKey) && !reloading){ // Hrac ztaměřil, zbran se mu dá k hlave a bude střílet pořesněji

        //dispersion = dispersion * aimDispersionMultiplier;
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPos, Time.deltaTime * aimSpeed);
            isAiming = true;
            gunCam.fieldOfView = Mathf.Lerp(gunCam.fieldOfView, 40, Time.deltaTime * aimSpeed);
        } else {

            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * aimSpeed);
            isAiming = false;
            gunCam.fieldOfView = Mathf.Lerp(gunCam.fieldOfView, 60, Time.deltaTime * aimSpeed);
        }

    }
    void Start()
    {
        currentAmmo = magazineSize;

        recoil = GetComponent<Recoil_Script>();
    }

    void Update()
    {
        Aim();

        if(Input.GetKeyDown(reloadKey) && !reloading){

            Debug.Log("Reloading....");

            //reloadAnim.Play();

            reloading = true;

            currentAmmo = magazineSize;

            reloadTimer = reloadTime;

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
                        recoil.ApplyRecoil(isAiming);

                        soundEffect.Play(); // Beng Beng saund zahrání
                        currentAmmo--;
                        Debug.Log(currentAmmo);
                    }
            }
        }
    }

    void Shoot (){
        RaycastHit hit;

        bool got_kill;

        MuzzleFlash.Play();

        int layer = 90; // Nějhkaé random číslo vrstvy jelikož jich mít tolik rozjofně nebudu tak to uděšlám takoto asi to jde i jinak ale ted idk casenm se muze opravit

        Vector3 dispersion_vector = new Vector3(Random.Range(0, dispersion), Random.Range(0, dispersion), Random.Range(0, dispersion));

        if (Physics.Raycast(shootingDirection.transform.position, shootingDirection.transform.forward + dispersion_vector, out hit, range)){ // Or dispersion vectopr

            Healt_Controler target = hit.transform.GetComponent<Healt_Controler>();

            Rigidbody position = hit.transform.GetComponent<Rigidbody>();

            if(target != null){

                if(hit.rigidbody != null){

                    layer = hit.collider.gameObject.layer;

                //hit.rigidbody.AddForce(-hit.normal * impactForce);

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
                if(got_kill){

                    killCount++;
                    Debug.Log(killCount);
                }
            }
            }

            GameObject impactGO;

            if(layer == 8){
                impactGO = Instantiate(playerHitEffect, hit.point, Quaternion.LookRotation(hit.normal)); // Efeeeekt particle na místo výstřelu
            } else {
                impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); // Efeeeekt particle na místo výstřelu
            }
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
