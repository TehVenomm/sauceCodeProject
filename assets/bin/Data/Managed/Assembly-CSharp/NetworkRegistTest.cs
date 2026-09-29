// Decompiled with JetBrains decompiler
// Type: NetworkRegistTest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class NetworkRegistTest : MonoBehaviour
{
  private NetworkRegistTest.PROGRESS m_progress;
  private bool m_isSending;

  public NetworkRegistTest.PROGRESS progress => this.m_progress;

  public bool isRegistOK => this.m_progress == NetworkRegistTest.PROGRESS.REGIST_OK;

  public bool isSending => this.m_isSending;

  private void Awake() => Debug.Log((object) "NetowrkTest Awake!");

  private void Start()
  {
  }

  private void Update()
  {
  }

  public void SendRequest()
  {
    if (this.m_isSending)
      return;
    switch (this.m_progress)
    {
      case NetworkRegistTest.PROGRESS.CHECK_REGISTER:
        Protocol.Send<CheckRegisterModel>(CheckRegisterModel.URL, (Action<CheckRegisterModel>) (ret => this.RecvResult<CheckRegisterModel.Param>((BaseModel) ret, ret.result)));
        break;
      case NetworkRegistTest.PROGRESS.ASSET_BUNDLE_VERSION:
        Protocol.Send<AssetBundleVersionModel>(AssetBundleVersionModel.URL, (Action<AssetBundleVersionModel>) (ret => this.RecvResult<AssetBundleVersionModel.Param>((BaseModel) ret, ret.result)));
        break;
      case NetworkRegistTest.PROGRESS.REGIST_CREATE:
        Protocol.Send<RegistCreateSendParam, RegistCreateModel>(RegistCreateModel.URL, new RegistCreateSendParam()
        {
          d = "TestDevice"
        }, (Action<RegistCreateModel>) (ret => this.RecvResult<RegistCreateModel.Param>((BaseModel) ret, ret.result)));
        break;
      case NetworkRegistTest.PROGRESS.USER_INFO:
        Protocol.Send<OnceStatusInfoModel>(OnceStatusInfoModel.URL, (Action<OnceStatusInfoModel>) (ret => this.RecvResult<OnceStatusInfoModel.Param>((BaseModel) ret, ret.result)));
        break;
      default:
        return;
    }
    this.m_isSending = true;
  }

  private void RecvResult<R>(BaseModel ret, R result)
  {
    string name = ret.GetType().Name;
    if (ret.Error == Error.None)
    {
      Debug.Log((object) $"{name} result:{(object) result}");
      switch (name)
      {
        case "CheckRegisterModel":
          CheckRegisterModel checkRegisterModel = (CheckRegisterModel) ret;
          this.SetUser(checkRegisterModel.result.userInfo);
          this.SetAccount(checkRegisterModel.result.uh);
          break;
        case "AssetBundleVersionModel":
          this.SetUser(((AssetBundleVersionModel) ret).result.userInfo);
          break;
        case "RegistCreateModel":
          RegistCreateModel registCreateModel = (RegistCreateModel) ret;
          this.SetUser(registCreateModel.result.userInfo);
          this.SetAccount(registCreateModel.result.uh);
          break;
        case "StatusInfoModel":
          this.SetUser(((OnceStatusInfoModel) ret).result.user);
          break;
      }
    }
    else
    {
      Debug.LogError((object) $"{name} error:{(object) ret.Error}");
      MonoBehaviourSingleton<AccountManager>.I.ClearAccount();
    }
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
      this.m_progress = NetworkRegistTest.PROGRESS.REGIST_OK;
    else
      ++this.m_progress;
    Debug.Log((object) ("progress:" + (object) this.m_progress));
    this.m_isSending = false;
  }

  private void SetUser(Network.UserInfo userInfo)
  {
    if (userInfo == null || userInfo.id <= 0)
      return;
    MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(userInfo);
  }

  private void SetAccount(string uh) => MonoBehaviourSingleton<AccountManager>.I.SaveAccount(uh);

  public enum PROGRESS
  {
    CHECK_REGISTER,
    ASSET_BUNDLE_VERSION,
    REGIST_CREATE,
    USER_INFO,
    REGIST_FAILED,
    REGIST_OK,
  }
}
