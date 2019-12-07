using UnityEngine;

public class PuniController : MonoBehaviour
{
	public Camera uiCamera;

	public SlimeController slime;

	public int renderQueue = 3000;

	public float posZ;

	public float updateAddAnimTime = 0.075f;

	private Transform slimeParentTransform;

	private Vector3 endPos = Vector3.zero;

	private void Awake()
	{
		slimeParentTransform = base.transform;
		Vector3 position = slimeParentTransform.position;
		position.z = posZ;
		slimeParentTransform.position = position;
		slime.GetComponent<MeshRenderer>().material.renderQueue = renderQueue;
		slime.updateAnimTime = updateAddAnimTime;
	}

	private void LateUpdate()
	{
		if (endPos != Vector3.zero)
		{
			Vector3 to = endPos - slimeParentTransform.position;
			float num = Vector3.Angle(Vector3.up, to) * Mathf.Sign(to.x);
			slimeParentTransform.localRotation = Quaternion.Euler(0f, 0f, 0f - num);
			slime.SetTargetPos(new Vector3(0f, (endPos - slimeParentTransform.position).magnitude / slimeParentTransform.lossyScale.x, 0f));
		}
	}

	public void SetStartPosition(Vector3 start_screen_pos)
	{
		slimeParentTransform.position = uiCamera.ScreenToWorldPoint(start_screen_pos);
		endPos = Vector3.zero;
		slime.TouchStartSlime();
	}

	public void SetEndPosition(Vector3 end_screen_pos)
	{
		endPos = uiCamera.ScreenToWorldPoint(end_screen_pos);
	}

	public void Reset()
	{
		endPos = Vector3.zero;
		slime.TouchEndSlime();
	}
}
