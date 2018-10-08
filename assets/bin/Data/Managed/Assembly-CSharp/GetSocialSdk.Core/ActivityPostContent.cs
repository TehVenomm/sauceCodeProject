using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class ActivityPostContent : IConvertableToNative
	{
		public class Builder
		{
			private readonly ActivityPostContent _content = new ActivityPostContent();

			internal Builder()
			{
			}

			public Builder WithText(string text)
			{
				_content._text = text;
				return this;
			}

			public Builder WithImage(Texture2D image)
			{
				_content._image = image;
				return this;
			}

			public Builder WithButton(string title, string action)
			{
				_content._buttonTitle = title;
				_content._buttonAction = action;
				return this;
			}

			public Builder WithVideo(byte[] video)
			{
				_content._video = video;
				return this;
			}

			public ActivityPostContent Build()
			{
				return _content;
			}
		}

		private string _text;

		private Texture2D _image;

		private string _buttonTitle;

		private string _buttonAction;

		private byte[] _video;

		private ActivityPostContent()
		{
		}

		public static Builder CreateBuilder()
		{
			return new Builder();
		}

		public AndroidJavaObject ToAjo()
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			AndroidJavaObject ajo = new AndroidJavaObject("im.getsocial.sdk.activities.ActivityPostContent$Builder", new object[0]);
			if (_text != null)
			{
				ajo.CallAJO("withText", _text);
			}
			if (_image != null)
			{
				ajo.CallAJO("withImage", _image.ToAjoBitmap());
			}
			if (_buttonAction != null && _buttonTitle != null)
			{
				ajo.CallAJO("withButton", _buttonTitle, _buttonAction);
			}
			if (_video != null)
			{
				ajo.CallAJO("withVideo", _video);
			}
			return ajo.CallAJO("build");
		}
	}
}
