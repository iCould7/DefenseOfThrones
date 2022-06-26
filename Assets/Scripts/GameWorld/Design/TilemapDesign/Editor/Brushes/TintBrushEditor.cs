using UnityEditor;
using UnityEditor.Tilemaps;

namespace ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Editor.Brushes
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

            tintBrush.Tint = EditorGUILayout.ColorField("Tint", tintBrush.Tint);

            EditorGUILayout.Space(30);
            EditorGUILayout.LabelField("---------------------BUILT-IN-FIELDS------------------------");
        }
    }
}