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
				Debug.LogError("Error JSONInStream " + tag + " " + ex.ToString() + " " + ex2.ToString());
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
				Debug.LogError("Error JSONInStream " + tag + " " + ex.ToString() + " " + ex2.ToString());
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
				Debug.LogError("Error JSONInStream " + idx + " " + ex.ToString() + " " + ex2.ToString());
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(idx);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(idx);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(idx);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(idx);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
		}
		if (jSONBooleanFieldValue != null)
		{
			value = jSONBooleanFieldValue.value;
		}
		return this;
	}

	public JSONInStream Content(string tag, out Vector2 value)
	{
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector2.zero;
			return this;
		}
		float[] fs = new float[2];
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Vector2(fs[0], fs[1]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Vector2 value)
	{
		IJSONFieldValue field = node.GetField(tag);
		if (field == null)
		{
			return this;
		}
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector2.zero;
			return this;
		}
		float[] fs = new float[2];
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Vector2(fs[0], fs[1]);
		return this;
	}

	public JSONInStream Content(out Vector2 value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out Vector2 value)
	{
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector2.zero;
			return this;
		}
		float[] fs = new float[2];
		List(idx, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Vector2(fs[0], fs[1]);
		return this;
	}

	public JSONInStream Content(string tag, out Vector3 value)
	{
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector3.zero;
			return this;
		}
		float[] fs = new float[3];
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Vector3(fs[0], fs[1], fs[2]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Vector3 value)
	{
		IJSONFieldValue field = node.GetField(tag);
		if (field == null)
		{
			return this;
		}
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector3.zero;
			return this;
		}
		float[] fs = new float[3];
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Vector3(fs[0], fs[1], fs[2]);
		return this;
	}

	public JSONInStream Content(out Vector3 value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out Vector3 value)
	{
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector3.zero;
			return this;
		}
		float[] fs = new float[3];
		List(idx, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Vector3(fs[0], fs[1], fs[2]);
		return this;
	}

	public JSONInStream Content(string tag, out Vector4 value)
	{
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector4.zero;
			return this;
		}
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Vector4(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Vector4 value)
	{
		IJSONFieldValue field = node.GetField(tag);
		if (field == null)
		{
			return this;
		}
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector4.zero;
			return this;
		}
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Vector4(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Vector4 value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out Vector4 value)
	{
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Vector4.zero;
			return this;
		}
		float[] fs = new float[4];
		List(idx, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Vector4(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(string tag, out Quaternion value)
	{
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Quaternion.identity;
			return this;
		}
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Quaternion(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Quaternion value)
	{
		IJSONFieldValue field = node.GetField(tag);
		if (field == null)
		{
			return this;
		}
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Quaternion.identity;
			return this;
		}
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Quaternion(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Quaternion value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out Quaternion value)
	{
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Quaternion.identity;
			return this;
		}
		float[] fs = new float[4];
		List(idx, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Quaternion(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(string tag, out Color value)
	{
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Color.white;
			return this;
		}
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Color(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Color value)
	{
		IJSONFieldValue field = node.GetField(tag);
		if (field == null)
		{
			return this;
		}
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Color.white;
			return this;
		}
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Color(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Color value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out Color value)
	{
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = Color.white;
			return this;
		}
		float[] fs = new float[4];
		List(idx, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Color(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(string tag, out Rect value)
	{
		IJSONFieldValue field = node.GetField(tag);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = default(Rect);
			return this;
		}
		float[] fs = new float[4];
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Rect(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream ContentOptional(string tag, ref Rect value)
	{
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
		List(tag, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Rect(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(out Rect value)
	{
		return Content(0, out value);
	}

	public JSONInStream Content(int idx, out Rect value)
	{
		IJSONFieldValue field = node.GetField(idx);
		if (field.GetType() == typeof(JSONNullFieldValue))
		{
			value = default(Rect);
			return this;
		}
		float[] fs = new float[4];
		List(idx, delegate(int i, JSONInStream stream)
		{
			stream.Content(out float value2);
			fs[i] = value2;
		});
		value = new Rect(fs[0], fs[1], fs[2], fs[3]);
		return this;
	}

	public JSONInStream Content(string tag, out XorInt value)
	{
		JSONNumberFieldValue jSONNumberFieldValue = null;
		try
		{
			jSONNumberFieldValue = (JSONNumberFieldValue)node.GetField(tag);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(idx);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(idx);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(idx);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
			callback(num++, jSONInStream);
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
			callback(num++, jSONInStream);
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
		catch (Exception message)
		{
			Debug.LogError(message);
			Debug.LogError(tag);
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
