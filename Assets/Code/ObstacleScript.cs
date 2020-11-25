using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    void Update()
    {
        if (GameController.Instance.GameRunning)
        {
            transform.Translate(-Vector3.up * Time.deltaTime * GameController.Instance.GameSpeed, Space.World);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FloorDestroyTrigger"))
        {
            Destroy(gameObject);
        }
    }
}
