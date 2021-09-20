using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    private const float FIRST_ENEMY_TRIGGER = 2f;
    private const float FIRST_ENEMY_DIRECTION = 22f;
    bool isInitFirstEnemy = false;

    private const float GROUND = -0.5f;
    private const float BACKGROUD = 1f;

    private Transform camera_transform;

    // Start is called before the first frame update
    void Start()
    {
        camera_transform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInitFirstEnemy && camera_transform.position.x > 10)
        {
            isInitFirstEnemy = true;
            Instantiate(Resources.Load("enemy"), new Vector3(FIRST_ENEMY_DIRECTION, GROUND, BACKGROUD), new Quaternion(0f, 0f, 0f, 0f));
        }
    }
}
