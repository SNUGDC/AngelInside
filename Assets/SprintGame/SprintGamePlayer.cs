using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SprintGamePlayer : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(0, 5);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SprintGameManager.Instance.GameOver();
        }
    }
}
