#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// アタッチしたオブジェクトの子オブジェクトをすべてDestroyする
/// </summary>
namespace Yagiri
{
    public class ChildrenDestroyAll : MonoBehaviour
    {

        public void Do()
        {
            foreach (Transform i in transform)
            {
                EditorApplication.delayCall += () => DestroyImmediate(i.gameObject);
            }
        }

        [CanEditMultipleObjects]
        [CustomEditor(typeof(ChildrenDestroyAll))]
        public class ChildrenDestroyAllEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                if (GUILayout.Button("ChildrenDestroyAll"))
                {
                    (target as ChildrenDestroyAll).Do();
                }
            }
        }
    }
}
#endif
