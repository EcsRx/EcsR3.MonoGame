using System;

namespace EcsR3.MonoGame.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ToRenderTexture2dAttribute : Attribute
{
    public int RenderTextureId { get; }

    public ToRenderTexture2dAttribute(int renderTextureId)
    {
        RenderTextureId = renderTextureId;
    }
}