using System.Runtime.CompilerServices;
using UnityEngine;

public class ak_script : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float dispersion = 1f;
    public float impactForce = 30f;
    public float fireRate = 8f;
    public GameObject impactEffect;
    public ParticleSystem MuzzleFlash;
    public Camera fpsCam;
    private float nextTimeToFire = 0f;
    public AudioSource soundEffect;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot(); 
            soundEffect.Play();
        }


    }

    void Shoot (){
        RaycastHit hit;

        MuzzleFlash.Play();

        Vector3 dispersion_vector = new Vector3(Random.Range(0, dispersion), Random.Range(0, dispersion), Random.Range(0, dispersion));

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward + dispersion_vector, out hit, range)){

            Debug.Log(hit.transform.name);

            Test_Target target = hit.transform.GetComponent<Test_Target>();

            if(target != null){

                target.TakeDamage(damage);

            }


            if(hit.rigidbody != null){

                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 1f);

        }
    }
}
