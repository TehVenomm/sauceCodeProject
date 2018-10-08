using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public class PublicUser : IConvertableFromNative<PublicUser>
	{
		private Dictionary<string, string> _publicProperties;

		public string Id
		{
			get;
			protected set;
		}

		public string DisplayName
		{
			get;
			protected set;
		}

		public string AvatarUrl
		{
			get;
			protected set;
		}

		public Dictionary<string, string> Identities
		{
			get;
			protected set;
		}

		public Dictionary<string, string> AllPublicProperties => new Dictionary<string, string>(_publicProperties);

		public PublicUser()
		{
		}

		internal PublicUser(Dictionary<string, string> publicProperties, string id, string displayName, string avatarUrl, Dictionary<string, string> identities)
		{
			_publicProperties = publicProperties;
			Id = id;
			DisplayName = displayName;
			AvatarUrl = avatarUrl;
			Identities = identities;
		}

		public override string ToString()
		{
			return $"[PublicUser: Id={Id}, DisplayName={DisplayName}, Identities={Identities.ToDebugString()}, PublicProperties={_publicProperties.ToDebugString()}]";
		}

		protected bool Equals(PublicUser other)
		{
			return _publicProperties.DictionaryEquals(other._publicProperties) && string.Equals(Id, other.Id) && string.Equals(DisplayName, other.DisplayName) && string.Equals(AvatarUrl, other.AvatarUrl) && Identities.DictionaryEquals(other.Identities);
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
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((PublicUser)obj);
		}

		public override int GetHashCode()
		{
			int num = (_publicProperties != null) ? _publicProperties.GetHashCode() : 0;
			num = ((num * 397) ^ ((Id != null) ? Id.GetHashCode() : 0));
			num = ((num * 397) ^ ((DisplayName != null) ? DisplayName.GetHashCode() : 0));
			num = ((num * 397) ^ ((AvatarUrl != null) ? AvatarUrl.GetHashCode() : 0));
			return (num * 397) ^ ((Identities != null) ? Identities.GetHashCode() : 0);
		}

		public PublicUser ParseFromAJO(AndroidJavaObject ajo)
		{
			Id = ajo.CallStr("getId");
			DisplayName = ajo.CallStr("getDisplayName");
			AvatarUrl = ajo.CallStr("getAvatarUrl");
			Identities = ajo.CallAJO("getIdentities").FromJavaHashMap();
			_publicProperties = ajo.CallAJO("getAllPublicProperties").FromJavaHashMap();
			return this;
		}
	}
}
