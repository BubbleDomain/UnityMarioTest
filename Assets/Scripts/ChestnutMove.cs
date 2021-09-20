using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestnutMove : MonoBehaviour
{
    public int collisionTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.left * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionTime++;
    }
}
