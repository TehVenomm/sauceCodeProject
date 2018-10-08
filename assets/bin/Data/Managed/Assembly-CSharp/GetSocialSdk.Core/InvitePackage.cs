using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class InvitePackage : IConvertableFromNative<InvitePackage>
	{
		public string Subject
		{
			get;
			private set;
		}

		public string Text
		{
			get;
			private set;
		}

		public string UserName
		{
			get;
			private set;
		}

		public string ReferralDataUrl
		{
			get;
			private set;
		}

		public Texture2D Image
		{
			get;
			private set;
		}

		public string ImageUrl
		{
			get;
			private set;
		}

		public InvitePackage()
		{
		}

		internal InvitePackage(string subject, string text, string userName, string referralDataUrl, Texture2D image, string imageUrl)
		{
			Subject = subject;
			Text = text;
			UserName = userName;
			ReferralDataUrl = referralDataUrl;
			Image = image;
			ImageUrl = imageUrl;
		}

		public override string ToString()
		{
			return $"[InvitePackage: Subject={Subject}, Text={Text}, UserName={UserName}, HasImage={Image != null}, ImageUrl={ImageUrl}, ReferralDataUrl={ReferralDataUrl}]";
		}

		private bool Equals(InvitePackage other)
		{
			return string.Equals(Subject, other.Subject) && string.Equals(Text, other.Text) && string.Equals(UserName, other.UserName) && string.Equals(ReferralDataUrl, other.ReferralDataUrl) && Image.Texture2DEquals(other.Image) && string.Equals(ImageUrl, other.ImageUrl);
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
			return obj is InvitePackage && Equals((InvitePackage)obj);
		}

		public override int GetHashCode()
		{
			int num = (Subject != null) ? Subject.GetHashCode() : 0;
			num = ((num * 397) ^ ((Text != null) ? Text.GetHashCode() : 0));
			num = ((num * 397) ^ ((UserName != null) ? UserName.GetHashCode() : 0));
			num = ((num * 397) ^ ((ReferralDataUrl != null) ? ReferralDataUrl.GetHashCode() : 0));
			num = ((num * 397) ^ ((Image != null) ? Image.GetHashCode() : 0));
			return (num * 397) ^ ((ImageUrl != null) ? ImageUrl.GetHashCode() : 0);
		}

		public InvitePackage ParseFromAJO(AndroidJavaObject ajo)
		{
			JniUtils.CheckIfClassIsCorrect(ajo, "InvitePackage");
			try
			{
				Subject = ajo.CallStr("getSubject");
				Text = ajo.CallStr("getText");
				UserName = ajo.CallStr("getUserName");
				ReferralDataUrl = ajo.CallStr("getReferralUrl");
				Image = ajo.CallAJO("getImage").FromAndroidBitmap();
				ImageUrl = ajo.CallStr("getImageUrl");
				return this;
			}
			finally
			{
				((IDisposable)ajo)?.Dispose();
			}
		}
	}
}
