using UnityEngine;

public class BallisticLineRenderer : MonoBehaviour
{
	private const int lineDivide = 30;

	private LineRenderer lineRenderer;

	private BulletData bulletData;

	private void Start()
	{
		Transform transform = ResourceUtility.Realizes(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.bulletLine, MonoBehaviourSingleton<StageObjectManager>.I._transform);
		if (!(transform == null))
		{
			lineRenderer = transform.GetComponent<LineRenderer>();
			if (lineRenderer != null)
			{
				lineRenderer.enabled = false;
				lineRenderer.startColor = Color.yellow;
				lineRenderer.endColor = Color.yellow;
				lineRenderer.material.renderQueue = 3001;
			}
		}
	}

	public void SetBulletData(BulletData bullet)
	{
		bulletData = bullet;
	}

	public void SetVisible(bool enabled)
	{
		lineRenderer.enabled = enabled;
	}

	public void UpdateLine(Vector3 shotPos, Vector3 shotVec)
	{
		if (lineRenderer == null || !lineRenderer.enabled || bulletData == null)
		{
			return;
		}
		shotVec.Normalize();
		float d = 0f;
		float num = 0f;
		float d2 = 0f;
		if (bulletData.type == BulletData.BULLET_TYPE.FALL)
		{
			d = bulletData.dataFall.gravityRate;
			num = bulletData.dataFall.gravityStartTime;
			d2 = bulletData.data.speed;
		}
		else if (bulletData.type == BulletData.BULLET_TYPE.CANNONBALL)
		{
			d = bulletData.dataCannonball.gravityRate;
			num = bulletData.dataCannonball.gravityStartTime;
			d2 = bulletData.data.speed;
		}
		Vector3 a = Physics.gravity * d;
		lineRenderer.positionCount = 30;
		lineRenderer.SetPosition(0, shotPos);
		float num2 = bulletData.data.appearTime / 30f;
		float num3 = 0f;
		for (int i = 1; i < 30; i++)
		{
			num3 = ((!(num3 <= num)) ? (num3 + num2) : (num3 + num2 * 0.1f));
			Vector3 position = shotPos + shotVec * d2 * num3;
			if (num3 >= num)
			{
				position += a * (num3 - num) * (num3 - num) / 2f;
			}
			lineRenderer.SetPosition(i, position);
		}
	}

	private void OnDestroy()
	{
		if (lineRenderer != null)
		{
			Object.Destroy(lineRenderer.gameObject);
			lineRenderer = null;
		}
	}
}
