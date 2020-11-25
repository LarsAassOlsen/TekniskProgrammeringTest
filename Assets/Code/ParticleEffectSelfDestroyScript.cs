using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectSelfDestroyScript : MonoBehaviour
{
    float destroyTimer = 5f;

    void Update()
    {
        destroyTimer -= Time.deltaTime;
        if(destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
