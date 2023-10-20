using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assault_Mechanic : MonoBehaviour
{
    public GameObject Gun;
    public GameObject bulletPrefab; // Objekt pro střely
    public Transform firePoint; // Místo, odkud bude střela vystřelována
    public float bulletSpeed = 100.0f; // Rychlost střely v m/s
    public float bulletDrag = 0.1f; // Množství zpomalení střely (Něco jako odpor vzduchu)
    public float gravity = -9.81f; // Hodnota gravitace 
    public int maxAmmo = 30; // Maximální počet nábojů
    private int AmmoCount; // Aktuální počet nábojů
    private float nextFireTime = 0f; // Čas pro další výstřel (potřeba pro funkci automatu)

    public float reloadCooldown = 2f; // Čas jak dlouho trvá než se bude moct zase přebíjet

    private double reloadTimer = 0.0; // Časovač pro přebíjení

    public int fireRate = 10; // Počet výstřelů za vteřinu

    private bool reloading = false; // proměná jeslti se zrovna přebíjí nebo ne

   


    public int dispersion = 100;

    public Transform gun;

    private void Start()
    {
        AmmoCount = maxAmmo; // Nastaví se max počet nábojů na aktuálního počtu nábojů
    }
    private System.Random random = new System.Random();

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Gun.GetComponent<Animator>().Play("Test_Gun_In");
        }

        if(Input.GetMouseButtonUp(1))
        {
            Gun.GetComponent<Animator>().Play("Test_Gun_Idle");
        }

        if (reloading) // Pokud zrovna přebíjí tak není možnost vystřelit
        {
            reloadTimer -= Time.deltaTime; // Snížení časovače během přebíjení

            if (reloadTimer <= 0)
            {
                reloading = false; // Konec přebíjení
            }
        } else if (Input.GetMouseButton(0) && Time.time > nextFireTime && AmmoCount > 0) // Pokud už se nepřebíjí tak se hned ptáme jestli nechcou vystřelit
        {
            Shoot(); // Zavolání fce pro výstřel
            nextFireTime = Time.time + 1f / fireRate; // vypočítání času pro další výstřel
            Debug.Log(AmmoCount); // vypsání do konzole pro kontrolu
        }

        if(Input.GetKeyDown("r") && !reloading){ // zmáčnutí r pro přebití
            {
                // Spustí se animace přebíjení
                // Nějaký Coldown pro střelbu aby se přebilo
                AmmoCount = maxAmmo; //Nastaví se maximální počet 
                reloading = true; //Nastaví stav přebíjení na aktivní aby se ve funkci ke střelbě, střelba pozastavila
                reloadTimer = reloadCooldown;
            }
        }

    }

    void Shoot()
    {
        //Rozptyl

        if(Input.GetKey(KeyCode.LeftControl)){ // Podmínka že když míří tak má menší rozptyl

        }

        int randomNumber = random.Next(1, 101); // Generuje náhodné číslo od 1 do 100
        Debug.Log(randomNumber);
        //firePoint.rotation.x







        // Vytvoření instance střely (projektilu)
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Nastavení rychlosti střely
        rb.velocity = firePoint.forward * bulletSpeed;

        // Nastavení zpomalení střely
        rb.drag = bulletDrag;

        // Přidání gravitačního efektu
        rb.useGravity = true;
        rb.AddForce(Vector3.up * gravity, ForceMode.Acceleration);

        // Odečtení jednoho náboje
        AmmoCount--;

        // Zničení střely po určité době (na jistotu, aby se nepoletěla nekonečně)
        Destroy(bullet, 5.0f);

        // Zde bude další kod který po zásahu nějakého objektu kulku zneškodní
    }
}
