using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class QuestLocationImage : MonoBehaviour
{
	public const int WIDTH = 480;

	public const int HEIGHT = 344;

	public const float ASPECT = 43f / 60f;

	public const int TEX_WIDTH = 1024;

	public const int TEX_HEIGHT = 734;

	public GameObject skyPrefab;

	public Transform spritesNode;

	private RenderTexture renderTexture;

	public Transform sky
	{
		get;
		private set;
	}

	private void Awake()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid() && MonoBehaviourSingleton<GameSceneManager>.I.isInitialized)
		{
			Camera component = GetComponent<Camera>();
			if (component.targetTexture != null)
			{
				component.targetTexture.DiscardContents();
				component.targetTexture = null;
			}
		}
		else
		{
			Init();
		}
	}

	private void OnEnable()
	{
		if (!Application.isPlaying)
		{
			Init();
		}
	}

	private void OnValidate()
	{
		if (!Application.isPlaying)
		{
			Init();
		}
	}

	public void Init(int w = 0, int h = 0)
	{
		if (w == 0)
		{
			w = 480;
			h = 344;
		}
		else if (w > 1024)
		{
			h = (int)((float)w / 1024f * 734f);
			w = 1024;
		}
		if (spritesNode != null)
		{
			float num = 46.8664856f;
			spritesNode.transform.localScale = new Vector3(num, num, 1f);
		}
		Camera component = GetComponent<Camera>();
		base.transform.position = new Vector3(0f, 500f, 0f);
		Utility.SetLayerWithChildren(base.transform, 5);
		RenderTexture targetTexture = component.targetTexture;
		if (targetTexture == null)
		{
			_ = Application.isPlaying;
			targetTexture = new RenderTexture(w, h, 24, RenderTextureFormat.Default);
			if (!Application.isPlaying)
			{
				targetTexture.hideFlags = HideFlags.HideAndDontSave;
			}
			targetTexture.name = "(QuestLocationImage)";
			targetTexture.filterMode = FilterMode.Point;
			component.targetTexture = targetTexture;
			renderTexture = targetTexture;
		}
		component.cullingMask = 32;
		component.nearClipPlane = 0f;
		component.farClipPlane = 100f;
		component.clearFlags = CameraClearFlags.Color;
		component.backgroundColor = Color.black;
		component.orthographic = true;
		component.orthographicSize = (int)(480f / (float)w * (float)h) / 2 - 1;
		if (Application.isPlaying && sky == null && skyPrefab != null)
		{
			sky = ResourceUtility.Realizes(skyPrefab, base.transform, base.gameObject.layer);
			sky.localPosition = new Vector3(0f, 0f, 50f);
			sky.localRotation = Quaternion.identity;
			sky.localScale = new Vector3(480f, 344f, 1f);
		}
	}
}
