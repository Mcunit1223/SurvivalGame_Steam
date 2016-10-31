using UnityEngine;
using System.Collections;

public class BerryBush : MonoBehaviour {
    public GameObject[] berries;
    public float resetTimeInMinutes = 0.05f;
    float currentTimer;
    int berryIndex;
    public bool berriesGone;


    void Start()
    {
        resetTimeInMinutes *= 60;
        berries = new GameObject[5];
        berries[0] = Resources.Load<GameObject>("Objects/Yellow_Berries");
        berries[1] = Resources.Load<GameObject>("Objects/Blue_Berries");
        berries[2] = Resources.Load<GameObject>("Objects/Green_Berries");
        berries[3] = Resources.Load<GameObject>("Objects/Red_Berries");
        berries[4] = Resources.Load<GameObject>("Objects/White_Berries");

        berryIndex = Random.Range(0, 4);

        spawnBerries();
    }

    void Update()
    {
        if (berriesGone)
        {
            if (currentTimer < resetTimeInMinutes)
            {
                currentTimer += Time.deltaTime;
            }
            else
            {
                spawnBerries();
            }
        }
    }

    void spawnBerries()
    {
        GameObject berry = Instantiate(berries[berryIndex]);
        berry.transform.SetParent(transform);
        berry.transform.localPosition = Vector3.zero;
        berriesGone = false;
        currentTimer = 0;
    }
}
