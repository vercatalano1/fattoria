using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform swordPoint;
    [SerializeField] private GameObject[] sword;
    [SerializeField]private AudioClip swordSound;

    private Animator anim;
    private movement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<movement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && cooldownTimer > attackCooldown && playerMovement.canAttack()) 
            Attack();

        cooldownTimer += Time.deltaTime;

    }
   
    public void Attack()
    {
        SoundManager.instance.PlaySound(swordSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        sword[FindSword()].transform.position = swordPoint.position;
        sword[FindSword()].GetComponent<SwordPlayer>().SetDirection(Mathf.Sign(transform.localScale.x));
        
    }
    
    private int FindSword()
    {
       for(int i=0; i<sword.Length; i++)
        {
            if (!sword[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    
}
