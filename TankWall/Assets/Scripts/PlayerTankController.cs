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
        float currentSpeed = rb.velocity.x;

        // Если танк движется вправо
        if (currentSpeed > 0.1f)
        {
            // Поворачиваем вправо
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // Если танк движется влево
        else if (currentSpeed < -0.1f)
        {
            // Поворачиваем влево
            transform.rotation = Quaternion.Euler(0, 180, 0);
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