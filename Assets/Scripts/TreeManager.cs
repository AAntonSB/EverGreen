using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Trees
{
    public class TreeManager : MonoBehaviour
    {

        public GameObject trunks;
        public GameObject leaves;

        public bool hasDecreasingBranches = true;
        [Range(-0.8f, 0.8f)]
        public float decreasingBranchesRate = 0;

        public bool isGeneratingBranch = false;
        public GameObject leaf;
        public GameObject fruit;
        public bool isGeneratingFirstBranch = false;
        MeshFilter mesh;
        Vector3[] vertices;
        Vector3[] prevVerts = new Vector3[4];
        Vector3[] newVerts = new Vector3[4];
        public bool hasSmallerBranches = true;
        [Range(-0.8f, 0.8f)]


        public float smallerBranchesRate = 0.5f;

        [Header("Public Variables")]
        [Range(0, 100)]
        public float planeHeight = 0;

        public bool isShorterBranchesHigherUp = true;

        [Range(0, 100)]
        public int branchLengthMin = 1;
        [Range(0, 100)]
        public int branchLengthMax = 1;

        public bool isnotspawningbranchesbelow = true;
        [Range(0, 50)]
        public int branchSpawnStartHeightMax = 0;
        [Range(0, 50)]
        public int branchSpawnStartHeightMin = 0;
        public bool hasLeaves = false;
        public bool hasFruit = true;
        public bool isRotateTrunk = true;
        public int currectTreeHeight = 0;
        [Header("Random Ranges")]
        [Range(0, 1)]
        public float randomVerticeHeight = 0;
        [Range(0, 1)]
        public float randomVerticeWidth = 0;
        [Range(0, 10)]
        //Hur många delar som stammen består av
        public int randomTrunkHeight = 100;
        [Range(1, 10)]
        //Hur många delar som stammen består av
        public float randomRadiusRange = 0;
        [Range(0, 100)]
        public float BranchSpawnChance = 0;
        [Range(0, 100)]
        public float LeafSpawnChance = 0;
        [Range(0, 10)]
        public int branchesAmount = 0;

        [Range(0, 100)]
        public int treeHeightmin = 0;
        [Range(0, 100)]
        public int treeHeightmax = 0;
        [Range(0, 10)]
        public float branchRotationRandom = 0;

        public Color trunkColor;
        public Color leavesColor;


        int branchSpawnStartHeight;

        int listSize;

        public List<Vector3> createBranchesv1;
        public List<Vector3> createBranchesv2;
        public List<Vector3> createBranchesv3;
        public List<Vector3> createBranchesv4;




        //private void Update()
        //{
        //    GenerateTrunk();
        //}




        private void Start()
        {

            leavesColor = new Color(Random.Range(0f, 0.3f), Random.Range(0.2f, 1f), 0, 1);

            trunkColor = new Color(1, Random.Range(0.4f, 0.6f), Random.Range(0f, 0.2f), 1f);

            (trunks = new GameObject()).transform.parent = transform;

            trunks.name = ("trunks");

            trunks.AddComponent<MeshCombiner>();

            trunks.GetComponent<MeshRenderer>().material.color = trunkColor;

            (leaves = new GameObject()).transform.parent = transform;

            leaves.name = ("leaves");

            leaves.AddComponent<MeshCombiner>();
            leaves.GetComponent<MeshRenderer>().material.color = leavesColor;
            


            branchSpawnStartHeight = Random.Range(branchSpawnStartHeightMin, branchSpawnStartHeightMax);

            GenerateFruit();


            InitVert();

            randomTrunkHeight = Random.Range(0, randomTrunkHeight);

            GenerateTrunk();

            listSize = createBranchesv1.Count;

            for (int i = 0; i < branchesAmount; i++)
            {

                GenerateBranches();

            }

        }


        void GenerateFruit()
        {
            fruit = GameObject.CreatePrimitive(PrimitiveType.Cube);

            fruit.GetComponent<MeshRenderer>().material.color = Color.red;

            Destroy(fruit);

        }

        void GenerateLeaf()
        {

            leaf = GameObject.CreatePrimitive(PrimitiveType.Cube);

            leaf.GetComponent<MeshRenderer>().material.color = Color.green;

            Destroy(leaf);

        }
        void GenerateTrunk()
        {



            for (int i = 0; i < (Random.Range(treeHeightmin, treeHeightmax)); i++)
            {


                if (isRotateTrunk)
                {
                    Vector3 line1 = prevVerts[1] - prevVerts[0];
                    Vector3 line2 = prevVerts[2] - prevVerts[0];

                    Vector3 normal = Vector3.Cross(line1, line2);

                    GenerateVertecies(-normal * planeHeight);
                }
                else
                {
                    GenerateVertecies(new Vector3(0, planeHeight, 0));
                }


                //GenerateVertecies(new Vector3(0,planeHeight,0));

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


            for (int i = 0; i < 1; i++)
            {
                GenerateLeaf();
                leaf.GetComponent<MeshRenderer>().material.color = leavesColor;

                Vector3 leafSize = new Vector3(Random.Range(1, 4), Random.Range(1, 4), Random.Range(1, 4));
                leaf.transform.localScale = leafSize;

                if (hasLeaves)
                {

                    (Instantiate(leaf, newVerts[Random.Range(0, 3)], new Quaternion(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10))) as GameObject).transform.parent = leaves.transform;

                    //leaf.transform.parent = transform;

                }




            }




        }
        void GenerateVertecies(Vector3 offset)
        {
            for (int i = 0; i < 4; i++)
            {
                newVerts[i].x = prevVerts[i].x + Random.Range(-randomVerticeWidth, randomVerticeWidth) + offset.x;
                newVerts[i].z = prevVerts[i].z + Random.Range(-randomVerticeWidth, randomVerticeWidth) + offset.z;
                newVerts[i].y = prevVerts[i].y + Random.Range(-randomVerticeHeight, randomVerticeHeight) + offset.y;






                if (LeafSpawnChance >= Random.Range(0.1f, 100) && isGeneratingBranch)
                {


                    Vector3 leafSize = new Vector3(Random.Range(1, 4), Random.Range(1, 4), Random.Range(1, 4));

                    leaf.transform.localScale = leafSize;

                    Vector3 leafpos = newVerts[Random.Range(0, 3)];

                    if (hasLeaves)
                    {

                        (Instantiate(leaf, leafpos, new Quaternion(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10))) as GameObject).transform.parent = leaves.transform;



                    }



                    fruit.transform.localScale = new Vector3(Random.Range(0.5f, 0.6f), Random.Range(0.5f, 0.6f), Random.Range(0.5f, 0.6f));


                    if (hasFruit)
                    {
                        Instantiate(fruit, (leafpos - new Vector3(0, leafSize.y / 2, 0)), transform.rotation);

                    }



                }

            }
            Vector3 midpoint = (newVerts[0] + newVerts[1] + newVerts[2] + newVerts[3]) / 4;

            if (isGeneratingFirstBranch)
            {


                for (int i = 0; i < 4; i++)
                {


                    float dist = Vector3.Distance(midpoint, newVerts[i]);

                    newVerts[i] = Vector3.MoveTowards(newVerts[i], midpoint, dist * smallerBranchesRate);
                }

            }
            else
            {
                if (hasDecreasingBranches)
                {
                    for (int i = 0; i < 4; i++)
                    {


                        float dist = Vector3.Distance(midpoint, newVerts[i]);

                        newVerts[i] = Vector3.MoveTowards(newVerts[i], midpoint, dist * decreasingBranchesRate);
                    }

                }

            }

            isGeneratingFirstBranch = false;





        }

        void InitVert()
        {
            prevVerts[0] = transform.position + new Vector3(1, 0, 1) * Random.Range(1, randomRadiusRange);
            prevVerts[1] = transform.position + new Vector3(0, 0, 1) * Random.Range(1, randomRadiusRange);
            prevVerts[2] = transform.position + new Vector3(0, 0, 0) * Random.Range(1, randomRadiusRange);
            prevVerts[3] = transform.position + new Vector3(1, 0, 0) * Random.Range(1, randomRadiusRange);

        }

        void CreateMesh(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            Mesh m = new Mesh();
            m.name = "Scriptable Mesh";
            m.vertices = new Vector3[] { v1, v2, v3, v4 };

            m.uv = new Vector2[]
            {
                new Vector2(0,0),
                new Vector2(0,1),
                new Vector2(1,1),
                new Vector2(1,0),
            };

            m.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
            m.RecalculateNormals();



            if (BranchSpawnChance >= Random.Range(0.1f, 100) && isnotspawningbranchesbelow && currectTreeHeight > branchSpawnStartHeight)
            {
                createBranchesv1.Add(v1);
                createBranchesv2.Add(v2);
                createBranchesv3.Add(v3);
                createBranchesv4.Add(v4);
            }
            else
            {
                GameObject plane = new GameObject("plane");
                plane.transform.parent = trunks.transform;
                MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
                meshFilter.mesh = m;
                MeshRenderer renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;



                //renderer.material.color = new Color(1, Random.Range(0.4f,0.6f), Random.Range(0f, 0.2f), 1f);

                //renderer.material.shader = Shader
            }

        }

        void GenerateBranches()
        {

            isGeneratingBranch = true;



            BranchSpawnChance = 0;

            for (int n = 0; n < listSize; n++)
            {

                

                isGeneratingFirstBranch = true;

                prevVerts[3] = createBranchesv1[n];
                prevVerts[2] = createBranchesv2[n];
                prevVerts[1] = createBranchesv3[n];
                prevVerts[0] = createBranchesv4[n];




                Vector3 line1 = prevVerts[1] - prevVerts[0];
                Vector3 line2 = prevVerts[2] - prevVerts[0];

                Vector3 normal = Vector3.Cross(line1, line2);

                if (isShorterBranchesHigherUp)
                {
                    //branchLength = 50 - currectTreeHeight;

                }

                normal = new Vector3((normal.x + Random.Range(-branchRotationRandom, branchRotationRandom)),
                    (normal.y + Random.Range(-branchRotationRandom, branchRotationRandom)),
                    (normal.z + Random.Range(-branchRotationRandom, branchRotationRandom)));

                for (int i = 0; i < Random.Range(branchLengthMin, branchLengthMax); i++)
                {

                    line1 = prevVerts[1] - prevVerts[0];
                    line2 = prevVerts[2] - prevVerts[0];

                    normal = Vector3.Cross(line1, line2);

                    normal = normal.normalized;



                    GenerateVertecies(-normal * planeHeight);

                    BindMeshCube();

                    prevVerts[0] = newVerts[0];
                    prevVerts[1] = newVerts[1];
                    prevVerts[2] = newVerts[2];
                    prevVerts[3] = newVerts[3];

                }

                GenerateTreeTop();

            }











        }

        void BindMeshCube()
        {

            if (!isGeneratingBranch)
            {
                currectTreeHeight++;

            }

            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                {
                    CreateMesh(prevVerts[0], prevVerts[i], newVerts[i], newVerts[0]);

                    break;

                }

                CreateMesh(prevVerts[i + 1], prevVerts[i], newVerts[i], newVerts[i + 1]);


            }


        }
    }
}