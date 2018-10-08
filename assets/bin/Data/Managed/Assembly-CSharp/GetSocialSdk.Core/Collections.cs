using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public static class Collections
	{
		public static bool DictionaryEquals<TKey, TValue>(this Dictionary<TKey, TValue> self, Dictionary<TKey, TValue> other)
		{
			return self.Count == other.Count && !self.Except(other).Any();
		}

		public unsafe static bool ListEquals<T>(this List<T> self, List<T> other)
		{
			if (self.Count != other.Count)
			{
				return false;
			}
			_003CListEquals_003Ec__AnonStorey7EC<T> _003CListEquals_003Ec__AnonStorey7EC;
			return !Enumerable.Where<T>((IEnumerable<T>)self, new Func<_003F, int, bool>((object)_003CListEquals_003Ec__AnonStorey7EC, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).Any();
		}

		public static bool Texture2DEquals(this Texture2D self, Texture2D other)
		{
			if (self == other)
			{
				return true;
			}
			if (self == null || other == null)
			{
				return false;
			}
			return self.GetPixels().ToList().ListEquals(other.GetPixels().ToList());
		}
	}
}
