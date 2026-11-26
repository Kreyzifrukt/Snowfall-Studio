using System.Windows.Media;
using SnowfallStudio.Core.Effects;

namespace SnowfallStudio.Rendering.Wpf.Effects.Leaves;

/// <summary>Placeholder renderer. It intentionally draws nothing.</summary>
public sealed class LeavesRenderer : IWeatherEffectRenderer
{
    public string EffectId => WeatherEffectId.Leaves;

    public void Draw(DrawingContext drawingContext, in EffectFrameContext frameContext)
    {
        // TODO: Draw leaf geometry after LeavesEffect exposes a render snapshot.
    }
}
