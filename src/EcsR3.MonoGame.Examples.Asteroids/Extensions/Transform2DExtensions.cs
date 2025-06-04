using EcsR3.MonoGame.Examples.Asteroids.Game.Components;
using Microsoft.Xna.Framework;
using SystemsR3.Plugins.Transforms.Extensions;
using SystemsR3.Plugins.Transforms.Models;
using Vector2 = System.Numerics.Vector2;

namespace EcsR3.MonoGame.Examples.Asteroids.Extensions;

public static class Transform2DExtensions
{
    public static Rectangle GetCollisionArea(this Transform2D transform, ColliderComponent collider)
    {
        return new Rectangle(
        (int)transform.Position.X, 
        (int)transform.Position.Y,
        (int)collider.Width, (int)collider.Height);
    }
    
    /// <summary>
    /// This is an MG fudged version of Right
    /// </summary>
    /// <remarks>https://community.monogame.net/t/asteroids-and-vector-forward/16729/7 apparently MG has an offset</remarks>
    public static Vector2 MGRight(this Transform2D transform)
    {
        return (transform.Rotation).RadiansToVector2();
    }
    /// <summary>
    /// This is an MG fudged version of Forward
    /// </summary>
    /// <remarks>https://community.monogame.net/t/asteroids-and-vector-forward/16729/7 apparently MG has an offset</remarks>
    public static Vector2 MGForward(this Transform2D transform)
    {
        return (transform.Rotation + MathConstants.ToRadians(-90)).RadiansToVector2();
    }
    
    /// <summary>
    /// This is an MG fudged version of LookAt
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static float MGLookAt(this Transform2D transform, Vector2 target)
    {
        return transform.GetLookAt(target) + MathConstants.ToRadians(90);
    }
}