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

    // Компонент Rigidbody2D танка
    private Rigidbody2D rb;

    void Start()
    {
        // Получаем компонент Rigidbody2D танка
        rb = GetComponent<Rigidbody2D>();

        // Получаем компонент Text для отображения количества жизней
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();

        // Обновляем отображение количества жизней
        UpdateLifeDisplay();
    }

    private void Update()
    {
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
}