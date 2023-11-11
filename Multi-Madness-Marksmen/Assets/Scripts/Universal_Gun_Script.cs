using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Universal_Gun_Script : MonoBehaviour
{
[Header("Settings")]
    public float normalDamage = 10f;
    public float headDemage = 30f;
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
    
    [Header("Keybinds")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode fireMode = KeyCode.F;
    public KeyCode reloadKey = KeyCode.R;
    public KeyCode aimKey = KeyCode.Mouse1;

    [Header("Components")]

    public TrailRenderer bulletTrail;
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

    public float currentAmmo;

    private float dispersion;

    public int killCount = 0;
    public bool wasAiming = false;
    static bool semiFire = true;

    private bool allreadyKilled = false;

    private void Aim(){

        if(Input.GetKey(aimKey) && !reloading){ // Hrac ztaměřil, zbran se mu dá k hlave a bude střílet pořesněji

            dispersion = aimDispersion;
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPos, Time.deltaTime * aimSpeed);
            isAiming = true;
            gunCam.fieldOfView = Mathf.Lerp(gunCam.fieldOfView, aimFOV, Time.deltaTime * aimSpeed);

        } else {

            dispersion = normalDispersion;
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * aimSpeed);
            isAiming = false;
            wasAiming = false;
            gunCam.fieldOfView = Mathf.Lerp(gunCam.fieldOfView, standartFOV, Time.deltaTime * aimSpeed);

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

    void Shoot (){
        RaycastHit hit;

        bool got_kill;

        MuzzleFlash.Play();

        int layer = 90; // Nějhkaé random číslo vrstvy jelikož jich mít tolik rozjofně nebudu tak to uděšlám takoto asi to jde i jinak ale ted idk casenm se muze opravit

        Vector3 dispersion_vector = new Vector3(Random.Range(-dispersion, dispersion), Random.Range(-dispersion, dispersion), Random.Range(-dispersion, dispersion));

        if (Physics.Raycast(shootingDirection.transform.position, shootingDirection.transform.forward + dispersion_vector, out hit, range)){ // Or dispersion vectopr

            TrailRenderer trail = Instantiate(bulletTrail, shootingDirection.transform.forward + dispersion_vector, Quaternion.identity);

            Healt_Controler target = hit.transform.GetComponent<Healt_Controler>();

            Rigidbody position = hit.transform.GetComponent<Rigidbody>();

            layer = hit.collider.gameObject.layer;

            StartCoroutine(SpawnTrail(trail, hit, layer));

            if(target != null){

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
                if(got_kill && !allreadyKilled){

                    killCount++;
                    allreadyKilled = true;
                    Debug.Log(killCount);
                    if(isAiming){
                        wasAiming = true;
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

    

