using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;



public class GruntScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject John;
    public int MaxHealth = 3;

    private float LastShoot;
    private int currentHealth;
    private void Start()
    {
        currentHealth = MaxHealth;
    }
    private void Update()
    {
        if (John == null) return;
        Vector3 direction = John.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(John.transform.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Debug.Log("Grunt disparo");
        Vector3 direction = (transform.localScale.x == 1.0f) ? Vector2.right : Vector2.left;
        
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction, gameObject);
    }
    public void Hit()
    {
        currentHealth--;
        Debug.Log(+currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Grunt ha muerto");
        Destroy(gameObject);
    }
}
