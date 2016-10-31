using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {
    public int health;
    Rigidbody rb;
    Vector3 force;
    public GameObject log;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        force = transform.forward * 1500;
    }

    IEnumerator DestroyTree()
    {
        // Push Tree Over
        rb.isKinematic = false;
        rb.AddForce(force);
        yield return new WaitForSeconds(7);

        //Spawn logs
        Vector3 spawnPos = transform.position;
        spawnLoot(log, 3, spawnPos);
        Destroy(gameObject);
    }

    void DestroyStone()
    {
        //Spawn Stones
        Vector3 spawnPos = transform.position;
        spawnLoot(log, 5, spawnPos);
        Destroy(gameObject);
    }

    public void hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (name.Contains("Stone"))
            {
                DestroyStone();
            }
            else
            {
                StartCoroutine(DestroyTree());
            }
        }

    }

    void spawnLoot(GameObject loot, int amount, Vector3 position)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(loot, new Vector3(position.x + i, position.y + i, position.z + i), Quaternion.identity);
        }
    }
}
