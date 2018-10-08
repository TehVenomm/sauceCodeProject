using Firebase.Messaging;
using Network;
using System;
using UnityEngine;

public class FCMManager : MonoBehaviourSingleton<FCMManager>
{
	public void StartRegist()
	{
		FirebaseMessaging.add_TokenReceived((EventHandler<TokenReceivedEventArgs>)OnTokenReceived);
		FirebaseMessaging.add_MessageReceived((EventHandler<MessageReceivedEventArgs>)OnMessageReceived);
	}

	public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
	{
		PushNotificationDevicePostModel.RequestSendForm requestSendForm = new PushNotificationDevicePostModel.RequestSendForm();
		requestSendForm.deviceToken = token.get_Token();
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
