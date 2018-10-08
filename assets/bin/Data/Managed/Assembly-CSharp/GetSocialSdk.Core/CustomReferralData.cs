using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	[Obsolete("Deprecated. Use LinkParams class instead.")]
	public sealed class CustomReferralData : Dictionary<string, string>, IConvertableFromNative<CustomReferralData>, IConvertableToNative
	{
		public CustomReferralData()
		{
		}

		public CustomReferralData(Dictionary<string, string> data)
			: base((IDictionary<string, string>)data)
		{
		}

		public CustomReferralData(IDictionary<string, object> data)
		{
			if (data != null)
			{
				foreach (KeyValuePair<string, object> datum in data)
				{
					this[datum.Key] = (datum.Value as string);
				}
			}
		}

		public override string ToString()
		{
			return $"[CustomReferralData: {this.ToDebugString()}]";
		}

		private bool Equals(CustomReferralData other)
		{
			return this.DictionaryEquals(other);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			return obj is CustomReferralData && Equals((CustomReferralData)obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public AndroidJavaObject ToAjo()
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			return new AndroidJavaObject("im.getsocial.sdk.invites.CustomReferralData", new object[1]
			{
				this.ToJavaHashMap()
			});
		}

		public CustomReferralData ParseFromAJO(AndroidJavaObject ajo)
		{
			try
			{
				return new CustomReferralData(ajo.FromJavaHashMap());
				IL_0013:
				CustomReferralData result;
				return result;
			}
			finally
			{
				((IDisposable)ajo)?.Dispose();
			}
		}
	}
}
