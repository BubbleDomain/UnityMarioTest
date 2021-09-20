using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    private const float GROUND = -1.5f;
    private const float BACKGROUD = 1f;

    private const float LINE1 = 2f;

    ArrayList blockVectors = new ArrayList();
    ArrayList blocks = new ArrayList();

    private void Awake()
    {
        InitBlock();

    }

    // Start is called before the first frame update
    void Start()
    {
        //Object o = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //Instantiate(o, new Vector3(1.0f,1.0f,1.0f), new Quaternion(0f,0f,0f,0f));

        foreach (Vector3 vector3 in blockVectors)
        {
            blocks.Add((GameObject)Instantiate(Resources.Load("test"), vector3, new Quaternion(0f, 0f, 0f, 0f)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitBlock()
    {
        // 地面一层砖
        for (float i = -20; i < 100; i++ )
        {
            blockVectors.Add(new Vector3(i, GROUND, BACKGROUD));
        }

        for (float i = 13; i < 16; i++)
        {
            blockVectors.Add(new Vector3(i, LINE1, BACKGROUD));
        }
    }
}
