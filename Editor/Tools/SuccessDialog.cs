using System;
using UnityEngine;
using UnityEditor;

// ReSharper disable once CheckNamespace
namespace Unity.GhysX.Framework.Plugin.Generator.Tools.Editors
{
    public class SuccessDialog : EditorWindow
    {
        private static PluginConfig _config;
        private static Action<int> _onComplete;

        private float _startTime;

        public static void Show(PluginConfig config, Action<int> onComplete)
        {
            _config = config;
            _onComplete = onComplete;
            var window = GetWindow<SuccessDialog>(true, "Generated Successfully");
            // window.minSize = new Vector2(300, 160);
            // window.maxSize = new Vector2(300, 160);
            window.minSize = new Vector2(350, 150);
            window.maxSize = new Vector2(350, 150);
        }

        void OnEnable()
        {
            _startTime = Time.realtimeSinceStartup;
        }

        private void OnGUI()
        {
            DrawRText();
            // DrawTex();
            // DrawNormal();
        }

        void DrawRText()
        {
            // fade-in animation
            float alpha = Mathf.Clamp01((Time.realtimeSinceStartup - _startTime) * 3);
            GUI.color = new Color(1, 1, 1, alpha);

            // Make sure to use the correct font
            if (EditorStyles.standardFont == null)
            {
                // EditorStyles.Initialize();
            }

            // Main window background
            GUILayout.BeginVertical(GUI.skin.window, GUILayout.Width(350), GUILayout.Height(150));
            {
                // Title area
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label(EditorGUIUtility.IconContent("Collab"), GUIStyle.none, GUILayout.Height(28));
                var titleStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 14,
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = new Color(0.1f, 0.1f, 0.1f) }
                };
                GUILayout.Label("Generated Successfully", titleStyle);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                // Content area
                var contentStyle = new GUIStyle(EditorStyles.wordWrappedLabel)
                {
                    margin = new RectOffset(10, 10, 5, 5),
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 12
                };
                GUILayout.Label($"Plugin: {_config.name}\nLocation: Assets/{_config.name}", contentStyle);

                // Button area
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();

                    // Standard Button Style
                    var buttonStyle = new GUIStyle(EditorStyles.miniButton)
                    {
                        padding = new RectOffset(12, 12, 6, 6),
                        fontSize = 12,
                        fixedHeight = 24
                    };
                    
                    // Highlight Button Style (with system rounded corners)
                    var highlightStyle = new GUIStyle(buttonStyle)
                    {
                        normal =
                        {
                            textColor = Color.white,
                            background = CreateRoundedTexture(1f, new Color(0.16f, 0.42f, 0.75f))
                        },
                        hover =
                        {
                            textColor = Color.white,
                            background = CreateRoundedTexture(1f, new Color(0.20f, 0.50f, 0.85f))
                        },
                        active =
                        {
                            textColor = Color.white,
                            background = CreateRoundedTexture(1f, new Color(0.12f, 0.35f, 0.65f))
                        }
                    };

                    if (GUILayout.Button("Open Directory", buttonStyle, GUILayout.Width(120)))
                        ExecuteAction(0);

                    if (GUILayout.Button("View In Project", highlightStyle, GUILayout.Width(120)))
                        ExecuteAction(1);

                    if (GUILayout.Button("OK", buttonStyle, GUILayout.Width(80)))
                        ExecuteAction(2);

                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
                EditorGUILayout.Space(10);
            }
            GUILayout.EndVertical();

            // Autofocus to the 'View In Project' button
            if (Event.current.type == EventType.Layout)
            {
                GUI.FocusControl("View In Project");
            }
        }

        // Create a rounded background texture
        private Texture2D CreateRoundedTexture(float radius, Color color)
        {
            int texSize = 32;
            Texture2D tex = new Texture2D(texSize, texSize);
            Color[] pixels = new Color[texSize * texSize];

            float center = texSize / 2f;
            float sqrRadius = radius * radius * texSize * texSize / 4f;

            for (int y = 0; y < texSize; y++)
            {
                for (int x = 0; x < texSize; x++)
                {
                    float dx = Mathf.Abs(x - center);
                    float dy = Mathf.Abs(y - center);

                    // Calculation of fillet area
                    if (dx + dy > radius * texSize / 2f + 1f)
                    {
                        float sqrDist = (dx * dx) + (dy * dy);
                        pixels[y * texSize + x] = sqrDist > sqrRadius ? Color.clear : color;
                    }
                    else
                    {
                        pixels[y * texSize + x] = color;
                    }
                }
            }

            tex.SetPixels(pixels);
            tex.Apply();
            tex.wrapMode = TextureWrapMode.Clamp;
            return tex;
        }

        void DrawTex()
        {
            // fade-in animation
            float alpha = Mathf.Clamp01((Time.realtimeSinceStartup - _startTime) * 3);
            GUI.color = new Color(1, 1, 1, alpha);

            // Make sure to use the correct font
            if (EditorStyles.standardFont == null)
            {
                // EditorStyles.Initialize();
            }

            // Main window background
            GUILayout.BeginVertical(GUI.skin.window, GUILayout.Width(350), GUILayout.Height(150));
            {
                // Title area
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label(EditorGUIUtility.IconContent("Collab"), GUIStyle.none, GUILayout.Height(28));
                var titleStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 14,
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = new Color(0.1f, 0.1f, 0.1f) }
                };
                GUILayout.Label("Generated Successfully", titleStyle);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();

                // Content area
                var contentStyle = new GUIStyle(EditorStyles.wordWrappedLabel)
                {
                    margin = new RectOffset(10, 10, 5, 5),
                    alignment = TextAnchor.MiddleCenter,
                    fontSize = 12
                };
                GUILayout.Label($"Plugin: {_config.name}\nLocation: Assets/{_config.name}", contentStyle);

                // Button area
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();

                    var button1 = new GUIContent("Open Directory");
                    var button2 = new GUIContent("View In Project");
                    var button3 = new GUIContent("OK");

                    // Button Style
                    var buttonStyle = new GUIStyle(EditorStyles.miniButton)
                    {
                        padding = new RectOffset(12, 12, 6, 6),
                        fontSize = 12,
                        fontStyle = FontStyle.Normal,
                        fixedHeight = 24
                    };

                    if (GUILayout.Button(button1, buttonStyle, GUILayout.Width(120)))
                        ExecuteAction(0);

                    // Highlight button special style
                    var highlightStyle = new GUIStyle(buttonStyle)
                    {
                        normal = { textColor = Color.white, background = MakeTex(1, 1, new Color(0.16f, 0.42f, 0.75f)) },
                        hover = { textColor = Color.white, background = MakeTex(1, 1, new Color(0.20f, 0.50f, 0.85f)) },
                        active = { textColor = Color.white, background = MakeTex(1, 1, new Color(0.12f, 0.35f, 0.65f)) }
                    };

                    if (GUILayout.Button(button2, highlightStyle, GUILayout.Width(120)))
                        ExecuteAction(1);

                    if (GUILayout.Button(button3, buttonStyle, GUILayout.Width(80)))
                        ExecuteAction(2);

                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
                EditorGUILayout.Space(10);
            }
            GUILayout.EndVertical();

            // Autofocus to the 'View In Project' button
            if (Event.current.type == EventType.Layout)
            {
                GUI.FocusControl("View In Project");
            }
        }
        
        // Create a solid color texture
        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        void DrawNormal()
        {
            GUILayout.Label($"Plugin {_config.name} Generated successfully!\nPath: Assets/{_config.name}");

            // Simulate default selection with style
            var style = new GUIStyle(GUI.skin.button);
            if (Event.current.type == EventType.Layout)
            {
                style.normal = style.onActive; // By default, the third button shows the "pressed" state
                style.normal = style.onFocused; // By default, the third button shows the "pressed" 
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Directory")) ExecuteAction(0);
            if (GUILayout.Button("View In Project", style)) ExecuteAction(1);
            if (GUILayout.Button("OK")) ExecuteAction(2);
            GUILayout.EndHorizontal();

            // Autofocus to the 'View In Project' button
            if (Event.current.type == EventType.Layout)
            {
                GUI.FocusControl("View In Project");
            }
        }

        void ExecuteAction(int index)
        {
            _onComplete?.Invoke(index);
            Close();
        }
    }
}