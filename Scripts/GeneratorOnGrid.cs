#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;


namespace Yagiri {
	public class GeneratorOnGrid : MonoBehaviour {

		[SerializeField]
		private GameObject obj;
		[SerializeField]
		private int xNum = 4;
		[SerializeField]
		private int yNum = 3;
		[SerializeField]
		private int zNum = 2;
		[SerializeField]
		private Vector3 delta = new Vector3(2.0f, 2.0f, 2.0f);

		// Use this for initialization
		void Start() {
			Generate();
		}

		public void Generate() {
			Transform _transform = GetComponent<Transform>();
			for (int i = 0; i < xNum; i++) {
				for (int j = 0; j < yNum; j++) {
					for (int k = 0; k < zNum; k++) {
						Vector3 d = new Vector3(delta.x * i, delta.y * j, delta.z * k);
						var tmp = PrefabUtility.InstantiatePrefab(obj) as GameObject;
						tmp.transform.position = _transform.position + d;
						tmp.transform.rotation = Quaternion.identity;
						tmp.transform.parent = transform;
					}
				}
			}
		}

		[CanEditMultipleObjects]
		[CustomEditor(typeof(GeneratorOnGrid))]
		public class GeneratorOnGridEditor : Editor {
			public override void OnInspectorGUI() {
				base.OnInspectorGUI();
				if (GUILayout.Button("オブジェクトを生成")) {
					(target as GeneratorOnGrid).Generate();
				}
			}
		}

	}
}
#endif