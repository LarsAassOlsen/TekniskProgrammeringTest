using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;

    void Update()
    {
        if (GameController.Instance.GameRunning)
        {
            if(playerTransform == null)
            {
                playerTransform = GameController.Instance.PlayerObject.transform;
            }
            transform.position = playerTransform.position;
        }
    }
}
