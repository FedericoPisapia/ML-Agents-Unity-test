using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake : MonoBehaviour
{
    private float speed = 3;
    private float rotationSpeed = 350;
    private float score = 0f;
    public GameObject coda;
    public GameObject plane;
    public GameObject newsnake;
    private Vector3 posizioneAttuale;
    private Vector3 start;
    public List<Transform> bodyParts = new List<Transform>();
    public float minDistance = 0.25f;
    private float dis;
    private Transform curBodyPart;
    private Transform PrevBodyPart;
    private float time = 0f;
    private void Start()
    {
        start = gameObject.transform.position;
        AddBodyPart();
    }

    private void FixedUpdate()
    {
        // transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
        time += Time.fixedDeltaTime;
        Move();
        var colliders = Physics.OverlapSphere(bodyParts[0].position, bodyParts[0].GetComponent<SphereCollider>().radius);
        
        foreach(var x in colliders)
        {
            if (x.gameObject != bodyParts[0].gameObject && x.gameObject != bodyParts[1].gameObject  && x.gameObject.CompareTag("Player") && time>1f )
            {
                print("morto");
                Reset();
            }
            if(x.gameObject.CompareTag("Food")) {
                Destroy(x.gameObject);
                score++;
                AddBodyPart();
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Wall"))
        {
            print("morto");
            Reset();
        }
    }

    public void Move()
    {
        float curspeed = speed;
        bodyParts[0].Translate(bodyParts[0].forward * curspeed * Time.smoothDeltaTime, Space.World);
        if (Input.GetAxis("Horizontal") != 0)
            bodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        for (int i = 1; i < bodyParts.Count; i++)
        {

            curBodyPart = bodyParts[i];
            PrevBodyPart = bodyParts[i - 1];

            dis = Vector3.Distance(PrevBodyPart.position, curBodyPart.position);

            Vector3 newpos = PrevBodyPart.position;

            newpos.y = bodyParts[0].position.y;

            float T = Time.deltaTime * dis / minDistance * curspeed;

            if (T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyPart.rotation, T);



        }
    }
    public void AddBodyPart()
    {

        Transform newpart = (Instantiate(coda, bodyParts[bodyParts.Count - 1].position, bodyParts[bodyParts.Count - 1].rotation) as GameObject).transform;

        newpart.SetParent(transform);

        bodyParts.Add(newpart);
    }

    private void Reset()
    {   
        Instantiate(newsnake, start, Quaternion.identity);
        Destroy(gameObject);
        
    }

}

