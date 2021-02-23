#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;


/// <summary>
/// アタッチされたオブジェクトの子オブジェクトとしてプレハブを生成
/// 生成位置は円内
/// </summary>
namespace Yagiri
{
    public class GeneratorInSphere : MonoBehaviour
    {

        [SerializeField]
        private GameObject obj;

        [SerializeField]
        private int num = 10;

        [SerializeField]
        private float radius = 3.0f;

        // Use this for initialization
        void Start()
        {
            Generate();
        }

        public void Generate()
        {
            for (int j = 0; j < num; j++)
            {
                var tmp = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                tmp.transform.position = transform.position + radius * Random.onUnitSphere;
                tmp.transform.rotation = Random.rotation;
                tmp.transform.parent = transform;
            }
        }

        // Scene ビュー上方「Gizmos」メニューから可視化/不可視化
        void OnDrawGizmosSelected()
        {

        }

        [CanEditMultipleObjects]
        [CustomEditor(typeof(GeneratorInSphere))]
        public class GeneratorInSphereEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                if (GUILayout.Button("オブジェクトを生成"))
                {
                    (target as GeneratorInSphere).Generate();
                }
            }
        }
    }
}
#endif
