using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class s4u : MonoBehaviour
{
    public Shader shader;
    Material material;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material == null)
            material = new Material(shader);

        Graphics.Blit(src, dest, material);
    }
}
