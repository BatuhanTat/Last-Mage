using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Handler : MonoBehaviour
{
    [SerializeField] private Animator myAnimator;

    public void Animation_1_Idle()
    {
        if (gameObject.activeSelf == true)
        {
            myAnimator.SetBool("Run", false);
            // Debug.Log("The enemy " + gameObject.name + " is Idling");
        }
    }
    public void Animation_2_Run()
    {
        if (gameObject.activeSelf == true)
        {
            myAnimator.SetBool("Run", true);
            // Debug.Log("The enemy " + gameObject.name + " is Running");
        }
    }
    public void Animation_3_Hit()
    {
        if (gameObject.activeSelf == true)
        {
            myAnimator.SetBool("Run", false);
            myAnimator.SetTrigger("Hit");
            //  Debug.Log("The enemy " + gameObject.name + " is being Hit");
        }

    }
    public void Animation_4_Death()
    {
        if (gameObject.activeSelf == true)
        {
            myAnimator.SetBool("Run", false);
            myAnimator.SetTrigger("Death");
            // Debug.Log("The enemy " + gameObject.name + " has died");
        }
    }
}
