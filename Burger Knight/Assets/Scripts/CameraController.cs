using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector2 offset;

    public static CameraController Instance;

    private PolygonCollider2D cameraBounds;
    private Camera cam;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        cam = GetComponent<Camera>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayer();
        FindBounds();
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void FindBounds()
    {
        GameObject boundsObj = GameObject.FindGameObjectWithTag("CameraBounds");
        if (boundsObj != null)
        {
            cameraBounds = boundsObj.GetComponent<PolygonCollider2D>();
        }
        else
        {
            cameraBounds = null;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 desiredPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        if (cameraBounds != null)
        {
            desiredPosition = ClampToBounds(desiredPosition);
        }

        transform.position = desiredPosition;
    }

    Vector3 ClampToBounds(Vector3 desiredPosition)
    {
        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * cam.aspect;

        Bounds bounds = cameraBounds.bounds;

        float minX = bounds.min.x + horzExtent;
        float maxX = bounds.max.x - horzExtent;
        float minY = bounds.min.y + vertExtent;
        float maxY = bounds.max.y - vertExtent;

        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        return new Vector3(clampedX, clampedY, desiredPosition.z);
    }
}