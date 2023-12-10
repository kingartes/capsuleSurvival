using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour, IHitable
{
    [SerializeField] private GameObject deathParticlesPrefab;
    [SerializeField] private float idleMaxTimer = 0.5f;
    [SerializeField] private List<Transform> patrolingRoute = new List<Transform>();
    [SerializeField] private BaseWeapon weapon;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float attackDistance = 15f;
    [SerializeField] private float startAttackTime = 1f;

    private Transform attackTarget;
    private int currentPatrolPointIndex = 0;

    private float idleTimer;
    private float startAttackTimer = 0;

    private EnemyState currentState;
    private IEnumerator shootingCoroutine;
    private bool isShooting;

    private enum EnemyState
    {
        Idle,
        Patroling,
        Attack
    }

    private Health health;

    private void Awake()
    {
        shootingCoroutine = weapon.Shoot();
        currentState = EnemyState.Idle;
        startAttackTimer = startAttackTime;
        health = GetComponent<Health>();
        health.OnHealthDropsZero += Health_OnHealthDropsZero;
    }

    private void Update()
    {
        if(!isShooting)
        {
            startAttackTimer += Time.deltaTime;
        }
        switch (currentState)
        {
            default:
            case EnemyState.Idle:
                idleTimer += Time.deltaTime;
                if (idleTimer > idleMaxTimer)
                {
                    currentState = EnemyState.Patroling;
                    idleTimer = 0;
                }
                break;
            case EnemyState.Patroling:
                Patrol();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    private void Patrol()
    {


        Transform patrolPoint = patrolingRoute[currentPatrolPointIndex];
        float distanceEps = 0.5f;

        float moveSpeed = 10f;
        if (Vector3.Distance(transform.position, patrolPoint.position) > distanceEps)
        {
            RotateToward(patrolPoint.position - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, patrolPoint.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolingRoute.Count;
        }
        if (TryFindTarget(out Transform target))
        {
            attackTarget = target;
            currentState = EnemyState.Attack;
        }

    }

    private void Attack()
    {

        Vector3 targetDirection = attackTarget.position - transform.position;
        RotateToward(targetDirection);


        if (Vector3.Angle(transform.forward, targetDirection) < 20f) {
            if (startAttackTimer >= startAttackTime)
            {
                StartShooting();
            }
        }
        if (!IsTargetInDistance())
        {
            StopShooting();
            startAttackTimer = 0;
            currentState = EnemyState.Idle;
        }
    }

    private void StartShooting()
    {
        if (!isShooting)
        {
            Debug.Log("Shoot");
            StartCoroutine(shootingCoroutine);
            isShooting = true;
        } 
    }

    private void StopShooting()
    {
        if (isShooting && shootingCoroutine != null)
        {
            Debug.Log("stop shooting");
            StopCoroutine(shootingCoroutine);
            isShooting = false;
        }
    }

    private bool IsTargetInDistance()
    {
        return Vector3.Distance(transform.position, attackTarget.position) < attackDistance;
    }

    private bool TryFindTarget(out Transform target)
    {
        target = null;
        List<Vector3> scanVectors = VectorUtils.GetVectorsInCone(transform.forward, 80f, 10);
        foreach (Vector3 scanDirection in scanVectors)
        {
            if (Physics.SphereCast(transform.position, 2f, scanDirection, out RaycastHit hitInfo, attackDistance, layerMask))
            {
                if (hitInfo.collider.TryGetComponent<Player>(out Player player))
                {
                    target = hitInfo.collider.transform;
                    break;
                }
            }
        }
        return target != null;
    }

    private void OnDestroy()
    {
        health.OnHealthDropsZero -= Health_OnHealthDropsZero;
    }

    private void Health_OnHealthDropsZero(object sender, System.EventArgs e)
    {
     
        Destroy(gameObject);
        PlayDeathParticlesEffect();
    }

    private void PlayDeathParticlesEffect()
    {
        GameObject deathParticles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        float destroyTime = deathParticles.GetComponent<ParticleSystem>().main.duration;
        Destroy(deathParticles, destroyTime);
    }

    private void RotateToward(Vector3 lookPoint)
    {
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, lookPoint, Time.deltaTime * rotationSpeed).normalized;
    }

    public void Hit(Bullet bullet)
    {
        health.TakeDamage(bullet.GetDamageValue());
    }
}
