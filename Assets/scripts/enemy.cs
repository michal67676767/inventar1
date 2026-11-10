using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 3f; 

    void Update()
    {
        if (player!= null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}