using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletClearing : MonoBehaviour
{
    public float lifespan = 10f;
    private float timer = 0f;

    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifespan)
        {
            Destroy(gameObject);
        }
    }
}
