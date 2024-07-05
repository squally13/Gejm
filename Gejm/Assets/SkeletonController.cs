using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Find the StateMachineBehaviour and assign the references
        foreach (var behaviour in animator.GetBehaviours<SkeletonWalk>())
        {
            behaviour.groundCheck = groundCheck;
            behaviour.groundLayer = groundLayer;
        }
    }
}
