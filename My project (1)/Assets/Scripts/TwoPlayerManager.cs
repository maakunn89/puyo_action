using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoPlayerManager : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform attackPoint;
    public float attackRadius;
    [SerializeField] LayerMask enemyLayer;
    Rigidbody2D rb;
    Animator animator;
    OnePlayerManager onePlayerManager;
    GroundCheck ground;
    OnePlayerManager enemyScript;
    public bool isAttackMode = false;
    public bool isDamageMode;
    private bool isGround = true;
    public bool airJumped = false;
    public bool isDead = false;
    public int maxHp = 500;
    public int hp = 0;
    public float knockBackPower;
    public Slider hpBar;
    [SerializeField] GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ground = GetComponent<GroundCheck>();
        isDead = false;
        if(hpBar)
        {
            hpBar.value = 1;
        }
        hp = maxHp;
        enemy = GameObject.Find("OnePlayer");
        enemyScript = enemy.GetComponent<OnePlayerManager>();
        animator.SetTrigger("StartBattle");
    }

    // Update is called once per frame
    void Update()
    {
        if(ground){
            isGround = ground.IsGround();
        }
        // }else{
        //     Debug.Log("はずれ");
        // }
        if(isDead){
        }
        else if (!isAttackMode && !isDamageMode){
            if (isGround){
                if (Input.GetKeyDown(KeyCode.M)){
                    animator.SetTrigger("IsAttack");
                }
                else if (Input.GetKeyDown(KeyCode.N)){
                    animator.SetTrigger("IsAttack2");
                }
                else if (Input.GetKeyDown(KeyCode.F)){
                    Jump();
                }
                else{
                    Movement();
                }
            } else {
                if (Input.GetKeyDown(KeyCode.F) && !airJumped){
                    AirJump();
                }
                else{
                    MovementInAir();
                }
            }
        }
        else{
            if(isGround){
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    void FixedUpdate()
    {
        if(isDamageMode){
            //向きでノックバック方向を判断
            if (transform.localScale.x > 0)
                {
                    this.rb.AddForce(transform.right * enemyScript.knockBackPower, ForceMode2D.Impulse);
                }
            else
                {
                    this.rb.AddForce(transform.right * -enemyScript.knockBackPower, ForceMode2D.Impulse);
                }
        }
    }

    void Movement()
    {
        if(isDamageMode){
        }else{
            airJumped = false;
            float x = (Input.GetKey(KeyCode.C) ? -1 : Input.GetKey(KeyCode.B) ? 1 : 0);
            if (enemy.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1.0f);
                if (x > 0){
                    rb.velocity = new Vector2(x*moveSpeed, rb.velocity.y);
                }else{
                    rb.velocity = new Vector2(x*moveSpeed*0.75f, rb.velocity.y);
                }
            }
            if (enemy.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
                if (x < 0){
                    rb.velocity = new Vector2(x*moveSpeed, rb.velocity.y);
                }else{
                    rb.velocity = new Vector2(x*moveSpeed*0.75f, rb.velocity.y);
                }
            }
            animator.SetFloat("Speed",Mathf.Abs(x));
        }
    }

    void MovementInAir()
    {
        float x = (Input.GetKey(KeyCode.C) ? -1 : Input.GetKey(KeyCode.B) ? 1 : 0);
        if (enemy.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.0f);
        }
        if (enemy.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        }
        animator.SetFloat("Speed",Mathf.Abs(x));
        rb.velocity = new Vector2(x*moveSpeed*0.6f, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x*0.01f, 20);
    }

    void AirJump()
    {
        airJumped = true;
        rb.velocity = new Vector2(rb.velocity.x*0.01f, 16);
    }

    public void Attack1()
    {
        isAttackMode = true;
        knockBackPower = 17.5f;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);
        foreach (Collider2D hitEnemy in hitEnemies)
        {
            onePlayerManager = hitEnemy.GetComponent<OnePlayerManager>();
            if(onePlayerManager){
                onePlayerManager.OnDamage(10);
            }
        }
    }

    public void Attack2()
    {
        isAttackMode = true;
        knockBackPower = 20.0f;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);
        foreach (Collider2D hitEnemy in hitEnemies)
        {
            onePlayerManager = hitEnemy.GetComponent<OnePlayerManager>();
            if(onePlayerManager){
                onePlayerManager.OnDamage(20);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    void OutAttackMode()
    {
        isAttackMode = false;
    }

    void InAttackMode()
    {
        isAttackMode = true;
    }

    public void OnDamage(int damage)
    {
        animator.SetTrigger("IsDamaged");
        hp = (hp - damage);
        hpBar.value = (float)hp / (float)maxHp;
        if (hp <= 0)
        {
            Die();
        }
    }

    void inDamageMode(){
        isDamageMode = true;
    }

    void outDamageMode(){
        isDamageMode = false;
    }

    void Die()
    {
        hp = 0;
        animator.SetTrigger("IsDead");
        isDead = true;
        GameVariableStatic.finishedBattle = true;
        if (onePlayerManager.isDead == true) {
            GameVariableStatic.winner = "both";
        } else {
        GameVariableStatic.winner = "1P";
        }
    }

}
