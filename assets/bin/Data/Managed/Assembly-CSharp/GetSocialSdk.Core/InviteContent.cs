using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class InviteContent : IConvertableToNative
	{
		public class Builder
		{
			private readonly InviteContent _inviteContent;

			protected internal Builder()
			{
				_inviteContent = new InviteContent();
			}

			public Builder WithSubject(string subject)
			{
				_inviteContent.Subject = subject;
				return this;
			}

			public Builder WithText(string text)
			{
				_inviteContent.Text = text;
				return this;
			}

			public Builder WithImageUrl(string imageUrl)
			{
				_inviteContent.ImageUrl = imageUrl;
				return this;
			}

			public Builder WithImage(Texture2D image)
			{
				_inviteContent.Image = image;
				return this;
			}

			public Builder WithVideo(byte[] videoBytes)
			{
				_inviteContent.Video = videoBytes;
				return this;
			}

			public InviteContent Build()
			{
				InviteContent inviteContent = new InviteContent();
				inviteContent.ImageUrl = _inviteContent.ImageUrl;
				inviteContent.Image = _inviteContent.Image;
				inviteContent.Subject = _inviteContent.Subject;
				inviteContent.Text = _inviteContent.Text;
				inviteContent.GifUrl = _inviteContent.GifUrl;
				inviteContent.VideoUrl = _inviteContent.VideoUrl;
				inviteContent.Video = _inviteContent.Video;
				return inviteContent;
			}
		}

		public string ImageUrl
		{
			get;
			private set;
		}

		public Texture2D Image
		{
			get;
			private set;
		}

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

		public string GifUrl
		{
			get;
			private set;
		}

		public string VideoUrl
		{
			get;
			private set;
		}

		public byte[] Video
		{
			get;
			private set;
		}

		public static Builder CreateBuilder()
		{
			return new Builder();
		}

		public override string ToString()
		{
			return $"[InviteContent: ImageUrl={ImageUrl}, Subject={Subject}, Text={Text}, HasImage={Image != null}, GifUrl={GifUrl}, VideoUrl={VideoUrl}]";
		}

		public AndroidJavaObject ToAjo()
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			AndroidJavaObject ajo = new AndroidJavaObject("im.getsocial.sdk.invites.InviteContent$Builder", new object[0]);
			if (Subject != null)
			{
				ajo.CallAJO("withSubject", Subject);
			}
			if (ImageUrl != null)
			{
				ajo.CallAJO("withImageUrl", ImageUrl);
			}
			if (Image != null)
			{
				ajo.CallAJO("withImage", Image.ToAjoBitmap());
			}
			if (Text != null)
			{
				ajo.CallAJO("withText", Text);
			}
			if (Video != null)
			{
				ajo.CallAJO("withVideo", Video);
			}
			return ajo.CallAJO("build");
		}
	}
}
