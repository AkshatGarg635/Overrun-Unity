using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;

    void Start()
{
    Destroy(gameObject, lifeTime);
    GetComponent<Rigidbody>().useGravity = false;
}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                enemyRb.AddForce(transform.forward * 5f, ForceMode.Impulse);
            }

            GameManager.instance.EnemyKilled();

            Destroy(collision.gameObject, 0.1f);
        }
        Destroy(gameObject);
    }
}