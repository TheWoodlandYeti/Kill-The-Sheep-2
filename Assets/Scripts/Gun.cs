
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    // defining ramage and general range of the gun
    public float damage = 10f;
    public float criticalDamage = 100f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;

    public int maxAmmo = 10;
    public static int magSize;
    public static int maxMagSize = 10;

    [SerializeField]
    private int currentAmmo;

    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Animator animator;
    public Text ammoDisplay;
    public Text magDisplay;
    void Start()
    {
        currentAmmo = maxAmmo;
        magSize = maxMagSize;
        
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    // Update is called once per frame
    void Update()
    {
        // registers a keypress for reloading
        if (Input.GetKeyDown(KeyCode.R))
        {

            if (currentAmmo == maxAmmo)
            {
                return;
            }

            else
            {
                animator.SetBool("Reloading", true);
                StartCoroutine(Reload());
                return;
            }

        }
        ammoDisplay.text = currentAmmo.ToString();
        magDisplay.text = magSize.ToString();
        if (isReloading)
        {
            return;
        }

        // calls shoot function
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            // leads to time between shots dependiong on firerate
            nextTimeToFire = Time.time + 20f / fireRate;

            Shoot();
        }

        // instantiaing Reload Enumerator
        IEnumerator Reload()
        {
            isReloading = true;
            Debug.Log("Reloading...");
            yield return new WaitForSeconds(reloadTime - .25f);
            animator.SetBool("Reloading", false);

            if (magSize <= 0)
            {
                magSize = 0;
            }
            else
            {
                int ammoRemainder = maxAmmo - currentAmmo;
                if (magSize <= maxAmmo)
                {
                    currentAmmo = maxAmmo;
                }
                else
                {
                    currentAmmo += ammoRemainder;
                }

                magSize -= ammoRemainder;
                if (magSize <= 0)
                {
                    magSize = 0;
                }
            }
            isReloading = false;
        }   
        
    }
    //instanciating Shoot function for use when firing
    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Shoot");
        // returns current ammo to 0 if it overflows to negative integers
        if (currentAmmo <= 0)
        {
            currentAmmo = 0;
            return;
        }
        else
        {
            // reduces current ammo every iteration of shoot
            currentAmmo--;
            // Ray cast from sniper object
            RaycastHit hit;
            // defines a limit for if the camera determines if the ray hits an object
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
                // activates muzzleflash
                muzzleFlash.Play();
                // instantiates an impact effect to a quaternion relative to player transform
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
                // clarifies a hit in console
                Debug.Log(hit.collider.transform.name);
                // if a hit is registered on an object with enemy tag
                if (hit.collider.transform.tag == "Enemy")
                {
                    // grabs components from target script
                    Target target = hit.transform.GetComponent<Target>();
                    // uses TakeDamage and parses damage variable
                    target.TakeDamage(damage);
                }
                // if a hit is registered on an object with head tag
                else if (hit.collider.transform.tag == "Head")
                {
                    Target target = hit.transform.GetComponentInParent<Target>();
                    float test1 = damage * 10;
                    target.TakeDamage(test1);
                }

                else
                {
                    return;
                }
            }
        }
    }
}