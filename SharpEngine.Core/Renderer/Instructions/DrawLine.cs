using Raylib_cs;
using System;

namespace SharpEngine.Core.Renderer.Instructions;

/// <summary>
/// Draws line.
/// </summary>
internal record DrawLine : Instruction
{
    internal override void Execute()
    {
        base.Execute();
        Raylib.DrawLine(
            Convert.ToInt32(Parameters[0]),
            Convert.ToInt32(Parameters[1]),
            Convert.ToInt32(Parameters[2]),
            Convert.ToInt32(Parameters[3]),
            (Utils.Color)Parameters[4]
        );
    }
}
