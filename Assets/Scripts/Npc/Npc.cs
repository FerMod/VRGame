using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Npc : NpcBase
{
    public Animator animator;

    new void Start()
    {
        base.Start();

        animator = GetComponent<Animator>();
        animator.SetFloat("IdleBlend", Random.Range(0, 5));
        animator.SetFloat("WalkBlend", Random.Range(0, 2));
    }

    new void Update()
    {
        base.Update();

        animator.SetFloat("Speed", Agent.velocity.magnitude);
    }
}
