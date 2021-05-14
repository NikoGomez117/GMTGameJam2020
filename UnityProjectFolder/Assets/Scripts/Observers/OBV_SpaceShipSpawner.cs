using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBV_SpaceShipSpawner : Observer
{
    public static ACT_AlienSpaceship[] allShips;

    public static bool spawning = true;

    [SerializeField]
    GameObject scrap;

    Queue<ACT_AlienSpaceship> inactivePool;

    float sceneStartTime;

    private void Awake()
    {
        sceneStartTime = Time.time;

        allShips = transform.GetComponentsInChildren<ACT_AlienSpaceship>(true);
        inactivePool = new Queue<ACT_AlienSpaceship>(allShips);

        StartCoroutine(SpawnerUpdate());
    }

    protected override void Subscribe()
    {
        ACT_AlienSpaceship.alienDestroyed += Despawn;
        ACT_AlienSpaceship.alienInvaded += Despawn;
    }

    protected override void UnSubscribe()
    {
        ACT_AlienSpaceship.alienDestroyed -= Despawn;
        ACT_AlienSpaceship.alienInvaded -= Despawn;
    }

    IEnumerator SpawnerUpdate()
    {
        while (spawning)
        {
            if (inactivePool.Count > 0)
            {
                ACT_AlienSpaceship newShip = inactivePool.Dequeue();
                newShip.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(Mathf.Max(6f - (OBV_LevelManager.instance.level / 3f) - ((Time.time - sceneStartTime) / 40f),0.1f));
        }
    }

    void Despawn(ACT_AlienSpaceship alienShip)
    {
        Instantiate(scrap, alienShip.transform.position, Quaternion.identity);
        alienShip.gameObject.SetActive(false);
        inactivePool.Enqueue(alienShip);
    }
}
