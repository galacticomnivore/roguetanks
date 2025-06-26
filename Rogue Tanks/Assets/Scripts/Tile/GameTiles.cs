using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameTiles : MonoBehaviour
{
    public List<GroupTile> groupTilesList = new List<GroupTile>();
    public List<SingleTile> singleTilesList = new List<SingleTile>();
    private GameFactory gameFactory;
    private Dictionary<string, Func<Vector3, int, int, object>> tileCreationMap;

    private void Start()
    {
        gameFactory = GetComponent<GameFactory>();

        // Initialize the tileCreationMap here, as gameFactory is now available  
        tileCreationMap = new Dictionary<string, Func<Vector3, int, int, object>>()
        {
            ["BrickTile"] = (pos, row, column) => gameFactory.CreateBrick(pos, row, column),
            ["ForestTile"] = (pos, row, column) => gameFactory.CreateForest(pos, row, column),
            ["WaterTile"] = (pos, row, column) => gameFactory.CreateWater(pos, row, column),
            ["StoneTile"] = (pos, row, column) => gameFactory.CreateStone(pos, row, column),
            ["IceTile"] = (pos, row, column) => gameFactory.CreateIce(pos, row, column),
            ["LavaTile"] = (pos, row, column) => gameFactory.CreateLava(pos, row, column),
            ["MudTile"] = (pos, row, column) => gameFactory.CreateMud(pos, row, column)
        };
    }

    public void Add(GroupTile groupTile)
    {
        groupTile.OnHit += Hit;
        groupTilesList.Add(groupTile);
    }
    public void Add(SingleTile singleTile)
    {
        singleTilesList.Add(singleTile);
    }

    public void Reset()
    {
        groupTilesList.ForEach(DestroyGroupTile);
        groupTilesList.Clear();
        singleTilesList.ForEach(DestroySingleTile);
        singleTilesList.Clear();
    }

    public void DestroyGroupTile(GroupTile groupTile)
    {
        groupTile.OnHit -= Hit;
        GameObject.Destroy(groupTile.gameObject);
    }
    public void DestroySingleTile(SingleTile singleTile)
    {
        GameObject.Destroy(singleTile.gameObject);
    }

    public void ChangeTileType(GameObject tileToReplace, string replacementTile)
    {
        Component tileComponent = tileToReplace.GetComponent<SingleTile>()
                           ?? (Component)tileToReplace.GetComponentInParent<GroupTile>();
        if (tileComponent == null)
        {
            Debug.LogWarning("Tile neither single nor group tile - shouldn't be here");
            return;
        }
        string[] parts = tileComponent.name.Split('_');
        int row = parts[1].ToInt();
        int column = parts[2].ToInt();

        if (tileCreationMap.TryGetValue(replacementTile, out var createTile))
        {  
            var newTile = createTile(tileToReplace.transform.position, row, column);

            SwapTile(tileComponent, newTile);
        }
        else
        {
            Debug.LogWarning($"Replacement tile type '{replacementTile}' not found in tileCreationMap.");
        }
    }
    private void SwapTile<T1, T2>(T1 tileToRemove, T2 tileToAdd)
    {
        if (tileToRemove == null || tileToAdd == null)
        {
            Debug.LogWarning("Tile to remove or add is null.");
            return;
        }
        var removeTileList = typeof(T1) == typeof(SingleTile)
            ? (System.Collections.IList)(object)singleTilesList
            : (System.Collections.IList)(object)groupTilesList;

        var addTileList = typeof(T2) == typeof(SingleTile)
            ? (System.Collections.IList)(object)singleTilesList
            : (System.Collections.IList)(object)groupTilesList;
        removeTileList.Remove(tileToRemove);
        GameObject.Destroy(((Component)(object)tileToRemove).gameObject);
        addTileList.Add(tileToAdd);
    }

    public void Hit(BulletController bullet, GroupTile groupTile, UnitTile unitTile)
    {
        if (bullet.Strength < groupTile.Strength) return;
        bullet.Direction
            .OnVertical(() =>
            {
                groupTile.RemoveUnitTile(unitTile.Row, unitTile.Column);
                if (unitTile.Column == 1)
                {
                    groupTile.RemoveUnitTile(unitTile.Row, unitTile.Column + 1);
                    var leftGroupTile = groupTilesList.SingleOrDefault(gt => gt.Row == groupTile.Row && gt.Column == groupTile.Column - 1);
                    if (leftGroupTile != null && bullet.Strength >= leftGroupTile.Strength)
                        leftGroupTile.RemoveUnitTile(unitTile.Row, 2);
                }
                if (unitTile.Column == 2)
                {
                    groupTile.RemoveUnitTile(unitTile.Row, unitTile.Column - 1);
                    var rightGroupTile = groupTilesList.SingleOrDefault(gt => gt.Row == groupTile.Row && gt.Column == groupTile.Column + 1);
                    if (rightGroupTile != null && bullet.Strength >= rightGroupTile.Strength)
                        rightGroupTile.RemoveUnitTile(unitTile.Row, 1);
                }
            })
            .OnHorizontal(() =>
            {
                groupTile.RemoveUnitTile(unitTile.Row, unitTile.Column);
                if (unitTile.Row == 1)
                {
                    groupTile.RemoveUnitTile(unitTile.Row + 1, unitTile.Column);
                    var downGroupTile = groupTilesList.SingleOrDefault(gt => gt.Row == groupTile.Row - 1 && gt.Column == groupTile.Column);
                    if (downGroupTile != null && bullet.Strength >= downGroupTile.Strength)
                        downGroupTile.RemoveUnitTile(2, unitTile.Column);
                }
                if (unitTile.Row == 2)
                {
                    groupTile.RemoveUnitTile(unitTile.Row - 1, unitTile.Column);
                    var topGroupTile = groupTilesList.SingleOrDefault(gt => gt.Row == groupTile.Row + 1 && gt.Column == groupTile.Column);
                    if (topGroupTile != null && bullet.Strength >= topGroupTile.Strength)
                        topGroupTile.RemoveUnitTile(1, unitTile.Column);
                }

            });
    }
}
