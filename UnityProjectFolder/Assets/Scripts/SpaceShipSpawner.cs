using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipSpawner : SubscribingMonoBehaviour
{
    public static AlienSpaceship[] allShips;

    Queue<AlienSpaceship> inactivePool;

    private void Awake()
    {
        allShips = transform.GetComponentsInChildren<AlienSpaceship>(true);
        inactivePool = new Queue<AlienSpaceship>(allShips);

        StartCoroutine(SpawnerUpdate());
    }

    protected override void Subscribe()
    {
        AlienSpaceship.alienDestroyed += Despawn;
        AlienSpaceship.alienInvaded += Despawn;
    }

    protected override void UnSubscribe()
    {
        AlienSpaceship.alienDestroyed -= Despawn;
        AlienSpaceship.alienInvaded -= Despawn;
    }

    IEnumerator SpawnerUpdate()
    {
        while (true)
        {
            if (inactivePool.Count > 0)
            {
                AlienSpaceship newShip = inactivePool.Dequeue();
                newShip.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(5f - Mathf.Log(Time.time,2) / 2);
        }
    }

    void Despawn(AlienSpaceship alienShip)
    {
        alienShip.gameObject.SetActive(false);
        inactivePool.Enqueue(alienShip);
    }
}
