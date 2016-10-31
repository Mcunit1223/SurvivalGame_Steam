using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayNight : MonoBehaviour {
    public float DayLengthInMinutes;
    float speed;
    public List<Skybox> skyboxes;

    void Start()
    {
        speed = 1 / DayLengthInMinutes * 6;
    }

	void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }
}
