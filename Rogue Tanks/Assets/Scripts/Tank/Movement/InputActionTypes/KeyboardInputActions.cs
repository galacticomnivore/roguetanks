using System;
using UnityEngine;

public class KeyboardInputActions : IInputActionController
{
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    private readonly Transform transform;
    public KeyboardInputActions(Transform transform) => this.transform = transform;

    private bool isEnabled = true;
    private Direction direction = Direction.Up;
    private Direction slidingDirection = Direction.Up;

    private int slideSignal = 0;
    private int totalSlidesCount = 0;
    private bool isSliding = false;
    private bool slidingIsPaused = false;

    private bool hasCalculatedDistance = false;
    private float distance = 0;
    public IInputActionController Up(Action onUp)
    {
        if (!isEnabled) return this;

        if(slidingIsPaused && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8)))
            slidingIsPaused = false;

        if (!isSliding)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Keypad8))
            {
                direction = Direction.Up;
            }
            
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8))  && direction == Direction.Up)
                onUp();
            if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Keypad8)) && slideSignal > 0 && totalSlidesCount != 0 && direction == Direction.Up)
            {
                slidingDirection = Direction.Up;
                slideSignal--;
                isSliding = true;
            }
            if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8)) && direction == Direction.Up)
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6))
                {
                    direction = Direction.Right;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4))
                {
                    direction = Direction.Left;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5))
                {
                    direction = Direction.Down;
                    hasCalculatedDistance = false;
                }
            }
        }
        else if (isSliding && slidingDirection == Direction.Up)
        {
            if (!hasCalculatedDistance)
            {
                distance = (transform.position.y + 4f).RoundToNearestEven();
                hasCalculatedDistance = true;
            }

            if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8)) && slidingDirection == Direction.Up)
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6))
                {
                    slidingDirection = Direction.Right;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4))
                {
                    slidingDirection = Direction.Left;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5))
                {
                    slidingDirection = Direction.Down;
                    hasCalculatedDistance = false;
                }
            }

            if (transform.position.y <= distance && !slidingIsPaused)
                onUp();
            else
            {
                isSliding = false;
                hasCalculatedDistance = false;
            }
        }

        return this;
    }

    public IInputActionController Down(Action onDown)
    {
        if (!isEnabled) return this;

        if(slidingIsPaused && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5)))
                slidingIsPaused = false;

        if (!isSliding)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Keypad5))
            {
                direction = Direction.Down;
            }

            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5)) && direction == Direction.Down)
                onDown();
            if ((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.Keypad5)) && slideSignal > 0 && totalSlidesCount != 0 && direction == Direction.Down)
            {
                slidingDirection = Direction.Down;
                slideSignal--;
                isSliding = true;
            }
            if (!(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5)) && direction == Direction.Down)
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8))
                {
                    direction = Direction.Up;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4))
                {
                    direction = Direction.Left;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6))
                {
                    direction = Direction.Right;
                    hasCalculatedDistance = false;
                }
            }
        }
        else if (isSliding && slidingDirection == Direction.Down)
        {
            if (!hasCalculatedDistance)
            {
                distance = (transform.position.y - 4f).RoundToNearestEven();
                hasCalculatedDistance = true;
            }

            if (!(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5)) && slidingDirection == Direction.Down)
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8))
                {
                    slidingDirection = Direction.Up;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4))
                {
                    slidingDirection = Direction.Left;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6))
                {
                    slidingDirection = Direction.Right;
                    hasCalculatedDistance = false;
                }
            }

            if (transform.position.y >= distance && !slidingIsPaused)
                onDown();
            else
            {
                isSliding = false;
                hasCalculatedDistance = false;
            }
        }

        return this;
    }

    public IInputActionController Left(Action onLeft)
    {
        if (!isEnabled) return this;

        if (slidingIsPaused && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4)))
                slidingIsPaused = false;

        if (!isSliding)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                direction = Direction.Left;
            }

            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4)) && direction == Direction.Left)
                onLeft();
            if ((Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.Keypad4)) && slideSignal > 0 && totalSlidesCount != 0 && direction == Direction.Left)
            {
                slidingDirection = Direction.Left;
                slideSignal--;
                isSliding = true;
            }
            if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4)) && direction == Direction.Left)
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6))
                {
                    direction = Direction.Right;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8))
                {
                    direction = Direction.Up;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5))
                {
                    direction = Direction.Down;
                    hasCalculatedDistance = false;
                }
            }
        }
        else if (isSliding && slidingDirection == Direction.Left)
        {
            if (!hasCalculatedDistance)
            {
                distance = (transform.position.x - 4f).RoundToNearestEven();
                hasCalculatedDistance = true;
            }

            if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4)) && slidingDirection == Direction.Left)
            {
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6))
                {
                    slidingDirection = Direction.Right;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8))
                {
                    slidingDirection = Direction.Up;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5))
                {
                    slidingDirection = Direction.Down;
                    hasCalculatedDistance = false;
                }
            }

            if (transform.position.x >= distance && !slidingIsPaused)
                onLeft();
            else
            {
                isSliding = false;
                hasCalculatedDistance = false;
            }
        }

        return this;
    }

    public IInputActionController Right(Action onRight)
    {
        if (!isEnabled) return this;

        if (slidingIsPaused && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6)))
                slidingIsPaused = false;

        if (!isSliding)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                direction = Direction.Right;
            }

            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6)) && direction == Direction.Right)
                onRight();
            if ((Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Keypad6)) && slideSignal > 0 && totalSlidesCount != 0 && direction == Direction.Right)
            {
                slidingDirection = Direction.Right;
                slideSignal--;
                isSliding = true;
            }
            if (!(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad6)) && direction == Direction.Right)
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4))
                {
                    direction = Direction.Left;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8))
                {
                    direction = Direction.Up;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5))
                {
                    direction = Direction.Down;
                    hasCalculatedDistance = false;
                }
            }
        }
        else if (isSliding && slidingDirection == Direction.Right)
        {
            if (!hasCalculatedDistance)
            {
                distance = (transform.position.x + 4f).RoundToNearestEven();
                hasCalculatedDistance = true;
            }

            if (!(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Keypad4)) && slidingDirection == Direction.Right)
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Keypad4))
                {
                    slidingDirection = Direction.Left;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Keypad8))
                {
                    slidingDirection = Direction.Up;
                    hasCalculatedDistance = false;
                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Keypad5))
                {
                    slidingDirection = Direction.Down;
                    hasCalculatedDistance = false;
                }
            }

            if (transform.position.x <= distance && !slidingIsPaused)
                onRight();
            else
            {
                isSliding = false;
                hasCalculatedDistance = false;
            }
        }

        return this;
    }

    public IInputActionController Fire(Action onFire)
    {
        if (isEnabled && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad0))
            onFire();
        return this;
    }

    public void SlideIn()
    {
        slideSignal++;
        totalSlidesCount++;
    }
    public void SlideOut()
    {
        totalSlidesCount--;
        if (totalSlidesCount == 0)                                                                      //koristi se kuga korisniko e zadrzal UP srelkata i ide prez nekolku ice tiles
        {                                                                                               //i presmetkata kazuva oti treba da zastane nad ice-o
            isSliding = false;
            hasCalculatedDistance = false;
        }
    }

    public void Enable() => isEnabled = true;
    public void Disable() => isEnabled = false;

    public void ObstacleDetected()
    {
        slidingIsPaused = true;
    }

    public void Reset()
    {
        slidingIsPaused= isSliding= hasCalculatedDistance = false;
        totalSlidesCount = slideSignal = 0;
        distance = 0;
    }
}
