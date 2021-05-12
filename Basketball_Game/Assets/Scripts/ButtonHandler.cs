using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public static ButtonHandler Instance;

    public GameObject level1, level2;
    public bool IsPlay;

    WaitForSeconds startDelay;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        else
            Instance = this;
    }

    private void Start()
    {
        startDelay = new WaitForSeconds(0.5f);
        IsPlay = false;
    }

    public void NextLevel()
    {
        LevelCountEditor();
        int currentLevelNumber = PlayerPrefs.GetInt("currentLevelNumber");
        GameEvent.OnNextLevelMethod(currentLevelNumber);
    }

    public void PlayButton()
    {
        StartCoroutine(StartWait());
    }

    public void SetActiveFalse(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void LevelCountEditor()
    {
        int currentLevelNumber = PlayerPrefs.GetInt("currentLevelNumber");
        PlayerPrefs.SetInt("currentLevelNumber", ++currentLevelNumber);
    }

    IEnumerator StartWait()
    {
        yield return startDelay;
        IsPlay = true;
    }
}
