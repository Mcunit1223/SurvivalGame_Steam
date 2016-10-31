using UnityEngine;
using System.Collections;

public class FriendlyAnimal : MonoBehaviour {
    Animator anim;
    GameObject player;
    public float maxDistance = 1;
    public float speed = 3;
    public float rotationSpeed = 5;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < maxDistance)
        {
            anim.SetBool("isRunning", true);
            Move();
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
           Quaternion.LookRotation(-player.transform.position - transform.position), rotationSpeed * Time.deltaTime);
            transform.position -= transform.forward * speed * Time.deltaTime;
    }
}
