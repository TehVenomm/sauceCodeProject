using UnityEngine;

namespace GetSocialSdk.Core
{
	public interface IConvertableFromNative<out T>
	{
		T ParseFromAJO(AndroidJavaObject ajo);
	}
}
