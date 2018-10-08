using System;
using System.Collections.Generic;

namespace BestHTTP.Authentication
{
	internal static class DigestStore
	{
		private static Dictionary<string, Digest> Digests = new Dictionary<string, Digest>();

		public static Digest Get(Uri uri)
		{
			Digest value = null;
			if (Digests.TryGetValue(uri.Host, out value) && !value.IsUriProtected(uri))
			{
				return null;
			}
			return value;
		}

		public static Digest GetOrCreate(Uri uri)
		{
			Digest value = null;
			if (!Digests.TryGetValue(uri.Host, out value))
			{
				Digests.Add(uri.Host, value = new Digest(uri));
			}
			return value;
		}

		public static void Remove(Uri uri)
		{
			Digests.Remove(uri.Host);
		}
	}
}
