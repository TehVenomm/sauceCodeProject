using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class PostAuthor : PublicUser, IConvertableFromNative<PostAuthor>
	{
		public bool IsVerified
		{
			get;
			private set;
		}

		public PostAuthor()
		{
		}

		internal PostAuthor(Dictionary<string, string> publicProperties, string id, string displayName, string avatarUrl, Dictionary<string, string> identities, bool isVerified)
			: base(publicProperties, id, displayName, avatarUrl, identities)
		{
			IsVerified = isVerified;
		}

		public override string ToString()
		{
			return $"[PostAuthor: Id={base.Id}, DisplayName={base.DisplayName}, Identities={base.Identities.ToDebugString()}, IsVerified={IsVerified}]";
		}

		private bool Equals(PostAuthor other)
		{
			return Equals((PublicUser)other) && IsVerified == other.IsVerified;
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
			return obj is PostAuthor && Equals((PostAuthor)obj);
		}

		public override int GetHashCode()
		{
			return (base.GetHashCode() * 397) ^ IsVerified.GetHashCode();
		}

		public new PostAuthor ParseFromAJO(AndroidJavaObject ajo)
		{
			try
			{
				base.ParseFromAJO(ajo);
				IsVerified = ajo.CallBool("isVerified");
				return this;
			}
			finally
			{
				((IDisposable)ajo)?.Dispose();
			}
		}
	}
}
