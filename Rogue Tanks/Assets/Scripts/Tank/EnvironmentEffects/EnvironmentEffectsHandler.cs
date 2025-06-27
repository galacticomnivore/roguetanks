using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentEffectsHandler
{
    private GameEngine gameEngine;
    private Dictionary<string, Sprite> spriteMap;
    public EnvironmentEffectsHandler(GameEngine gameEngine)
    {
        this.gameEngine = gameEngine;
        this.spriteMap = new Dictionary<string, Sprite>()
        {
            ["IceTile"] = gameEngine.GameTiles.tankIceSprite,
            ["LavaTile"] = gameEngine.GameTiles.tankLavaSprite,
            ["WaterTile"] = gameEngine.GameTiles.tankWaterSprite,
            ["BrickTile"] = null,
            ["Tile"] = null,
            ["SteelTile"] = null, 
            ["MudTile"] = gameEngine.GameTiles.tankMudSprite, 
            ["ForestTile"] = gameEngine.GameTiles.tankForestSprite, 
        };
    }

    // Enable sprite swap
    
    public void HandleEnvirontmentEffect(GameObject tileCrossed, TankMovementController tank, bool isDeactivate = false)
    {
        SpriteRenderer renderer = tank.GetComponentInChildren<SpriteRenderer>();
        string tileLayer = LayerMask.LayerToName(tileCrossed.layer);
        Sprite newSprite;
        if (isDeactivate)
        {
            newSprite = gameEngine.GameTiles.normalTankSprite;
        } else
        {
            newSprite = spriteMap[tileLayer];
        }
        if (newSprite != null)
        {
            renderer.sprite = newSprite;
        }
        // ApplyEffect()
    }
    // Environment effects (fire, lava, ice, water) ..



}
