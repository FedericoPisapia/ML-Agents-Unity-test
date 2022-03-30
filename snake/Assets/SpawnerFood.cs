using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerFood : MonoBehaviour
{
    public GameObject food;
    public GameObject plane;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = plane.gameObject.transform.position;
        var new_pos = new Vector3(Random.Range(-9.0f + pos.x, 9.0f + pos.x), 0.5f, Random.Range(-9.0f + pos.z, 9.0f + pos.z));
        var new_food = Instantiate(food, new_pos, Quaternion.identity);
        new_food.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount == 0)
        {
            var new_pos = new Vector3(Random.Range(-9.0f + pos.x, 9.0f + pos.x), 0.5f, Random.Range(-9.0f + pos.z, 9.0f + pos.z));
            var new_food = Instantiate(food, new_pos, Quaternion.identity);
            new_food.transform.parent = gameObject.transform;
        }
    }
}
