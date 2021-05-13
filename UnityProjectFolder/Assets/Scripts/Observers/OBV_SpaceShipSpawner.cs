using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBV_SpaceShipSpawner : SubscribingMonoBehaviour
{
    public static AlienSpaceship[] allShips;

    public static bool spawning = true;

    [SerializeField]
    GameObject scrap;

    Queue<AlienSpaceship> inactivePool;

    float sceneStartTime;

    private void Awake()
    {
        sceneStartTime = Time.time;

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
        while (spawning)
        {
            if (inactivePool.Count > 0)
            {
                AlienSpaceship newShip = inactivePool.Dequeue();
                newShip.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(Mathf.Max(6f - (OBV_LevelManager.instance.level / 3f) - ((Time.time - sceneStartTime) / 40f),0.1f));
        }
    }

    void Despawn(AlienSpaceship alienShip)
    {
        Instantiate(scrap, alienShip.transform.position, Quaternion.identity);
        alienShip.gameObject.SetActive(false);
        inactivePool.Enqueue(alienShip);
    }
}
