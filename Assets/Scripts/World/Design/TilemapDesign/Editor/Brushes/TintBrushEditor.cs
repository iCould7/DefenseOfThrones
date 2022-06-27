using UnityEditor;
using UnityEditor.Tilemaps;

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Editor.Brushes
{
    [CustomEditor(typeof(TintBrush))]
    public class TintBrushEditor : GridBrushEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(1);
            PlainOnInspectorGUI();
        }

        protected virtual void PlainOnInspectorGUI()
        {
            var tintBrush = (TintBrush)target;

            tintBrush._Tint = EditorGUILayout.ColorField("Tint", tintBrush._Tint);

            EditorGUILayout.Space(30);
            EditorGUILayout.LabelField("---------------------BUILT-IN-FIELDS------------------------");
        }
    }
}