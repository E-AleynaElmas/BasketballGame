using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    public GameObject spawnObject;

    private float newSpawnDuration = 1f;

    public int spawnSize;

    public bool lastFall;

    [SerializeField]
    Transform startPos;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        else
            Instance = this;
    }

    void SpawnNewObject()
    {
        spawnSize--;
        GameEvent.OnShootMethod(spawnSize);

        if (spawnSize > 0)
        {
            GameObject newBall = BallPooling.Instance.UsingBall();
            newBall.transform.position = startPos.position;
            newBall.SetActive(true);
            if (spawnSize == 1)
            {
                newBall.GetComponent<SwipeDetection>().EndBallActive();
            }
        }
    }

    public void NewSpawnRequest()
    {
        Invoke("SpawnNewObject", newSpawnDuration);
    }
}

