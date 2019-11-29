using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    public List<GameObject> levelsList;
    public GameObject church;
    private static int TOTAL_CHUNKS_PER_LEVEL = 5;
    private static int LEVELS = 3;
    private static int TOTAL_CHUNKS = LEVELS * TOTAL_CHUNKS_PER_LEVEL;
    private static int TOTAL_PREFABS_PER_LEVEL = 8;

    // Start is called before the first frame update
    void Start()
    {
        // local variables
        int listLeftLimit, listRightLimit, nextInt;
        Quaternion quat = new Quaternion(0,0,0,1);

        int index;
        for (index = 0; index < TOTAL_CHUNKS; ++index)
        {
            listLeftLimit = Mathf.RoundToInt(Mathf.Floor(index / TOTAL_CHUNKS_PER_LEVEL) * TOTAL_PREFABS_PER_LEVEL);
            listRightLimit = listLeftLimit + TOTAL_PREFABS_PER_LEVEL;
            nextInt = Random.Range(listLeftLimit, listRightLimit);
            Instantiate(levelsList[nextInt], new Vector3(10, -0.5f, (253 + 250 * index)), quat);
        }

        var churchInstance = (GameObject)Instantiate(church);
        churchInstance.transform.localScale = new Vector3(10, 10, 10);
        churchInstance.transform.position = new Vector3(99, -22.5f, 1000);
        churchInstance.transform.rotation = Quaternion.identity;
        churchInstance.transform.Rotate(-1.551f, -90f, -0.178f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
