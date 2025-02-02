﻿namespace TilePuzzle
{
    // 빅 벤
    public class BigBen: WonderTile
    {
        public override void AddToDelegate()
        {
            TileManager.Instance.CalculateBonusByWonder += WonderFunction;
        }

        // 상업 중심지 건설 점수 +WonderBonus
        public override void WonderFunction(Tile currentTile, TileBuilding tileBuilding)
        {
            if (tileBuilding == TileBuilding.CommercialHub)
            {
                currentTile.ChangeBonus(wonderBonus);
            }
        }

        // 상업 중심지 옆 강 타일에 건설 가능
        public override bool WonderLimit(Tile currentTile)
        {
            if (currentTile.MyTileType == TileType.Water ||
                currentTile.MyTileType == TileType.Mountain)
            {
                return false;
            }

            if (currentTile.MyTileType == TileType.River)
            {
                foreach (Tile neighbor in currentTile.NeighborTiles)
                {
                    if (neighbor.MyTileBuilding == TileBuilding.CommercialHub)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}