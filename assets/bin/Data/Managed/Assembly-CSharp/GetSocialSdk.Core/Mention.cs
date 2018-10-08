using UnityEngine;

namespace GetSocialSdk.Core
{
	public class Mention : IConvertableFromNative<Mention>
	{
		public string UserId
		{
			get;
			private set;
		}

		public int StartIndex
		{
			get;
			private set;
		}

		public int EndIndex
		{
			get;
			private set;
		}

		public string Type
		{
			get;
			private set;
		}

		public Mention()
		{
		}

		internal Mention(string userId, int startIndex, int endIndex, string type)
		{
			UserId = userId;
			StartIndex = startIndex;
			EndIndex = endIndex;
			Type = type;
		}

		public override string ToString()
		{
			return $"{UserId} ({StartIndex}, {EndIndex}) - {Type}";
		}

		protected bool Equals(Mention other)
		{
			return string.Equals(UserId, other.UserId) && StartIndex == other.StartIndex && EndIndex == other.EndIndex && string.Equals(Type, other.Type);
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
			return Equals((Mention)obj);
		}

		public override int GetHashCode()
		{
			int num = (UserId != null) ? UserId.GetHashCode() : 0;
			num = ((num * 397) ^ StartIndex);
			num = ((num * 397) ^ EndIndex);
			return (num * 397) ^ ((Type != null) ? Type.GetHashCode() : 0);
		}

		public Mention ParseFromAJO(AndroidJavaObject ajo)
		{
			UserId = ajo.CallStr("getUserId");
			StartIndex = ajo.CallInt("getStartIndex");
			EndIndex = ajo.CallInt("getEndIndex");
			Type = ajo.CallStr("getType");
			return this;
		}
	}
}
