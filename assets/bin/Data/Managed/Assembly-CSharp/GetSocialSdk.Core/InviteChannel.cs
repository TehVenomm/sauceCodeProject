using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class InviteChannel : IConvertableFromNative<InviteChannel>
	{
		public string Id
		{
			get;
			private set;
		}

		public string Name
		{
			get;
			private set;
		}

		public string IconImageUrl
		{
			get;
			private set;
		}

		public int DisplayOrder
		{
			get;
			private set;
		}

		public bool IsEnabled
		{
			get;
			private set;
		}

		internal InviteChannel(string id, string name, string iconImageUrl, int displayOrder, bool isEnabled)
		{
			Id = id;
			Name = name;
			IconImageUrl = iconImageUrl;
			DisplayOrder = displayOrder;
			IsEnabled = isEnabled;
		}

		public InviteChannel()
		{
		}

		public override string ToString()
		{
			return $"[InviteChannel: Id={Id}, Name={Name}, IconImageUrl={IconImageUrl}, DisplayOrder={DisplayOrder}, IsEnabled={IsEnabled}]";
		}

		private bool Equals(InviteChannel other)
		{
			return string.Equals(Id, other.Id) && string.Equals(Name, other.Name) && string.Equals(IconImageUrl, other.IconImageUrl) && DisplayOrder == other.DisplayOrder && IsEnabled == other.IsEnabled;
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
			return obj is InviteChannel && Equals((InviteChannel)obj);
		}

		public override int GetHashCode()
		{
			int num = (Id != null) ? Id.GetHashCode() : 0;
			num = ((num * 397) ^ ((Name != null) ? Name.GetHashCode() : 0));
			num = ((num * 397) ^ ((IconImageUrl != null) ? IconImageUrl.GetHashCode() : 0));
			num = ((num * 397) ^ DisplayOrder);
			return (num * 397) ^ IsEnabled.GetHashCode();
		}

		public InviteChannel ParseFromAJO(AndroidJavaObject ajo)
		{
			JniUtils.CheckIfClassIsCorrect(ajo, "InviteChannel");
			try
			{
				Id = ajo.CallStr("getChannelId");
				Name = ajo.CallStr("getChannelName");
				IconImageUrl = ajo.CallStr("getIconImageUrl");
				DisplayOrder = ajo.CallInt("getDisplayOrder");
				IsEnabled = ajo.CallBool("isEnabled");
				return this;
			}
			finally
			{
				((IDisposable)ajo)?.Dispose();
			}
		}
	}
}
