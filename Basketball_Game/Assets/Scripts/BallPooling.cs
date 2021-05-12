using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPooling : MonoBehaviour
{
    public static BallPooling Instance;

    public enum BallType
    {
        ball1,
        ball2
    }

    private BallType currentLevelBallType;

    [SerializeField]
    private GameObject ballPrefab1, ballPrefab2;

    private GameObject ballParent1, ballParent2;

    private List<GameObject> ballPoolType1 = new List<GameObject>();
    private List<GameObject> ballPoolType2 = new List<GameObject>();

    [SerializeField]
    private int poolStartSize = 10;

    bool isUsingBallTyep1, isUsingBallTyep2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        else
            Instance = this;

        BallGenerator();
    }

    private void BallGenerator()
    {
        GameObject newBall1;
        GameObject newBall2;

        ballParent1 = new GameObject("BallPool1");
        ballParent2 = new GameObject("BallPool2");

        Vector3 newStartPosObj = Vector3.zero;

        for (int i = 0; i < poolStartSize; i++)
        {
            newBall1 = Instantiate(ballPrefab1, newStartPosObj, Quaternion.identity);
            newBall1.name = "1newBall" + (i + 1);
            ballPoolType1.Add(newBall1);
            newBall1.transform.parent = ballParent1.transform;
            newBall1.SetActive(false);
        }

        for (int i = 0; i < poolStartSize; i++)
        {
            newBall2 = Instantiate(ballPrefab2, newStartPosObj, Quaternion.identity);
            newBall2.name = "2newBall" + (i + 1);
            ballPoolType2.Add(newBall2);
            newBall2.transform.parent = ballParent2.transform;
            newBall2.SetActive(false);
        }
    }

    public GameObject UsingBall()
    {
        if (currentLevelBallType == BallType.ball1)
        {
            isUsingBallTyep1 = true;
            return UsingBall1();
        }

        else if (currentLevelBallType == BallType.ball2)
        {
            isUsingBallTyep2 = true;
            return UsingBall2();
        }

        else
            return null;
    }

    private GameObject UsingBall1()
    {
        GameObject useBall1;

        if (ballPoolType1.Count > 0 || ballPoolType1[0].activeSelf)
        {
            useBall1 = ballPoolType1[0];
            ballPoolType1.RemoveAt(0);
            ballPoolType1.Add(useBall1);
        }
        else
        {
            useBall1 = Instantiate(ballPrefab1, Vector3.zero, Quaternion.identity);
            useBall1.name = "1newBall" + ballPoolType1.Count;
            ballPoolType1.Add(useBall1);
            useBall1.transform.parent = ballParent1.transform;
            useBall1.SetActive(false);
        }

        return useBall1;
    }

    private GameObject UsingBall2()
    {
        GameObject useBall2;

        if (ballPoolType2.Count > 0 || ballPoolType2[0].activeSelf)
        {
            useBall2 = ballPoolType2[0];
            ballPoolType2.RemoveAt(0);
            ballPoolType2.Add(useBall2);
        }
        else
        {
            useBall2 = Instantiate(ballPrefab2, Vector3.zero, Quaternion.identity);
            useBall2.name = "2newBall" + ballPoolType2.Count;
            ballPoolType2.Add(useBall2);
            useBall2.transform.parent = ballParent2.transform;
            useBall2.SetActive(false);
        }

        return useBall2;
    }

    public void BallReset(GameObject prevBall)
    {
        Rigidbody prevBallRB = prevBall.GetComponent<Rigidbody>();
        prevBall.GetComponent<SwipeDetection>().SetIsShoot(false);

        prevBall.SetActive(false);

        prevBall.transform.position = Vector3.zero;
        prevBall.transform.rotation = Quaternion.identity;
        prevBallRB.velocity = Vector3.zero;
        prevBallRB.angularVelocity = Vector3.zero;
    }

    public void PoolingReset()
    {
        GameObject ballPoolItem1;
        GameObject ballPoolItem2;

        int ballPoolCount1 = ballPoolType1.Count;
        int ballPoolCount2 = ballPoolType2.Count;


        if (isUsingBallTyep1)
        {
            for (int i = 0; i < ballPoolCount1; i++)
            {
                ballPoolItem1 = ballPoolType1[i];

                BallReset(ballPoolItem1);
            }
        }

        if (isUsingBallTyep2)
        {
            for (int i = 0; i < ballPoolCount2; i++)
            {
                ballPoolItem2 = ballPoolType2[i];

                BallReset(ballPoolItem2);
            }
        }

        isUsingBallTyep1 = false;
        isUsingBallTyep2 = false;
    }

    public void CurrentLevelBallTypeEditor(BallType LevelBallType)
    {
        currentLevelBallType = LevelBallType;
    }
}
