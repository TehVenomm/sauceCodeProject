using UnityEngine;

public class UIGeometry
{
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	public BetterList<Color32> cols = new BetterList<Color32>();

	private BetterList<Vector3> mRtpVerts = new BetterList<Vector3>();

	private Vector3 mRtpNormal;

	private Vector4 mRtpTan;

	public bool hasVertices => verts.size > 0;

	public bool hasTransformed => mRtpVerts != null && mRtpVerts.size > 0 && mRtpVerts.size == verts.size;

	public void Clear()
	{
		verts.Clear();
		uvs.Clear();
		cols.Clear();
		mRtpVerts.Clear();
	}

	public void ApplyTransform(Matrix4x4 widgetToPanel, bool generateNormals = true)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		if (verts.size > 0)
		{
			mRtpVerts.Clear();
			int i = 0;
			for (int size = verts.size; i < size; i++)
			{
				mRtpVerts.Add(widgetToPanel.MultiplyPoint3x4(verts[i]));
			}
			if (generateNormals)
			{
				Vector3 val = widgetToPanel.MultiplyVector(Vector3.get_back());
				mRtpNormal = val.get_normalized();
				Vector3 val2 = widgetToPanel.MultiplyVector(Vector3.get_right());
				Vector3 normalized = val2.get_normalized();
				mRtpTan = new Vector4(normalized.x, normalized.y, normalized.z, -1f);
			}
		}
		else
		{
			mRtpVerts.Clear();
		}
	}

	public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		if (mRtpVerts != null && mRtpVerts.size > 0)
		{
			if (n == null)
			{
				for (int i = 0; i < mRtpVerts.size; i++)
				{
					v.Add(mRtpVerts.buffer[i]);
					u.Add(uvs.buffer[i]);
					c.Add(cols.buffer[i]);
				}
			}
			else
			{
				for (int j = 0; j < mRtpVerts.size; j++)
				{
					v.Add(mRtpVerts.buffer[j]);
					u.Add(uvs.buffer[j]);
					c.Add(cols.buffer[j]);
					n.Add(mRtpNormal);
					t.Add(mRtpTan);
				}
			}
		}
	}
}
