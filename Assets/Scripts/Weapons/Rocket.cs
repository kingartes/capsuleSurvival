using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Rocket : Bullet
{
    [SerializeField] private float explosionRadius;

    protected override void OnCollision(Collision collision)
    {
        Collider[] collided = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider col in collided)
        {
            if( col.gameObject.TryGetComponent<IHitable>(out IHitable hitable))
            {
                hitable.Hit(this);
            }
        }
    }
}
