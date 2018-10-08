using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal class GraphRequest : MenuBase
	{
		private string apiQuery = string.Empty;

		private Texture2D profilePic;

		protected override void GetGui()
		{
			bool enabled = GUI.enabled;
			GUI.enabled = (enabled && FB.IsLoggedIn);
			if (Button("Basic Request - Me"))
			{
				FB.API("/me", HttpMethod.GET, (FacebookDelegate<IGraphResult>)base.HandleResult, (IDictionary<string, string>)null);
			}
			if (Button("Retrieve Profile Photo"))
			{
				FB.API("/me/picture", HttpMethod.GET, (FacebookDelegate<IGraphResult>)ProfilePhotoCallback, (IDictionary<string, string>)null);
			}
			if (Button("Take and Upload screenshot"))
			{
				StartCoroutine(TakeScreenshot());
			}
			LabelAndTextField("Request", ref apiQuery);
			if (Button("Custom Request"))
			{
				FB.API(apiQuery, HttpMethod.GET, (FacebookDelegate<IGraphResult>)base.HandleResult, (IDictionary<string, string>)null);
			}
			if ((Object)profilePic != (Object)null)
			{
				GUILayout.Box(profilePic);
			}
			GUI.enabled = enabled;
		}

		private void ProfilePhotoCallback(IGraphResult result)
		{
			if (string.IsNullOrEmpty(result.Error) && (Object)result.Texture != (Object)null)
			{
				profilePic = result.Texture;
			}
			HandleResult(result);
		}

		private IEnumerator TakeScreenshot()
		{
			yield return (object)new WaitForEndOfFrame();
			int width = Screen.width;
			int height = Screen.height;
			Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
			tex.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
			tex.Apply();
			byte[] screenshot = tex.EncodeToPNG();
			WWWForm wwwForm = new WWWForm();
			wwwForm.AddBinaryData("image", screenshot, "InteractiveConsole.png");
			wwwForm.AddField("message", "herp derp.  I did a thing!  Did I do this right?");
			FB.API("me/photos", HttpMethod.POST, base.HandleResult, wwwForm);
		}
	}
}
