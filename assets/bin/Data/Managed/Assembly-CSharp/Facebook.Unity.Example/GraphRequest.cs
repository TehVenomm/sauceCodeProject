using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal class GraphRequest : MenuBase
	{
		private string apiQuery = string.Empty;

		private Texture2D profilePic;

		protected unsafe override void GetGui()
		{
			//IL_0080: Unknown result type (might be due to invalid IL or missing references)
			bool enabled = GUI.get_enabled();
			GUI.set_enabled(enabled && FB.get_IsLoggedIn());
			if (Button("Basic Request - Me"))
			{
				FB.API("/me", 0, new FacebookDelegate<IGraphResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), (IDictionary<string, string>)null);
			}
			if (Button("Retrieve Profile Photo"))
			{
				FB.API("/me/picture", 0, new FacebookDelegate<IGraphResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), (IDictionary<string, string>)null);
			}
			if (Button("Take and Upload screenshot"))
			{
				this.StartCoroutine(TakeScreenshot());
			}
			LabelAndTextField("Request", ref apiQuery);
			if (Button("Custom Request"))
			{
				FB.API(apiQuery, 0, new FacebookDelegate<IGraphResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), (IDictionary<string, string>)null);
			}
			if (profilePic != null)
			{
				GUILayout.Box(profilePic, (GUILayoutOption[])new GUILayoutOption[0]);
			}
			GUI.set_enabled(enabled);
		}

		private void ProfilePhotoCallback(IGraphResult result)
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Expected O, but got Unknown
			if (string.IsNullOrEmpty(result.get_Error()) && result.get_Texture() != null)
			{
				profilePic = result.get_Texture();
			}
			HandleResult(result);
		}

		private unsafe IEnumerator TakeScreenshot()
		{
			yield return (object)new WaitForEndOfFrame();
			int width = Screen.get_width();
			int height = Screen.get_height();
			Texture2D tex = new Texture2D(width, height, 3, false);
			tex.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
			tex.Apply();
			byte[] screenshot = tex.EncodeToPNG();
			WWWForm wwwForm = new WWWForm();
			wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
			wwwForm.AddField("message", "herp derp.  I did a thing!  Did I do this right?");
			FB.API("me/photos", 1, new FacebookDelegate<IGraphResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), wwwForm);
		}
	}
}
