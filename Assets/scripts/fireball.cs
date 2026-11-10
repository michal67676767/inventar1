using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    public GameObject fireballP;
    public float speed = 8f;
    public float delay = 1.8f;

    private float firespeed = 0f;
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame && Time.time >= firespeed)
        {
            firespeed = Time.time + delay;

            GameObject fireball = Instantiate(fireballP, transform.position, transform.rotation);

            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = transform.right * speed;
            }

            Destroy(fireball, 4f);
        }
    }
}