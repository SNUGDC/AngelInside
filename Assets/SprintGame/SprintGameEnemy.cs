using UnityEngine;

public class SprintGameEnemy : MonoBehaviour
{
    public static float speed = 5f;

    private void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;

        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
}
