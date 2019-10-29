using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    public GameObject l1_1;
    public GameObject l2_1;
    private List<GameObject> levelsList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // filling the list
        this.levelsList.Add(l1_1);
        this.levelsList.Add(l2_1);

        Quaternion quat = new Quaternion(0,0,0,1);
        for (int index = 0; index<2; ++index)
        {
            Instantiate(levelsList[(index)], new Vector3(80, 0, (253 + 250 * index)), quat);
        }

        //Instantiate(l1_1, new Vector3(80, 0, 253), Quaternion);
        //Instantiate(l2_1, new Vector3(80, 0, (253+250)), new Quaternion(0, 0, 0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
