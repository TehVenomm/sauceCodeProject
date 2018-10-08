using UnityEngine;

public class NetworkTest : MonoBehaviour
{
	private NetworkRegistTest netRegister;

	public bool isAutoMode = true;

	public bool isLocalhost = true;

	private void Awake()
	{
		base.gameObject.AddComponent<NetworkManager>();
		base.gameObject.AddComponent<AccountManager>();
		netRegister = base.gameObject.AddComponent<NetworkRegistTest>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (isAutoMode)
		{
			netRegister.SendRequest();
		}
	}

	private void OnGUI()
	{
		GUILayout.BeginArea(new Rect(10f, 30f, 250f, 100f));
		if (netRegister.progress < NetworkRegistTest.PROGRESS.REGIST_FAILED)
		{
			string text = "Network Request\n[" + netRegister.progress + "] " + ((!netRegister.isSending) ? string.Empty : "Sending...");
			if (GUILayout.Button(text))
			{
				netRegister.SendRequest();
			}
		}
		if (GUILayout.Button("ClearSaveData"))
		{
			MonoBehaviourSingleton<AccountManager>.I.ClearAccount();
		}
		GUILayout.EndArea();
	}
}
