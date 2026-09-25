// Decompiled with JetBrains decompiler
// Type: NativeGameService
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Network;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

#nullable disable
public class NativeGameService : MonoBehaviourSingleton<NativeGameService>
{
  private const string FLAG_AUTO_LOGIN_KEY = "signin_game_service_auto";
  private const string FLAG_OLD_USER_LOGIN_KEY = "old_user_login_gg";
  private bool isRunLogin;
  private bool isFirstRun;
  private bool isFixed;

  protected override void Awake()
  {
    switch (CryptoPrefs.GetInt("signin_game_service_auto"))
    {
      case -1:
        return;
      case 0:
        this.isFirstRun = true;
        break;
      default:
        this.isFirstRun = false;
        break;
    }
    this.InitData();
  }

  private void InitData()
  {
    Singleton<AchievementIdTable>.Create();
    Singleton<AchievementIdTable>.I.CreateTable();
  }

  public void SetOldUserLogin()
  {
    PlayerPrefs.SetInt("old_user_login_gg", 1);
    PlayerPrefs.Save();
  }

  private bool isOldPlayerLogin() => PlayerPrefs.GetInt("old_user_login_gg", 0) == 1;

  public void FixAchievement()
  {
    if (!this.isConnected() || this.isFixed)
      return;
    this.isFixed = true;
    List<TaskInfo> taskInos = MonoBehaviourSingleton<AchievementManager>.I.GetTaskInfos();
    int listCount = taskInos.Count;
    Singleton<AchievementIdTable>.I.ForEach((Action<AchievementIdTable.AchievementIdData>) (data =>
    {
      for (int index = 0; index < listCount; ++index)
      {
        if (taskInos[index].taskId == data.taskId)
        {
          if (taskInos[index].progress <= 0)
            break;
          double num = (double) taskInos[index].progress * 100.0 / (double) data.goalNum;
          Social.ReportProgress(data.key, num, (Action<bool>) (success => { }));
          break;
        }
      }
    }));
  }

  public void SignIn()
  {
    if (this.isConnected())
      return;
    if (this.isOldPlayerLogin())
    {
      this.Login();
    }
    else
    {
      if (this.isFirstRun)
        return;
      this.Login();
    }
  }

  public void SignInFirstTime()
  {
    if (this.isConnected() || !this.isFirstRun)
      return;
    CryptoPrefs.SetInt("signin_game_service_auto", 2);
    this.Login();
  }

  private void Login()
  {
    if (CryptoPrefs.GetInt("signin_game_service_auto") == -1 || this.isRunLogin)
      return;
    this.isRunLogin = true;
    PlayGamesPlatform.Activate();
    Social.localUser.Authenticate((Action<bool>) (success =>
    {
      if (success)
        CryptoPrefs.SetInt("signin_game_service_auto", 1);
      else
        ((PlayGamesLocalUser) Social.localUser).GetStats((Action<CommonStatusCodes, PlayerStats>) ((rc, stats) =>
        {
          if (rc == CommonStatusCodes.SignInRequired || rc == CommonStatusCodes.ServiceDisabled)
          {
            if (CryptoPrefs.GetInt("signin_game_service_auto") == 1)
              return;
            CryptoPrefs.SetInt("signin_game_service_auto", -1);
          }
          else
            CryptoPrefs.SetInt("signin_game_service_auto", 2);
        }));
    }));
  }

  private bool isConnected() => Social.localUser.authenticated;

  public void SetAchievementStep(int taskID, int currentStep, int oldStep)
  {
    if (!this.isConnected())
      return;
    AchievementIdTable.AchievementIdData byTask = Singleton<AchievementIdTable>.I.GetByTask(taskID);
    if (byTask == null)
      return;
    int goalNum = byTask.goalNum;
    if (currentStep < goalNum)
    {
      double num = (double) currentStep * 100.0 / (double) goalNum;
      Social.ReportProgress(byTask.key, num, (Action<bool>) (success => { }));
    }
    else
      Social.ReportProgress(byTask.key, 100.0, (Action<bool>) (success => { }));
  }

  public void SHowAchievementUI()
  {
    if (!this.isConnected())
      return;
    Social.ShowAchievementsUI();
  }

  public void GetAllAchievementID()
  {
    if (!this.isConnected())
      return;
    Social.LoadAchievementDescriptions((Action<IAchievementDescription[]>) (descriptions =>
    {
      if (descriptions.Length == 0)
        return;
      foreach (IAchievementDescription description in descriptions)
        ;
    }));
  }

  public void ResetAchievement()
  {
  }

  public void UnlockAchievement(int taskID)
  {
    if (!this.isConnected())
      return;
    AchievementIdTable.AchievementIdData byTask = Singleton<AchievementIdTable>.I.GetByTask(taskID);
    if (byTask == null)
      return;
    int num;
    Social.ReportProgress(byTask.key, 100.0, (Action<bool>) (success => num = success ? 1 : 0));
  }
}
