#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Yagiri {
	public class ChildrenRandomRotation : MonoBehaviour {

		[SerializeField]
		private Vector3 angleMin = new Vector3(0.0f, 0.0f, 0.0f);
		[SerializeField]
		private Vector3 angleMax = new Vector3(0.0f, 360.0f, 0.0f);

		public void DoChildrenRandomRotation() {
			Transform _transform = GetComponent<Transform>();
			int count = 0;
			foreach (Transform i in _transform) {
				i.transform.localRotation = Quaternion.Euler(
					Random.Range(angleMin.x, angleMax.x),
					Random.Range(angleMin.y, angleMax.y),
					Random.Range(angleMin.z, angleMax.z)
					);
				count++;
			}
		}

		[CanEditMultipleObjects]
		[CustomEditor(typeof(ChildrenRandomRotation))]
		public class ChildrenRandomRotationEditor : Editor {
			public override void OnInspectorGUI() {
				base.OnInspectorGUI();
				if (GUILayout.Button("ChildrenRandomRotation")) {
					(target as ChildrenRandomRotation).DoChildrenRandomRotation();
				}
			}
		}
	}
}
#endif
