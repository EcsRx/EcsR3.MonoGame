using System;

namespace EcsR3.MonoGame.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ToRenderTextureCubeAttribute : Attribute
{
    public int RenderTextureId { get; }

    public ToRenderTextureCubeAttribute(int renderTextureId)
    {
        RenderTextureId = renderTextureId;
    }
}