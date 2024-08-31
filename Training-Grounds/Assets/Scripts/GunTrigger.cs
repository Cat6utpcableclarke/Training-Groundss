using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTrigger : MonoBehaviour
{
    // Reference Object
    private Animator animations;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public ParticleSystem flash;

    public Vector3 moveDirection = Vector3.zero; 
    public float bulletSpeed = 10f;
    public float fireRate = 0.1f; 

    public float reloadDuration = 1.0f;
    private float nextFireTime = 0f;
    private float reloadTimeRemaining = 0.0f;
    private bool isReloading = false;

    private void Start()
    {
        // Access to the animations controller
        animations = gameObject.GetComponent<Animator>();
        animations.SetInteger("Movement", 0);
    }

    void Update()
    {
        HandleShooting();
        HandleReloading();
        GunPerspectiveMovements();
    }

    private void HandleShooting()
    {
        // if left click is on bold by user
        if (Input.GetMouseButton(0)) 
        {
            // if enough time has passed to fire again
            if (Time.time > nextFireTime)
            {
                // Instantiate the bullet (Fire)
                var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;

                // Particle Effect
                flash.Play();

                // Set the time for the next shot
                nextFireTime = Time.time + fireRate;

                animations.SetInteger("Fire", 2);
                animations.SetInteger("Movement", -1);
            }
        }
        else
        {
            animations.SetInteger("Fire", -1);
        }
    }

    private void HandleReloading()
    {
        bool Reload = Input.GetKey(KeyCode.R);

        if (Reload)
        {
            if (!isReloading)
            {
                // Start the reload process
                isReloading = true;
                reloadTimeRemaining = reloadDuration;
                animations.SetInteger("Reload", 0);
            }
        }

        if (isReloading)
        {
            reloadTimeRemaining -= Time.deltaTime;
            print(reloadTimeRemaining);
            if (reloadTimeRemaining <= 0.0f)
            {
                // Reload complete
                animations.SetInteger("Reload", -1);
                isReloading = false;
            }
        }
    }

    private void GunPerspectiveMovements()
    {
        bool idenMovement = Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0;
        bool Run = Input.GetKey(KeyCode.LeftShift);

        if (Run)
        {
            animations.SetInteger("Movement", 2);
        }
        else
        {
            animations.SetInteger("Movement", (idenMovement ? 0 : 1));
        }

        bool Scope = Input.GetKey(KeyCode.Mouse1);

        if (Scope)
        {
            animations.SetBool("Sight", true);
        }
        else
        {
            animations.SetBool("Sight", false);
        }
    }
}
