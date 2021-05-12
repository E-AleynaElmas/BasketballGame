using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField]
    Text scoreCounterText, ballSize, panelScoreText;

    [SerializeField]
    GameObject continuePopUp, startPanel;

    WaitForSeconds popupDelay;

    public int scoreCounter = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        else
            Instance = this;
    }

    private void Start()
    {
        popupDelay = new WaitForSeconds(0.3f);

        startPanel.SetActive(true);
        scoreCounterText.text = scoreCounter.ToString();
        ballSize.text = "x" + Spawner.Instance.spawnSize.ToString();
    }

    private void OnScore()
    {
        scoreCounter++;
        scoreCounterText.text = scoreCounter.ToString();
    }

    private void OnShoot(int spawnSize)
    {
        ballSize.text = "x" + spawnSize.ToString();
    }

    private void OnEndLevel()
    {
        panelScoreText.text = scoreCounter.ToString();
        StartCoroutine(PopUpActivator());
    }

    public void WriteScore()
    {
        scoreCounterText.text = scoreCounter.ToString();
    }

    public void WriteSpawnSize()
    {
        ballSize.text = "x" + Spawner.Instance.spawnSize.ToString();
    }

    private void OnEnable()
    {
        GameEvent.OnScore += OnScore;
        GameEvent.OnShoot += OnShoot;
        GameEvent.OnEndLevel += OnEndLevel;
    }

    private void OnDisable()
    {
        GameEvent.OnScore -= OnScore;
        GameEvent.OnShoot -= OnShoot;
        GameEvent.OnEndLevel -= OnEndLevel;
    }

    private IEnumerator PopUpActivator()
    {
        yield return popupDelay;
        continuePopUp.SetActive(true);
    }
}
