using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("setings")]
    public float Speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) moveY += 1f;
            if (Keyboard.current.sKey.isPressed) moveY -= 1f;
            if (Keyboard.current.aKey.isPressed) moveX -= 1f;
            if (Keyboard.current.dKey.isPressed) moveX += 1f;
        }

        moveInput = new Vector2(moveX, moveY).normalized;

        Vector3 pozicia = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float AngleRad = Mathf.Atan2(pozicia.y - transform.position.y, pozicia.x - transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * Speed * Time.fixedDeltaTime);
    }

}