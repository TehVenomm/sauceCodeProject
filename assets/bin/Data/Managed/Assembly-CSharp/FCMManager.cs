// Decompiled with JetBrains decompiler
// Type: FCMManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Firebase.Messaging;
using Network;
using System;
using UnityEngine;

#nullable disable
public class FCMManager : MonoBehaviourSingleton<FCMManager>
{
  public void StartRegist()
  {
    FirebaseMessaging.TokenReceived += new EventHandler<TokenReceivedEventArgs>(this.OnTokenReceived);
    FirebaseMessaging.MessageReceived += new EventHandler<MessageReceivedEventArgs>(this.OnMessageReceived);
  }

  public void OnTokenReceived(object sender, TokenReceivedEventArgs token)
  {
    MonoBehaviourSingleton<NetworkManager>.I.Request<PushNotificationDevicePostModel.RequestSendForm, PushNotificationDevicePostModel>(PushNotificationDevicePostModel.URL, new PushNotificationDevicePostModel.RequestSendForm()
    {
      deviceToken = token.Token,
      clientVer = NetworkNative.getNativeVersionNameRemoveDot()
    }, (Action<PushNotificationDevicePostModel>) (ret =>
    {
      if (ret.Error != Error.None)
        return;
      PlayerPrefs.SetString("fcm_registed", NetworkNative.getNativeVersionNameRemoveDot());
    }));
  }

  public void OnMessageReceived(object sender, MessageReceivedEventArgs e)
  {
  }
}
