using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] CompositeCollider2D tilemapCollider;

    Camera mainCamera;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (tilemapCollider != null)
        {
            float halfHeight = mainCamera.orthographicSize;
            float halfWidth = mainCamera.aspect * halfHeight;

            float x = tilemapCollider.bounds.center.x;
            float y = tilemapCollider.bounds.center.y;
            float width = tilemapCollider.bounds.size.x / 2;
            float height = tilemapCollider.bounds.size.y / 2;

            if (player != null)
            {
                transform.position = new Vector3(
                    Mathf.Clamp(player.position.x, x - width + halfWidth, x + width - halfWidth),
                    Mathf.Clamp(player.position.y, y - height + halfHeight, y + height - halfHeight),
                    transform.position.z);
            }

        }
    }

    // Camera follows player
    /* void Update()
    {
        if (GameManager.instance.isGameActive)
        {
            transform.position = player.position + new Vector3(0, 0, -10);
        }
    } */
}
