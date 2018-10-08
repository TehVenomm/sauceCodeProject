using Firebase.Messaging;
using Network;
using UnityEngine;

public class FCMManager : MonoBehaviourSingleton<FCMManager>
{
	public void StartRegist()
	{
		FirebaseMessaging.TokenReceived += OnTokenReceived;
		FirebaseMessaging.MessageReceived += OnMessageReceived;
	}

	public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
	{
		PushNotificationDevicePostModel.RequestSendForm requestSendForm = new PushNotificationDevicePostModel.RequestSendForm();
		requestSendForm.deviceToken = token.Token;
		requestSendForm.clientVer = NetworkNative.getNativeVersionNameRemoveDot();
		MonoBehaviourSingleton<NetworkManager>.I.Request(PushNotificationDevicePostModel.URL, requestSendForm, delegate(PushNotificationDevicePostModel ret)
		{
			if (ret.Error == Error.None)
			{
				PlayerPrefs.SetString("fcm_registed", NetworkNative.getNativeVersionNameRemoveDot());
			}
		}, string.Empty, string.Empty);
	}

	public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
	{
	}
}
