using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    public GameObject l1_1;
    public GameObject l2_1;
    private List<GameObject> prefabList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(l1_1, new Vector3(80, 0, 253), new Quaternion(0,0,0,1));
        Instantiate(l2_1, new Vector3(80, 0, (253+250)), new Quaternion(0, 0, 0, 1));
        //Instantiate(this.l1_1);
        //Instantiate(this.l2_1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
