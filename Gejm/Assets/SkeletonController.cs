using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float detectionRange = 5f;  // Expose the detection range in the Inspector

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
            behaviour.detectionRange = detectionRange;  // Assign the detection range
        }
    }
}
