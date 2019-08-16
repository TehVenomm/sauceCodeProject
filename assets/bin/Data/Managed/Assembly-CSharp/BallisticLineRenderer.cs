using UnityEngine;

public class BallisticLineRenderer : MonoBehaviour
{
	private const int lineDivide = 30;

	private LineRenderer lineRenderer;

	private BulletData bulletData;

	public BallisticLineRenderer()
		: this()
	{
	}

	private void Start()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.bulletLine, MonoBehaviourSingleton<StageObjectManager>.I._transform);
		if (!(val == null))
		{
			lineRenderer = val.GetComponent<LineRenderer>();
			if (lineRenderer != null)
			{
				lineRenderer.set_enabled(false);
				lineRenderer.SetColors(Color.get_yellow(), Color.get_yellow());
				lineRenderer.get_material().set_renderQueue(3001);
			}
		}
	}

	public void SetBulletData(BulletData bullet)
	{
		bulletData = bullet;
	}

	public void SetVisible(bool enabled)
	{
		lineRenderer.set_enabled(enabled);
	}

	public void UpdateLine(Vector3 shotPos, Vector3 shotVec)
	{
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		if (lineRenderer == null || !lineRenderer.get_enabled() || bulletData == null)
		{
			return;
		}
		shotVec.Normalize();
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		if (bulletData.type == BulletData.BULLET_TYPE.FALL)
		{
			num = bulletData.dataFall.gravityRate;
			num2 = bulletData.dataFall.gravityStartTime;
			num3 = bulletData.data.speed;
		}
		else if (bulletData.type == BulletData.BULLET_TYPE.CANNONBALL)
		{
			num = bulletData.dataCannonball.gravityRate;
			num2 = bulletData.dataCannonball.gravityStartTime;
			num3 = bulletData.data.speed;
		}
		Vector3 val = Physics.get_gravity() * num;
		lineRenderer.SetVertexCount(30);
		lineRenderer.SetPosition(0, shotPos);
		float num4 = bulletData.data.appearTime / 30f;
		float num5 = 0f;
		for (int i = 1; i < 30; i++)
		{
			num5 = ((!(num5 <= num2)) ? (num5 + num4) : (num5 + num4 * 0.1f));
			Vector3 val2 = shotPos + shotVec * num3 * num5;
			if (num5 >= num2)
			{
				val2 += val * (num5 - num2) * (num5 - num2) / 2f;
			}
			lineRenderer.SetPosition(i, val2);
		}
	}

	private void OnDestroy()
	{
		if (lineRenderer != null)
		{
			Object.Destroy(lineRenderer.get_gameObject());
			lineRenderer = null;
		}
	}
}
