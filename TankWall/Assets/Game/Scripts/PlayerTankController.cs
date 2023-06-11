using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerTankController : MonoBehaviour
{
    // Скорость перемещения танка
    public float speed;

    // Количество жизней у игрока
    public int playerLives = 3;

    // Текстовое поле для отображения количества жизней
    public Text lifeText;

    // Пуля для стрельбы
    public GameObject bulletPrefab;

    // Место, откуда вылетают пули
    public Transform bulletSpawn;

    // Время между выстрелами
    public float fireRate = 0.5f;

    private float nextFire = 0f;

    // Компонент Rigidbody2D танка
    private Rigidbody2D rb;

    private GameObject enemy;



    void Start()
    {
        // Получаем компонент Rigidbody2D танка
        rb = GetComponent<Rigidbody2D>();

        // Получаем компонент Text для отображения количества жизней
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();

        // Обновляем отображение количества жизней
        UpdateLifeDisplay();

        enemy = GameObject.FindWithTag("Enemy");
    }

    private void Update()
    {
        // Стрельба при нажатии на пробел
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }

        // Получаем текущую скорость танка по осям X и Y
        float currentSpeedx = rb.velocity.x;
        float currentSpeedy = rb.velocity.y;

        // Поворачиваем танк в зависимости от направления движения
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
        // Получаем значения осей Horizontal и Vertical для движения танка
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Создаем вектор движения
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Задаем скорость танка
        rb.velocity = movement * speed;
    }

    public void TakeLife()
    {
        // Уменьшаем количество жизней на 1
        playerLives--;

        // Если жизней не осталось - выводим сообщение "GAME OVER!"
        if (playerLives <= 0)
        {
            // Проигрышь 
        }

        // Обновляем отображение количества жизней
        UpdateLifeDisplay();
    }

    void UpdateLifeDisplay()
    {
        // Обновляем текстовое поле для отображения количества жизней
        lifeText.text = "Lives: " + playerLives.ToString();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Если танк столкнулся с пулей - теряем одну жизнь
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeLife();

            Destroy(collision.gameObject);
        }
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        
        // Удалить снаряд через 5 секунд
        Destroy(bulletInstance, 5f);

        Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
        Vector2 force = (enemy.transform.position - bulletSpawn.position).normalized * 10.0f;
        bulletRb.AddForce(force, ForceMode2D.Impulse);
    }
}