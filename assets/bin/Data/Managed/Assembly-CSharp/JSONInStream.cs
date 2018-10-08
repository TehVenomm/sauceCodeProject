using System;
using UnityEngine;

public class JSONInStream
{
	public JSONNode node;

	public int Count => node.GetFieldCount();

	public JSONInStream(string input)
	{
		JSONParser jSONParser = new JSONParser();
		node = jSONParser.Parse(new FlashCompatibleTextReader(input));
	}

	public JSONInStream(JSONNode node)
	{
		this.node = node;
	}

	public bool Has(string tag)
	{
		return node.GetField(tag) != null;
	}

	public JSONInStream Content(string tag, out string value)
	{
		try
		{
			JSONStringFieldValue jSONStringFieldValue = (JSONStringFieldValue)node.GetField(tag);
			value = jSONStringFieldValue.value;
			return this;
		}
		catch (Exception ex)
		{
			try
			{
				JSONNullFieldValue jSONNullFieldValue = (JSONNullFieldValue)node.GetField(tag);
				value = null;
				return this;
			}
			catch (Exception ex2)
			{
				Debug.LogError((object)("Error JSONInStream " + tag + " " + ex.ToString() + " " + ex2.ToString()));
				value = null;
				return null;
				IL_0084:
				return this;
			}
		}
	}

	public JSONInStream ContentOptional(string tag, ref string value)
	{
		try
		{
			JSONStringFieldValue jSONStringFieldValue = (JSONStringFieldValue)node.GetField(tag);
			if (jSONStringFieldValue != null)
			{
				value = jSONStringFieldValue.value;
				return this;
			}
			return this;
		}
		catch (Exception ex)
		{
			try
			{
				JSONNullFieldValue jSONNullFieldValue = (JSONNullFieldValue)node.GetField(tag);
				value = null;
				return this;
			}
			catch (Exception ex2)
			{
				Debug.LogError((object)("Error JSONInStream " + tag + " " + ex.ToString() + " " + ex2.ToString()));
				return null;
				IL_008f:
				return this;
			}
		}
	}

	public JSONInStream Content(out string value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out string value)
	{
		try
		{
			JSONStringFieldValue jSONStringFieldValue = (JSONStringFieldValue)node.GetField(idx);
			value = jSONStringFieldValue.value;
			return this;
		}
		catch (Exception ex)
		{
			try
			{
				JSONNullFieldValue jSONNullFieldValue = (JSONNullFieldValue)node.GetField(idx);
				value = null;
				return this;
			}
			catch (Exception ex2)
			{
				Debug.LogError((object)("Error JSONInStream " + idx + " " + ex.ToString() + " " + ex2.ToString()));
				value = null;
				return null;
				IL_0089:
				return this;
			}
		}
	}

	public JSONInStream Content(string tag, out float value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		value = (float)jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream Content(out float value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out float value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(idx);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)idx);
		}
		value = (float)jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream Content(string tag, out double value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		value = jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream Content(out double value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out double value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(idx);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)idx);
		}
		value = jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref double value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		if (jSONNumberFieldValue != null)
		{
			value = jSONNumberFieldValue.value;
		}
		return this;
	}

	public JSONInStream Content(string tag, out int value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		value = (int)jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream Content(out int value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out int value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(idx);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)idx);
		}
		value = (int)jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref int value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		if (jSONNumberFieldValue != null)
		{
			value = (int)jSONNumberFieldValue.value;
		}
		return this;
	}

	public JSONInStream Content(string tag, out bool value)
	{
		JSONBooleanFieldValue jSONBooleanFieldValue = null;
		try
		{
			jSONBooleanFieldValue = (JSONBooleanFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		value = jSONBooleanFieldValue.value;
		return this;
	}

	public JSONInStream Content(out bool value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out bool value)
	{
		JSONBooleanFieldValue jSONBooleanFieldValue = null;
		try
		{
			jSONBooleanFieldValue = (JSONBooleanFieldValue)node.GetField(idx);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)idx);
		}
		value = jSONBooleanFieldValue.value;
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref bool value)
	{
		JSONBooleanFieldValue jSONBooleanFieldValue = null;
		try
		{
			jSONBooleanFieldValue = (JSONBooleanFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		if (jSONBooleanFieldValue != null)
		{
			value = jSONBooleanFieldValue.value;
		}
		return this;
	}

	public unsafe JSONInStream Content(string tag, out Vector2 value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector2.get_zero();
			return this;
		}
		float[] fs = new float[2];
		_003CContent_003Ec__AnonStorey84C _003CContent_003Ec__AnonStorey84C;
		List(tag, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey84C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1]);
		return this;
	}

	public unsafe JSONInStream ContentOptional(string tag, ref Vector2 value)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field == null)
		{
			return this;
		}
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector2.get_zero();
			return this;
		}
		float[] fs = new float[2];
		_003CContentOptional_003Ec__AnonStorey84D _003CContentOptional_003Ec__AnonStorey84D;
		List(tag, new Action<int, JSONInStream>((object)_003CContentOptional_003Ec__AnonStorey84D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1]);
		return this;
	}

	public JSONInStream Content(out Vector2 value)
	{
		return Content(0, out value);
	}

	public unsafe JSONInStream Content(int idx, out Vector2 value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector2.get_zero();
			return this;
		}
		float[] fs = new float[2];
		_003CContent_003Ec__AnonStorey84E _003CContent_003Ec__AnonStorey84E;
		List(idx, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey84E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1]);
		return this;
	}

	public unsafe JSONInStream Content(string tag, out Vector3 value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector3.get_zero();
			return this;
		}
		float[] fs = new float[3];
		_003CContent_003Ec__AnonStorey84F _003CContent_003Ec__AnonStorey84F;
		List(tag, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey84F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2]);
		return this;
	}

	public unsafe JSONInStream ContentOptional(string tag, ref Vector3 value)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field == null)
		{
			return this;
		}
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector3.get_zero();
			return this;
		}
		float[] fs = new float[3];
		_003CContentOptional_003Ec__AnonStorey850 _003CContentOptional_003Ec__AnonStorey;
		List(tag, new Action<int, JSONInStream>((object)_003CContentOptional_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2]);
		return this;
	}

	public JSONInStream Content(out Vector3 value)
	{
		return Content(0, out value);
	}

	public unsafe JSONInStream Content(int idx, out Vector3 value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector3.get_zero();
			return this;
		}
		float[] fs = new float[3];
		_003CContent_003Ec__AnonStorey851 _003CContent_003Ec__AnonStorey;
		List(idx, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2]);
		return this;
	}

	public unsafe JSONInStream Content(string tag, out Vector4 value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector4.get_zero();
			return this;
		}
		float[] fs = new float[4];
		_003CContent_003Ec__AnonStorey852 _003CContent_003Ec__AnonStorey;
		List(tag, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public unsafe JSONInStream ContentOptional(string tag, ref Vector4 value)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field == null)
		{
			return this;
		}
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector4.get_zero();
			return this;
		}
		float[] fs = new float[4];
		_003CContentOptional_003Ec__AnonStorey853 _003CContentOptional_003Ec__AnonStorey;
		List(tag, new Action<int, JSONInStream>((object)_003CContentOptional_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Vector4 value)
	{
		return Content(0, out value);
	}

	public unsafe JSONInStream Content(int idx, out Vector4 value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector4.get_zero();
			return this;
		}
		float[] fs = new float[4];
		_003CContent_003Ec__AnonStorey854 _003CContent_003Ec__AnonStorey;
		List(idx, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public unsafe JSONInStream Content(string tag, out Quaternion value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Quaternion.get_identity();
			return this;
		}
		float[] fs = new float[4];
		_003CContent_003Ec__AnonStorey855 _003CContent_003Ec__AnonStorey;
		List(tag, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public unsafe JSONInStream ContentOptional(string tag, ref Quaternion value)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field == null)
		{
			return this;
		}
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Quaternion.get_identity();
			return this;
		}
		float[] fs = new float[4];
		_003CContentOptional_003Ec__AnonStorey856 _003CContentOptional_003Ec__AnonStorey;
		List(tag, new Action<int, JSONInStream>((object)_003CContentOptional_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Quaternion value)
	{
		return Content(0, out value);
	}

	public unsafe JSONInStream Content(int idx, out Quaternion value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Quaternion.get_identity();
			return this;
		}
		float[] fs = new float[4];
		_003CContent_003Ec__AnonStorey857 _003CContent_003Ec__AnonStorey;
		List(idx, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public unsafe JSONInStream Content(string tag, out Color value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Color.get_white();
			return this;
		}
		float[] fs = new float[4];
		_003CContent_003Ec__AnonStorey858 _003CContent_003Ec__AnonStorey;
		List(tag, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public unsafe JSONInStream ContentOptional(string tag, ref Color value)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field == null)
		{
			return this;
		}
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Color.get_white();
			return this;
		}
		float[] fs = new float[4];
		_003CContentOptional_003Ec__AnonStorey859 _003CContentOptional_003Ec__AnonStorey;
		List(tag, new Action<int, JSONInStream>((object)_003CContentOptional_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Color value)
	{
		return Content(0, out value);
	}

	public unsafe JSONInStream Content(int idx, out Color value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Color.get_white();
			return this;
		}
		float[] fs = new float[4];
		_003CContent_003Ec__AnonStorey85A _003CContent_003Ec__AnonStorey85A;
		List(idx, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey85A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public unsafe JSONInStream Content(string tag, out Rect value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = default(Rect);
			return this;
		}
		float[] fs = new float[4];
		_003CContent_003Ec__AnonStorey85B _003CContent_003Ec__AnonStorey85B;
		List(tag, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey85B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public unsafe JSONInStream ContentOptional(string tag, ref Rect value)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(tag);
		if (field == null)
		{
			return this;
		}
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = default(Rect);
			return this;
		}
		float[] fs = new float[4];
		_003CContentOptional_003Ec__AnonStorey85C _003CContentOptional_003Ec__AnonStorey85C;
		List(tag, new Action<int, JSONInStream>((object)_003CContentOptional_003Ec__AnonStorey85C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Rect value)
	{
		return Content(0, out value);
	}

	public unsafe JSONInStream Content(int idx, out Rect value)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = default(Rect);
			return this;
		}
		float[] fs = new float[4];
		_003CContent_003Ec__AnonStorey85D _003CContent_003Ec__AnonStorey85D;
		List(idx, new Action<int, JSONInStream>((object)_003CContent_003Ec__AnonStorey85D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		value._002Ector(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(string tag, out XorInt value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		value = (int)jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream Content(out XorInt value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out XorInt value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(idx);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)idx);
		}
		value = (int)jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref XorInt value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		if (jSONNumberFieldValue != null)
		{
			value = (int)jSONNumberFieldValue.value;
		}
		return this;
	}

	public JSONInStream Content(string tag, out XorUInt value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		value = (uint)jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream Content(out XorUInt value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out XorUInt value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(idx);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)idx);
		}
		value = (uint)jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref XorUInt value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		if (jSONNumberFieldValue != null)
		{
			value = (uint)jSONNumberFieldValue.value;
		}
		return this;
	}

	public JSONInStream Content(string tag, out XorFloat value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		value = (float)jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream Content(out XorFloat value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out XorFloat value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(idx);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)idx);
		}
		value = (float)jSONNumberFieldValue.value;
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref XorFloat value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		if (jSONNumberFieldValue != null)
		{
			value = (float)jSONNumberFieldValue.value;
		}
		return this;
	}

	public JSONInStream List(string tag, Action<int, JSONInStream> callback)
	{
		JSONListFieldValue jSONListFieldValue = null;
		try
		{
			jSONListFieldValue = (JSONListFieldValue)node.GetField(tag);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
		}
		int num = 0;
		foreach (IJSONFieldValue item in jSONListFieldValue.value)
		{
			JSONNode jSONNode = new JSONNode(item);
			JSONInStream jSONInStream = new JSONInStream(jSONNode);
			try
			{
				JSONObjectFieldValue jSONObjectFieldValue = (JSONObjectFieldValue)item;
				if (jSONObjectFieldValue != null)
				{
					jSONInStream = jSONInStream.Start(0);
				}
			}
			catch
			{
			}
			callback.Invoke(num++, jSONInStream);
		}
		return this;
	}

	public JSONInStream List(int idx, Action<int, JSONInStream> callback)
	{
		JSONListFieldValue jSONListFieldValue = (JSONListFieldValue)node.GetField(idx);
		int num = 0;
		foreach (IJSONFieldValue item in jSONListFieldValue.value)
		{
			JSONNode jSONNode = new JSONNode(item);
			JSONInStream jSONInStream = new JSONInStream(jSONNode);
			try
			{
				JSONObjectFieldValue jSONObjectFieldValue = (JSONObjectFieldValue)item;
				if (jSONObjectFieldValue != null)
				{
					jSONInStream = jSONInStream.Start(0);
				}
			}
			catch
			{
			}
			callback.Invoke(num++, jSONInStream);
		}
		return this;
	}

	public JSONInStream Start(string tag)
	{
		try
		{
			JSONObjectFieldValue jSONObjectFieldValue = (JSONObjectFieldValue)node.GetField(tag);
			jSONObjectFieldValue.value.parent = node;
			node = jSONObjectFieldValue.value;
			return this;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			Debug.LogError((object)tag);
			return this;
		}
	}

	public JSONInStream Start(int idx)
	{
		JSONObjectFieldValue jSONObjectFieldValue = (JSONObjectFieldValue)node.GetField(idx);
		jSONObjectFieldValue.value.parent = node;
		node = jSONObjectFieldValue.value;
		return this;
	}

	public JSONInStream End()
	{
		if (node.parent != null)
		{
			node = node.parent;
		}
		return this;
	}
}
