using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Animator anim;

    [SerializeField]
    float distance = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < distance)
        {
            anim.SetBool("open", true);
        }
        else
        {
            anim.SetBool("open", false);
        }
    }
}
