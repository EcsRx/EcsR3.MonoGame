using System.Linq;
using EcsR3.MonoGame.Attributes;
using EcsR3.MonoGame.Systems;

namespace EcsR3.MonoGame.Extensions;

public static class SpriteBatchSystemExtensions
{
    public static int GetRenderTexture2dId(this SpriteBatchSystem system)
    {
        var possibleAttributes = system.GetType()
            .GetCustomAttributes(typeof(ToRenderTexture2dAttribute), true);

        if (!possibleAttributes.Any())
        { return -1; }

        var attribute = (ToRenderTexture2dAttribute) possibleAttributes.First();
        return attribute.RenderTextureId;
    }
     
    public static int GetRenderTextureCubeId(this SpriteBatchSystem system)
    {
        var possibleAttributes = system.GetType()
            .GetCustomAttributes(typeof(ToRenderTextureCubeAttribute), true);

        if (!possibleAttributes.Any())
        { return -1; }

        var attribute = (ToRenderTextureCubeAttribute) possibleAttributes.First();
        return attribute.RenderTextureId;
    }
}