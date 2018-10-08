using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public class ConflictUser : PublicUser, IConvertableFromNative<ConflictUser>
	{
		public new ConflictUser ParseFromAJO(AndroidJavaObject ajo)
		{
			try
			{
				base.ParseFromAJO(ajo);
				return this;
			}
			finally
			{
				((IDisposable)ajo)?.Dispose();
			}
		}
	}
}
