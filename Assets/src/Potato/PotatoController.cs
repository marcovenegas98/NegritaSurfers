using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            //var newPos = new Vector3(transform.localPosition.x, localYPos + animator.GetFloat("Yoff"), transform.localPosition.z);
            //transform.localPosition = newPos;
        } else
        {
            //var newPos = new Vector3(transform.localPosition.x, localYPos + (animator.GetFloat("Yoff") - 0.5f)/5, transform.localPosition.z);
            //transform.localPosition = newPos;
            hoverCollectTime -= Time.deltaTime;
            if (hoverCollectTime <= 25 )
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
        var player = other.GetComponent<KidController2>();
        if (player)
        {
            CollectPotato(player);
            UpdateUIScore();
        }
    }

    private void CollectPotato(KidController2 player)
    {
        TotalPotatoes++;
        this.transform.parent = player.transform.transform;
        var kiddo = player.transform.Find("Character");
        if (kiddo)
        {
            var newPos = kiddo.transform.localPosition;
            newPos[1] += 1.2f;
            localYPos = newPos[1];
            transform.localPosition = newPos;
            Debug.Log("Found Kiddo");
        }
        else
        {
            var newPos = transform.position;
            newPos[1] += 1.5f;
            transform.position = newPos;
        }
        isCollected = true;
    }

    private void UpdateUIScore()
    {
        var canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (canvas)
        {
            List<Text> texts = new List<Text>();
            canvas.GetComponentsInChildren<Text>(texts);
            foreach (Text text in texts)
            {
                if (text.name == "PotatoText")
                {
                    text.text = $"Papas recolectadas: {TotalPotatoes}";
                    break;
                }
            }
        }

    }
}
