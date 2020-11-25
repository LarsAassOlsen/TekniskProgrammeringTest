using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public MainCanvasController MainCanvasController;

    public GameObject PlayerObject;
    public Transform LaneParent;
    public Transform PickupObstacleParent;
    public List<LaneScript> Lanes = new List<LaneScript>();

    [Header("Prefabs")]
    public GameObject PlayerPrefab;
    public GameObject ObstaclePrefab;
    public GameObject PickupPrefab;
    public GameObject LanePrefab;
    public GameObject PickupParticlesPrefab;

    [Header("UI")]
    public TextMeshProUGUI ScoreText;

    [Header("Game Stats")]
    public int CurrentHighScore = 0;
    public int CurrentScore = 0;
    public int CurrentMoves = 0;

    public bool GameStarted = false;
    public bool GameRunning = false;
    public bool GameEnded = true;
    public int CurrentLane = 0;
    public float GameSpeed = 1;
    public float GameRunningTime = 0;

    [Header("Game Settings")]
    public float ObstacleSpawnCooldown;
    public float GameSpeedMin;
    public float GameSpeedMax;

    public float PlayerMinSpeed;
    public float PlayerMaxSpeed;

    public int LaneAmountMin = 3;
    public int LaneAmountMax = 64;

    public float SpawnTimeRangeMin;
    public float SpawnTimeRangeMax;

    private void Awake()
    {
        Instance = this;

        CurrentHighScore = PlayerPrefs.GetInt("HighScore", 0);
    }


    void Update()
    {
        if (GameRunning)
        {
            GameRunningTime += Time.deltaTime;
            GameSpeed = Mathf.Clamp(GameSpeedMin + (GameRunningTime / 5), GameSpeedMin, GameSpeedMax);
            ScoreText.text = "Score: " + CurrentScore;
        }
        if (!GameEnded && GameStarted)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause(GameRunning);
            }
        }
    }

    public void StartGame(int laneAmount)
    {
        SpawnLanes(laneAmount);
        SpawnPlayer(Lanes[laneAmount / 2]);
        CurrentLane = laneAmount / 2;
        CurrentScore = 0;
        GameRunningTime = 0;
        GameStarted = true;
        GameRunning = true;
        GameEnded = false;
    }

    public void TogglePause(bool pause)
    {
        if (pause)
        {
            GameRunning = false;
        }
        else
        {
            GameRunning = true;
        }
        MainCanvasController.TogglePauseMenu(pause);
        MainCanvasController.ToggleInGameUI(!pause);
    }

    public void GameOver()
    {
        if(CurrentScore > CurrentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", CurrentScore);
            CurrentHighScore = CurrentScore;
            MainCanvasController.ToggleGameOverMenu(true, true);
        }
        else
        {
            MainCanvasController.ToggleGameOverMenu(true,false);
        }
        GameStarted = false;
        GameRunning = false;
        GameEnded = true;
    } 

    void SpawnLanes(int laneAmount)
    {
        for (int i = 0; i < laneAmount; i++)
        {
            GameObject instantiatedLane = Instantiate(LanePrefab, new Vector3(i * 4.0F, LaneParent.position.y, LaneParent.position.z), Quaternion.identity, LaneParent);
            instantiatedLane.GetComponent<LaneScript>().PickupObstacleSpawner.SpawnerIndex = i;
            Lanes.Add(instantiatedLane.GetComponent<LaneScript>());
        }
        LaneParent.position = new Vector3(-laneAmount * 4.0F / laneAmount, LaneParent.position.y, LaneParent.position.z);
    }

    void SpawnPlayer(LaneScript laneToSpawnIn)
    {
        GameObject instantiatedPlayer = Instantiate(PlayerPrefab, laneToSpawnIn.PlayerPoint.position, Quaternion.identity);
        PlayerObject = instantiatedPlayer;
    }

    public void DeSpawnEverything()
    {
        foreach(Transform child in LaneParent)
        {
            Destroy(child.gameObject);
        }
        foreach(Transform child in PickupObstacleParent)
        {
            Destroy(child.gameObject);
        }
        Lanes.Clear();
        Destroy(PlayerObject);
    }
}
