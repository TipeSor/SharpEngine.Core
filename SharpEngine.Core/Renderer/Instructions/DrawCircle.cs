using Raylib_cs;
using System;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws circle.
/// </summary>
internal record DrawCircle : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawCircle(
            Convert.ToInt32(Parameters[0]),
            Convert.ToInt32(Parameters[1]),
            Convert.ToSingle(Parameters[2]),
            (Utils.Color)Parameters[3]
        );
    }
}
