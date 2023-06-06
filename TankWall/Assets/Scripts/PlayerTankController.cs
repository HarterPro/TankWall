using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankController : MonoBehaviour

{
    public float speed;
    public int playerLives = 3;
    public EnemyTankController enemyTankController;
    public bool gameOver = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Получаем текущую скорость танка по оси X
        float currentSpeedx = rb.velocity.x;
        float currentSpeedy = rb.velocity.y;

        // Если танк движется вправо
        if (currentSpeedx > 1f && currentSpeedy == 0)
        {
            // Поворачиваем вправо
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // Если танк движется влево
        else if (currentSpeedx < -1f && currentSpeedy == 0)
        {
            // Поворачиваем влево
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (currentSpeedx == 0 && currentSpeedy > 1f)
        {
            // Поворачиваем вверх
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (currentSpeedx == 0 && currentSpeedy < -1f)
        {
            // Поворачиваем вниз
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (currentSpeedx > 1f && currentSpeedy > 1f)
        {
            // Поворачиваем право-вверх
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (currentSpeedx > 1f && currentSpeedy < -1f)
        {
            // Поворачиваем право-низ
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        else if (currentSpeedx < -1f && currentSpeedy > 1f)
        {
            // Поворачиваем лево-вверх
            transform.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (currentSpeedx < -1f && currentSpeedy < -1f)
        {
            // Поворачиваем лево-низ
            transform.rotation = Quaternion.Euler(0, 0, -135);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * speed;
    }
    public void TakeLife()
    {
        playerLives--;
        if (playerLives <= 0)
        {
            // Если жизни закончились, выполните действия проигрыша или перезапустите игру
            gameOver = true; 
        }
    }
}