using System;
using System.Numerics;
using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Component;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Manager;

/// <summary>
/// Class which manager Camera information
/// </summary>
public class CameraManager
{
    /// <summary>
    /// Entity followed on mode Follow and FollowSmooth
    /// <seealso cref="Mode"/>
    /// </summary>
    [UsedImplicitly]
    public Entity.Entity? FollowEntity { get; set; }

    /// <summary>
    /// Camera Mode
    /// </summary>
    [UsedImplicitly]
    public Utils.CameraMode Mode { get; set; }

    /// <summary>
    /// Minimum Speed used when the mode is FollowSmooth
    /// </summary>
    [UsedImplicitly]
    public float MinSpeed { get; set; } = 30;

    /// <summary>
    /// Minimum Effect Length used when the mode is FollowSmooth
    /// </summary>
    [UsedImplicitly]
    public float MinEffectLength { get; set; } = 10;

    /// <summary>
    /// Fraction Speed used when the mode is FollowSmooth
    /// </summary>
    [UsedImplicitly]
    public float FractionSpeed { get; set; } = 0.8f;

    /// <summary>
    /// Rotation of Camera
    /// </summary>
    [UsedImplicitly]
    public float Rotation
    {
        get => Camera2D.Rotation;
        set => Camera2D.Rotation = value;
    }

    /// <summary>
    /// Zoom of Camera
    /// </summary>
    [UsedImplicitly]
    public float Zoom
    {
        get => Camera2D.Zoom;
        set => Camera2D.Zoom = value;
    }

    /// <summary>
    /// Position of Camera
    /// </summary>
    [UsedImplicitly]
    public Vec2 Position
    {
        get => Camera2D.Target;
        set => Camera2D.Target = value;
    }

    internal Camera2D Camera2D;

    /// <summary>
    /// Create Camera Manager
    /// </summary>
    public CameraManager()
    {
        Camera2D = new Camera2D(Vector2.Zero, Vector2.Zero, 0, 1);
        Mode = Utils.CameraMode.Basic;
    }

    internal void SetScreenSize(Vec2 screenSize)
    {
        Camera2D.Offset = screenSize / 2;
        if (Mode == Utils.CameraMode.Basic)
            Camera2D.Target = Camera2D.Offset;
    }

    /// <summary>
    /// Update Camera Manager
    /// </summary>
    /// <param name="delta">Delta frame</param>
    public void Update(float delta)
    {
        switch (Mode)
        {
            case Utils.CameraMode.Follow:
                if (FollowEntity?.GetComponentAs<TransformComponent>() is { } transform)
                    Camera2D.Target = transform.Position;
                break;
            case Utils.CameraMode.FollowSmooth:
                if (FollowEntity?.GetComponentAs<TransformComponent>() is { } transformSmooth)
                {
                    var diff = transformSmooth.Position - (Vec2)Camera2D.Target;
                    var length = diff.Length();
                    if (length > MinEffectLength)
                    {
                        var speed = MathF.Max(FractionSpeed * length, MinSpeed);
                        Camera2D.Target += new Vector2(
                            diff.X * (speed * delta / length),
                            diff.Y * (speed * delta / length)
                        );
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Transform position from screen space to world space
    /// </summary>
    /// <param name="screen">Position in screen space</param>
    /// <returns>Position in world space</returns>
    public Vec2 ScreenToWorld(Vec2 screen)
    {
        var mat = GetCameraMatrix();
        Matrix4x4.Invert(mat, out var inv);

        Vector3 result = Vector3.Transform(new Vector3(screen.X, screen.Y, 0), inv);
        return new Vec2(result.X, result.Y);
    }

    /// <summary>
    /// Transform position from world space to screen space
    /// </summary>
    /// <param name="world">Position in world space</param>
    /// <returns>Position in screen space</returns>
    public Vec2 WorldToScreen(Vec2 world)
    {
        var mat = GetCameraMatrix();
        Vector3 result = Vector3.Transform(new Vector3(world.X, world.Y, 0), mat);
        return new Vec2(result.X, result.Y);
    }

    /// <summary>
    /// Get camera transformation matrix
    /// </summary>
    /// <returns>Transformation matrix from world space to screen space</returns>
    public Matrix4x4 GetCameraMatrix()
    {
        float rot = MathF.PI / 180f * Rotation;

        return
            Matrix4x4.CreateTranslation(-Position.X, -Position.Y, 0) *
            Matrix4x4.CreateRotationZ(rot) *
            Matrix4x4.CreateScale(Zoom, Zoom, 1) *
            Matrix4x4.CreateTranslation(Camera2D.Offset.X, Camera2D.Offset.Y, 0);
    }
}
