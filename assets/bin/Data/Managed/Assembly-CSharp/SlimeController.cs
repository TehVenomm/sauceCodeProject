using UnityEngine;

public class SlimeController : MonoBehaviour
{
	[Tooltip("FadeInアニメ\u30fcション再生時間(秒)")]
	public float fadeInAnimTime = 0.3f;

	[Tooltip("FadeInの色アニメ\u30fcション再生時間(秒)")]
	public float fadeInColorAnimTime = 0.3f;

	public AnimationCurve animFadeIn;

	[Tooltip("FadeOutアニメ\u30fcション再生時間(秒)")]
	public float fadeOutAnimTime = 0.6f;

	[Tooltip("FadeOutの色アニメ\u30fcション再生時間(秒)")]
	public float fadeOutColorAnimTime = 0.6f;

	public AnimationCurve animFadeOut;

	[Tooltip("Crushアニメ\u30fcション再生時間(秒)")]
	public float crushAnimTime = 0.1f;

	[Tooltip("Crushの色アニメ\u30fcション再生時間(秒)")]
	public float crushColorAnimTime = 0.4f;

	public AnimationCurve animCrush;

	public AnimationCurve animCrush_temp;

	[Tooltip("ScaleUpDownアニメ\u30fcション再生時間(秒)")]
	public float scaleUpDownAnimTime = 1f;

	public AnimationCurve animScaleUpDown;

	[Tooltip("ScaleUpアニメ\u30fcション最大倍率")]
	public float scaleupAnimMaxScale = 2.4f;

	[Tooltip("ScaleUpアニメ\u30fcション再生時間(秒)")]
	public float scaleupAnimTime = 0.8f;

	public SlimeAnimation slimeAnim;

	private Vector3 targetVector = Vector3.get_zero();

	private int subdivionsWidth;

	private int subdivionsHeight;

	private ParametricPlane parametricPlane;

	private MeshFilter meshFilter;

	private MeshRenderer meshRenderer;

	private Vector3[] firstVectors;

	private Vector3[] nowVectors;

	private Vector3[] vertWork;

	private float[] inv_lenghts;

	private Vector3 dragPos;

	private Vector3 startPosition = Vector3.get_zero();

	private float animTime;

	private bool isMeshUpdate;

	private bool isDrag;

	private bool isSlimeStart;

	private const float MOVE_V = 0.15f;

	private const float RAND_VR = 0.1f;

	private const float RAND_V = 5f;

	private const float RAND_W = 100f;

	public float updateAnimTime
	{
		get;
		set;
	}

	public SlimeController()
		: this()
	{
	}//IL_0064: Unknown result type (might be due to invalid IL or missing references)
	//IL_0069: Unknown result type (might be due to invalid IL or missing references)
	//IL_006f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0074: Unknown result type (might be due to invalid IL or missing references)


	private void Awake()
	{
		meshFilter = this.GetComponent<MeshFilter>();
		meshRenderer = this.GetComponent<MeshRenderer>();
	}

	private void Start()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		Color color = meshRenderer.get_material().get_color();
		color.a = 0f;
		meshRenderer.get_material().set_color(color);
		SetInvisible();
		parametricPlane = this.GetComponent<ParametricPlane>();
		parametricPlane.CreateMesh();
		Initialize();
		slimeAnim = new SlimeAnimation(this);
		isMeshUpdate = false;
	}

	private void Update()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		if (subdivionsHeight <= 0 || subdivionsWidth <= 0)
		{
			return;
		}
		if (isMeshUpdate)
		{
			if (targetVector.y < 0.5f * parametricPlane._height)
			{
				isDrag = false;
				targetVector = Vector3.get_zero();
			}
			else
			{
				isDrag = true;
			}
			SmoothingFilter();
			int i = 0;
			for (int num = nowVectors.Length; i < num; i++)
			{
				vertWork[i] = nowVectors[i];
			}
			meshFilter.get_mesh().set_vertices(vertWork);
		}
		TouchSlimeUpdateAnim();
	}

	private void Initialize()
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		subdivionsWidth = parametricPlane._subdivisionsWidth + 1;
		subdivionsHeight = parametricPlane._subdivisionsHeight + 1;
		firstVectors = meshFilter.get_mesh().get_vertices();
		vertWork = (Vector3[])new Vector3[firstVectors.Length];
		inv_lenghts = new float[firstVectors.Length];
		nowVectors = (Vector3[])new Vector3[firstVectors.Length];
		int num = subdivionsWidth / 2;
		int num2 = 0;
		dragPos = firstVectors[num2 * subdivionsWidth + num];
		int num3 = 0;
		int num4 = subdivionsWidth;
		int num5 = subdivionsHeight;
		for (int i = 0; i < num5; i++)
		{
			for (int j = 0; j < num4; j++)
			{
				nowVectors[num3] = firstVectors[num3];
				int num6 = (num - j) * (num - j) + (num2 - i) * (num2 - i);
				if (num6 != 0)
				{
					inv_lenghts[num3] = 1f / Mathf.Sqrt((float)num6);
				}
				else
				{
					inv_lenghts[num3] = 1f;
				}
				num3++;
			}
		}
	}

	private void SmoothingFilter()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		if (targetVector != Vector3.get_zero())
		{
			SmoothingTargetNotZero();
		}
		else
		{
			SmoothingTargetZero();
		}
		CountAnimTime();
	}

	private void SmoothingTargetNotZero()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		float num = targetVector.x - dragPos.x;
		float num2 = targetVector.y - dragPos.y;
		int i = 0;
		for (int num3 = nowVectors.Length; i < num3; i++)
		{
			Vector3 val = firstVectors[i];
			float num4 = inv_lenghts[i];
			val.x += num * num4;
			val.y += num2 * num4;
			nowVectors[i] = val;
		}
		ResetAnimTime();
		if (startPosition == Vector3.get_zero())
		{
			startPosition = this.get_transform().get_localPosition();
		}
	}

	private void SmoothingTargetZero()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		int i = 0;
		for (int num = nowVectors.Length; i < num; i++)
		{
			Vector3 val = firstVectors[i];
			Vector3 val2 = nowVectors[i];
			float num2 = 1f - animTime;
			val.x = val.x * animTime + val2.x * num2;
			val.y = val.y * animTime + val2.y * num2;
			nowVectors[i] = val;
		}
		if (startPosition != Vector3.get_zero())
		{
			startPosition = Vector3.get_zero();
		}
	}

	public void TouchStartSlime()
	{
		isSlimeStart = true;
		MeshPosInit();
		SetVisible();
		TouchStartSlimeAnim();
	}

	public void TouchEndSlime()
	{
		isSlimeStart = false;
		ResetTarget();
		TouchEndSlimeAnim();
	}

	public void SetTargetPos(Vector3 target)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		targetVector = target;
		isMeshUpdate = true;
	}

	private void TouchStartSlimeAnim()
	{
		slimeAnim.TouchOn();
	}

	private void TouchEndSlimeAnim()
	{
		if (isDrag)
		{
			slimeAnim.TouchOff();
			return;
		}
		CrushPolygon();
		slimeAnim.Crush();
	}

	private void TouchSlimeUpdateAnim()
	{
		slimeAnim.Update();
	}

	private void ResetTarget()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		targetVector = Vector3.get_zero();
		isMeshUpdate = true;
	}

	public void SetVisible()
	{
		if (!meshRenderer.get_enabled())
		{
			meshRenderer.set_enabled(true);
		}
	}

	private void MeshPosInit()
	{
		if (animTime < 1f)
		{
			animTime = 0.99f;
			SmoothingFilter();
		}
		meshRenderer.set_enabled(true);
	}

	public void SetInvisible()
	{
		if (meshRenderer.get_enabled())
		{
			meshRenderer.set_enabled(false);
		}
	}

	public bool IsVisible()
	{
		return meshRenderer.get_enabled();
	}

	private void ResetAnimTime()
	{
		animTime = 0f;
		isMeshUpdate = true;
	}

	private void CountAnimTime()
	{
		if (animTime < 1f)
		{
			animTime += updateAnimTime;
			if (animTime > 1f)
			{
				animTime = 1f;
			}
		}
		else
		{
			isMeshUpdate = false;
			if (!isSlimeStart && !slimeAnim.IsPlaying())
			{
				SetInvisible();
			}
		}
	}

	public bool isDragSlime()
	{
		return isDrag;
	}

	public void CrushPolygon()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		ResetAnimTime();
		float num = Random.Range(5f, 5.1f);
		float num2 = Random.Range(0f, 100f);
		float num3 = 0.15f * parametricPlane._height * 0.5f;
		int i = 0;
		for (int num4 = nowVectors.Length; i < num4; i++)
		{
			Vector3 val = nowVectors[i];
			Vector3 val2 = firstVectors[i];
			float num5 = Mathf.Atan2(val.y, val.x);
			float num6 = num3 * (Mathf.Sin(num5 * num + num2) + 1f);
			val.x = val2.x + Mathf.Cos(num5) * num6;
			val.y = val2.y + Mathf.Sin(num5) * num6;
			nowVectors[i] = val;
		}
	}

	private void ButtonPolygon(float rate)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		ResetAnimTime();
		float num = 4f;
		float num2 = 45f;
		float num3 = 0.5f * rate;
		this.get_transform().set_localRotation(Quaternion.Euler(0f, 0f, 0f));
		int i = 0;
		for (int num4 = nowVectors.Length; i < num4; i++)
		{
			Vector3 val = nowVectors[i];
			float num5 = Mathf.Atan2(val.y, val.x);
			float num6 = num5 * 57.29578f;
			if (num6 >= -45f && 135f >= num6)
			{
				Vector3 val2 = firstVectors[i];
				float num7 = num3 * (Mathf.Sin(num5 * num + num2) + 1f) / 2f;
				val.x = val2.x + Mathf.Cos(num5) * num7;
				val.y = val2.y + Mathf.Sin(num5) * num7;
				nowVectors[i] = val;
			}
		}
		SmoothingFilter();
	}
}
