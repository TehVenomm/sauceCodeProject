using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Network;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class NativeGameService : MonoBehaviourSingleton<NativeGameService>
{
	private const string FLAG_AUTO_LOGIN_KEY = "signin_game_service_auto";

	private const string FLAG_OLD_USER_LOGIN_KEY = "old_user_login_gg";

	private bool isRunLogin;

	private bool isFirstRun;

	private bool isFixed;

	private new void Awake()
	{
		switch (CryptoPrefs.GetInt("signin_game_service_auto", 0))
		{
		case -1:
			return;
		case 0:
			isFirstRun = true;
			break;
		default:
			isFirstRun = false;
			break;
		}
		InitData();
	}

	private void InitData()
	{
		Singleton<AchievementIdTable>.Create();
		Singleton<AchievementIdTable>.I.CreateTable(null);
	}

	public void SetOldUserLogin()
	{
		PlayerPrefs.SetInt("old_user_login_gg", 1);
		PlayerPrefs.Save();
	}

	private bool isOldPlayerLogin()
	{
		return PlayerPrefs.GetInt("old_user_login_gg", 0) == 1;
	}

	public void FixAchievement()
	{
		if (isConnected() && !isFixed)
		{
			isFixed = true;
			List<TaskInfo> taskInos = MonoBehaviourSingleton<AchievementManager>.I.GetTaskInfos();
			int listCount = taskInos.Count;
			Singleton<AchievementIdTable>.I.ForEach(delegate(AchievementIdTable.AchievementIdData data)
			{
				int num = 0;
				while (true)
				{
					if (num >= listCount)
					{
						return;
					}
					if (taskInos[num].taskId == data.taskId)
					{
						break;
					}
					num++;
				}
				if (taskInos[num].progress > 0)
				{
					double num2 = (double)taskInos[num].progress * 100.0 / (double)data.goalNum;
					Social.ReportProgress(data.key, num2, (Action<bool>)delegate
					{
					});
				}
			});
		}
	}

	public void SignIn()
	{
		if (!isConnected())
		{
			if (isOldPlayerLogin())
			{
				Login();
			}
			else if (!isFirstRun)
			{
				Login();
			}
		}
	}

	public void SignInFirstTime()
	{
		if (!isConnected() && isFirstRun)
		{
			CryptoPrefs.SetInt("signin_game_service_auto", 2);
			Login();
		}
	}

	private unsafe void Login()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (CryptoPrefs.GetInt("signin_game_service_auto", 0) != -1 && !isRunLogin)
		{
			isRunLogin = true;
			PlayGamesPlatform.Activate();
			Social.get_localUser().Authenticate((Action<bool>)delegate(bool success)
			{
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Expected O, but got Unknown
				if (success)
				{
					CryptoPrefs.SetInt("signin_game_service_auto", 1);
				}
				else
				{
					PlayGamesLocalUser obj = (PlayGamesLocalUser)Social.get_localUser();
					if (_003C_003Ef__am_0024cache8 == null)
					{
						_003C_003Ef__am_0024cache8 = new Action<CommonStatusCodes, PlayerStats>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					}
					obj.GetStats(_003C_003Ef__am_0024cache8);
				}
			});
		}
	}

	private bool isConnected()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Social.get_localUser().get_authenticated();
	}

	public void SetAchievementStep(int taskID, int currentStep, int oldStep)
	{
		if (isConnected())
		{
			AchievementIdTable.AchievementIdData byTask = Singleton<AchievementIdTable>.I.GetByTask(taskID);
			if (byTask != null)
			{
				int goalNum = byTask.goalNum;
				if (currentStep < goalNum)
				{
					double num = (double)currentStep * 100.0 / (double)goalNum;
					Social.ReportProgress(byTask.key, num, (Action<bool>)delegate
					{
					});
				}
				else
				{
					Social.ReportProgress(byTask.key, 100.0, (Action<bool>)delegate
					{
					});
				}
			}
		}
	}

	public void SHowAchievementUI()
	{
		if (isConnected())
		{
			Social.ShowAchievementsUI();
		}
	}

	public void GetAllAchievementID()
	{
		if (isConnected())
		{
			Social.LoadAchievementDescriptions((Action<IAchievementDescription[]>)delegate(IAchievementDescription[] descriptions)
			{
				if (descriptions.Length > 0)
				{
					foreach (IAchievementDescription val in descriptions)
					{
					}
				}
			});
		}
	}

	public void ResetAchievement()
	{
	}

	public void UnlockAchievement(int taskID)
	{
		if (isConnected())
		{
			AchievementIdTable.AchievementIdData byTask = Singleton<AchievementIdTable>.I.GetByTask(taskID);
			if (byTask != null)
			{
				Social.ReportProgress(byTask.key, 100.0, (Action<bool>)delegate(bool success)
				{
					if (!success)
					{
						return;
					}
				});
			}
		}
	}
}
