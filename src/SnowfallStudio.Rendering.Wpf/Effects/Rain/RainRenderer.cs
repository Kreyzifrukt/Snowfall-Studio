using System.Windows.Media;
using SnowfallStudio.Core.Effects;

namespace SnowfallStudio.Rendering.Wpf.Effects.Rain;

/// <summary>Placeholder renderer. It intentionally draws nothing.</summary>
public sealed class RainRenderer : IWeatherEffectRenderer
{
    public string EffectId => WeatherEffectId.Rain;

    public void Draw(DrawingContext drawingContext, in EffectFrameContext frameContext)
    {
        // TODO: Draw rain streaks after RainEffect exposes a render snapshot.
    }
}
