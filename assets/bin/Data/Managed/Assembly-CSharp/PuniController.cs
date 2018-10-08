using UnityEngine;

public class PuniController
{
	public Camera uiCamera;

	public SlimeController slime;

	public int renderQueue = 3000;

	public float posZ;

	public float updateAddAnimTime = 0.075f;

	private Transform slimeParentTransform;

	private Vector3 endPos = Vector3.get_zero();

	public PuniController()
		: this()
	{
	}//IL_0017: Unknown result type (might be due to invalid IL or missing references)
	//IL_001c: Unknown result type (might be due to invalid IL or missing references)


	private void Awake()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		slimeParentTransform = this.get_transform();
		Vector3 position = slimeParentTransform.get_position();
		position.z = posZ;
		slimeParentTransform.set_position(position);
		slime.GetComponent<MeshRenderer>().get_material().set_renderQueue(renderQueue);
		slime.updateAnimTime = updateAddAnimTime;
	}

	private void LateUpdate()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		if (endPos != Vector3.get_zero())
		{
			Vector3 val = endPos - slimeParentTransform.get_position();
			float num = Vector3.Angle(Vector3.get_up(), val) * Mathf.Sign(val.x);
			slimeParentTransform.set_localRotation(Quaternion.Euler(0f, 0f, 0f - num));
			SlimeController slimeController = slime;
			Vector3 val2 = endPos - slimeParentTransform.get_position();
			float magnitude = val2.get_magnitude();
			Vector3 lossyScale = slimeParentTransform.get_lossyScale();
			slimeController.SetTargetPos(new Vector3(0f, magnitude / lossyScale.x, 0f));
		}
	}

	public void SetStartPosition(Vector3 start_screen_pos)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		slimeParentTransform.set_position(uiCamera.ScreenToWorldPoint(start_screen_pos));
		endPos = Vector3.get_zero();
		slime.TouchStartSlime();
	}

	public void SetEndPosition(Vector3 end_screen_pos)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		endPos = uiCamera.ScreenToWorldPoint(end_screen_pos);
	}

	public void Reset()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		endPos = Vector3.get_zero();
		slime.TouchEndSlime();
	}
}
