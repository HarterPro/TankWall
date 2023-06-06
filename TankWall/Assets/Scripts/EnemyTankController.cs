using UnityEngine;
using System;

public class EnemyTankController : MonoBehaviour
{
    public float speed;
    public float shotCooldown;
    public GameObject bulletPrefab;
    public Transform GunBarrelEnd;
    public PlayerTankController playerTankController;

    private Rigidbody2D rb;
    private float nextShotTime;
    private GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextShotTime = 0.0f;
        player = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        Vector2 playerPos = player.transform.position;
        Vector2 tankPos = transform.position;
        Vector2 movement = (playerPos - tankPos).normalized;
        rb.velocity = movement * speed;

        if (Time.time >= nextShotTime)
        {
            UpdateShoot();
            nextShotTime = Time.time + shotCooldown;
        }

        float currentSpeedx = rb.velocity.x;
        float currentSpeedy = rb.velocity.y;


        if (currentSpeedx > 0.1f && Math.Abs(currentSpeedx) > Math.Abs(currentSpeedy) * 2)
        {
            // Поворачиваем вправо
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // Если танк движется влево
        else if (currentSpeedx < -0.1f && Math.Abs(currentSpeedx) > Math.Abs(currentSpeedy) * 2)
        {
            // Поворачиваем влево
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (currentSpeedy > 0.1f && Math.Abs(currentSpeedy) > Math.Abs(currentSpeedx) * 2)
        {
            // Поворачиваем вверх
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (currentSpeedy < -0.1f && Math.Abs(currentSpeedy) > Math.Abs(currentSpeedx) * 2)
        {
            // Поворачиваем вниз
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (currentSpeedx > 0.1f && currentSpeedy > 0.1f)
        {
            // Поворачиваем право-вверх
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (currentSpeedx > 0.1f && currentSpeedy < -0.1f)
        {
            // Поворачиваем право-низ
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        else if (currentSpeedx < -0.1f && currentSpeedy > 0.1f)
        {
            // Поворачиваем лево-вверх
            transform.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (currentSpeedx < -0.1f && currentSpeedy < -0.1f)
        {
            // Поворачиваем лево-низ
            transform.rotation = Quaternion.Euler(0, 0, -135);
        }
    }

    void UpdateShoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, GunBarrelEnd.position, transform.rotation);

        // Удалить снаряд через 5 секунд
        Destroy(bulletInstance, 5f);

        Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
        Vector2 force = (player.transform.position - GunBarrelEnd.position).normalized * 10.0f;
        bulletRb.AddForce(force, ForceMode2D.Impulse);
    }

    public void PlayerDamaged()
    {
        playerTankController.TakeLife();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && playerTankController != null)
        {
            playerTankController.TakeLife();
        }
    }
}