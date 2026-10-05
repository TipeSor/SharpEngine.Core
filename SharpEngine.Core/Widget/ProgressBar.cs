using JetBrains.Annotations;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which represents ProgressBar
/// </summary>
public class ProgressBar : Widget
{
    /// <summary>
    /// Value of Bar (0 to 100)
    /// </summary>
    [UsedImplicitly]
    public float Value
    {
        get;
        set => field = MathHelper.Clamp(value, 0, 100);
    } = 0;

    /// <summary>
    /// Size of Bar
    /// </summary>
    [UsedImplicitly]
    public Vec2 Size { get; set; } = new Vec2(150, 60);

    /// <summary>
    /// Color of Bar
    /// </summary>
    [UsedImplicitly] 
    public Color Color { get; set; } = Color.Green;

    /// <summary>
    /// If Bar is Horizontal
    /// </summary>
    [UsedImplicitly]
    public bool Horizontal { get; set; } = true;

	/// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        if (!Displayed || Size == Vec2.Zero)
            return;

        SERender.DrawRectangle(
            new Rect(RealPosition, Size),
            Size / 2,
            0,
            Color.Black,
            InstructionSource.Ui,
            ZLayer
        );
        SERender.DrawRectangle(
            new Rect(RealPosition, Size - 4),
            (Size - 4) / 2,
            0,
            Color.White,
            InstructionSource.Ui,
            ZLayer + 0.00001f
        );
        SERender.DrawRectangle(
            Horizontal ? new Rect(RealPosition, (Size.X - 8) * Value / 100, Size.Y - 8) : new Rect(RealPosition, Size.X - 8, (Size.Y - 8) * Value / 100),
            (Size - 8) / 2,
            0,
            Color,
            InstructionSource.Ui,
            ZLayer + 0.00002f
        );
    }
}
