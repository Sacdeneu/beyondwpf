namespace BeyondWPF.Common.Hardware;


public readonly struct DisplayDpi
{
    public DisplayDpi(double dpiScaleX, double dpiScaleY)
    {
        DpiScaleX = dpiScaleX;
        DpiScaleY = dpiScaleY;

        DpiX = (int)Math.Round(DpiHelper.DefaultDpi * dpiScaleX, MidpointRounding.AwayFromZero);
        DpiY = (int)Math.Round(DpiHelper.DefaultDpi * dpiScaleY, MidpointRounding.AwayFromZero);
    }

    public DisplayDpi(int dpiX, int dpiY)
    {
        DpiX = dpiX;
        DpiY = dpiY;

        DpiScaleX = dpiX / (double)DpiHelper.DefaultDpi;
        DpiScaleY = dpiY / (double)DpiHelper.DefaultDpi;
    }

    public int DpiX { get; }


    public int DpiY { get; }


    public double DpiScaleX { get; }

    public double DpiScaleY { get; }
}
