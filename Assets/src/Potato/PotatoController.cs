using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public float localYPos = 5;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = new Vector3(transform.localPosition.x, localYPos + animator.GetFloat("Yoff"), transform.localPosition.z);
        transform.localPosition = newPos;
    }
}
