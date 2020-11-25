using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool moving = false;
    bool allowInput = true;
    Transform transformTarget;

    void Update()
    {
        if (GameController.Instance.GameRunning)
        {
            if (allowInput)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                {
                    // Move one lane to the left
                    MoveToLane(Mathf.Clamp(GameController.Instance.CurrentLane - 1, 0, GameController.Instance.Lanes.Count - 1));
                    allowInput = false;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    // Move one lane to the right
                    MoveToLane(Mathf.Clamp(GameController.Instance.CurrentLane + 1, 0, GameController.Instance.Lanes.Count - 1));
                    allowInput = false;
                }
            }

            if (moving)
            {
                float step = Mathf.Clamp(GameController.Instance.GameSpeed * 2,
                    GameController.Instance.PlayerMinSpeed,
                    GameController.Instance.PlayerMaxSpeed) * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, transformTarget.position, step);

                if (Vector3.Distance(transform.position, transformTarget.position) < 2f)
                {
                    allowInput = true;
                }
                if (Vector3.Distance(transform.position, transformTarget.position) < 0.001f)
                {
                    moving = false;
                }
            }
        }
    }

    void MoveToLane(int laneNumber)
    {
        moving = true;
        GameController.Instance.CurrentMoves++;
        GameController.Instance.CurrentLane = laneNumber;
        transformTarget = GameController.Instance.Lanes[laneNumber].PlayerPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            Instantiate(GameController.Instance.PickupParticlesPrefab, other.transform.position, Quaternion.identity);
            GameController.Instance.CurrentScore++;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Obstacle"))
        {
            GameController.Instance.GameOver();
            Destroy(other.gameObject);
        }
    }
}
