using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    public List<GameObject> levelsList;
    private static int TOTAL_CHUNKS_PER_LEVEL = 8; //2 chunks per dev, per level
    private static int LEVELS = 3;
    private static int TOTAL_CHUNKS = LEVELS * TOTAL_CHUNKS_PER_LEVEL;
    private static int TOTAL_PREFABS_PER_LEVEL = 1;

    // Start is called before the first frame update
    void Start()
    {
        // local variables
        int listLeftLimit, listRightLimit, nextInt;
        Quaternion quat = new Quaternion(0,0,0,1);

        for (int index = 0; index< TOTAL_CHUNKS; ++index)
        {
            listLeftLimit = Mathf.RoundToInt(Mathf.Floor(index / TOTAL_CHUNKS_PER_LEVEL) * TOTAL_PREFABS_PER_LEVEL);
            listRightLimit = listLeftLimit + TOTAL_PREFABS_PER_LEVEL;
            nextInt = Random.Range(listLeftLimit, listRightLimit);
            Instantiate(levelsList[nextInt], new Vector3(10, -0.5f, (253 + 250 * index)), quat);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
