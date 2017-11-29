using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuwafuwa : MonoBehaviour {
    public float swingDistance = 0.1f;
    private MeshFilter meshFilter;

    private SkinnedMeshRenderer skinnedMR;

    private Vector3[] vertices;
    private List<Vector3> resetVertices = new List<Vector3>();
    //private List<Vector3> originalVertices = new List<Vector3>();
    //private List<Vector3> currentVertices = new List<Vector3>();
    //private List<Vector3> targetlVertices = new List<Vector3>();
    //private List<float> timeGap = new List<float>();
    //private Dictionary<int, int> correspondenceTable = new Dictionary<int, int>();
    // <すべてのindex,originalのindex>
    /**
     * Awake.
     */
    public void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        //vertices = meshFilter.mesh.vertices;
        skinnedMR = GetComponent<SkinnedMeshRenderer>();
        if(meshFilter == null)
        {
            vertices = skinnedMR.sharedMesh.vertices;
        }
        else
        {
            vertices = meshFilter.mesh.vertices;
        }

        for (int i = 0; i < vertices.Length; ++i)
        {
            Vector3 vertex = vertices[i];
            resetVertices.Add(vertex);
            //if (!originalVertices.Contains(vertex))
            //{
            //    correspondenceTable[i] = originalVertices.Count;
            //    originalVertices.Add(vertex);
            //    currentVertices.Add(vertex);

            //    // ランダムな位置を作る
            //    targetlVertices.Add(new Vector3(
            //        vertex.x + Random.Range(-swingDistance, swingDistance),
            //        vertex.y + Random.Range(-swingDistance, swingDistance),
            //        vertex.z + Random.Range(-swingDistance, swingDistance)
            //    ));
            //}
            //else
            //{
            //    correspondenceTable[i] = originalVertices.FindIndex(vec => vec == vertex);
            //}
        }

        //for (int i = 0; i < originalVertices.Count; ++i)
        //{
        //    timeGap.Add(Random.Range(0.0f, 1.0f));
        //}
    }

    /**
     * Update.
     */
    public void Update()
    {
        // 現在位置の更新
        //for (int i = 0; i < currentVertices.Count; ++i)
        //{
        //    Vector3 originalPos = originalVertices[i];
        //    Vector3 targetPos = targetlVertices[i];
        //    Vector3 currentPos = currentVertices[i];

        //    timeGap[i] += Time.deltaTime * 1.6f;
        //    currentPos = Vector3.Slerp(originalPos, targetPos, Mathf.PingPong(timeGap[i], 1.0f));

        //    currentVertices[i] = currentPos;
        //}

        //// verticesに渡す頂点を作成
        //for (int i = 0; i < vertices.Length; ++i)
        //{
        //    int vid = correspondenceTable[i];
        //    vertices[i] = currentVertices[vid];
        //}

        //// 回転演出
        //transform.Rotate(Vector3.up * Time.deltaTime * 12.0f, Space.World);

        // 0へ
        //for (int i = 0; i < vertices.Length; ++i)
        //{
        //    Vector3 vertex = vertices[i];
        //    vertex.y += (0 - vertex.y)/100;
        //    vertices[i] = vertex;
            
        //}

        for (int i = 0; i < vertices.Length; ++i)
        {
            //Vector3 vertex = resetVertices[i] * (Mathf.Sin(Time.time * 10)*0.1f + 1f);
            Vector3 vertex = vertices[i];
            vertex.x = resetVertices[i].x * (Mathf.Sin(Time.time * 6) * 0.05f + 1f);
            vertex.y = resetVertices[i].y * (Mathf.Cos(Time.time * 6) * 0.1f + 1f);
            vertex.z = resetVertices[i].z * (Mathf.Sin(Time.time * 6) * 0.05f + 1f);
            vertices[i] = vertex;

        }

        // 頂点を渡す
        //meshFilter.mesh.vertices = vertices;

        if (meshFilter == null)
        {
            skinnedMR.sharedMesh.vertices = vertices;
        }
        else
        {
            meshFilter.mesh.vertices = vertices;
        }
        

        
    }
    public void OnDestroy()
    {
        for (int i = 0; i < vertices.Length; ++i)
        {
            vertices[i] = resetVertices[i];
        }
        if (meshFilter == null)
        {
            skinnedMR.sharedMesh.vertices = vertices;
        }
        else
        {
            meshFilter.mesh.vertices = vertices;
        }
    }


}
