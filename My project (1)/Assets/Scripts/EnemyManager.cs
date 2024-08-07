using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public int maxHp = 5;
    public int hp = 0;
    Animator animator;
    public Slider hpBar;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(hpBar)
        {
            hpBar.value = 1;
        }
        hp = maxHp;
    }

    public void OnDamage(int damage)
    {
        hp = (hp - damage);
        hpBar.value = (float)hp / (float)maxHp; ;
        animator.SetTrigger("IsDamaged");
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        hp = 0;
        animator.SetTrigger("IsDead");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OutAttackMode()
    {

    }
}
