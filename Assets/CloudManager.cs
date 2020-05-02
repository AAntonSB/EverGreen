using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class CloudManager : MonoBehaviour
{

    public int cloudPartAmount = 5;
    GameObject cloudpart;
    public float randomVectorRange = 200;
    Vector3 randomVector3;
    public float speed = 1;



    private void Update()
    {
        
        transform.position=transform.position+transform.forward*speed;


    }

    void Start()
    {

        

        GenerateCloudParts();

        GenerateCloud();

        transform.gameObject.AddComponent<MeshCombiner>();
        transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;

    }

    void GenerateRandomVector3()
    {

        randomVector3 = new Vector3(
            Random.Range(-randomVectorRange, randomVectorRange),
            0,
            Random.Range(-randomVectorRange, randomVectorRange));

    }

    void GenerateCloud()
    {

        for (int i = 0; i < cloudPartAmount; i++)
        {

            GenerateRandomVector3();
            GenerateCloudParts();

            (Instantiate(cloudpart, transform.position+randomVector3, new Quaternion(Random.Range(0,10), Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10))) as GameObject).transform.parent = transform;

            

        }
    }


    void GenerateCloudParts()
    {
        cloudpart = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cloudpart.GetComponent<MeshRenderer>().material.color = Color.white;

        Vector3 cloudPartSize = new Vector3(Random.Range(20, 50), Random.Range(20, 50), Random.Range(20, 50));
        cloudpart.transform.localScale = cloudPartSize;

        Destroy(cloudpart);
        

    }
    


    


   
}
