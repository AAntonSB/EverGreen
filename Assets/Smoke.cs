using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{

    public float randomfreq = 5f;

    public GameObject smokepart;

    


    void Start()
    {
        
    }

    void GenerateSmokePart()
    {

        smokepart = GameObject.CreatePrimitive(PrimitiveType.Cube);

        smokepart.GetComponent<MeshRenderer>().material.color = Color.white;

        Destroy(smokepart.GetComponent<BoxCollider>());

        

        Vector3 cloudPartSize = new Vector3(Random.Range(1, 3), Random.Range(1, 3), Random.Range(1, 3));
        smokepart.transform.localScale = cloudPartSize;

        

        Destroy(smokepart);


    }

    

   
    void Update()
    {
        


        if(randomfreq > Random.Range(0, 1000))
        {

            GenerateSmokePart();

            smokepart.AddComponent<Smokepart>();

            if (Vector3.Distance(transform.position, (GameObject.Find("Main Camera").transform.position)) < 300)
            {
                (Instantiate(smokepart, transform.position, transform.rotation)as GameObject).transform.parent = transform;

            }

            



            Debug.Log("hur ofta??");


        }




    }
}
