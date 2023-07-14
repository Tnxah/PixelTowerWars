using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    public CharacterUnit characterUnit;
    public Animator animator;


    public float lastAttackTime;

    private void Start()
    {
        characterUnit = GetComponent<CharacterUnit>();
        animator = characterUnit.animator;
    }

    private void FixedUpdate()
    {
        animator.SetFloat("Run", Mathf.Abs(characterUnit.rb.velocity.x));
        animator.SetBool("Attack", characterUnit.isAttack);
    }

    
    public void Hit()
    {
        animator.SetTrigger("Hit");
    }

    public void Death()
    {
        animator.SetTrigger("Death");
    }
}
