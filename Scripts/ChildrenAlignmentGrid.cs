#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// アタッチしたオブジェクトの子オブジェクトたちの位置、角度をずらしながら整列する
/// </summary>
namespace Yagiri
{
    public class ChildrenAlignmentGrid : MonoBehaviour
    {
        [SerializeField]
        public Vector3 difHorizontal = new Vector3(1.0f, 0.0f, 0.0f);
        [SerializeField]
        public Vector3 difVertical = new Vector3(0.0f, -1.0f, 0.0f);
        [SerializeField]
        public int columnNum = 4;

        public void DoChildrenAlignment()
        {
            int count = 0;
            foreach (Transform i in GetComponent<Transform>())
            {
                int row = count / columnNum;
                int column = count % columnNum;
                i.transform.localPosition = row * difVertical + column * difHorizontal;
                count++;
            }
        }

        [CanEditMultipleObjects]
        [CustomEditor(typeof(ChildrenAlignmentGrid))]
        public class ChildrenAlignmentEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                if (GUILayout.Button("ChildrenAlignmentGrid"))
                {
                    (target as ChildrenAlignmentGrid).DoChildrenAlignment();
                }
            }
        }
    }
}
#endif
