using UnityEngine;

public class NetworkTest : MonoBehaviour
{
	private NetworkRegistTest netRegister;

	public bool isAutoMode = true;

	public bool isLocalhost = true;

	public NetworkTest()
		: this()
	{
	}

	private void Awake()
	{
		this.get_gameObject().AddComponent<NetworkManager>();
		this.get_gameObject().AddComponent<AccountManager>();
		netRegister = this.get_gameObject().AddComponent<NetworkRegistTest>();
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
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		GUILayout.BeginArea(new Rect(10f, 30f, 250f, 100f));
		if (netRegister.progress < NetworkRegistTest.PROGRESS.REGIST_FAILED)
		{
			string text = "Network Request\n[" + netRegister.progress + "] " + ((!netRegister.isSending) ? string.Empty : "Sending...");
			if (GUILayout.Button(text, (GUILayoutOption[])new GUILayoutOption[0]))
			{
				netRegister.SendRequest();
			}
		}
		if (GUILayout.Button("ClearSaveData", (GUILayoutOption[])new GUILayoutOption[0]))
		{
			MonoBehaviourSingleton<AccountManager>.I.ClearAccount();
		}
		GUILayout.EndArea();
	}
}
