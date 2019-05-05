using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColourGenerator : MonoBehaviour {
    public Material mat;
    public Gradient gradient;
    public float normalOffsetWeight;

    Texture2D texture;
    const int textureResolution = 50;

    void Init () {
        if (texture == null || texture.width != textureResolution) {
            texture = new Texture2D (textureResolution, 1, TextureFormat.RGBA32, false);
        }
    }

    void Update () {
        Init ();
        UpdateTexture ();

        MeshGenerator m = FindObjectOfType<MeshGenerator> ();

        float boundsY = m.boundsSize * m.numChunks.y;

        mat.SetFloat ("boundsY", boundsY);
        mat.SetFloat ("normalOffsetWeight", normalOffsetWeight);

        mat.SetTexture ("ramp", texture);
    }

    void UpdateTexture () {
        if (gradient != null) {
            Color[] colours = new Color[texture.width];
            for (int i = 0; i < textureResolution; i++) {
                Color gradientCol = gradient.Evaluate (i / (textureResolution - 1f));
                colours[i] = gradientCol;
            }

            texture.SetPixels (colours);
            texture.Apply ();
        }
    }
}