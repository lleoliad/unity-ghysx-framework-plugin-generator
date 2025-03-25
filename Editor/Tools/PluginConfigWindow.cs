using System;
using System.Collections.Generic;
using System.IO;

// ReSharper disable RedundantNameQualifier
// ReSharper disable once CheckNamespace
namespace Unity.GhysX.Framework.Plugin.Generator.Tools.Editors
{
    using UnityEditor;
    using UnityEngine;

    public class PluginConfigWindow : EditorWindow
    {
        private Vector2 _scrollPosition;

        private readonly PluginConfig _config = new PluginConfig();

        [MenuItem("GhysX/Tools/Plugin Generator")]
        public static void ShowWindow()
        {
            GetWindow<PluginConfigWindow>("Plugin Config");
        }

        void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            EditorGUILayout.TextField("Root Path", Application.dataPath);

            _config.name = EditorGUILayout.TextField("Plugin Name", _config.name);
            _config.author = EditorGUILayout.TextField("Author", _config.author);
            _config.version = EditorGUILayout.TextField("Version", _config.version);
            _config.unity = EditorGUILayout.TextField("Unity", _config.unity);
            _config.description = EditorGUILayout.TextField("Description", _config.description);
            EditorGUILayout.LabelField("Keywords", EditorStyles.boldLabel);
            DrawKeywordsList();

            _config.email = EditorGUILayout.TextField("Email", _config.email);
            _config.url = EditorGUILayout.TextField("URL", _config.url);
            _config.category = EditorGUILayout.TextField("Category", _config.category);

            if (GUILayout.Button("Generate", GUILayout.Height(36)))
            {
                if (string.IsNullOrEmpty(_config.name))
                {
                    EditorUtility.DisplayDialog("Error", "Name is required!", "OK");
                    return;
                }

                bool success = new PluginGenerator(_config).Generate();
                if (success)
                {
                    int result = EditorUtility.DisplayDialogComplex("Generated Successfully",
                        $"Plugin {_config.name} Generated successfully!\n Path: Assets/{_config.name}",
                        "Open Directory", // Button 0 
                        "View In Project", // Button 1 
                        "OK" //Button 2
                    );

                    switch (result)
                    {
                        case 0: //Open Directory 
                            OpenPluginDirectory();
                            break;
                        case 1: //View In Project 
                            FocusInProjectWindow();
                            break;
                        case 2: // OK 
                            break;
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", "Failed to generate, please check console output for details", "OK");
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawKeywordsList()
        {
            for (int i = 0; i < _config.keywords.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                _config.keywords[i] = EditorGUILayout.TextField($"Keyword {i + 1}", _config.keywords[i]);

                if (GUILayout.Button("-", GUILayout.Width(20)))
                {
                    _config.keywords.RemoveAt(i);
                    break; // Exit the current loop to avoid indexing errors
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("+ Add Keyword", GUILayout.Width(120)))
            {
                _config.keywords.Add("");
            }

            EditorGUILayout.EndHorizontal();
        }

        private string GetPlatformSafePath(string unityRelativePath)
        {
            string path = Path.Combine(Application.dataPath, "../", unityRelativePath);
            return Path.GetFullPath(path)
                .Replace('\\', '/') // Uniform use of forward slashes
                .Replace("//", "/"); // Handling double slashes
        }

        // private void OpenPluginDirectory()
        // {
        //     string relativePath = $"Assets/{_config.name}";
        //     string absolutePath = Path.GetFullPath(Path.Combine(Application.dataPath, "../", relativePath));
        //
        //     // Select the directory in Explorer
        //     EditorUtility.RevealInFinder(absolutePath);
        //
        //     // Also highlight in the Unity Project window
        //     UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(relativePath);
        //     Selection.activeObject = obj;
        //     EditorGUIUtility.PingObject(obj);
        // }

        private void FocusInProjectWindow()
        {
            string relativePath = $"Assets/{_config.name}";
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(relativePath);

            if (obj != null)
            {
                Selection.activeObject = obj;
                EditorGUIUtility.PingObject(obj);

#if UNITY_2022_1_OR_NEWER
                // Try opening the directory (for Unity 2021 +)
                EditorApplication.ExecuteMenuItem("Window/General/Project");
#endif
            }
        }

        private void OpenPluginDirectory()
        {
            try
            {
                string relativePath = $"Assets/{_config.name}";
                if (!Directory.Exists(Path.Combine(Application.dataPath, _config.name)))
                {
                    Debug.LogError($"Directory does not exist: {relativePath}");
                    EditorUtility.DisplayDialog("Error", "The generated directory does not exist. Please check the generation process", "OK");
                    return;
                }

                string absolutePath = GetPlatformSafePath(relativePath);
#if UNITY_2021_1_OR_NEWER
                EditorUtility.OpenWithDefaultApp(absolutePath);
#else
                EditorUtility.RevealInFinder(absolutePath);
#endif
                FocusInProjectWindow();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to open directory: {e.Message}");
                EditorUtility.DisplayDialog("Error", "Unable to open directory, please access manually", "OK");
            }
        }
    }

    [System.Serializable]
    public class PluginConfig
    {
        public string author = "lleoliad";
        public string name = "Plugin Template";
        public string version = "0.0.1";
        public string unity = "2018.4";
        public string description = "Unity Editor Extension";
        public List<string> keywords = new List<string>() { "Tools" };
        public string email = "lleoliad@gmail.com";
        public string url = "https://github.com/{author}/{projectName}";
        public string category = "Tools";
    }
}