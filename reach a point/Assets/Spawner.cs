using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool val = true;
    public GameObject scena;
    public GameObject scena2;
    public GameObject scena3;
    public int x = 6;
    public int z = 6;
    GameObject group1;
    public float cambioScena;
    private List<GameObject> gruppi = new List<GameObject>();
    private int cambio = 0;
    private List<GameObject> scene = new List<GameObject>() ;
    

    // Start is called before the first frame update
    private void OnValidate()
    {
        scene.Add(scena);
        scene.Add(scena2);
        scene.Add(scena3);
        scene.Add(scena3);
    }

    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        print(allObjects.Length + "LUNGHEZZA");
        foreach (GameObject go in allObjects)
        {
            if (go.CompareTag("scena"))
                StartCoroutine(Destroy(go));
        }
        if (group1 != null)
        {
            foreach (var item in gruppi)
            {
                StartCoroutine(Destroy(item.gameObject));
            }

        }
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                group1 = Instantiate(scena, new Vector3(i * 20, 0, j * 20), Quaternion.identity);
                gruppi.Add(group1);
            }

        }


    }

    // Update is called once per frame
    void Update()
    {
         if (Jumper.avg > cambioScena)
         {
             foreach (var item in gruppi)
             {
                 StartCoroutine(Destroy(item.gameObject));
             }
             
                 for (int i = 0; i < x; i++)
                 {
                     for (int j = 0; j < z; j++)
                     {
                         group1 = Instantiate(scene[cambio], new Vector3(i * 20, 0, j * 20), Quaternion.identity);
                         gruppi.Add(group1);
                     }

                 }
            cambio++;
         }
    }
    IEnumerator Destroy(GameObject go)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(go);
    }
}
