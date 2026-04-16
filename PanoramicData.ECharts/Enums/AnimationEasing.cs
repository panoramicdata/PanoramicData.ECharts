using System.Text.Json.Serialization;

namespace PanoramicData.ECharts;

/// <summary>
/// See https://echarts.apache.org/examples/en/editor.html?c=line-easing
/// </summary>
[JsonConverter(typeof(CamelCaseEnumConverter<AnimationEasing>))]
public enum AnimationEasing
{
	/// <summary>Constant speed animation with no acceleration or deceleration.</summary>
	Linear,
	/// <summary>Quadratic easing that accelerates from zero velocity.</summary>
	QuadraticIn,
	/// <summary>Quadratic easing that decelerates to zero velocity.</summary>
	QuadraticOut,
	/// <summary>Quadratic easing that accelerates then decelerates.</summary>
	QuadraticInOut,
	/// <summary>Cubic easing that accelerates from zero velocity.</summary>
	CubicIn,
	/// <summary>Cubic easing that decelerates to zero velocity.</summary>
	CubicOut,
	/// <summary>Cubic easing that accelerates then decelerates.</summary>
	CubicInOut,
	/// <summary>Quartic easing that accelerates from zero velocity.</summary>
	QuarticIn,
	/// <summary>Quartic easing that decelerates to zero velocity.</summary>
	QuarticOut,
	/// <summary>Quartic easing that accelerates then decelerates.</summary>
	QuarticInOut,
	/// <summary>Quintic easing that accelerates from zero velocity.</summary>
	QuinticIn,
	/// <summary>Quintic easing that decelerates to zero velocity.</summary>
	QuinticOut,
	/// <summary>Quintic easing that accelerates then decelerates.</summary>
	QuinticInOut,
	/// <summary>Sinusoidal easing that accelerates from zero velocity.</summary>
	SinusoidalIn,
	/// <summary>Sinusoidal easing that decelerates to zero velocity.</summary>
	SinusoidalOut,
	/// <summary>Sinusoidal easing that accelerates then decelerates.</summary>
	SinusoidalInOut,
	/// <summary>Exponential easing that accelerates from zero velocity.</summary>
	ExponentialIn,
	/// <summary>Exponential easing that decelerates to zero velocity.</summary>
	ExponentialOut,
	/// <summary>Exponential easing that accelerates then decelerates.</summary>
	ExponentialInOut,
	/// <summary>Circular easing that accelerates from zero velocity.</summary>
	CircularIn,
	/// <summary>Circular easing that decelerates to zero velocity.</summary>
	CircularOut,
	/// <summary>Circular easing that accelerates then decelerates.</summary>
	CircularInOut,
	/// <summary>Elastic (spring-like overshoot) easing that accelerates from zero velocity.</summary>
	ElasticIn,
	/// <summary>Elastic (spring-like overshoot) easing that decelerates to zero velocity.</summary>
	ElasticOut,
	/// <summary>Elastic (spring-like overshoot) easing that accelerates then decelerates.</summary>
	ElasticInOut,
	/// <summary>Back easing (slight overshoot) that accelerates from zero velocity.</summary>
	BackIn,
	/// <summary>Back easing (slight overshoot) that decelerates to zero velocity.</summary>
	BackOut,
	/// <summary>Back easing (slight overshoot) that accelerates then decelerates.</summary>
	BackInOut,
	/// <summary>Bounce easing that accelerates from zero velocity.</summary>
	BounceIn,
	/// <summary>Bounce easing that decelerates to zero velocity.</summary>
	BounceOut,
	/// <summary>Bounce easing that accelerates then decelerates.</summary>
	BounceInOut
}
