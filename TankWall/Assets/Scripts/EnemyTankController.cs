using UnityEngine;

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

        // Проверяем направление движения танка
        if (rb.velocity.x > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rb.velocity.x < -0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
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