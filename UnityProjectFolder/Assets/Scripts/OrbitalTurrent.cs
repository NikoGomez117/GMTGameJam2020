using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalTurrent : SubscribingMonoBehaviour
{
    [SerializeField]
    float dis = 2.5f;

    [SerializeField]
    AudioSource chargeSound;

    [SerializeField]
    AudioSource fireSound;

    [SerializeField]
    Transform turretHead;

    [SerializeField]
    Transform ammoGUI;

    float _ammo = 10;

    float Ammo
    {
        get
        {
            return _ammo;
        }
        set
        {
            _ammo = value;

            for (int i = 0; i < ammoGUI.childCount; i++)
            {
                if (i < _ammo - 1)
                {
                    ammoGUI.GetChild(i).gameObject.SetActive(true);
                    ammoGUI.GetChild(i).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0.09f);
                }
                else if (i < _ammo)
                {
                    ammoGUI.GetChild(i).gameObject.SetActive(true);
                    ammoGUI.GetChild(i).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0.09f * (_ammo % 1));
                }
                else
                {
                    ammoGUI.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }

    float orbitSpeed = 1080f;
    float attackRange = 2.75f;
    float attackSpeed = 0.5f;

    float startRotTime;

    // float rotVel = 0f;
    float orbitTime = 0f;

    float rot = 0f;
    float targetRot = 0f;

    Vector2 prvPos;
    Vector2 nextPos;

    AlienSpaceship targetShip;

    LineRenderer bolt;

    IEnumerator shootingBehaviour = null;

    private void Awake()
    {
        bolt = GetComponent<LineRenderer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        prvPos = transform.position.normalized * dis;
        nextPos = transform.position.normalized * dis;
    }

    // Selection / Movement
    private void Update()
    {
        if (Ammo > 0)
        {
            UpdateMovement();
            Reposition();
            TargetEnemy();

            if (targetShip != null)
            {
                transform.right = (targetShip.transform.position - transform.position).normalized;
            }
        }

        ShootingUpdate();
    }

    void UpdateMovement()
    {
        rot = Mathf.Lerp(0, targetRot, (Time.time - startRotTime) / orbitTime);
    }

    void Reposition()
    {
        transform.position = Vector3.RotateTowards(prvPos, nextPos, rot * Mathf.Deg2Rad, dis);

        if ((Vector2)transform.position != nextPos)
        {
            Ammo -= Time.deltaTime * Mathf.PI / 2f;
        }
        // transform.right = Vector3.RotateTowards(prvPos, nextPos, rot * Mathf.Deg2Rad, dis);
    }

    public void OnTarget(Vector2 pos)
    {
        rot = 0f;

        prvPos = transform.position.normalized * dis;
        nextPos = pos.normalized * dis;

        targetRot = Vector2.Angle(prvPos, nextPos);

        orbitTime = targetRot / orbitSpeed;
        orbitTime *= 2 * Mathf.PI * dis;

        startRotTime = Time.time;
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
            if (targetShip != null && Ammo > 0)
            {
                shootingBehaviour = ShootRoutine();
                StartCoroutine(shootingBehaviour);
            }
        }
        else
        {
            if ((targetShip == null || Ammo <= 0) && shootingBehaviour != null)
            {
                chargeSound.Stop();

                StopCoroutine(shootingBehaviour);
                shootingBehaviour = null;
            }
        }
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            chargeSound.Play();
            yield return new WaitForSeconds(0.5f / attackSpeed);
            Fire();
            StartCoroutine(AnimateBolt());
            yield return new WaitForSeconds(0.5f / attackSpeed);
        }
    }

    IEnumerator AnimateBolt()
    {
        bolt.SetPositions(
            new Vector3[]
            {
                    turretHead.localPosition,
                    Vector2.Distance(targetShip.transform.position,turretHead.position) * Vector2.right
            }
            );
        bolt.material.SetTextureOffset("_MainTex", Vector2.right);
        bolt.enabled = true;
        yield return new WaitForSeconds(0.05f / attackSpeed);
        bolt.material.SetTextureOffset("_MainTex", Vector2.zero);
        yield return new WaitForSeconds(0.05f / attackSpeed);
        bolt.enabled = false;
        yield return new WaitForSeconds(0.4f / attackSpeed);
    }

    void Fire()
    {
        fireSound.Play();

        Ammo -= 1;

        targetShip.Health -= 1;
        Debug.DrawRay(transform.position,targetShip.transform.position - transform.position,Color.red,1f);
    }
}
