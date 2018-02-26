using UnityEngine;

public static class ColorExtensions
{
    private static System.Random random = new System.Random();

    public static Color Random(
        this Color color,
        float hueMin = 0, float hueMax = 1,
        float saturationMin = 0, float saturationMax = 1,
        float valueMin = 0, float valueMax = 1,
        float alpha = 1
    )
    {
        Color rgbColor = RandomRGBColor(
            hueMin, hueMax,
            saturationMin, saturationMax,
            valueMin, valueMax
        );
        color.r = rgbColor.r;
        color.g = rgbColor.g;
        color.b = rgbColor.b;

        color.a = alpha;

        return color;
    }

    private static Color RandomRGBColor(
        float hueMin, float hueMax,
        float saturationMin, float saturationMax,
        float valueMin, float valueMax)
    {
        float hue = random.NextInRange(hueMin, hueMax);
        float saturation = random.NextInRange(saturationMin, saturationMax);
        float val = random.NextInRange(valueMin, valueMax);
        Color color = Color.HSVToRGB(hue, saturation, val);

        return color;
    }
}
