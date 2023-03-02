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
        rb = GetComponent<Rigidbody2D>();//��ȡ�ӵ��������
        rb.velocity = transform.right * speed;//�ƶ�
        Destroy(gameObject, 2);//2��������ӵ�
    }

    void OnTriggerEnter2D(Collider2D collision)//�����������ײ����ʱ��
    {
        EnemyControl enemy = collision.GetComponent<EnemyControl>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Instantiate(impact, transform.position, transform.rotation);
        
        Destroy(gameObject);//ֻҪ��ײ����ײ��ʹݻ��ӵ�����
    }
}