using QuestPDF.Helpers;
using SkiaSharp;

namespace Odonto.Application.Services.Util;

public class DocumentosUtil
{
    public static QuestPDF.Infrastructure.Image LoadImageWithTransparency(string arquivo, float transparencia)
    {
        using var originalImage = SKImage.FromEncodedData(arquivo);

        using var surface = SKSurface.Create(originalImage.Width, originalImage.Height, SKColorType.Rgba8888, SKAlphaType.Premul);
        using var canvas = surface.Canvas;

        using var transparenciayPaint = new SKPaint
        {
            ColorFilter = SKColorFilter.CreateBlendMode(SKColors.White.WithAlpha((byte)(transparencia * 255)), SKBlendMode.DstIn)
        };

        canvas.DrawImage(originalImage, new SKPoint(0, 0), transparenciayPaint);

        var encodedImage = surface.Snapshot().Encode(SKEncodedImageFormat.Png, 100).ToArray();
        return QuestPDF.Infrastructure.Image.FromBinaryData(encodedImage);
    }
}
