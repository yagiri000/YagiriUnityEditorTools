
using UnityEngine;
using UnityEditor;

/// <summary>
/// 
/// 情報を詰め込んだテクスチャ用のパラメータ設定
/// 
/// ※Editorフォルダ以下に配置して下さい。
/// 
/// 【使い方】
/// 1. Projectビューでテクスチャを右クリック
/// 2. コンテキストメニューで、「Assets/Yagiri-DataTextureImportSettingsSetter」をクリック
/// 3. 出てきたウィンドウで、「パラメータ設定」ボタンを押す
/// 
/// </summary>
namespace Yagiri
{
    public class DataTextureImportSettingsSetter : EditorWindow
    {
        // TODO: テクスチャアセットをクリックしたときだけ表示されるようにする
        // TODO: ウィンドウにドラッグアンドドロップした時に対応する

        private Texture2D m_target = null;

        [MenuItem("Assets/Yagiri-DataTextureImportSettingsSetter")]
        static void Open()
        {
            EditorWindow.GetWindow<DataTextureImportSettingsSetter>("DataTextureImportSettingsSetter");
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField(
                "情報を詰め込んだテクスチャ用のパラメータ設定"
                );

            {
                GUIStyle style = new GUIStyle(GUI.skin.label);
                style.wordWrap = true;
                style.normal.textColor = new Color(0.3f, 0.3f, 0.3f);
                EditorGUILayout.LabelField(
                    "MipMap無効、Filter ModeをPointにするなど、情報を焼いたテクスチャ用の設定を行います。",
                    style
                    );
                EditorGUILayout.LabelField(
                    "必要な設定が全て出来ていなかった場合は、手動で設定などして下さい。",
                    style
                    );
            }

            if (Selection.activeObject)
            {
                m_target = Selection.activeObject as Texture2D;
            }

            m_target = EditorGUILayout.ObjectField(
                "Target",
                m_target,
                typeof(Texture2D),
                true
            ) as Texture2D;

            if (GUILayout.Button("パラメータ設定"))
            {
                SetParams(m_target);
            }
        }

        /// <summary>
        /// パラメータを設定
        /// </summary>
        /// <param name="target"></param>
        public void SetParams(Texture2D target)
        {
            var path = AssetDatabase.GetAssetPath(target);
            var tex = AssetImporter.GetAtPath(path) as TextureImporter;

            Debug.Log($"テクスチャのImport Settingsを上書きします。 path: {path}");

            tex.sRGBTexture = false;
            tex.npotScale = TextureImporterNPOTScale.None;
            tex.mipmapEnabled = false;
            tex.filterMode = FilterMode.Point;
            tex.textureCompression = TextureImporterCompression.Uncompressed;
            tex.SaveAndReimport();
        }
    }
}