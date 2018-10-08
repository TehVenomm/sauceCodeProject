using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AccountManager : MonoBehaviourSingleton<AccountManager>
{
	public class Account
	{
		public string token;

		public string userHash;

		public bool IsRegist()
		{
			return !string.IsNullOrEmpty(token);
		}

		public override string ToString()
		{
			return "token=" + token + ",userHash=" + userHash;
		}
	}

	public bool sendAsset;

	public Account account
	{
		get;
		protected set;
	}

	public List<LoginBonus> logInBonus
	{
		get;
		private set;
	}

	public int logInBonusLimitedCount
	{
		get;
		private set;
	}

	public bool IsRecvLogInBonus
	{
		get;
		private set;
	}

	public static void ResetAccount()
	{
		GameSaveData.Delete();
		if (MonoBehaviourSingleton<AccountManager>.IsValid())
		{
			MonoBehaviourSingleton<AccountManager>.I.ClearAccount();
		}
		FieldRewardPool.DeleteSave();
		PlayerPrefs.SetInt("LastNewsID", -1);
		new DataTableCache(null).RemoveAll();
		TutorialReadData.DeleteSave();
		if (Singleton<TutorialMessageTable>.IsValid() && Singleton<TutorialMessageTable>.I.ReadData != null)
		{
			Singleton<TutorialMessageTable>.I.ReadData.LoadSaveData();
			Singleton<TutorialMessageTable>.I.ReadData.Save();
		}
		Native.ResetPackagePreferences();
	}

	public static void SaveAsEmptyData()
	{
		SaveData.SetData(SaveData.Key.Account, new Account());
		SaveData.Save();
	}

	protected override void Awake()
	{
		base.Awake();
		LoadSaveData();
	}

	private void LoadSaveData()
	{
		bool flag = false;
		if (!SaveData.HasKey(SaveData.Key.Account))
		{
			flag = true;
			account = new Account();
			SaveData.SetData(SaveData.Key.Account, account);
		}
		account = SaveData.GetData<Account>(SaveData.Key.Account);
		if (flag)
		{
			SaveData.Save();
		}
		else
		{
			NetworkNative.setCookieToken(account.token);
		}
	}

	public void SaveAccount(string user_hash, string token = null)
	{
		account.userHash = user_hash;
		if (token != null)
		{
			account.token = token;
			if (!string.IsNullOrEmpty(token))
			{
				NetworkNative.setCookieToken(account.token);
				Native.getList();
			}
		}
		else if (!string.IsNullOrEmpty(MonoBehaviourSingleton<NetworkManager>.I.tokenTemp))
		{
			account.token = MonoBehaviourSingleton<NetworkManager>.I.tokenTemp;
			NetworkNative.setCookieToken(account.token);
			Native.getList();
		}
		SaveData.SetData(SaveData.Key.Account, account);
		SaveData.Save();
	}

	public void ClearAccount()
	{
		string empty = string.Empty;
		SaveAccount(empty, empty);
		MonoBehaviourSingleton<NetworkManager>.I.tokenTemp = empty;
	}

	public unsafe void SendCheckRegister(string ntc_data, Action<bool> call_back)
	{
		CheckRegisterModel.RequestSendForm requestSendForm = new CheckRegisterModel.RequestSendForm();
		requestSendForm.data = ntc_data;
		_003CSendCheckRegister_003Ec__AnonStorey543 _003CSendCheckRegister_003Ec__AnonStorey;
		Protocol.Send(CheckRegisterModel.URL, requestSendForm, delegate(CheckRegisterModel ret)
		{
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c2: Expected O, but got Unknown
			//IL_011f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0124: Expected O, but got Unknown
			switch (ret.Error)
			{
			case Error.None:
				MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result.userInfo, ret.result.tutorialStep);
				MonoBehaviourSingleton<UserInfoManager>.I.SetNewsID(ret.result.newsId);
				MonoBehaviourSingleton<UserInfoManager>.I.userIdHash = ret.result.userIdHash;
				sendAsset = ret.result.sendAsset;
				if (ret.result.recommendUpdate)
				{
					RecommendUpdateCheck(new Action((object)_003CSendCheckRegister_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
				else if (call_back != null)
				{
					call_back(true);
				}
				break;
			case Error.ERR_AUTH_FAILED:
				if (string.IsNullOrEmpty(account.token))
				{
					if (ret.result.recommendUpdate)
					{
						RecommendUpdateCheck(new Action((object)_003CSendCheckRegister_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
					}
					else if (call_back != null)
					{
						call_back(false);
					}
				}
				else
				{
					MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.GetErrorMessage((uint)ret.Error), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102u), null, null), delegate(string btn)
					{
						if (btn == "YES")
						{
							GameSceneEvent.PopStay();
							ResetAccount();
							TutorialReadData.SaveAsEmptyData();
							SaveAsEmptyData();
							MonoBehaviourSingleton<AppMain>.I.Reset();
						}
						else
						{
							SendCheckRegister(ntc_data, call_back);
						}
					}, true, (int)ret.Error);
				}
				break;
			case Error.WRN_MAINTENANCE:
				OpenMessageDialog(ret.Error, delegate
				{
					MonoBehaviourSingleton<GameSceneManager>.I.OpenInfoDialog(delegate
					{
						SendCheckRegister(ntc_data, call_back);
					}, true);
				});
				break;
			case Error.WRN_UPDATE_FORCE:
				OpenMessageDialog(ret.Error, delegate
				{
					Native.launchMyselfMarket();
					SendCheckRegister(ntc_data, call_back);
				});
				break;
			case Error.WRN_BAN_USER:
				OpenMessageDialog(ret.Error, delegate
				{
					SendCheckRegister(ntc_data, call_back);
				});
				break;
			case Error.WRN_RESIGNED_USER:
				OpenMessageDialog(ret.Error, delegate
				{
					SendCheckRegister(ntc_data, call_back);
				});
				break;
			default:
				OpenYesNoDialog(ret.Error, delegate(string sel)
				{
					if ("YES" == sel)
					{
						SendCheckRegister(ntc_data, call_back);
					}
					else
					{
						Application.Quit();
					}
				});
				break;
			}
		}, string.Empty);
	}

	public void SendRegistCreate(Action<bool> call_back)
	{
		RegistCreateSendParam registCreateSendParam = new RegistCreateSendParam();
		registCreateSendParam.d = NetworkNative.getUniqueDeviceId();
		registCreateSendParam.SetAttribute(Native.GetInstallReferrer());
		bool is_success = false;
		Protocol.Send(RegistCreateModel.URL, registCreateSendParam, delegate(RegistCreateModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
				MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result.userInfo, 0);
				MonoBehaviourSingleton<UserInfoManager>.I.userIdHash = ret.result.userIdHash;
				SaveAccount(ret.result.uh, null);
				Dictionary<string, object> values = new Dictionary<string, object>
				{
					{
						"fb_login",
						"no"
					},
					{
						"colopl_login",
						"no"
					},
					{
						"guest_login",
						"yes"
					}
				};
				MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Account_Register", "Account", values);
				call_back(is_success);
			}
			else
			{
				OpenYesNoDialog(ret.Error, delegate(string sel)
				{
					if ("YES" == sel)
					{
						MonoBehaviourSingleton<NetworkManager>.I.tokenTemp = null;
						SendRegistCreate(call_back);
					}
					else
					{
						Application.Quit();
					}
				});
			}
		}, string.Empty);
	}

	private void RecommendUpdateCheck(Action call_back)
	{
		MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.Get(STRING_CATEGORY.COMMON, 11u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102u), null, null), delegate(string sel)
		{
			if ("YES" == sel)
			{
				Native.launchMyselfMarket();
			}
			call_back.Invoke();
		}, true, 0);
	}

	public void SendLogInBonus(Action<bool> callback)
	{
		logInBonus = null;
		logInBonusLimitedCount = 0;
		GameSaveData.instance.showIAPAdsPop = string.Empty;
		Protocol.Send(LoginBonusModel.URL, delegate(LoginBonusModel ret)
		{
			bool obj = false;
			if (ret.Error == Error.None)
			{
				obj = true;
				logInBonus = ret.result;
				GameSaveData.instance.logInBonus = ret.result;
				GameSaveData.Save();
				int i = 0;
				for (int count = logInBonus.Count; i < count; i++)
				{
					if (logInBonus[i].priority > 0)
					{
						logInBonusLimitedCount++;
					}
				}
				IsRecvLogInBonus = true;
			}
			if (callback != null)
			{
				callback(obj);
			}
		}, string.Empty);
	}

	public void DisplayLogInBonusSection()
	{
		IsRecvLogInBonus = false;
	}

	public void SendRegistCreateRobAccount(string email, string password, string confirmPassword, int secretQuestionType, string secretQuestionAnswer, Action<bool> call_back)
	{
		RegistCreateRobAccountModel.RequestSendForm requestSendForm = new RegistCreateRobAccountModel.RequestSendForm();
		requestSendForm.email = email;
		requestSendForm.password = password;
		requestSendForm.confirmPassword = confirmPassword;
		requestSendForm.secretQuestionType = secretQuestionType;
		requestSendForm.secretQuestionAnswer = secretQuestionAnswer;
		bool is_success = false;
		Protocol.Send(RegistCreateRobAccountModel.URL, requestSendForm, delegate(RegistCreateRobAccountModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
				MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result, 0);
				Dictionary<string, object> values = new Dictionary<string, object>
				{
					{
						"fb_login",
						"no"
					},
					{
						"colopl_login",
						"yes"
					},
					{
						"guest_login",
						"no"
					}
				};
				MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Account_Register", "Account", values);
			}
			call_back(is_success);
		}, string.Empty);
	}

	public void SendRegistAuthRob(string email, string password, Action<bool> call_back)
	{
		MonoBehaviourSingleton<NetworkManager>.I.tokenTemp = null;
		RegistAuthRobModel.RequestSendForm requestSendForm = new RegistAuthRobModel.RequestSendForm();
		requestSendForm.email = email;
		requestSendForm.password = password;
		requestSendForm.d = NetworkNative.getUniqueDeviceId();
		bool is_success = false;
		Protocol.Send(RegistAuthRobModel.URL, requestSendForm, delegate(RegistAuthRobModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
				MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result.userInfo, 0);
				MonoBehaviourSingleton<UserInfoManager>.I.SetNewsID(ret.result.newsId);
				MonoBehaviourSingleton<UserInfoManager>.I.userIdHash = ret.result.userIdHash;
				SaveAccount(ret.result.uh, null);
			}
			call_back(is_success);
		}, string.Empty);
	}

	public void SendRegistChangePasswordRob(string currentPassword, string newPassword, string newPasswordConfirm, Action<bool> call_back)
	{
		RegistChangePasswordRobModel.RequestSendForm requestSendForm = new RegistChangePasswordRobModel.RequestSendForm();
		requestSendForm.currentPassword = currentPassword;
		requestSendForm.newPassword = newPassword;
		requestSendForm.newPasswordConfirm = newPasswordConfirm;
		bool is_success = false;
		Protocol.Send(RegistChangePasswordRobModel.URL, requestSendForm, delegate(RegistChangePasswordRobModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
			}
			call_back(is_success);
		}, string.Empty);
	}

	public void SendRegistChangeSecretQuestion(string password, int secretQuestionType, string secretQuestionAnswer, Action<bool> call_back)
	{
		RegistChangeSecretQuestionModel.RequestSendForm requestSendForm = new RegistChangeSecretQuestionModel.RequestSendForm();
		requestSendForm.password = password;
		requestSendForm.secretQuestionType = secretQuestionType;
		requestSendForm.secretQuestionAnswer = secretQuestionAnswer;
		bool is_success = false;
		Protocol.Send(RegistChangeSecretQuestionModel.URL, requestSendForm, delegate(RegistChangeSecretQuestionModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
			}
			call_back(is_success);
		}, string.Empty);
	}

	public void SendRegistCreateGoogleAccount(string account, string key, string password, string confirmPassword, Action<bool> call_back)
	{
		RegistCreateGoogleAccountModel.RequestSendForm requestSendForm = new RegistCreateGoogleAccountModel.RequestSendForm();
		requestSendForm.account = account;
		requestSendForm.accountKey = key;
		requestSendForm.password = password;
		requestSendForm.confirmPassword = confirmPassword;
		bool is_success = false;
		Protocol.Send(RegistCreateGoogleAccountModel.URL, requestSendForm, delegate(RegistCreateGoogleAccountModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
				MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result, 0);
			}
			call_back(is_success);
		}, string.Empty);
	}

	public void SendRegistAuthGoogle(string account, string key, string password, Action<bool> call_back)
	{
		MonoBehaviourSingleton<NetworkManager>.I.tokenTemp = null;
		RegistAuthGoogleModel.RequestSendForm requestSendForm = new RegistAuthGoogleModel.RequestSendForm();
		requestSendForm.account = account;
		requestSendForm.accountKey = key;
		requestSendForm.password = password;
		requestSendForm.d = NetworkNative.getUniqueDeviceId();
		requestSendForm.purchasetype = 0;
		bool is_success = false;
		Protocol.Send(RegistAuthGoogleModel.URL, requestSendForm, delegate(RegistAuthGoogleModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
				MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result.userInfo, 0);
				MonoBehaviourSingleton<UserInfoManager>.I.SetNewsID(ret.result.newsId);
				MonoBehaviourSingleton<UserInfoManager>.I.userIdHash = ret.result.userIdHash;
				SaveAccount(ret.result.uh, null);
			}
			call_back(is_success);
		}, string.Empty);
	}

	public void SendRegistChangePasswordGoogle(string currentPassword, string newPassword, string newPasswordConfirm, Action<bool> call_back)
	{
		RegistChangePasswordGoogleModel.RequestSendForm requestSendForm = new RegistChangePasswordGoogleModel.RequestSendForm();
		requestSendForm.currentPassword = currentPassword;
		requestSendForm.newPassword = newPassword;
		requestSendForm.newPasswordConfirm = newPasswordConfirm;
		bool is_success = false;
		Protocol.Send(RegistChangePasswordGoogleModel.URL, requestSendForm, delegate(RegistChangePasswordGoogleModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
			}
			call_back(is_success);
		}, string.Empty);
	}

	public void SendRegistAuthFacebook(string access_token, Action<bool> call_back)
	{
		RegistAuthFacebookModel.RequestSendForm requestSendForm = new RegistAuthFacebookModel.RequestSendForm();
		requestSendForm.accessToken = access_token;
		requestSendForm.d = NetworkNative.getUniqueDeviceId();
		bool is_success = false;
		Protocol.Send(RegistAuthFacebookModel.URL, requestSendForm, delegate(RegistAuthFacebookModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
				MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result.userInfo, 0);
				MonoBehaviourSingleton<UserInfoManager>.I.SetNewsID(ret.result.newsId);
				SaveAccount(ret.result.uh, null);
				Dictionary<string, object> values = new Dictionary<string, object>
				{
					{
						"fb_login",
						"yes"
					},
					{
						"colopl_login",
						"no"
					},
					{
						"guest_login",
						"no"
					}
				};
				MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Account_Register", "Account", values);
			}
			call_back(is_success);
		}, string.Empty);
	}

	public void SendRegistLinkFacebook(string access_token, Action<bool, RegistLinkFacebookModel> call_back)
	{
		RegistLinkFacebookModel.RequestSendForm requestSendForm = new RegistLinkFacebookModel.RequestSendForm();
		requestSendForm.accessToken = access_token;
		bool is_success = false;
		Protocol.Send(RegistLinkFacebookModel.URL, requestSendForm, delegate(RegistLinkFacebookModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
				MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result, 0);
			}
			call_back.Invoke(is_success, ret);
		}, string.Empty);
	}

	public void SendRegistOverrideFacebook(string access_token, Action<bool> call_back)
	{
		RegistLinkFacebookModel.RequestOverrideSendForm requestOverrideSendForm = new RegistLinkFacebookModel.RequestOverrideSendForm();
		requestOverrideSendForm.accessToken = access_token;
		requestOverrideSendForm.overwriteOldData = 1;
		bool is_success = false;
		Protocol.Send(RegistLinkFacebookModel.URL, requestOverrideSendForm, delegate(RegistLinkFacebookModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
				MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result, 0);
			}
			call_back(is_success);
		}, string.Empty);
	}

	public void SendRegistUnlinkFacebook(Action<bool> call_back)
	{
		bool is_success = false;
		Protocol.Send(RegistUnlinkFacebookModel.URL, delegate(RegistUnlinkFacebookModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
				MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(ret.result, 0);
			}
			call_back(is_success);
		}, string.Empty);
	}

	public void SendTrackInviteFacebook(string access_token, List<string> list, Action<bool> call_back)
	{
		TrackFriendInviteModel.SendForm sendForm = new TrackFriendInviteModel.SendForm();
		sendForm.listFriend = list;
		bool is_success = false;
		Protocol.Send(TrackFriendInviteModel.URL, sendForm, delegate(TrackFriendInviteModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
			}
			call_back(is_success);
		}, string.Empty);
	}

	public void SendRegistLogout(Action<bool, RegistLogoutModel> call_back)
	{
		bool is_success = false;
		Protocol.Send(RegistLogoutModel.URL, delegate(RegistLogoutModel ret)
		{
			if (ret.Error == Error.None)
			{
				is_success = true;
			}
			call_back.Invoke(is_success, ret);
		}, string.Empty);
	}

	private void OpenMessageDialog(Error msg_id, Action<string> callback)
	{
		GameSceneEvent.PushStay();
		MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.GetErrorMessage((uint)msg_id), null, null, null, null), delegate(string ret)
		{
			GameSceneEvent.PopStay();
			callback(ret);
		}, true, 0);
	}

	private void OpenYesNoDialog(Error msg_id, Action<string> callback)
	{
		GameSceneEvent.PushStay();
		MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.GetErrorMessage((uint)msg_id), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 112u), null, null), delegate(string ret)
		{
			GameSceneEvent.PopStay();
			callback(ret);
		}, true, 0);
	}

	public void SetLoginBonusFromCache(List<LoginBonus> cacheLogInBonus)
	{
		logInBonusLimitedCount = 0;
		logInBonus = cacheLogInBonus;
		int i = 0;
		for (int count = logInBonus.Count; i < count; i++)
		{
			if (logInBonus[i].priority > 0)
			{
				logInBonusLimitedCount++;
			}
		}
		IsRecvLogInBonus = true;
	}
}
