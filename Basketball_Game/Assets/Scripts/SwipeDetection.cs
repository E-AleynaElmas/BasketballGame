using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class SwipeDetection : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float throwForceInX = 1f, throwForceInY = 1f, throwForceInZ = 50f;
    private float touchTimeStart, touchTimeEnd, timeInterval;

    private Vector2 startPos, endPos, direction;

    private bool isShoot;
    public bool isFall;
    private bool endBall;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (ButtonHandler.Instance.IsPlay)
        {
            Touch();
        }
    }

    private void Touch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchTimeStart = Time.time;
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchTimeEnd = Time.time;
            timeInterval = touchTimeEnd - touchTimeStart;

            endPos = Input.mousePosition;

            direction = startPos - endPos;

            Shoot();
        }
    }

    private void Shoot()
    {
        float SpeedY = LevelController.Instance.yForce;
        float SpeedZ = LevelController.Instance.zForce;

        float xForce = Mathf.Clamp(-direction.x * throwForceInX, -10, 10);
        float yForce = Mathf.Clamp(-direction.y * throwForceInY, 100, 600 + SpeedY);
        float zForce = ((throwForceInZ + SpeedZ) / timeInterval);

        if (isShoot)
            return;

        rb.AddForce(xForce, yForce, zForce);
        Spawner.Instance.NewSpawnRequest();
        StartCoroutine(FallBall());

        isShoot = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "scoreArea")
        {
            Debug.Log("collision");
            GameEvent.OnScoreMethod();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && endBall && isShoot)
        {
            endBall = false;
            GameEvent.OnEndLevelMethod();
        }
    }

    IEnumerator FallBall()
    {
        yield return new WaitForSeconds(5);
        BallPooling.Instance.BallReset(this.gameObject);
    }

    public void SetIsShoot(bool isShootType)
    {
        isShoot = isShootType;
    }

    public void EndBallActive()
    {
        endBall = true;
    }
}

