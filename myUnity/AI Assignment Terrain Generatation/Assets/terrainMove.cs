﻿//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;

//public class terrainMove : MonoBehaviour
//{
//    public List<GameObject> terrains = new List<GameObject>();
//    private Queue<GameObject> terrainQueue = new Queue<GameObject>();
//    public GameObject theCube = null;
//    private Rect rect;
//    public int terrainWidth = 0;
//    public int terrainLength = 0;
//    float speed = 1000;
//    float gravity = 0;
//    Vector3 moveDirection;
//    int size = 200;
//    List<SplatPrototype> s = new List<SplatPrototype>();
        
//    // Use this for initialization
//    void Start()
//    {
//        SplatPrototype s1 = new SplatPrototype();
//        s1.texture = (Texture2D)Resources.Load("GoodDirt", typeof(Texture2D));
//        s.Add(s1);
//        for (int i = 0; i < 9; i++)
//        {
//            GenerateNewTerrain();
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        terrains = GameObject.FindGameObjectsWithTag("Terrain").ToList();
//        foreach (var t in terrains)
//        {
//            terrainQueue.Enqueue(t);
//        }

//        CharacterController controller = GetComponent<CharacterController>();
//        {
//            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//            moveDirection = transform.TransformDirection(moveDirection);
//            moveDirection *= speed;
//        }

//        moveDirection.y -= gravity * Time.deltaTime;
//        int i = 0;
//        controller.Move(moveDirection * Time.deltaTime);
//        Vector2 pos = new Vector2(theCube.transform.position.x, theCube.transform.position.z);
//        bool flag = false;
       
//        foreach (GameObject t in terrainQueue)
//        {
//            rect = new Rect(t.transform.position.x, t.transform.position.z, t.GetComponent<Terrain>().terrainData.size.x, t.GetComponent<Terrain>().terrainData.size.z);
            
//            if (rect.Contains(pos))
//            {
//                flag = true;
//            }
//        }
//        if (!flag)
//        {
//            try
//            {
//                var t2 = terrainQueue.Dequeue();
//                if (terrains.Count > 8)
//                    ReorderTerrains(t2);
//                else
//                    GenerateNewTerrain();

//                terrainQueue.Enqueue(t2);
//            }
//            catch (System.Exception)
//            {
//                GenerateNewTerrain();
//            }
//        }
//                return;
      
//    }

//    void GenerateNewTerrain()
//    {
//        float frequency = 0.10f;
//        TerrainData td = new TerrainData();
//        float[,] heights = new float[size, (int)(size / frequency)];
//        System.Random rand = new System.Random();
//        td.size = new Vector3(size, size, size);

//        for (int i = 0; i < size; i++)
//        {
//            for (int j = 0; j < (int)(size / frequency); j++)
//                Noise.generateSineWave(ref i, ref heights[i, j], frequency, 10);
//        }
//        heights = Noise.normalizePerlin(heights, new Vector2(size, (int)(size / frequency)));
//        td.heightmapResolution = (int)(size / frequency);
//        td.splatPrototypes = s.ToArray();
//        td.SetHeights(0, 0, heights);
//        var terrain = Terrain.CreateTerrainGameObject(td);
//        terrain.tag = "Terrain";
//        terrain.AddComponent<Rigidbody>();
//        //terrain.AddComponent<TerrainCollider>();
//        terrain.rigidbody.useGravity = false;
//        terrain.rigidbody.isKinematic = true;
//    }




//    void ReorderTerrains(GameObject t)
//    {
//        var t1 = t.GetComponent<Terrain>();
//        t.transform.position = new Vector3(theCube.transform.position.x - t1.terrainData.size.x / 2, 0, theCube.transform.position.z - t1.terrainData.size.z / 2);
        
//    }


//    void Normalize(List<Terrain> terrains)
//    {

//    }


//}

////static class Noise
////{

//    //public static void generateSineWave(ref int x, ref float z, float frequency, float amplitude)
//    //{
//    //    z = Mathf.Sin(x) * amplitude;
//    //}

//    //public static float Generate(float x, float y)
//    //{
//    //    const float F2 = 0.366025403f; // F2 = 0.5*(sqrt(3.0)-1.0)
//    //    const float G2 = 0.211324865f; // G2 = (3.0-Math.sqrt(3.0))/6.0

//    //    float n0, n1, n2; // Noise contributions from the three corners

//    //    // Skew the input space to determine which simplex cell we're in
//    //    float s = (x + y) * F2; // Hairy factor for 2D
//    //    float xs = x + s;
//    //    float ys = y + s;
//    //    int i = FastFloor(xs);
//    //    int j = FastFloor(ys);

//    //    float t = (float)(i + j) * G2;
//    //    float X0 = i - t; // Unskew the cell origin back to (x,y) space
//    //    float Y0 = j - t;
//    //    float x0 = x - X0; // The x,y distances from the cell origin
//    //    float y0 = y - Y0;

//    //    // For the 2D case, the simplex shape is an equilateral triangle.
//    //    // Determine which simplex we are in.
//    //    int i1, j1; // Offsets for second (middle) corner of simplex in (i,j) coords
//    //    if (x0 > y0) { i1 = 1; j1 = 0; } // lower triangle, XY order: (0,0)->(1,0)->(1,1)
//    //    else { i1 = 0; j1 = 1; }      // upper triangle, YX order: (0,0)->(0,1)->(1,1)

//    //    // A step of (1,0) in (i,j) means a step of (1-c,-c) in (x,y), and
//    //    // a step of (0,1) in (i,j) means a step of (-c,1-c) in (x,y), where
//    //    // c = (3-sqrt(3))/6

//    //    float x1 = x0 - i1 + G2; // Offsets for middle corner in (x,y) unskewed coords
//    //    float y1 = y0 - j1 + G2;
//    //    float x2 = x0 - 1.0f + 2.0f * G2; // Offsets for last corner in (x,y) unskewed coords
//    //    float y2 = y0 - 1.0f + 2.0f * G2;

//    //    // Wrap the integer indices at 256, to avoid indexing perm[] out of bounds
//    //    int ii = i % 256;
//    //    int jj = j % 256;

//    //    // Calculate the contribution from the three corners
//    //    float t0 = 0.5f - x0 * x0 - y0 * y0;
//    //    if (t0 < 0.0f) n0 = 0.0f;
//    //    else
//    //    {
//    //        t0 *= t0;
//    //        n0 = t0 * t0 * grad(perm[ii + perm[jj]], x0, y0);
//    //    }

//    //    float t1 = 0.5f - x1 * x1 - y1 * y1;
//    //    if (t1 < 0.0f) n1 = 0.0f;
//    //    else
//    //    {
//    //        t1 *= t1;
//    //        n1 = t1 * t1 * grad(perm[ii + i1 + perm[jj + j1]], x1, y1);
//    //    }

//    //    float t2 = 0.5f - x2 * x2 - y2 * y2;
//    //    if (t2 < 0.0f) n2 = 0.0f;
//    //    else
//    //    {
//    //        t2 *= t2;
//    //        n2 = t2 * t2 * grad(perm[ii + 1 + perm[jj + 1]], x2, y2);
//    //    }

//    //    // Add contributions from each corner to get the final noise value.
//    //    // The result is scaled to return values in the interval [-1,1].
//    //    return  5 * (n0 + n1 + n2); // TODO: The scale factor is preliminary!
//    //}
    
//    //    private static int FastFloor(float x)
//    //    {
//    //        return (x > 0) ? ((int)x) : (((int)x) - 1);
//    //    }

//    //    private static int Mod(int x, int m)
//    //    {
//    //        int a = x % m;
//    //        return a < 0 ? a + m : a;
//    //    }

//    //    private static float grad( int hash, float x )
//    //    {
//    //        int h = hash & 15;
//    //        float grad = 1.0f + (h & 7);   // Gradient value 1.0, 2.0, ..., 8.0
//    //        if ((h & 8) != 0) grad = -grad;         // Set a random sign for the gradient
//    //        return ( grad * x );           // Multiply the gradient with the distance
//    //    }

//    //    private static float grad( int hash, float x, float y )
//    //    {
//    //        int h = hash & 7;      // Convert low 3 bits of hash code
//    //        float u = h<4 ? x : y;  // into 8 simple gradient directions,
//    //        float v = h<4 ? y : x;  // and compute the dot product with (x,y).
//    //        return ((h&1) != 0 ? -u : u) + ((h&2) != 0 ? -2.0f*v : 2.0f*v);
//    //    }

//    //    private static float grad( int hash, float x, float y , float z ) {
//    //        int h = hash & 15;     // Convert low 4 bits of hash code into 12 simple
//    //        float u = h<8 ? x : y; // gradient directions, and compute dot product.
//    //        float v = h<4 ? y : h==12||h==14 ? x : z; // Fix repeats at h = 12 to 15
//    //        return ((h&1) != 0 ? -u : u) + ((h&2) != 0 ? -v : v);
//    //    }

//    //    private static float grad( int hash, float x, float y, float z, float t ) {
//    //        int h = hash & 31;      // Convert low 5 bits of hash code into 32 simple
//    //        float u = h<24 ? x : y; // gradient directions, and compute dot product.
//    //        float v = h<16 ? y : z;
//    //        float w = h<8 ? z : t;
//    //        return ((h&1) != 0 ? -u : u) + ((h&2) != 0 ? -v : v) + ((h&4) != 0 ? -w : w);
//    //    }

//    //    public static byte[] perm = new byte[512] { 151,160,137,91,90,15,
//    //          131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
//    //          190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
//    //          88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
//    //          77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
//    //          102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
//    //          135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
//    //          5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
//    //          223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
//    //          129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
//    //          251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
//    //          49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
//    //          138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180,
//    //          151,160,137,91,90,15,
//    //          131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
//    //          190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
//    //          88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
//    //          77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
//    //          102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
//    //          135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
//    //          5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
//    //          223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
//    //          129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
//    //          251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
//    //          49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
//    //          138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180 
//    //        };

//    //    public static float[,] normalizePerlin(float[,] heightMap, Vector2 arraySize)
//    //    {
//    //        int Tx = (int)arraySize.x;
//    //        int Ty = (int)arraySize.y;
//    //        int Mx;
//    //        int My;
//    //        float highestPoint = 1.0f;
//    //        float lowestPoint = -1f;

//    //        // Normalise...
//    //        float heightRange = highestPoint - lowestPoint;
//    //        float normalisedHeightRange = 1.0f;
//    //        float normaliseMin = 0.0f;
//    //        for (My = 0; My < Ty; My++)
//    //        {
//    //            for (Mx = 0; Mx < Tx; Mx++)
//    //            {
//    //                float normalisedHeight = ((heightMap[Mx, My] - lowestPoint) / heightRange) * normalisedHeightRange;
//    //                heightMap[Mx, My] = normaliseMin + normalisedHeight;
//    //            }
//    //        }

//    //        // return the height map
//    //        return heightMap;
//    //    }
	
////}

