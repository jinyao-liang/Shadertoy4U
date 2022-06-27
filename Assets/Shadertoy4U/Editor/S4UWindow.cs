using System.IO;
using UnityEngine;
using UnityEditor; 

namespace Shadertoy4U
{

public class S4UWindow : EditorWindow
{
    public string content;
    public Shader shader;

    public TextAsset upperText;
    public TextAsset lowerText;

    [MenuItem ("Window/Shadertoy4U")]
    public static void  ShowWindow () {
        EditorWindow.GetWindow(typeof(S4UWindow), false, "Shadertoy4U");
    }

    void OnGUI () 
    {
        upperText = EditorGUILayout.ObjectField("upperText", upperText, typeof(TextAsset), false) as TextAsset;
        lowerText = EditorGUILayout.ObjectField("lowerText", lowerText, typeof(TextAsset), false) as TextAsset;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Shadertoy content:");
        content = EditorGUILayout.TextArea(content);

        shader = EditorGUILayout.ObjectField("Export shader", shader, typeof(Shader), false) as Shader;
        EditorGUILayout.Space();
        if(GUILayout.Button("Generate"))
        {
            var hlsl = Glsl2Hlsl.Convert(content);

            var str = $@"
{upperText.text}
{hlsl}
{lowerText.text}
";
            str = str.Replace("\r\n", "\n");

            File.WriteAllText(AssetDatabase.GetAssetPath(shader), str);
            AssetDatabase.Refresh();
        }
    }
}

} // namespace Shadertoy4U
