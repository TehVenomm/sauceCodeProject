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

	private Vector3 targetVector = Vector3.zero;

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

	private Vector3 startPosition = Vector3.zero;

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

	private void Awake()
	{
		meshFilter = GetComponent<MeshFilter>();
		meshRenderer = GetComponent<MeshRenderer>();
	}

	private void Start()
	{
		Color color = meshRenderer.material.color;
		color.a = 0f;
		meshRenderer.material.color = color;
		SetInvisible();
		parametricPlane = GetComponent<ParametricPlane>();
		parametricPlane.CreateMesh();
		Initialize();
		slimeAnim = new SlimeAnimation(this);
		isMeshUpdate = false;
	}

	private void Update()
	{
		if (subdivionsHeight <= 0 || subdivionsWidth <= 0)
		{
			return;
		}
		if (isMeshUpdate)
		{
			if (targetVector.y < 0.5f * parametricPlane._height)
			{
				isDrag = false;
				targetVector = Vector3.zero;
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
			meshFilter.mesh.vertices = vertWork;
		}
		TouchSlimeUpdateAnim();
	}

	private void Initialize()
	{
		subdivionsWidth = parametricPlane._subdivisionsWidth + 1;
		subdivionsHeight = parametricPlane._subdivisionsHeight + 1;
		firstVectors = meshFilter.mesh.vertices;
		vertWork = new Vector3[firstVectors.Length];
		inv_lenghts = new float[firstVectors.Length];
		nowVectors = new Vector3[firstVectors.Length];
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
					inv_lenghts[num3] = 1f / Mathf.Sqrt(num6);
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
		if (targetVector != Vector3.zero)
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
		float num = targetVector.x - dragPos.x;
		float num2 = targetVector.y - dragPos.y;
		int i = 0;
		for (int num3 = nowVectors.Length; i < num3; i++)
		{
			Vector3 vector = firstVectors[i];
			float num4 = inv_lenghts[i];
			vector.x += num * num4;
			vector.y += num2 * num4;
			nowVectors[i] = vector;
		}
		ResetAnimTime();
		if (startPosition == Vector3.zero)
		{
			startPosition = base.transform.localPosition;
		}
	}

	private void SmoothingTargetZero()
	{
		int i = 0;
		for (int num = nowVectors.Length; i < num; i++)
		{
			Vector3 vector = firstVectors[i];
			Vector3 vector2 = nowVectors[i];
			float num2 = 1f - animTime;
			vector.x = vector.x * animTime + vector2.x * num2;
			vector.y = vector.y * animTime + vector2.y * num2;
			nowVectors[i] = vector;
		}
		if (startPosition != Vector3.zero)
		{
			startPosition = Vector3.zero;
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
		targetVector = Vector3.zero;
		isMeshUpdate = true;
	}

	public void SetVisible()
	{
		if (!meshRenderer.enabled)
		{
			meshRenderer.enabled = true;
		}
	}

	private void MeshPosInit()
	{
		if (animTime < 1f)
		{
			animTime = 0.99f;
			SmoothingFilter();
		}
		meshRenderer.enabled = true;
	}

	public void SetInvisible()
	{
		if (meshRenderer.enabled)
		{
			meshRenderer.enabled = false;
		}
	}

	public bool IsVisible()
	{
		return meshRenderer.enabled;
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
		ResetAnimTime();
		float num = Random.Range(5f, 5.1f);
		float num2 = Random.Range(0f, 100f);
		float num3 = 0.15f * parametricPlane._height * 0.5f;
		int i = 0;
		for (int num4 = nowVectors.Length; i < num4; i++)
		{
			Vector3 vector = nowVectors[i];
			Vector3 vector2 = firstVectors[i];
			float num5 = Mathf.Atan2(vector.y, vector.x);
			float num6 = num3 * (Mathf.Sin(num5 * num + num2) + 1f);
			vector.x = vector2.x + Mathf.Cos(num5) * num6;
			vector.y = vector2.y + Mathf.Sin(num5) * num6;
			nowVectors[i] = vector;
		}
	}

	private void ButtonPolygon(float rate)
	{
		ResetAnimTime();
		float num = 4f;
		float num2 = 45f;
		float num3 = 0.5f * rate;
		base.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
		int i = 0;
		for (int num4 = nowVectors.Length; i < num4; i++)
		{
			Vector3 vector = nowVectors[i];
			float num5 = Mathf.Atan2(vector.y, vector.x);
			float num6 = num5 * 57.29578f;
			if (num6 >= -45f && 135f >= num6)
			{
				Vector3 vector2 = firstVectors[i];
				float num7 = num3 * (Mathf.Sin(num5 * num + num2) + 1f) / 2f;
				vector.x = vector2.x + Mathf.Cos(num5) * num7;
				vector.y = vector2.y + Mathf.Sin(num5) * num7;
				nowVectors[i] = vector;
			}
		}
		SmoothingFilter();
	}
}
