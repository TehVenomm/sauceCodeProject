using System;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal class DialogShare : MenuBase
	{
		private string shareLink = "https://developers.facebook.com/";

		private string shareTitle = "Link Title";

		private string shareDescription = "Link Description";

		private string shareImage = "http://i.imgur.com/j4M7vCO.jpg";

		private string feedTo = string.Empty;

		private string feedLink = "https://developers.facebook.com/";

		private string feedTitle = "Test Title";

		private string feedCaption = "Test Caption";

		private string feedDescription = "Test Description";

		private string feedImage = "http://i.imgur.com/zkYlB.jpg";

		private string feedMediaSource = string.Empty;

		protected override bool ShowDialogModeSelector()
		{
			return true;
		}

		protected unsafe override void GetGui()
		{
			bool enabled = GUI.get_enabled();
			if (Button("Share - Link"))
			{
				FacebookDelegate<IShareResult> val = new FacebookDelegate<IShareResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				FB.ShareLink(new Uri("https://developers.facebook.com/"), string.Empty, string.Empty, (Uri)null, val);
			}
			if (Button("Share - Link Photo"))
			{
				FB.ShareLink(new Uri("https://developers.facebook.com/"), "Link Share", "Look I'm sharing a link", new Uri("http://i.imgur.com/j4M7vCO.jpg"), new FacebookDelegate<IShareResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			LabelAndTextField("Link", ref shareLink);
			LabelAndTextField("Title", ref shareTitle);
			LabelAndTextField("Description", ref shareDescription);
			LabelAndTextField("Image", ref shareImage);
			if (Button("Share - Custom"))
			{
				FB.ShareLink(new Uri(shareLink), shareTitle, shareDescription, new Uri(shareImage), new FacebookDelegate<IShareResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			GUI.set_enabled(enabled && (!Constants.get_IsEditor() || (Constants.get_IsEditor() && FB.get_IsLoggedIn())));
			if (Button("Feed Share - No To"))
			{
				FB.FeedShare(string.Empty, new Uri("https://developers.facebook.com/"), "Test Title", "Test caption", "Test Description", new Uri("http://i.imgur.com/zkYlB.jpg"), string.Empty, new FacebookDelegate<IShareResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			LabelAndTextField("To", ref feedTo);
			LabelAndTextField("Link", ref feedLink);
			LabelAndTextField("Title", ref feedTitle);
			LabelAndTextField("Caption", ref feedCaption);
			LabelAndTextField("Description", ref feedDescription);
			LabelAndTextField("Image", ref feedImage);
			LabelAndTextField("Media Source", ref feedMediaSource);
			if (Button("Feed Share - Custom"))
			{
				FB.FeedShare(feedTo, (!string.IsNullOrEmpty(feedLink)) ? new Uri(feedLink) : null, feedTitle, feedCaption, feedDescription, (!string.IsNullOrEmpty(feedImage)) ? new Uri(feedImage) : null, feedMediaSource, new FacebookDelegate<IShareResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			GUI.set_enabled(enabled);
		}
	}
}
