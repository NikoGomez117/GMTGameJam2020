using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * 0.05f,ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(0.05f,0.1f),ForceMode2D.Impulse);

        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);

        transform.right = Random.insideUnitCircle;
    }

    public void OnPickup()
    {
        Homeworld.instance.Scrap += 1;
        Destroy(gameObject);
    }
}
