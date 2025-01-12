using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private GameObject player;

    public int maxHealth;
    public int Armor;
    public int level;
    public int BulletSpeed;
    public int CoolDown;
    public int currentHealth;
    public HealthBar healthBar;

    Renderer rend;
    Color c;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        player = GameObject.Find("Player");
        rend = GetComponent<Renderer>();
        c = rend.material.color;
    }

    void OnCollisionEnter2D(Collision2D collison)
    {
        if (collison.collider.CompareTag("Enemy") && currentHealth > 0)
        {
            StartCoroutine("GetInvulnerable");
        }

        if (collison.collider.CompareTag("Boss") && currentHealth > 0)
        {
            StartCoroutine("GetInvulnerable");
        }

        if (collison.collider.CompareTag("BulletEnemy") && currentHealth > 0)
        {
            StartCoroutine("GetInvulnerable");
        }

    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("BulletEnemy"))
        {
            StartCoroutine("GetInvulnerable");
        }
    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        currentHealth = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    IEnumerator GetInvulnerable()
    {
        Physics2D.IgnoreLayerCollision (6, 7, true);
        c.a = 0.5f;
        rend.material.color = c;
        yield return new WaitForSeconds (1f);
        Physics2D.IgnoreLayerCollision (6, 7, false);
        c.a = 1f;
        rend.material.color = c;
    }
}
