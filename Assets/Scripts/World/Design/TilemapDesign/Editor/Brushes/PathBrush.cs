using ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.PathLayer;
using UnityEditor;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Editor.Brushes
{
    [CustomGridBrush(false, false, false, "Path Brush")]
    public class PathBrush : TintBrush
    {
        public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt selectedCellsBounds)
        {
            Undo.RegisterFullObjectHierarchyUndo(brushTarget, "PathBrush-BoxFill");
            base.BoxFill(gridLayout, brushTarget, selectedCellsBounds);

            var pathLayer = brushTarget.GetComponent<PathGridLayer>();

            foreach (var cellPos in selectedCellsBounds.allPositionsWithin)
            {
                pathLayer.AddPathSegment(new PathSegment(new RectInt((Vector2Int)cellPos, Vector2Int.one)));
            }
        }

        public override void BoxErase(GridLayout gridLayout, GameObject brushTarget, BoundsInt selectedCellsBounds)
        {
            Undo.RegisterFullObjectHierarchyUndo(brushTarget, "PathBrush-BoxErase");
            base.BoxErase(gridLayout, brushTarget, selectedCellsBounds);

            var pathLayer = brushTarget.GetComponent<PathGridLayer>();

            foreach (var cellPos in selectedCellsBounds.allPositionsWithin)
            {
                pathLayer.DeletePathSegment(new PathSegment(new RectInt((Vector2Int)cellPos, Vector2Int.one)));
            }
        }
    }
}