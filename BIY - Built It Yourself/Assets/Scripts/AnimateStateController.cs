using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateStateController : MonoBehaviour
{

    public Animator animator;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            animator.SetBool("iswalking", true);
        }
        else 
            animator.SetBool("iswalking", false);

    }
}
