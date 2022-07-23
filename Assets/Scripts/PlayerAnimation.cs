using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetParams(float x, float y)
    {
        anim.SetFloat("moveX", x);
        anim.SetFloat("moveY", y);
    }
}
