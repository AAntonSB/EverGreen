using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneManager : MonoBehaviour
{

    public int stonePartAmount = 5;
    GameObject stonepart;
    public float randomVectorRange = 2;
    Vector3 randomVector3;

   



    void Start()
    {

       

        GenerateStonePart();

        GenerateStone();

        transform.gameObject.AddComponent<MeshCombiner>();

        float rand = Random.Range(0.3f, 0.6f);

        transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(rand,rand,rand,1);

        
    }


    void GenerateRandomVector3()
    {

        randomVector3 = new Vector3(
            Random.Range(-randomVectorRange, randomVectorRange),
            0,
            Random.Range(-randomVectorRange, randomVectorRange));

    }

    void GenerateStone()
    {

        for (int i = 0; i < stonePartAmount; i++)
        {

            GenerateRandomVector3();
            GenerateStonePart();

            (Instantiate(stonepart, transform.position, new Quaternion(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10))) as GameObject).transform.parent = transform;



        }
    }


    void GenerateStonePart()
    {
        stonepart = GameObject.CreatePrimitive(PrimitiveType.Cube);

        stonepart.GetComponent<MeshRenderer>().material.color = Color.white;

        Vector3 cloudPartSize = new Vector3(Random.Range(2, 10), Random.Range(2, 10), Random.Range(2, 10));


        stonepart.transform.localScale = cloudPartSize;

        stonepart.transform.position = Vector3.zero;


        Destroy(stonepart);


    }





}
