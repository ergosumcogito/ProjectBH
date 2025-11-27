using Core.Enemy_Logic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private float damage;
    private float critChance;
    private Vector3 direction;

    public void Init(Vector3 dir, float speed, float damage, float critChance)
    {
        this.direction = dir;
        this.speed = speed;
        this.damage = damage;
        this.critChance = critChance;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyAbstract enemy = collision.GetComponent<EnemyAbstract>();
        if (enemy == null) return;

        float finalDamage = damage;

        if (Random.value < critChance)
            finalDamage *= 2f;

        enemy.TakeDamage(finalDamage);
        Destroy(gameObject);
    }

}