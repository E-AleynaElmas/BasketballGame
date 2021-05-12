using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public float zForce = 0;
    public float yForce = 0;

    [SerializeField]
    GameObject ballParent;

    [SerializeField]
    Transform startPos;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        else
            Instance = this;
    }

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevelNumber");
        if(currentLevel == 0)
        {
            PlayerPrefs.SetInt("currentLevelNumber", 1);
        }        
        LevelDesign(PlayerPrefs.GetInt("currentLevelNumber"));
    }

    private void LevelDesign(int level)
    {
        if (level > 10)
        {
            PlayerPrefs.SetInt("currentLevelNumber", 1);
        }

        int randomSpawnSize = Random.Range(10, 15);

        Debug.Log(level);
        if (level == 1)
        {
            startPos.transform.position = new Vector3(0, 0.3f, 12f);
            Spawner.Instance.spawnSize = randomSpawnSize;
            BallPooling.Instance.CurrentLevelBallTypeEditor(BallPooling.BallType.ball1);
            Restart();
        }
        else if (level == 2)
        {
            startPos.transform.position = new Vector3(0, 0.3f, 8f);
            Spawner.Instance.spawnSize = randomSpawnSize;
            yForce = 5;
            zForce = 10;
            BallPooling.Instance.CurrentLevelBallTypeEditor(BallPooling.BallType.ball1);

            Restart();
        }
        else if (level == 3)
        {
            /*Top değişecek*/
            startPos.transform.position = new Vector3(0, 0.3f, 12f);
            Spawner.Instance.spawnSize = randomSpawnSize;
            yForce = 5;
            zForce = 9;
            BallPooling.Instance.CurrentLevelBallTypeEditor(BallPooling.BallType.ball2);

            Restart();
        }
        else if (level == 4)
        {
            startPos.transform.position = new Vector3(0, 0.3f, 14f);
            Spawner.Instance.spawnSize = randomSpawnSize;
            zForce = -10;
            BallPooling.Instance.CurrentLevelBallTypeEditor(BallPooling.BallType.ball1);

            Restart();
        }
        else if (level == 5)
        {
            startPos.transform.position = new Vector3(0, 0.3f, 9f);
            Spawner.Instance.spawnSize = randomSpawnSize;
            yForce = 5;
            zForce = 10;
            BallPooling.Instance.CurrentLevelBallTypeEditor(BallPooling.BallType.ball1);

            Restart();
        }
        else if (level == 6)
        {
            startPos.transform.position = new Vector3(0, 0.3f, 4f);
            Spawner.Instance.spawnSize = randomSpawnSize;
            BallPooling.Instance.CurrentLevelBallTypeEditor(BallPooling.BallType.ball1);

            Restart();
        }
        else if (level == 7)
        {
            /*Hafif bir top farklı renk*/
            startPos.transform.position = new Vector3(0, 0.3f, 4f);
            Spawner.Instance.spawnSize = randomSpawnSize;
            yForce = 10;
            zForce = 25;
            BallPooling.Instance.CurrentLevelBallTypeEditor(BallPooling.BallType.ball2);

            Restart();
        }
        else if (level == 8)
        {
            startPos.transform.position = new Vector3(0, 0.3f, 7f);
            Spawner.Instance.spawnSize = randomSpawnSize;
            yForce = 5;
            zForce = 15;
            BallPooling.Instance.CurrentLevelBallTypeEditor(BallPooling.BallType.ball1);

            Restart();
        }
        else if (level == 9)
        {
            startPos.transform.position = new Vector3(0, 0.3f, 10f);
            Spawner.Instance.spawnSize = randomSpawnSize;
            yForce = 3;
            zForce = 7;
            BallPooling.Instance.CurrentLevelBallTypeEditor(BallPooling.BallType.ball1);

            Restart();
        }
        else if (level == 10)
        {
            startPos.transform.position = new Vector3(0, 0.3f, 6f);
            Spawner.Instance.spawnSize = randomSpawnSize;
            yForce = 5;
            zForce = 25;
            BallPooling.Instance.CurrentLevelBallTypeEditor(BallPooling.BallType.ball1);

            Restart();
        }

        CameraController.Instance.CameraPosEditor();
    }

    private void Restart()
    {
        GameController.Instance.scoreCounter = 0;
        GameController.Instance.WriteScore();
        GameController.Instance.WriteSpawnSize();
        BallPooling.Instance.PoolingReset();

        GameObject newBall = BallPooling.Instance.UsingBall();
        newBall.transform.position = startPos.position;
        newBall.SetActive(true);
    }

    private void OnEnable()
    {
        GameEvent.OnNextLevel += LevelDesign;
    }

    private void OnDisable()
    {
        GameEvent.OnNextLevel -= LevelDesign;
    }
}
