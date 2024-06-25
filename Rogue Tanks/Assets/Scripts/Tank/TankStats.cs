using System;
using System.Collections.Generic;
using UnityEngine;

public class TankStats : MonoBehaviour
{
    private Dictionary<int, int> tankPoints = new Dictionary<int, int>();
    public Dictionary<int, int> TankPoints { get => tankPoints; }
    public int TankType { get; private set; }
    public int Points { get; private set; }
    public int Lives { get; private set; } = 1;

    public void initialize(int tankType, int lives)
    {
        this.TankType = tankType;
        this.Lives = lives;
        this.Tank = GetComponentInParent<Tank>();
    }

    public Tank Tank { get; private set; }

    public void AddLive(int numberOfLives) => Lives += numberOfLives;

    public void AddLive(int numberOfLives, Action<int> onLiveAdd)
    {
        AddLive(numberOfLives);
        onLiveAdd(Lives);
    }

    public TankStats DecreaseLife(Action onDecreaseLife)
    {
        Lives -= 1;
        onDecreaseLife();
        return this;
    }

    public TankStats DecreaseLife(Action<int> onLiveDecreased)
    {
        Lives -= 1;
        onLiveDecreased(Lives);
        return this;
    }

    public TankStats TankStillHaveLivesLeft(Action onLivesLeft)
    {
        Lives.IsGreaterThanOrEqualTo(1, onLivesLeft);
        return this;
    }

    public TankStats TankHasNoMoreLivesLeft(Action onNoMoreLivesLeft)
    {
        Lives.Equals(0, onNoMoreLivesLeft);
        return this;
    }

    public void AddPoint(int destroyedTankType)=>
        tankPoints.AddValue(destroyedTankType, 1);
}