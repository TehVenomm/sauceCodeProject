using UnityEngine;

namespace GetSocialSdk.Core
{
	public interface IConvertableToNative
	{
		AndroidJavaObject ToAjo();
	}
}
