using Raylib_cs;
using SharpEngine.Core.Math;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws line.
/// </summary>
internal record DrawLineV : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawLineV(
            (Vec2)Parameters[0],
            (Vec2)Parameters[1],
            (Utils.Color)Parameters[2]
        );
    }
}

