using UnityEngine;
using System.Collections;

public class AxeAnimator : MonoBehaviour {
    // Animator
    Animator anim;

    // Audio
    AudioSource audioSource;
    public AudioClip hitWood;
    public AudioClip swingSound;

    // Swing Stats
    public float coolDown;
    float timer;
    public float swingDistance;
    public float damage;

    // Booleans
    bool canSwing = true;

    // Inventory
    Inventory inventory;

	void Start () {
        anim = gameObject.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
	}

	void Update () {
        CharacterController cc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();

        // Set Movement Speed
        float speed = cc.velocity.magnitude * 2;
        if(speed < 1)
        {
            anim.SetFloat("speed", 1);
        }
        else
        {
            anim.SetFloat("speed", speed);
        }
        timer += Time.deltaTime;
        if (timer > coolDown)
        {
            canSwing = true;
        }
        else
        {
            canSwing = false;
        }
        // Swing
        if (Input.GetButtonDown("Fire1"))
        {
            // Swing
            if (canSwing)
            {
                // Play Sound
                if (name == "Starter_Pickaxe")
                {
                    StartCoroutine(playSwingSound(0.75f));
                }
                else
                {
                    StartCoroutine(playSwingSound(0.5f));
                }

                anim.Play("Swing");
                timer = 0;

                // Check for tree hit
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, swingDistance))
                {
                    if (hitInfo.collider.gameObject.GetComponent<Tree>())
                    {
                        if (GameObject.Find("Player").GetComponent<PickUpItem>().getCorrectName(hitInfo.collider.name) == "Stone_NonGrabbable" && gameObject.name != "Starter_Pickaxe")
                        {
                            return;
                        }
                        StartCoroutine(hitTree(hitInfo));
                    }
                    else if (hitInfo.transform.tag == "NPC")
                    {
                        StartCoroutine(hitNPC(hitInfo));
                    }
                }
            }
        }
	}

    IEnumerator playSwingSound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        audioSource.clip = swingSound;
        audioSource.Play();
    }

    IEnumerator hitTree(RaycastHit hitInfo)
    {
        if (name == "Starter_Pickaxe")
        {
            yield return new WaitForSeconds(0.88f);
        }
        else
        {
            yield return new WaitForSeconds(0.7f);
        }
        audioSource.clip = hitWood;
        audioSource.Play();
        GameObject tree = hitInfo.transform.gameObject;
        tree.GetComponent<Tree>().hit(1);
    }

    IEnumerator hitNPC(RaycastHit hitInfo)
    {
        yield return new WaitForSeconds(0.7f);
        NPCHealth health = hitInfo.transform.GetComponent<NPCHealth>();
        health.hit(damage);
    }

    void hitStone()
    {

    }
}
