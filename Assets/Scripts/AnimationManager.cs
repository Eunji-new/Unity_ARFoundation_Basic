using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    public string[] AnimName = { "Attack", "Hit", "Dizzy", "Run", "Walk", "Recovery", "Attack2", "Defend" };
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(AnimName[Random.Range(0, 7)], true);
    }
}
