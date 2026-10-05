using JetBrains.Annotations;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display frame
/// </summary>
public class Frame : Widget
{
    /// <summary>
    /// Color of Frame Border
    /// </summary>
    [UsedImplicitly]
    public Color BorderColor { get; set; } = Color.Black;

    /// <summary>
    /// Size of Frame
    /// </summary>
    [UsedImplicitly]
    public required Vec2 Size { get; set; }

    /// <summary>
    /// Size of Frame Border
    /// </summary>
    [UsedImplicitly]
    public int BorderSize { get; set; } = 3;

    /// <summary>
    /// Color of Frame Background
    /// </summary>
    [UsedImplicitly]
    public Color? BackgroundColor { get; set; }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (!Displayed || Size == Vec2.Zero)
            return;

        if (BackgroundColor != null)
            SERender.DrawRectangle(
                new Rect(RealPosition.X, RealPosition.Y, Size.X, Size.Y),
                Size / 2,
                0,
                BackgroundColor.Value,
                InstructionSource.Ui,
                ZLayer
            );
        SERender.DrawRectangleLines(
            new Rect(RealPosition.X - Size.X / 2, RealPosition.Y - Size.Y / 2, Size.X, Size.Y),
            BorderSize,
            BorderColor,
            InstructionSource.Ui,
            ZLayer + 0.00001f
        );
    }
}
