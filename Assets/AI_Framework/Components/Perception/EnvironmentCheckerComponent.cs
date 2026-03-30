using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentCheckerComponent : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsWalkable;
    [SerializeField] private LayerMask whatIsWalls;

    [SerializeField] private Collider2D enemyCollider;
    [SerializeField] private float groundRayLength;
    [SerializeField] private float wallsRayLength;

    [SerializeField] private EntityMediator enemy;

    private bool IsGrounded;
    private bool IsWallAhead;
    private bool IsLedgeAhead;

    private bool isGroundedLeft;
    private bool isGroundedRight;

    private void Update()
    {
        enemy.E_Context.IsGrounded = IsGrounded; 
        enemy.E_Context.IsWallAhead = IsWallAhead; 
        enemy.E_Context.IsLedgeAhead = IsLedgeAhead; 
    }

    private void FixedUpdate()
    {
        CheckGround();
        CheckWalls(enemy.E_Movement.FacingDirection);
        CheckLedges();
    }

    private void CheckGround()
    {
        Vector2 leftRayPos = new Vector2(enemyCollider.bounds.min.x, enemyCollider.bounds.min.y);
        Vector2 rightRayPos = new Vector2(enemyCollider.bounds.max.x, enemyCollider.bounds.min.y);

        isGroundedLeft = Physics2D.Raycast(leftRayPos, Vector2.down, groundRayLength, whatIsWalkable);
        isGroundedRight = Physics2D.Raycast(rightRayPos, Vector2.down, groundRayLength, whatIsWalkable);

        Debug.DrawRay(leftRayPos, Vector2.down * groundRayLength, Color.red);
        Debug.DrawRay(rightRayPos, Vector2.down * groundRayLength, Color.red);


        IsGrounded = isGroundedLeft || isGroundedRight;
    }

    private void CheckWalls(float dir)
    {
        Vector2 origin = dir > 0
        ? new(enemyCollider.bounds.max.x, enemyCollider.bounds.center.y)
        : new(enemyCollider.bounds.min.x, enemyCollider.bounds.center.y);

        Vector2 direction = dir > 0 ? Vector2.right : Vector2.left;

        bool hit = Physics2D.Raycast(origin, direction, wallsRayLength, whatIsWalls);

        Debug.DrawRay(origin, direction * wallsRayLength, Color.yellow);

        IsWallAhead = hit;
    }

    private void CheckLedges()
    {
        if (enemy.E_Movement.FacingDirection > 0)
            IsLedgeAhead = !isGroundedRight;
        else
            IsLedgeAhead = !isGroundedLeft;
    }
}
