using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Idle Sprites")]
    [SerializeField] private Sprite frontIdle;
    [SerializeField] private Sprite backIdle;
    [SerializeField] private Sprite sideIdle;

    [Header("Walk Sprites")]
    [SerializeField] private Sprite[] frontWalkSprites;
    [SerializeField] private Sprite[] backWalkSprites;
    [SerializeField] private Sprite[] sideWalkSprites;

    [Header("Animation Settings")]
    [SerializeField] private float frameRate = 12f;

    private float frameTimer;
    private int currentFrame;

    private Direction currentDirection = Direction.Front;

    private enum Direction
    {
        Front,
        Back,
        Side
    }

    private void Update()
    {
        if (playerMove == null || spriteRenderer == null) return;

        UpdateDirection();

        if (playerMove.IsMoving)
        {
            PlayWalkAnimation();
        }
        else
        {
            ShowIdleSprite();
        }
    }

    private void UpdateDirection()
    {
        Vector3 dir = playerMove.LastMoveDirection;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            currentDirection = Direction.Side;

            if (dir.x > 0f)
            {
                spriteRenderer.flipX = false;
            }
            else if (dir.x < 0f)
            {
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            spriteRenderer.flipX = false;

            if (dir.z > 0f)
            {
                currentDirection = Direction.Back;
            }
            else if (dir.z < 0f)
            {
                currentDirection = Direction.Front;
            }
        }
    }

    private void PlayWalkAnimation()
    {
        Sprite[] sprites = GetCurrentWalkSprites();

        if (sprites == null || sprites.Length == 0)
        {
            ShowIdleSprite();
            return;
        }

        frameTimer += Time.deltaTime;

        if (frameTimer >= 1f / frameRate)
        {
            frameTimer = 0f;
            currentFrame++;

            if (currentFrame >= sprites.Length)
            {
                currentFrame = 0;
            }
        }

        spriteRenderer.sprite = sprites[currentFrame];
    }

    private void ShowIdleSprite()
    {
        frameTimer = 0f;
        currentFrame = 0;

        switch (currentDirection)
        {
            case Direction.Front:
                spriteRenderer.sprite = frontIdle;
                break;

            case Direction.Back:
                spriteRenderer.sprite = backIdle;
                break;

            case Direction.Side:
                spriteRenderer.sprite = sideIdle;
                break;
        }
    }

    private Sprite[] GetCurrentWalkSprites()
    {
        switch (currentDirection)
        {
            case Direction.Front:
                return frontWalkSprites;

            case Direction.Back:
                return backWalkSprites;

            case Direction.Side:
                return sideWalkSprites;

            default:
                return frontWalkSprites;
        }
    }
}