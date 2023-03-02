using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{

    public float speed = 5f;
    private int damage = 35;
    public Rigidbody2D rb;
    public GameObject impact;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//获取子弹刚体组件
        rb.velocity = transform.right * speed;//移动
        Destroy(gameObject, 2);//2秒后销毁子弹
    }

    void OnTriggerEnter2D(Collider2D collision)//触碰到别的碰撞器的时候
    {
        EnemyControl enemy = collision.GetComponent<EnemyControl>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Instantiate(impact, transform.position, transform.rotation);
        
        Destroy(gameObject);//只要碰撞到碰撞体就摧毁子弹本身
    }
}