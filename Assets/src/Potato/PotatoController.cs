using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public float localYPos = 5;
    public static int TotalPotatoes = 0;
    public float hoverCollectTime = 1.5f;
    public bool isCollected = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCollected)
        {
            var newPos = new Vector3(transform.localPosition.x, localYPos + animator.GetFloat("Yoff"), transform.localPosition.z);
            transform.localPosition = newPos;
        } else
        {
            var newPos = new Vector3(transform.localPosition.x, localYPos + (animator.GetFloat("Yoff") - 0.5f)/5, transform.localPosition.z);
            transform.localPosition = newPos;
            hoverCollectTime -= Time.deltaTime;
            if (hoverCollectTime <= 0 )
            {
                Destroy(this.gameObject);
            }
        }

        if (transform.position.z < -10)
        {
            Debug.LogWarning("Destroing potato out of bounds");
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        TotalPotatoes++;
        var player = other.GetComponent<KidController2>();
        if (player)
        {
            this.transform.parent = other.transform.transform;
            var kiddo = other.transform.Find("Character");
            if (kiddo)
            {
                var newPos = kiddo.transform.localPosition;
                newPos[1] += 1.2f;
                localYPos = newPos[1];
                transform.localPosition = newPos;
                Debug.Log("Found Kiddo");
            } else
            {
                var newPos = transform.position;
                newPos[1] += 1.5f;
                transform.position = newPos;
            }
            isCollected = true;
            //StartCoroutine("CollectCoroutine");
            Debug.Log($"Score: {TotalPotatoes}");
        }
    }

    private IEnumerable CollectCoroutine()
    {
        Debug.Log("Started Collect");
        yield return new WaitForSeconds(hoverCollectTime);
        Debug.Log("Finished Collect");
    }
}
