using System;
using UnityEngine;

public class TankMovementController : MonoBehaviour
{
    private Vector3 MovementDirection = Vector3.up;
    public float Speed = 10.0f;
    public SpriteController SpriteController { get; set; }
    public IInputActionController Movement = new NoActions();

    private void MoveUp()
    {
        MovementDirection.OnHorizontal(RoundX);
        transform.Translate((MovementDirection = Vector3.up) * Speed * Time.deltaTime);
    }
    private void MoveDown()
    {
        MovementDirection.OnHorizontal(RoundX);
        transform.Translate((MovementDirection = Vector3.down) * Speed * Time.deltaTime);
    }
    private void MoveLeft()
    {
        MovementDirection.OnVertical(RoundY);
        transform.Translate((MovementDirection = Vector3.left) * Speed * Time.deltaTime);
    }
    private void MoveRight()
    {
        MovementDirection.OnVertical(RoundY);
        transform.Translate((MovementDirection = Vector3.right) * Speed * Time.deltaTime);
    }

    private void Update() =>
        Movement
            .Up(() => SpriteController.FaceUp().Raycasts.CanMoveInDirectionOf(Vector2.up, MoveUp, Movement.ObstacleDetected))
            .Down(() => SpriteController.FaceDown().Raycasts.CanMoveInDirectionOf(Vector2.down, MoveDown, Movement.ObstacleDetected))
            .Left(() => SpriteController.FaceLeft().Raycasts.CanMoveInDirectionOf(Vector2.left, MoveLeft, Movement.ObstacleDetected))
            .Right(() => SpriteController.FaceRight().Raycasts.CanMoveInDirectionOf(Vector2.right, MoveRight, Movement.ObstacleDetected));

    public void ResetMovement() => Movement.Reset();

    private void RoundY() => transform.Translate(transform.position.y.RoundToNearestEven().Subtract(transform.position.y).ToYVector());
    private void RoundX() => transform.Translate(transform.position.x.RoundToNearestEven().Subtract(transform.position.x).ToXVector());

    public void SlideIn() => Movement.SlideIn();
    public void SlideOut() => Movement.SlideOut();
}
