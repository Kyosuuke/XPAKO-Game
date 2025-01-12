using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int lifeTime = 500;
    private int tmp;

    void Update ()
    {
        if (tmp == lifeTime)
        {
            Destroy(gameObject);
        } else {
            tmp++;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject parent = GameObject.Find("Keyboard Key");
            gameObject.transform.SetParent(parent.transform);
            Destroy(gameObject);
        } else {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
        }
    }
}
