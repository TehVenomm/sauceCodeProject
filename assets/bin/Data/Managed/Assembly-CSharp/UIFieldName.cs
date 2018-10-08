using UnityEngine;

public class UIFieldName
{
	private enum Phase
	{
		StartWait,
		FadeIn,
		Display,
		FadeOut,
		None
	}

	private const float baseWidth = 120f;

	private Phase phase = Phase.FadeIn;

	private float waitSeconds;

	private bool initialized;

	private float bgLineWidth;

	private float bgLineWidthMax;

	private float bgLineWidthStep;

	public UILabel nameLabelU;

	public UILabel nameLabelD;

	public Transform labelRoot;

	public UITweenCtrl tweenCtrl;

	public UIWidget bgLight;

	public UIWidget bgBlack;

	public UIWidget bgLine;

	public TweenAlpha tweenTop;

	public UIFieldName()
		: this()
	{
	}

	private void Start()
	{
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		if (!(null == nameLabelU) && !(null == nameLabelD) && !(null == labelRoot) && !(null == tweenCtrl) && !(null == bgLight) && !(null == bgBlack) && !(null == bgLine) && !(null == tweenTop))
		{
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			if (fieldMapData == null)
			{
				this.get_gameObject().SetActive(false);
			}
			else if (string.Compare(fieldMapData.stageName, 0, "FI", 0, 2) != 0)
			{
				this.get_gameObject().SetActive(false);
			}
			else if (QuestManager.IsValidInGameWaveMatch())
			{
				this.get_gameObject().SetActive(false);
			}
			else
			{
				nameLabelU.text = fieldMapData.mapName;
				nameLabelD.text = fieldMapData.mapName;
				float num = (float)nameLabelU.width;
				Vector3 localPosition = labelRoot.get_localPosition();
				localPosition.x += num * 0.5f;
				labelRoot.set_localPosition(localPosition);
				float num2 = num - 120f;
				float num3 = (float)bgLight.width + num2;
				bgLight.width = (int)num3;
				num3 = (float)bgBlack.width + num2;
				bgBlack.width = (int)num3;
				bgLineWidthMax = (float)bgLine.width + num2;
				bgLineWidth = 0f;
				bgLine.width = (int)bgLineWidth;
				bgLineWidthStep = bgLineWidthMax / 12f;
				nameLabelU.fontStyle = 2;
				nameLabelD.fontStyle = 2;
				phase = Phase.StartWait;
				waitSeconds = 0.8f;
				initialized = true;
			}
		}
	}

	private void Phase_StartWait()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		if (0f >= waitSeconds)
		{
			phase = Phase.FadeIn;
			waitSeconds = 1.2f;
			UITweenCtrl.Reset(tweenCtrl.get_transform(), 0);
			UITweenCtrl.Play(tweenCtrl.get_transform(), true, null, false, 0);
		}
	}

	private void Phase_FadeIn()
	{
		bgLineWidth += bgLineWidthStep;
		if (bgLineWidthMax <= bgLineWidth)
		{
			bgLineWidth = bgLineWidthMax;
		}
		bgLine.width = (int)bgLineWidth;
		if (0f >= waitSeconds)
		{
			phase = Phase.Display;
			waitSeconds = 2f;
		}
	}

	private void Phase_Display()
	{
		if (0f >= waitSeconds)
		{
			phase = Phase.FadeOut;
			waitSeconds = 1f;
			tweenTop.ResetToBeginning();
			tweenTop.PlayForward();
		}
	}

	private void Phase_FadeOut()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		if (0f >= waitSeconds)
		{
			this.get_gameObject().SetActive(false);
			phase = Phase.None;
		}
	}

	private void Update()
	{
		if (initialized)
		{
			waitSeconds -= Time.get_deltaTime();
			switch (phase)
			{
			case Phase.None:
				break;
			case Phase.StartWait:
				Phase_StartWait();
				break;
			case Phase.FadeIn:
				Phase_FadeIn();
				break;
			case Phase.Display:
				Phase_Display();
				break;
			case Phase.FadeOut:
				Phase_FadeOut();
				break;
			}
		}
	}
}
