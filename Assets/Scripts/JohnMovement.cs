using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class JohnMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;
    public int MaxHealth = 5;
    public TextMeshProUGUI vidaTextHUD;


    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private int currentHealth;
    private Transform checkpointObject;



    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        currentHealth = MaxHealth;
        vidaTextHUD = GameObject.Find("VidaText").GetComponent<TextMeshProUGUI>();
        vidaTextHUD.text = "Vida: " + MaxHealth + " / " + MaxHealth;
      
        

    }
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f);



        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction, gameObject);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(Horizontal, Rigidbody2D.linearVelocity.y);
    }

    public void Hit()
    {
        if (currentHealth <= 0) return;
        currentHealth--;
        Debug.Log("John Recibio daño. Vida restante:" + currentHealth);
        if (vidaTextHUD != null)
        {
            vidaTextHUD.text = "Vida: " + currentHealth + " / " + MaxHealth;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("John ha muerto");

        currentHealth = MaxHealth;
        if (vidaTextHUD != null)
            vidaTextHUD.text = "Vida: " + currentHealth + " / " + MaxHealth;

        if (checkpointObject != null)
        {
            transform.position = checkpointObject.position;
            Debug.Log("Respawn en checkpoint");
        }
        else
        {
            Debug.LogWarning("No hay checkpoint asignado.Reiniciando escena");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void UpdateCheckpoint(Transform checkpoint)
{
    checkpointObject = checkpoint;
    Debug.Log("Checkpoint asignado: " + checkpoint.position);
}



}
