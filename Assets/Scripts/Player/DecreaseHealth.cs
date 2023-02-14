using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseHealth : MonoBehaviour
{
    [SerializeField] PlayerController playerScript;

    Rigidbody2D myRigidbody;
    SpriteFlash flashEffect;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        flashEffect = GetComponent<SpriteFlash>();
    }

    // Timer for decreasing health on continuous touch with an enemy. 
    float timer = 0.0f;
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            flashEffect.Flash();
            timer += Time.deltaTime;
            // Reduce health on per 0.2f.
            if (timer > 0.2f)
            {
                //Debug.Log("Inside timer");
                playerScript.DecreaseHealth();
                timer = 0.0f;
            }
        }
    }

}
