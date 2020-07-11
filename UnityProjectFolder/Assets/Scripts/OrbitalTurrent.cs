using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalTurrent : SubscribingMonoBehaviour
{
    [SerializeField]
    float dis = 2.5f;

    float orbitSpeed = 180f;
    float attackRange = 2f;
    float attackSpeed = 0.5f;

    float rotVel = 0f;
    float orbitTime = 0f;

    float rot = 0f;
    float targetRot = 0f;

    Vector2 prvPos;
    Vector2 nextPos;

    AlienSpaceship targetShip;

    IEnumerator shootingBehaviour = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        prvPos = transform.position.normalized * dis;
        nextPos = transform.position.normalized * dis;
    }

    // Selection / Movement
    private void Update()
    {
        UpdateMovement();
        Reposition();
        TargetEnemy();

        if (targetShip != null)
        {
            transform.right = (targetShip.transform.position - transform.position).normalized;
        }

        ShootingUpdate();
    }

    void UpdateMovement()
    {
        rot = Mathf.SmoothDamp(rot, targetRot, ref rotVel, orbitTime);
    }

    void Reposition()
    {
        transform.position = Vector3.RotateTowards(prvPos, nextPos, rot * Mathf.Deg2Rad, dis);
        transform.right = transform.position.normalized;
    }

    public void OnTarget(Vector2 pos)
    {
        rot = 0f;

        prvPos = transform.position.normalized * dis;
        nextPos = pos.normalized * dis;

        targetRot = Vector2.Angle(prvPos, nextPos);

        orbitTime = targetRot / orbitSpeed;
    }

    // Shooting
    void TargetEnemy()
    {
        if (targetShip != null && Vector2.Distance(transform.position, targetShip.transform.position) < attackRange && targetShip.gameObject.activeSelf)
            return;

        float minDis = 10f;
        targetShip = null;
        AlienSpaceship closestShip = null;

        foreach (AlienSpaceship ap in SpaceShipSpawner.allShips)
        {
            if (!ap.gameObject.activeSelf)
                continue;

            float apDis = Vector2.Distance(transform.position, ap.transform.position);

            if (apDis < minDis && apDis < attackRange)
            {
                minDis = apDis;
                closestShip = ap;
            }
        }

        targetShip = closestShip;
    }

    void ShootingUpdate()
    {
        if (shootingBehaviour == null)
        {
            if (targetShip != null)
            {
                shootingBehaviour = ShootRoutine();
                StartCoroutine(shootingBehaviour);
            }
        }
        else
        {
            if (targetShip == null)
            {
                StopCoroutine(shootingBehaviour);
                shootingBehaviour = null;
            }
        }
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f / attackSpeed);
            Fire();
        }
    }

    void Fire()
    {
        targetShip.Health -= 1;
        Debug.DrawRay(transform.position,targetShip.transform.position - transform.position,Color.red,1f);
    }
}
