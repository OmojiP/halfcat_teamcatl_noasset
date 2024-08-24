using UnityEditor;
using UnityEngine;

namespace HikanyanLaboratory.Editor
{
    public class ProjectStructureWindow : EditorWindow
    {
        private string projectName = "My";

        [MenuItem("HikanyanTools/Project Structure Generator")]
        public static void ShowWindow()
        {
            GetWindow<ProjectStructureWindow>("Project Structure Generator");
        }

        private void OnGUI()
        {
            GUILayout.Label("プロジェクト構造生成", EditorStyles.boldLabel);

            projectName = EditorGUILayout.TextField("プロジェクト名", projectName);

            if (GUILayout.Button("フォルダを生成"))
            {
                ProjectStructureGenerator generator = new ProjectStructureGenerator();
                generator.CreateProjectFolders(projectName);
                Debug.Log("プロジェクトフォルダが生成されました。");
            }
        }
    }
}
