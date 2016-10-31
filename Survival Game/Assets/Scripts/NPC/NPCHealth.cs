using UnityEngine;
using System.Collections;

public class NPCHealth : MonoBehaviour {
    public float maxHealth;
    float currentHealth;
    GameObject rawMeat;

    void Start()
    {
        currentHealth = maxHealth;
        rawMeat = Resources.Load<GameObject>("Objects/Raw_Meat");
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            onDeath();
        }
    }

    public void hit(float damage)
    {
        currentHealth -= damage;
    }

    void onDeath()
    {
        Instantiate(rawMeat, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}
