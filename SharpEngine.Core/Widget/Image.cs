using JetBrains.Annotations;
using SharpEngine.Core.Math;
using SharpEngine.Core.Renderer;
using SharpEngine.Core.Utils;

namespace SharpEngine.Core.Widget;

/// <summary>
/// Class which display Image
/// </summary>
public class Image : Widget
{
    /// <summary>
    /// Name of Texture which be displayed
    /// </summary>
    [UsedImplicitly]
    public string Texture { get; set; } = string.Empty;

    /// <summary>
    /// Scale of Image
    /// </summary>
    [UsedImplicitly]
    public Vec2 Scale { get; set; } = Vec2.One;

    /// <summary>
    /// Rotation of Image
    /// </summary>
    [UsedImplicitly]
    public int Rotation { get; set; } = 0;

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var window = Scene?.Window;

        if (!Displayed || Texture.Length <= 0 || window == null)
            return;

        var finalTexture = window.TextureManager.GetTexture(Texture);
        SERender.DrawTexture(
            finalTexture,
            new Rect(0, 0, finalTexture.Width, finalTexture.Height),
            new Rect(RealPosition.X, RealPosition.Y, finalTexture.Width * Scale.X, finalTexture.Height * Scale.Y),
            new Vec2(finalTexture.Width / 2f * Scale.X, finalTexture.Height / 2f * Scale.Y),
            Rotation,
            Color.White,
            InstructionSource.Ui,
            ZLayer
        );
    }
}
