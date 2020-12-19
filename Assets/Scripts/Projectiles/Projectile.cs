using AdventureTime.Interfaces;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float velocity;
    public float fireRate;
    public float activeTime;

    private void Awake()
    {
        Destroy(gameObject, activeTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        //if (collision.gameObject is IDamageable<float>)
        //{
        //    //check collision, if damageable do damage
        //}
        Destroy(this);
    }
}
