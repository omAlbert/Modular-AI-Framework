using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionComponent : MonoBehaviour
{
    [Header("Vision")]
    [Space(5f)]
    [SerializeField] private float visionRange;
    [SerializeField] private float viewAngle;

    [Space(10f)]
    [Header("Direction")]
    [Space(5f)]
    [SerializeField] private VisionDirection visionDirection;

    [Space(10f)]

    [SerializeField] private EntityMediator enemy;



    [SerializeField] float alertRadius = 8f;

    [SerializeField] private LayerMask layers;

    private GameObject player;
    public GameObject playerGameObject => player;

    private bool IsPlayerVisible;

    public Vector2 LastKnownPlayerPosition { get; private set; }

    private float viewThreshold;

    public event Action OnPlayerDetected;

    private void Awake()
    {
        viewThreshold = Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad);
    }

    private void Start()
    {
        player = PlayerController.instance.PlayerGameObject;
    }

    private void Update()
    {
        if (IsPlayerInAlertRange())
            LastKnownPlayerPosition = player.transform.position;

        IsPlayerInVision();



        enemy.E_Context.IsPlayerVisible = IsPlayerVisible;
        enemy.E_Context.IsPlayerInAlertRange = IsPlayerInAlertRange();
    }

    private void IsPlayerInVision()
    {
        if (player == null)
        {
            IsPlayerVisible = false;
            return;
        }

        Vector2 rawDirection = player.transform.position - transform.position;
        float sqrDist = rawDirection.sqrMagnitude;

        if (sqrDist > visionRange * visionRange)
        {
            IsPlayerVisible = false;
            return;
        }

        Vector2 visionDir = GetVisionForward();

        /*if (ignoreVerticalAngle)
        {
            visionDir.y = 0f;

            if (visionDir.sqrMagnitude < 0.001f)
            {
                IsPlayerVisible = false;
                return;
            }
        }*/

        visionDir.Normalize();

        //Vector2 facingDir = new Vector2(enemy.E_Movement.FacingDirection, 0f);
        float dot = Vector2.Dot(visionDir, rawDirection.normalized);

        if (dot < viewThreshold)
        {
            IsPlayerVisible = false;
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            rawDirection.normalized,
            visionRange,
            layers
        );

        Debug.DrawRay(transform.position, rawDirection.normalized * visionRange, Color.green);

        bool newValue = hit && hit.transform.CompareTag("Player");

        if (!IsPlayerVisible && newValue)
            OnPlayerDetected?.Invoke();

        IsPlayerVisible = newValue;
    }

    private Vector2 GetVisionForward()
    {
        return visionDirection switch
        {
            VisionDirection.Forward => new Vector2(enemy.E_Movement.FacingDirection, 0f),
            VisionDirection.Up => Vector2.up,
            VisionDirection.Down => Vector2.down,
            _ => Vector2.right
        };
    }
    private bool IsPlayerInAlertRange()
    {
        float sqrDist = (player.transform.position - transform.position).sqrMagnitude;
        return sqrDist <= alertRadius * alertRadius;
    }

    public bool IsPlayerInRange(float range)
    {
        float sqrRange = range * range;
        return (player.transform.position - transform.position).sqrMagnitude <= sqrRange;
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 facing = new Vector2(enemy.E_Movement.FacingDirection, 0f);
        float halfAngle = viewAngle * 0.5f;

        Vector2 left = Quaternion.Euler(0, 0, halfAngle) * GetVisionForward();
        Vector2 right = Quaternion.Euler(0, 0, -halfAngle) * GetVisionForward();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(left * visionRange));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(right * visionRange));
    }
}

public enum VisionDirection
{
    Forward,
    Up,
    Down
}
