using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashWhite : MonoBehaviour
{

    SpriteRenderer sr;
    Material matDefault;
    Material matFlashWhite;
    Material matDestination;
    float flashWhiteSpeed = 0.05f;
    [HideInInspector]
    public float flashWhiteDurration = .5f;
    //bool takingDamage = false;

    private void Start()
    {        
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
        matDestination = matDefault;
        matFlashWhite = Resources.Load("FlashWhite", typeof(Material)) as Material;
        //Invoke ("Test", .5f);
    }

    private void Test()
    {
        matDestination = matFlashWhite;        
        Invoke("Test2", .5f);
    }

    private void Test2()
    {
        matDestination = matDefault;        
        Invoke("Test", .5f);
    }

    public void FlashWhiteCalled()
    {
        StopAllCoroutines();
        StartCoroutine(FlashWhiteActive(matDefault, matFlashWhite, flashWhiteSpeed, flashWhiteDurration));
    }

    IEnumerator FlashWhiteActive(Material matDefault, Material matFlashWhite, float flashWhiteSpeed, float flashWhiteDurration)
    {
        float flashWhiteDurrationTimer = 0f;
        float flashWhiteSpeedTimer = flashWhiteSpeed;
        while (flashWhiteDurrationTimer <= flashWhiteDurration)
        {
            flashWhiteDurrationTimer += Time.deltaTime;
            flashWhiteSpeedTimer += Time.deltaTime;
            if (flashWhiteSpeedTimer >= flashWhiteSpeed)
            {
                if (matDestination == matDefault)
                    matDestination = matFlashWhite;
                else
                    matDestination = matDefault;

                flashWhiteSpeedTimer = 0f;
            }
            yield return null;
        }
        matDestination = matDefault;        
        StopCoroutine(FlashWhiteActive(matDefault, matFlashWhite, flashWhiteSpeed, flashWhiteDurration));
    }

    // // Use this if you don't want to use a coroutine.
    //void FlashWhite()
    //{
    //    flashWhiteDurrationTimer += Time.deltaTime;

    //    if (flashWhiteDurrationTimer <= flashWhiteDurration)
    //    {
    //        flashWhiteSpeedTimer += Time.deltaTime;

    //        if (flashWhiteSpeedTimer >= flashWhiteSpeed)
    //        {
    //            if (matDestination == matDefault)
    //            {
    //                matDestination = matFlashWhite;
    //            }

    //            else
    //            {
    //                matDestination = matDefault;
    //            }

    //            flashWhiteSpeedTimer = 0f;
    //            return;
    //        }
    //    }

    //    else if (flashWhiteDurrationTimer > flashWhiteDurration)
    //    {
    //        takingDamage = false;
    //        isInvincible = false;
    //        matDestination = matDefault;
    //        flashWhiteDurrationTimer = 0f;
    //        flashWhiteSpeedTimer = flashWhiteSpeed;            
    //    }               

    //}


    private void LateUpdate()
    {
        sr.material = matDestination;
    }
}
