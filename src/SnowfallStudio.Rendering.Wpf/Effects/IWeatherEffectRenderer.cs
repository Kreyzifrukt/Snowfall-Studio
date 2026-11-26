using System.Windows.Media;
using SnowfallStudio.Core.Effects;

namespace SnowfallStudio.Rendering.Wpf.Effects;

/// <summary>WPF drawing adapter for an effect's current simulation state.</summary>
public interface IWeatherEffectRenderer
{
    string EffectId { get; }
    void Draw(DrawingContext drawingContext, in EffectFrameContext frameContext);
}
