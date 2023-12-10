using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;

    private void Awake()
    {
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision);
        Destroy(gameObject);
    }

    protected virtual void OnCollision(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IHitable>(out IHitable hitTarget))
        {
            hitTarget.Hit(this);
        }
    }

    public void Init(float damage)
    {
        this.damage = damage;
    }

   public float GetDamageValue()
    {
        return damage;
    }
}
