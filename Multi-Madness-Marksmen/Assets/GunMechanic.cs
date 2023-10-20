using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMechanic : MonoBehaviour
{
public GameObject bulletPrefab; // Prefab střely
    public Transform firePoint; // Místo, odkud bude střela vystřelována
    public float bulletSpeed = 100.0f; // Rychlost střely v m/s
    public float bulletDrag = 0.1f; // Množství zpomalení střely
    public float gravity = -9.81f; // Hodnota gravitace (upravte podle potřeby)
    public int maxAmmo = 30; // Maximální počet nábojů
    private int currentAmmo; // Aktuální počet nábojů
    private float nextFireTime = 0f;

    public int fireRate = 10;
    private void Start()
    {
        currentAmmo = maxAmmo; // Nastavíme maximální počet nábojů na začátku
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFireTime && currentAmmo > 0)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
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
        currentAmmo--;

        // Zničení střely po určité době (na jistotu, aby se nepoletěla nekonečně)
        Destroy(bullet, 5.0f);
    }
}
