using System;
using System.IO;
using UnityEditor;
using UnityEngine;
 
public class TextureCombine 
{
    [MenuItem("Tools/TextureCombine")]
    static void Combine()
    {
        Texture2D tex512 = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/hero.png");
        Texture2D tex128 = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/star.png");
        Texture2D @new  = CombineTexture(
            new[] { tex512, tex128 }, //贴图
            new[] { (0, 0), (600, 600) }, //偏移
            1024);//最终贴图大小
 
        File.WriteAllBytes("Assets/out.jpg", @new.EncodeToJPG());
        AssetDatabase.Refresh();
    }
 
    static Texture2D CombineTexture(Texture2D []texs,ValueTuple<int,int>[]offests,int size)
    {
        Texture2D @out = new Texture2D(size, size, TextureFormat.RGBA32, true);
        for (int i = 0; i < texs.Length; i++)
        {
            var tex = texs[i];
            var offest = offests[i];
            var width = tex.width;
            var height = tex.height;
            RenderTexture tmp = RenderTexture.GetTemporary(width,height,0,RenderTextureFormat.Default,RenderTextureReadWrite.Linear);
            Graphics.Blit(tex, tmp);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = tmp;
            Texture2D @new = new Texture2D(width, height);
            @new.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            @new.Apply();
            @out.SetPixels(offest.Item1, offest.Item2, width, height, @new.GetPixels());
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(tmp);
        }
        return @out;
    }
   
}
