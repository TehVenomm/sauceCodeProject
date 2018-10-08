using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class LinkParams : Dictionary<string, object>, IConvertableFromNative<LinkParams>, IConvertableToNative
	{
		public const string KeyCustomTitle = "$title";

		public const string KeyCustomDescription = "$description";

		public const string KeyCustomImage = "$image";

		public const string KeyCustomYouTubeVideo = "$youtube_video";

		public LinkParams()
		{
		}

		public LinkParams(Dictionary<string, string> data)
		{
			if (data != null)
			{
				foreach (KeyValuePair<string, string> datum in data)
				{
					this[datum.Key] = datum.Value;
				}
			}
		}

		public LinkParams(Dictionary<string, object> data)
		{
			if (data != null)
			{
				foreach (KeyValuePair<string, object> datum in data)
				{
					this[datum.Key] = datum.Value;
				}
			}
		}

		public override string ToString()
		{
			return $"[LinkParams: {this.ToDebugString()}]";
		}

		private bool Equals(LinkParams other)
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
			return obj is LinkParams && Equals((LinkParams)obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public AndroidJavaObject ToAjo()
		{
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			Dictionary<string, object> dictionary = new Dictionary<string, object>(this);
			if (dictionary.ContainsKey("$image"))
			{
				object obj = dictionary["$image"];
				Texture2D val = obj as Texture2D;
				if (val != null)
				{
					dictionary["$image"] = val.ToAjoBitmap();
				}
			}
			return new AndroidJavaObject("im.getsocial.sdk.invites.LinkParams", new object[1]
			{
				dictionary.ToJavaHashMap()
			});
		}

		public LinkParams ParseFromAJO(AndroidJavaObject ajo)
		{
			return new LinkParams(ajo.FromJavaHashMap());
		}
	}
}
