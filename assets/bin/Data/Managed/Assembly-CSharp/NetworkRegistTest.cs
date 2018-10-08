using Network;
using UnityEngine;

public class NetworkRegistTest : MonoBehaviour
{
	public enum PROGRESS
	{
		CHECK_REGISTER,
		ASSET_BUNDLE_VERSION,
		REGIST_CREATE,
		USER_INFO,
		REGIST_FAILED,
		REGIST_OK
	}

	private PROGRESS m_progress;

	private bool m_isSending;

	public PROGRESS progress => m_progress;

	public bool isRegistOK => m_progress == PROGRESS.REGIST_OK;

	public bool isSending => m_isSending;

	private void Awake()
	{
		Debug.Log("NetowrkTest Awake!");
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SendRequest()
	{
		if (!m_isSending)
		{
			switch (m_progress)
			{
			default:
				return;
			case PROGRESS.CHECK_REGISTER:
				Protocol.Send(CheckRegisterModel.URL, delegate(CheckRegisterModel ret)
				{
					RecvResult(ret, ret.result);
				}, string.Empty);
				break;
			case PROGRESS.ASSET_BUNDLE_VERSION:
				Protocol.Send(AssetBundleVersionModel.URL, delegate(AssetBundleVersionModel ret)
				{
					RecvResult(ret, ret.result);
				}, string.Empty);
				break;
			case PROGRESS.REGIST_CREATE:
			{
				RegistCreateSendParam registCreateSendParam = new RegistCreateSendParam();
				registCreateSendParam.d = "TestDevice";
				Protocol.Send(RegistCreateModel.URL, registCreateSendParam, delegate(RegistCreateModel ret)
				{
					RecvResult(ret, ret.result);
				}, string.Empty);
				break;
			}
			case PROGRESS.USER_INFO:
				Protocol.Send(OnceStatusInfoModel.URL, delegate(OnceStatusInfoModel ret)
				{
					RecvResult(ret, ret.result);
				}, string.Empty);
				break;
			}
			m_isSending = true;
		}
	}

	private void RecvResult<R>(BaseModel ret, R result)
	{
		string name = ret.GetType().Name;
		if (ret.Error == Error.None)
		{
			Debug.Log(name + " result:" + result);
			switch (name)
			{
			case "CheckRegisterModel":
			{
				CheckRegisterModel checkRegisterModel = (CheckRegisterModel)ret;
				SetUser(checkRegisterModel.result.userInfo);
				SetAccount(checkRegisterModel.result.uh);
				break;
			}
			case "AssetBundleVersionModel":
			{
				AssetBundleVersionModel assetBundleVersionModel = (AssetBundleVersionModel)ret;
				SetUser(assetBundleVersionModel.result.userInfo);
				break;
			}
			case "RegistCreateModel":
			{
				RegistCreateModel registCreateModel = (RegistCreateModel)ret;
				SetUser(registCreateModel.result.userInfo);
				SetAccount(registCreateModel.result.uh);
				break;
			}
			case "StatusInfoModel":
			{
				OnceStatusInfoModel onceStatusInfoModel = (OnceStatusInfoModel)ret;
				SetUser(onceStatusInfoModel.result.user);
				break;
			}
			}
		}
		else
		{
			Debug.LogError(name + " error:" + ret.Error);
			MonoBehaviourSingleton<AccountManager>.I.ClearAccount();
		}
		if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null)
		{
			m_progress = PROGRESS.REGIST_OK;
		}
		else
		{
			m_progress++;
		}
		Debug.Log("progress:" + m_progress);
		m_isSending = false;
	}

	private void SetUser(UserInfo userInfo)
	{
		if (userInfo != null && userInfo.id > 0)
		{
			MonoBehaviourSingleton<UserInfoManager>.I.SetRecvUserInfo(userInfo, 0);
		}
	}

	private void SetAccount(string uh)
	{
		MonoBehaviourSingleton<AccountManager>.I.SaveAccount(uh, null);
	}
}
