using JetBrains.Annotations;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display colored Rect
/// </summary>
[UsedImplicitly]
public class ColorRect : Widget
{
    /// <summary>
    /// Color of Rect
    /// </summary>
    [UsedImplicitly]
    public Color Color { get; set; } = Color.Black;

    /// <summary>
    /// Size of Rect
    /// </summary>
    [UsedImplicitly]
    public Vec2 Size { get; set; } = Vec2.One;

    /// <summary>
    /// Rotation of Rect
    /// </summary>
    [UsedImplicitly]
    public int Rotation { get; set; } = 0;

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (!Displayed || Size == Vec2.Zero)
            return;

        SERender.DrawRectangle(
            new Rect(RealPosition.X, RealPosition.Y, Size.X, Size.Y),
            Size / 2,
            Rotation,
            Color,
            InstructionSource.Ui,
            ZLayer
        );
    }
}
