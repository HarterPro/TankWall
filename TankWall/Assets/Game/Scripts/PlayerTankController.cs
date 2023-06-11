using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerTankController : MonoBehaviour
{
    // �������� ����������� �����
    public float speed;

    // ���������� ������ � ������
    public int playerLives = 3;

    // ��������� ���� ��� ����������� ���������� ������
    public Text lifeText;

    // ���� ��� ��������
    public GameObject bulletPrefab;

    // �����, ������ �������� ����
    public Transform bulletSpawn;

    // ����� ����� ����������
    public float fireRate = 0.5f;

    private float nextFire = 0f;

    // ��������� Rigidbody2D �����
    private Rigidbody2D rb;

    private GameObject enemy;



    void Start()
    {
        // �������� ��������� Rigidbody2D �����
        rb = GetComponent<Rigidbody2D>();

        // �������� ��������� Text ��� ����������� ���������� ������
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();

        // ��������� ����������� ���������� ������
        UpdateLifeDisplay();

        enemy = GameObject.FindWithTag("Enemy");
    }

    private void Update()
    {
        // �������� ��� ������� �� ������
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }

        // �������� ������� �������� ����� �� ���� X � Y
        float currentSpeedx = rb.velocity.x;
        float currentSpeedy = rb.velocity.y;

        // ������������ ���� � ����������� �� ����������� ��������
        if (currentSpeedx > 1f && currentSpeedy == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (currentSpeedx < -1f && currentSpeedy == 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (currentSpeedx == 0 && currentSpeedy > 1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (currentSpeedx == 0 && currentSpeedy < -1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (currentSpeedx > 1f && currentSpeedy > 1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (currentSpeedx > 1f && currentSpeedy < -1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        else if (currentSpeedx < -1f && currentSpeedy > 1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (currentSpeedx < -1f && currentSpeedy < -1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, -135);
        }
    }

    void FixedUpdate()
    {
        // �������� �������� ���� Horizontal � Vertical ��� �������� �����
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ������� ������ ��������
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // ������ �������� �����
        rb.velocity = movement * speed;
    }

    public void TakeLife()
    {
        // ��������� ���������� ������ �� 1
        playerLives--;

        // ���� ������ �� �������� - ������� ��������� "GAME OVER!"
        if (playerLives <= 0)
        {
            // ��������� 
        }

        // ��������� ����������� ���������� ������
        UpdateLifeDisplay();
    }

    void UpdateLifeDisplay()
    {
        // ��������� ��������� ���� ��� ����������� ���������� ������
        lifeText.text = "Lives: " + playerLives.ToString();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ���� ���������� � ����� - ������ ���� �����
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeLife();

            Destroy(collision.gameObject);
        }
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        
        // ������� ������ ����� 5 ������
        Destroy(bulletInstance, 5f);

        Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
        Vector2 force = (enemy.transform.position - bulletSpawn.position).normalized * 10.0f;
        bulletRb.AddForce(force, ForceMode2D.Impulse);
    }
}