using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private PlayerControls playerControls;
    private Vector2 moveInput = Vector2.zero;
    private Vector2 lastPerformedInput = Vector2.zero;
    private Vector2 lastReleasedInput = Vector2.zero;
    private bool firePressed = false;

    private bool isEnabled = true;
    private Direction direction = Direction.Up;
    private Direction slidingDirection = Direction.Up;

    private int slideSignal = 0;
    private int totalSlidesCount = 0;
    private bool isSliding = false;
    private bool slidingIsPaused = false;

    private bool hasCalculatedDistance = false;
    private float distance = 0;

    public KeyboardInputActions(Transform transform)        // Constructor: Initializes input actions and event handlers for movement and fire.
    {
        this.transform = transform;
        playerControls = new PlayerControls();   // Sets up PlayerControls and binds movement and fire events.


        playerControls.Gameplay.Move.performed += ctx =>
        {
            lastPerformedInput = ctx.ReadValue<Vector2>();      // Updates moveInput and lastPerformedInput on movement.
            moveInput = lastPerformedInput;
        };

        playerControls.Gameplay.Move.canceled += ctx =>
        {
            lastReleasedInput = lastPerformedInput;     // Updates lastReleasedInput and resets moveInput on movement cancel.
            moveInput = Vector2.zero;                  
        };

        playerControls.Gameplay.Fire.performed += ctx =>
        {
            firePressed = true;                       // Sets firePressed when fire action is performed.
        };

        playerControls.Enable();   // Enables the input controls.
    }

    public IInputActionController Up(Action onUp)
    {
        if (!isEnabled) return this;

        if (slidingIsPaused && moveInput.y > 0.5f)
            slidingIsPaused = false;

        if (!isSliding)
        {
            if (moveInput.y > 0.5f)
                direction = Direction.Up;

            if (moveInput.y > 0.5f && direction == Direction.Up)
                onUp();

            if (lastReleasedInput.y > 0.5f && slideSignal > 0 && totalSlidesCount != 0 && direction == Direction.Up)
            {
                slidingDirection = Direction.Up;
                slideSignal--;
                isSliding = true;
                lastReleasedInput = Vector2.zero;
            }

            if (moveInput == Vector2.zero && direction == Direction.Up)
            {
                if (moveInput.x > 0.5f) direction = Direction.Right;
                else if (moveInput.x < -0.5f) direction = Direction.Left;    // If no input and last direction was up, update direction if a new key is pressed after releasing all keys.
                else if (moveInput.y < -0.5f) direction = Direction.Down;
                hasCalculatedDistance = false;
            }
        }
        else if (isSliding && slidingDirection == Direction.Up)
        {
            if (!hasCalculatedDistance)
            {
                distance = (transform.position.y + 4f).RoundToNearestEven();
                hasCalculatedDistance = true;
            }

            if (moveInput == Vector2.zero && slidingDirection == Direction.Up)
            {
                if (moveInput.x > 0.5f) slidingDirection = Direction.Right;     
                else if (moveInput.x < -0.5f) slidingDirection = Direction.Left;    // If no input and sliding up, update sliding direction if a new key is pressed.
                else if (moveInput.y < -0.5f) slidingDirection = Direction.Down;
                hasCalculatedDistance = false;
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

        if (slidingIsPaused && moveInput.y < -0.5f)
            slidingIsPaused = false;

        if (!isSliding)
        {
            if (moveInput.y < -0.5f)
                direction = Direction.Down;

            if (moveInput.y < -0.5f && direction == Direction.Down)
                onDown();

            if (lastReleasedInput.y < -0.5f && slideSignal > 0 && totalSlidesCount != 0 && direction == Direction.Down)
            {
                slidingDirection = Direction.Down;
                slideSignal--;
                isSliding = true;
                lastReleasedInput = Vector2.zero;
            }


            if (moveInput == Vector2.zero && direction == Direction.Down)
            {
                if (moveInput.x > 0.5f) direction = Direction.Right;
                else if (moveInput.x < -0.5f) direction = Direction.Left; // If no input and last direction was down, update direction if a new key is pressed after releasing all keys.
                else if (moveInput.y > 0.5f) direction = Direction.Up;
                hasCalculatedDistance = false;
            }
        }
        else if (isSliding && slidingDirection == Direction.Down)
        {
            if (!hasCalculatedDistance)
            {
                distance = (transform.position.y - 4f).RoundToNearestEven();
                hasCalculatedDistance = true;
            }

            if (moveInput == Vector2.zero && slidingDirection == Direction.Down)
            {
                if (moveInput.x > 0.5f) slidingDirection = Direction.Right;
                else if (moveInput.x < -0.5f) slidingDirection = Direction.Left;    // If no input and sliding down, update sliding direction if a new key is pressed.
                else if (moveInput.y > 0.5f) slidingDirection = Direction.Up;
                hasCalculatedDistance = false;
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

        if (slidingIsPaused && moveInput.x < -0.5f)
            slidingIsPaused = false;

        if (!isSliding)
        {
            if (moveInput.x < -0.5f)
                direction = Direction.Left;

            if (moveInput.x < -0.5f && direction == Direction.Left)
                onLeft();

            if (lastReleasedInput.x < -0.5f && slideSignal > 0 && totalSlidesCount != 0 && direction == Direction.Left)
            {
                slidingDirection = Direction.Left;
                slideSignal--;
                isSliding = true;
                lastReleasedInput = Vector2.zero;
            }

            if (moveInput == Vector2.zero && direction == Direction.Left)
            {
                if (moveInput.x > 0.5f) direction = Direction.Right;
                else if (moveInput.y > 0.5f) direction = Direction.Up;  // If no input and last direction was left, update direction if a new key is pressed after releasing all keys.
                else if (moveInput.y < -0.5f) direction = Direction.Down;
                hasCalculatedDistance = false;
            }
        }
        else if (isSliding && slidingDirection == Direction.Left)
        {
            if (!hasCalculatedDistance)
            {
                distance = (transform.position.x - 4f).RoundToNearestEven();
                hasCalculatedDistance = true;
            }

            if (moveInput == Vector2.zero && slidingDirection == Direction.Left)
            {
                if (moveInput.x > 0.5f) slidingDirection = Direction.Right;
                else if (moveInput.y > 0.5f) slidingDirection = Direction.Up;   // If no input and sliding left, update sliding direction if a new key is pressed.
                else if (moveInput.y < -0.5f) slidingDirection = Direction.Down;
                hasCalculatedDistance = false;
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

        if (slidingIsPaused && moveInput.x > 0.5f)
            slidingIsPaused = false;

        if (!isSliding)
        {
            if (moveInput.x > 0.5f)
                direction = Direction.Right;

            if (moveInput.x > 0.5f && direction == Direction.Right)
                onRight();

            if (lastReleasedInput.x > 0.5f && slideSignal > 0 && totalSlidesCount != 0 && direction == Direction.Right)
            {
                slidingDirection = Direction.Right;
                slideSignal--;                          // Start sliding to the right if the right key was just released, a slide is signaled, and sliding is allowed.
                isSliding = true;
                lastReleasedInput = Vector2.zero;
            }

            if (moveInput == Vector2.zero && direction == Direction.Right)
            {
                if (moveInput.x < -0.5f) direction = Direction.Left;
                else if (moveInput.y > 0.5f) direction = Direction.Up;      // If no input and last direction was right, reset direction if a new key is pressed after releasing all keys.
                else if (moveInput.y < -0.5f) direction = Direction.Down;
                hasCalculatedDistance = false;
            }
        }
        else if (isSliding && slidingDirection == Direction.Right)
        {
            if (!hasCalculatedDistance)
            {
                distance = (transform.position.x + 4f).RoundToNearestEven();
                hasCalculatedDistance = true;
            }

            if (moveInput == Vector2.zero && slidingDirection == Direction.Right)
            {
                if (moveInput.x < -0.5f) slidingDirection = Direction.Left;
                else if (moveInput.y > 0.5f) slidingDirection = Direction.Up;   // If no input and sliding right, check for new direction input and update sliding direction.
                else if (moveInput.y < -0.5f) slidingDirection = Direction.Down;
                hasCalculatedDistance = false;
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
        if (!isEnabled) return this;

        if (firePressed ||
            Keyboard.current.spaceKey.wasPressedThisFrame ||
            Keyboard.current.numpad0Key.wasPressedThisFrame ||      
            (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame))   // Checks if the set keys are pressed or if the fire action is triggered by a gamepad button.
        {
            onFire();
            firePressed = false;
        }

        return this;
    }

    public void SlideIn() => slideSignal++;

    public void SlideOut()
    {
        totalSlidesCount--;
        if (totalSlidesCount == 0)
        {
            isSliding = false;
            hasCalculatedDistance = false;
        }
    }

    public void ObstacleDetected() => slidingIsPaused = true;

    public void Reset()
    {
        slidingIsPaused = false;
        isSliding = false;
        hasCalculatedDistance = false;  // Resets the input action controller to its initial state.
        totalSlidesCount = 0;
        slideSignal = 0;
        distance = 0;
    }

    public void Enable()
    {
        isEnabled = true;
        playerControls?.Enable();        // Enables or disables the input action controller.
    }

    public void Disable()
    {
        isEnabled = false;
        playerControls?.Disable();
    }
}
