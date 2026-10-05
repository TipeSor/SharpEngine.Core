using JetBrains.Annotations;
using Raylib_cs;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Widget.Utils;
using Color = SharpEngine.Core.Utils.Color;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display Label
/// </summary>
public class Label : Widget
{
    /// <summary>
    /// Text of Label
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Font of Label
    /// </summary>
    [UsedImplicitly]
    public string Font { get; set; } = string.Empty;

    /// <summary>
    /// Color of Label
    /// </summary>
    [UsedImplicitly]
    public Color Color { get; set; } = Color.Black;

    /// <summary>
    /// If Lines is Centered
    /// </summary>
    [UsedImplicitly]
    public bool CenterAllLines { get; set; } = false;

    /// <summary>
    /// Rotation of Label
    /// </summary>
    [UsedImplicitly]
    public int Rotation { get; set; } = 0;

    /// <summary>
    /// Font Size of Label (or Null)
    /// </summary>
    [UsedImplicitly]
    public int? FontSize { get; set; }

    /// <summary>
    /// Style of Label
    /// </summary>
    [UsedImplicitly]
    public LabelStyles Style { get; set; } = LabelStyles.None;

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var finalFont = Scene?.Window?.FontManager.GetFont(Font);

        if (!Displayed || Scene == null || Text.Length <= 0 || Font.Length <= 0 || finalFont == null)
            return;

        var finalFontSize = FontSize ?? finalFont.Value.BaseSize;

        var textSize = Raylib.MeasureTextEx(finalFont.Value, Text, finalFontSize, 2);

        var lines = Text.Split("\n");
        for (var i = 0; i < lines.Length; i++)
        {
            var lineSize = Raylib.MeasureTextEx(finalFont.Value, lines[i], finalFontSize, 2);
            var finalPosition = new Vec2(
                CenterAllLines ? RealPosition.X - lineSize.X / 2 : RealPosition.X - textSize.X / 2,
                RealPosition.Y - textSize.Y / 2 + i * lineSize.Y
            );
            SERender.DrawText(
                finalFont.Value,
                lines[i],
                finalPosition,
                Vec2.Zero,
                Rotation,
                finalFontSize,
                2,
                Color,
                InstructionSource.Ui,
                ZLayer
            );

            if (Style.HasFlag(LabelStyles.Strike))
                SERender.DrawRectangle(
                    finalPosition.X,
                    finalPosition.Y + lineSize.Y / 2,
                    lineSize.X,
                    2,
                    Color.Black,
                    InstructionSource.Ui,
                    ZLayer + 0.00001f
                );
            if (Style.HasFlag(LabelStyles.Underline))
                SERender.DrawRectangle(
                    finalPosition.X,
                    finalPosition.Y + lineSize.Y,
                    lineSize.X,
                    2,
                    Color.Black,
                    InstructionSource.Ui,
                    ZLayer + 0.00001f
                );
        }
    }
}
