using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    // set animator var to use with Mechanim
    public Animator animator;
    // imports UI scope
    public GameObject scopeOverlay;
    // imports weapom camera for use
    public GameObject weaponCamera;
    // determines if gun(sniper generally) is scoped
    private bool isScoped = false;
    // import camera as var
    public Camera mainCamera;
    // defining normal and scoped FOV
    public float scopedFOV = 10f;
    private float normalFOV;
    private bool currentlyScoping = false;
    void Update()
    {
        // determines a right mouse button press to change animation frame
        if (!currentlyScoping)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                isScoped = !isScoped;
                animator.SetBool("Scoped", isScoped);

                // starts Coroutine that calls another function for scoping
                if (isScoped)
                    StartCoroutine(OnScoped());
                else
                    OnUnscoped();

            }
        }
        
            
    }
    // removes retecule
    void OnUnscoped()
    {   
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        // set FOV back to normal
        mainCamera.fieldOfView = normalFOV;
    }   
    // overlays retecule
    IEnumerator OnScoped()
    {
        currentlyScoping = true;
        // wait to overlay ret until Idle animation is over
        yield return new WaitForSeconds(.15f);

        currentlyScoping = false;
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        // set FOV lower to accomodate sniping
        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }
}
