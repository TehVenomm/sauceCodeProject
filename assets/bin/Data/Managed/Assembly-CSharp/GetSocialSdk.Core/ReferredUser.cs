using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class ReferredUser : PublicUser, IConvertableFromNative<ReferredUser>
	{
		public DateTime InstallationDate
		{
			get;
			private set;
		}

		public string InstallationChannel
		{
			get;
			private set;
		}

		public ReferredUser()
		{
		}

		internal ReferredUser(Dictionary<string, string> publicProperties, string id, string displayName, string avatarUrl, Dictionary<string, string> identities, DateTime installationDate, string installationChannel)
			: base(publicProperties, id, displayName, avatarUrl, identities)
		{
			InstallationDate = installationDate;
			InstallationChannel = installationChannel;
		}

		public override string ToString()
		{
			return $"[ReferredUser: Id={base.Id}, DisplayName={base.DisplayName}, Identities={base.Identities.ToDebugString()}, InstallationDate={InstallationDate}, InstallationChannel={InstallationChannel}]";
		}

		private bool Equals(ReferredUser other)
		{
			return Equals((PublicUser)other) && InstallationDate.Equals(other.InstallationDate) && string.Equals(InstallationChannel, other.InstallationChannel);
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
			return obj is ReferredUser && Equals((ReferredUser)obj);
		}

		public override int GetHashCode()
		{
			int hashCode = base.GetHashCode();
			hashCode = ((hashCode * 397) ^ InstallationDate.GetHashCode());
			return (hashCode * 397) ^ ((InstallationChannel != null) ? InstallationChannel.GetHashCode() : 0);
		}

		public new ReferredUser ParseFromAJO(AndroidJavaObject ajo)
		{
			if (!ajo.IsJavaNull())
			{
				try
				{
					base.ParseFromAJO(ajo);
					InstallationDate = DateUtils.FromUnixTime(ajo.CallLong("getInstallationDate"));
					InstallationChannel = ajo.CallStr("getInstallationChannel");
					return this;
				}
				finally
				{
					((IDisposable)ajo)?.Dispose();
				}
			}
			return null;
		}
	}
}
