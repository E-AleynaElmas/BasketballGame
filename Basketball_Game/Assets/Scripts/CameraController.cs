using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public Vector3 offset;
    private bool x;
    [SerializeField]
    Transform startPos;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        else
            Instance = this;
    }
    private void Update()
    {
        if (x)
        {
            transform.position = startPos.position + offset;
        }
    }
    public void CameraPosEditor()
    {
        x = true;
    }
}
