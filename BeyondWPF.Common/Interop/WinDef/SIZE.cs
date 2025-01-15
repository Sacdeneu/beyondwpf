using System.Runtime.InteropServices;

namespace BeyondWPF.Common.Interop.WinDef;

[StructLayout(LayoutKind.Sequential)]
public struct SIZE
{
    public long cx;

    public long cy;
}