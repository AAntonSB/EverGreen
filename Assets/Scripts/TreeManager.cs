using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Trees {
    public class TreeManager : MonoBehaviour
    {
        MeshFilter mesh;
        Vector3[] vertices;
        Vector3[] prevVerts = new Vector3[4];
        Vector3[] newVerts = new Vector3[4];

        [Header("Public Variables")]
        [Range(0, 100)]
        public float planeHeight = 0;

        [Header("Random Ranges")]
        [Range(0, 1)]
        public float randomVerticeHeight = 0;
        [Range(0, 1)]
        public float randomVerticeWidth = 0;
        [Range(2, 10)]
        //Hur många delar som stammen består av
        public int randomTrunkHeight = 100;
        [Range(1, 10)]
        //Hur många delar som stammen består av
        public float randomRadiusRange = 0;
        [Range(0, 100)]
        public float BranchSpawnChance = 0;

        //private void Update()
        //{
        //    GenerateTrunk();
        //}

        private void Start()
        {
            InitVert();

            randomTrunkHeight = Random.Range(0, randomTrunkHeight);

           GenerateTrunk();
        }

        void GenerateTrunk()
        {

            for (int i = 0; i < 10; i++)
            {
               
                GenerateVertecies(new Vector3(0,planeHeight,0));

                BindMeshCube();

                prevVerts[0] = newVerts[0];
                prevVerts[1] = newVerts[1];
                prevVerts[2] = newVerts[2];
                prevVerts[3] = newVerts[3];
            }
           
            GenerateTreeTop();


        }

        void GenerateTreeTop()
        {
            CreateMesh(newVerts[1], newVerts[0], newVerts[3], newVerts[2]);



        }
        void GenerateVertecies(Vector3 offset)
        {
            for (int i = 0; i < 4; i++)
            {
                newVerts[i].x = prevVerts[i].x + Random.Range(-randomVerticeWidth, randomVerticeWidth)+offset.x;
                newVerts[i].z = prevVerts[i].z + Random.Range(-randomVerticeWidth, randomVerticeWidth)+offset.z;
                newVerts[i].y = prevVerts[i].y + Random.Range(0, randomVerticeHeight)+offset.y;

            }
        }

        void InitVert()
        {
            prevVerts[0] = new Vector3(1, 0, 1) * Random.Range(1, randomRadiusRange);
            prevVerts[1] = new Vector3(0, 0, 1) * Random.Range(1, randomRadiusRange);
            prevVerts[2] = new Vector3(0, 0, 0) * Random.Range(1, randomRadiusRange);
            prevVerts[3] = new Vector3(1, 0, 0) * Random.Range(1, randomRadiusRange);

        }

        void CreateMesh(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            Mesh m = new Mesh();
            m.name = "Scriptable Mesh";
            m.vertices = new Vector3[]{ v1,v2,v3,v4 };

            m.uv = new Vector2[]
            {
                new Vector2(0,0),
                new Vector2(0,1),
                new Vector2(1,1),
                new Vector2(1,0),
            };

            m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
            m.RecalculateNormals();

            if (BranchSpawnChance >= Random.Range(0.1f, 100))
            {
            }
            else
            {
                GameObject plane = new GameObject("plane");
                MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
                meshFilter.mesh = m;
                MeshRenderer renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
                renderer.material.color = new Color(1, 0.4f, 0.1f, 1f);
                //renderer.material.shader = Shader
            }

        }

        void CreateBranch(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {

            






        }

        void BindMeshCube()
        {

            for (int i = 0; i < 4; i++)
            {
                if (i==3)
                {
                    CreateMesh(prevVerts[0], prevVerts[i], newVerts[i], newVerts[0]);

                    break;

                }

                CreateMesh(prevVerts[i+1], prevVerts[i], newVerts[i], newVerts[i+1]);


            }
            

        }
    }
}