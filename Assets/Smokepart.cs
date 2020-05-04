using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Smokepart : MonoBehaviour
{

    float smokespeed;
    Vector3 randomVector3;
    Vector3 randomShakeVector3;
    float randomVectorRange = 0.2f;
    Vector3 movedir;
    float smoothup = 0.001f;
    float spread = 0.2f;
    public Material mat;
    Shader shader;

    void Start()
    {

        mat = (Material)Resources.Load("SmokeMat", typeof(Material));

        GenerateRandomVector3();

        smokespeed = Random.Range(0.060f, 0.100f);

        movedir = (Vector3.up * smokespeed) + randomVector3*spread;

        float rand = Random.Range(1f, 0.2f);


        transform.gameObject.GetComponent<MeshRenderer>().material = mat;

       transform.gameObject.GetComponent<MeshRenderer>().material.color = new Color(rand, rand, rand, 0.0f);

        transform.gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Mode", 2f);

        Destroy(gameObject.GetComponent<BoxCollider>());
    }

    void GenerateRandomVector3()
    {
        randomVector3 = new Vector3(
           Random.Range(-randomVectorRange, randomVectorRange),
           0,
           Random.Range(-randomVectorRange, randomVectorRange));




    }
    void GenerateRandomShakeVector3()
    {
        randomShakeVector3 = new Vector3(
           Random.Range(-randomVectorRange, randomVectorRange),
           0,
           Random.Range(-randomVectorRange, randomVectorRange));




    }

    void Update()
    {

        movedir = Vector3.Lerp(movedir, (Vector3.up * smokespeed), smoothup);

        GenerateRandomShakeVector3();


        transform.position = Vector3.Lerp(transform.position, (transform.position +randomShakeVector3), 0.1f);

       transform.position = transform.position + movedir;

        Debug.Log(Vector3.Distance(transform.position, (transform.parent.transform.position)));


        if (Vector3.Distance(transform.position,(transform.parent.transform.position)) > 30)
        {

            Destroy(gameObject);
        }



    }
}
