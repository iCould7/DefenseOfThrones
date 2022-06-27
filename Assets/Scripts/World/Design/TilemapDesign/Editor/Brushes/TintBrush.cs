using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Editor.Brushes
{
    [CustomGridBrush(false, false, false, "Tint Brush")]
    public class TintBrush : GridBrush
    {
        public Color _Tint = Color.white;

        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt selectedCellsBounds)
        {
            base.BoxFill(gridLayout, brushTarget, selectedCellsBounds);

            var targetTilemap = brushTarget.GetComponent<Tilemap>();

            foreach (var cellPos in selectedCellsBounds.allPositionsWithin)
            {
                var cellLocalPos = cellPos - selectedCellsBounds.min;
                var cell = cells[GetCellIndexWrapAround(cellLocalPos.x, cellLocalPos.y, cellLocalPos.z)];
                if (cell.tile != null)
                {
                    targetTilemap.SetColor(cellPos, _Tint);
                }
            }
        }
    }
}