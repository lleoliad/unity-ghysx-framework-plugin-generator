using System.Collections.Generic;

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

            if (GUILayout.Button("Generate"))
            {
                if (string.IsNullOrEmpty(_config.name))
                {
                    EditorUtility.DisplayDialog("Error", "Name is required!", "OK");
                    return;
                }
                new PluginGenerator(_config).Generate();
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
    }

    [System.Serializable]
    public class PluginConfig
    {
        public string author = "Lleoliad";
        public string name = "Plugin Template";
        public string version = "0.0.1";
        public string unity = "2018.4";
        public string description = "Unity Editor Extension";
        public List<string> keywords = new List<string>() { "Tools" };
        public string email = "lleoliad@gmail.com";
        public string url = "https://github.com";
        public string category = "Tools";
    }
}