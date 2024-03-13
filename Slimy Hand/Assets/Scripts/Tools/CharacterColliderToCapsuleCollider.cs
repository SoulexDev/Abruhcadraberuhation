using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColliderToCapsuleCollider : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private CapsuleCollider capsuleCollider;
    private void FixedUpdate()
    {
        capsuleCollider.height = controller.height;
        capsuleCollider.radius = controller.radius;
        capsuleCollider.center = controller.center;
    }
}