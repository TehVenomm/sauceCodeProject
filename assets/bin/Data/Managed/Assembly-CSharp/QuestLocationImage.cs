using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class QuestLocationImage
{
	public const int WIDTH = 480;

	public const int HEIGHT = 344;

	public const float ASPECT = 0.716666639f;

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

	public QuestLocationImage()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid() && MonoBehaviourSingleton<GameSceneManager>.I.isInitialized)
		{
			Camera component = this.GetComponent<Camera>();
			if (component.get_targetTexture() != null)
			{
				component.get_targetTexture().DiscardContents();
				component.set_targetTexture(null);
			}
		}
		else
		{
			Init(0, 0);
		}
	}

	private void OnEnable()
	{
		if (!Application.get_isPlaying())
		{
			Init(0, 0);
		}
	}

	private void OnValidate()
	{
		if (!Application.get_isPlaying())
		{
			Init(0, 0);
		}
	}

	public void Init(int w = 0, int h = 0)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
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
			spritesNode.get_transform().set_localScale(new Vector3(num, num, 1f));
		}
		Camera component = this.GetComponent<Camera>();
		this.get_transform().set_position(new Vector3(0f, 500f, 0f));
		Utility.SetLayerWithChildren(this.get_transform(), 5);
		RenderTexture val = component.get_targetTexture();
		if (val == null)
		{
			if (!Application.get_isPlaying())
			{
				goto IL_00be;
			}
			goto IL_00be;
		}
		goto IL_00fb;
		IL_00fb:
		component.set_cullingMask(32);
		component.set_nearClipPlane(0f);
		component.set_farClipPlane(100f);
		component.set_clearFlags(2);
		component.set_backgroundColor(Color.get_black());
		component.set_orthographic(true);
		component.set_orthographicSize((float)((int)(480f / (float)w * (float)h) / 2 - 1));
		if (Application.get_isPlaying() && sky == null && skyPrefab != null)
		{
			sky = ResourceUtility.Realizes(skyPrefab, this.get_transform(), this.get_gameObject().get_layer());
			sky.set_localPosition(new Vector3(0f, 0f, 50f));
			sky.set_localRotation(Quaternion.get_identity());
			sky.set_localScale(new Vector3(480f, 344f, 1f));
		}
		return;
		IL_00be:
		val = new RenderTexture(w, h, 24, 7);
		if (!Application.get_isPlaying())
		{
			val.set_hideFlags(61);
		}
		val.set_name("(QuestLocationImage)");
		val.set_filterMode(0);
		component.set_targetTexture(val);
		renderTexture = val;
		goto IL_00fb;
	}
}
