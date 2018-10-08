using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace gogame
{
	public class JSONObject : IEnumerable
	{
		public enum Type
		{
			NULL,
			STRING,
			NUMBER,
			OBJECT,
			ARRAY,
			BOOL,
			BAKED
		}

		public delegate void AddJSONContents(JSONObject self);

		public delegate void FieldNotFound(string name);

		public delegate void GetFieldResponse(JSONObject obj);

		private const int MAX_DEPTH = 100;

		private const string INFINITY = "\"INFINITY\"";

		private const string NEGINFINITY = "\"NEGINFINITY\"";

		private const string NaN = "\"NaN\"";

		private const float maxFrameTime = 0.008f;

		public static readonly char[] WHITESPACE = new char[6]
		{
			' ',
			'\r',
			'\n',
			'\t',
			'\ufeff',
			'\t'
		};

		public Type type;

		public List<JSONObject> list;

		public List<string> keys;

		public string str;

		public float n;

		public bool useInt;

		public long i;

		public bool b;

		private static readonly Stopwatch printWatch = new Stopwatch();

		public bool isContainer => type == Type.ARRAY || type == Type.OBJECT;

		public int Count
		{
			get
			{
				if (list == null)
				{
					return -1;
				}
				return list.Count;
			}
		}

		public float f => n;

		public static JSONObject nullJO => Create(Type.NULL);

		public static JSONObject obj => Create(Type.OBJECT);

		public static JSONObject arr => Create(Type.ARRAY);

		public bool IsNumber => type == Type.NUMBER;

		public bool IsNull => type == Type.NULL;

		public bool IsString => type == Type.STRING;

		public bool IsBool => type == Type.BOOL;

		public bool IsArray => type == Type.ARRAY;

		public bool IsObject => type == Type.OBJECT || type == Type.BAKED;

		public JSONObject this[int index]
		{
			get
			{
				if (list.Count > index)
				{
					return list[index];
				}
				return null;
			}
			set
			{
				if (list.Count > index)
				{
					list[index] = value;
				}
			}
		}

		public JSONObject this[string index]
		{
			get
			{
				return GetField(index);
			}
			set
			{
				SetField(index, value);
			}
		}

		public JSONObject(Type t)
		{
			type = t;
			switch (t)
			{
			case Type.ARRAY:
				list = new List<JSONObject>();
				break;
			case Type.OBJECT:
				list = new List<JSONObject>();
				keys = new List<string>();
				break;
			}
		}

		public JSONObject(bool b)
		{
			type = Type.BOOL;
			this.b = b;
		}

		public JSONObject(float f)
		{
			type = Type.NUMBER;
			n = f;
		}

		public JSONObject(int i)
		{
			type = Type.NUMBER;
			this.i = i;
			useInt = true;
			n = (float)i;
		}

		public JSONObject(long l)
		{
			type = Type.NUMBER;
			i = l;
			useInt = true;
			n = (float)l;
		}

		public JSONObject(Dictionary<string, string> dic)
		{
			type = Type.OBJECT;
			keys = new List<string>();
			list = new List<JSONObject>();
			foreach (KeyValuePair<string, string> item in dic)
			{
				keys.Add(item.Key);
				list.Add(CreateStringObject(item.Value));
			}
		}

		public JSONObject(Dictionary<string, JSONObject> dic)
		{
			type = Type.OBJECT;
			keys = new List<string>();
			list = new List<JSONObject>();
			foreach (KeyValuePair<string, JSONObject> item in dic)
			{
				keys.Add(item.Key);
				list.Add(item.Value);
			}
		}

		public JSONObject(AddJSONContents content)
		{
			content(this);
		}

		public JSONObject(JSONObject[] objs)
		{
			type = Type.ARRAY;
			list = new List<JSONObject>(objs);
		}

		public JSONObject()
		{
		}

		public JSONObject(string str, int maxDepth = -2, bool storeExcessLevels = false, bool strict = false)
		{
			Parse(str, maxDepth, storeExcessLevels, strict);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public static JSONObject StringObject(string val)
		{
			return CreateStringObject(val);
		}

		public void Absorb(JSONObject obj)
		{
			list.AddRange(obj.list);
			keys.AddRange(obj.keys);
			str = obj.str;
			n = obj.n;
			useInt = obj.useInt;
			i = obj.i;
			b = obj.b;
			type = obj.type;
		}

		public static JSONObject Create()
		{
			return new JSONObject();
		}

		public static JSONObject Create(Type t)
		{
			JSONObject jSONObject = Create();
			jSONObject.type = t;
			switch (t)
			{
			case Type.ARRAY:
				jSONObject.list = new List<JSONObject>();
				break;
			case Type.OBJECT:
				jSONObject.list = new List<JSONObject>();
				jSONObject.keys = new List<string>();
				break;
			}
			return jSONObject;
		}

		public static JSONObject Create(bool val)
		{
			JSONObject jSONObject = Create();
			jSONObject.type = Type.BOOL;
			jSONObject.b = val;
			return jSONObject;
		}

		public static JSONObject Create(float val)
		{
			JSONObject jSONObject = Create();
			jSONObject.type = Type.NUMBER;
			jSONObject.n = val;
			return jSONObject;
		}

		public static JSONObject Create(int val)
		{
			JSONObject jSONObject = Create();
			jSONObject.type = Type.NUMBER;
			jSONObject.n = (float)val;
			jSONObject.useInt = true;
			jSONObject.i = val;
			return jSONObject;
		}

		public static JSONObject Create(long val)
		{
			JSONObject jSONObject = Create();
			jSONObject.type = Type.NUMBER;
			jSONObject.n = (float)val;
			jSONObject.useInt = true;
			jSONObject.i = val;
			return jSONObject;
		}

		public static JSONObject CreateStringObject(string val)
		{
			JSONObject jSONObject = Create();
			jSONObject.type = Type.STRING;
			jSONObject.str = val;
			return jSONObject;
		}

		public static JSONObject CreateBakedObject(string val)
		{
			JSONObject jSONObject = Create();
			jSONObject.type = Type.BAKED;
			jSONObject.str = val;
			return jSONObject;
		}

		public static JSONObject Create(string val, int maxDepth = -2, bool storeExcessLevels = false, bool strict = false)
		{
			JSONObject jSONObject = Create();
			jSONObject.Parse(val, maxDepth, storeExcessLevels, strict);
			return jSONObject;
		}

		public static JSONObject Create(AddJSONContents content)
		{
			JSONObject jSONObject = Create();
			content(jSONObject);
			return jSONObject;
		}

		public static JSONObject Create(Dictionary<string, string> dic)
		{
			JSONObject jSONObject = Create();
			jSONObject.type = Type.OBJECT;
			jSONObject.keys = new List<string>();
			jSONObject.list = new List<JSONObject>();
			foreach (KeyValuePair<string, string> item in dic)
			{
				jSONObject.keys.Add(item.Key);
				jSONObject.list.Add(CreateStringObject(item.Value));
			}
			return jSONObject;
		}

		private void Parse(string str, int maxDepth = -2, bool storeExcessLevels = false, bool strict = false)
		{
			if (!string.IsNullOrEmpty(str))
			{
				str = str.Trim(WHITESPACE);
				if (strict && str[0] != '[' && str[0] != '{')
				{
					type = Type.NULL;
					Debug.LogWarning((object)"Improper (strict) JSON formatting.  First character must be [ or {");
				}
				else if (str.Length > 0)
				{
					if (string.Compare(str, "true", true) == 0)
					{
						type = Type.BOOL;
						b = true;
					}
					else if (string.Compare(str, "false", true) == 0)
					{
						type = Type.BOOL;
						b = false;
					}
					else if (string.Compare(str, "null", true) == 0)
					{
						type = Type.NULL;
					}
					else if (str == "\"INFINITY\"")
					{
						type = Type.NUMBER;
						n = float.PositiveInfinity;
					}
					else if (str == "\"NEGINFINITY\"")
					{
						type = Type.NUMBER;
						n = float.NegativeInfinity;
					}
					else if (str == "\"NaN\"")
					{
						type = Type.NUMBER;
						n = float.NaN;
					}
					else if (str[0] == '"')
					{
						type = Type.STRING;
						this.str = str.Substring(1, str.Length - 2);
					}
					else
					{
						int num = 1;
						int num2 = 0;
						switch (str[num2])
						{
						case '{':
							type = Type.OBJECT;
							keys = new List<string>();
							list = new List<JSONObject>();
							break;
						case '[':
							type = Type.ARRAY;
							list = new List<JSONObject>();
							break;
						default:
							try
							{
								n = Convert.ToSingle(str);
								if (!str.Contains("."))
								{
									i = Convert.ToInt64(str);
									useInt = true;
								}
								type = Type.NUMBER;
							}
							catch (FormatException)
							{
								type = Type.NULL;
								Debug.LogWarning((object)("improper JSON formatting:" + str));
							}
							return;
						}
						string item = string.Empty;
						bool flag = false;
						bool flag2 = false;
						int num3 = 0;
						while (++num2 < str.Length)
						{
							if (Array.IndexOf(WHITESPACE, str[num2]) <= -1)
							{
								if (str[num2] == '\\')
								{
									num2++;
								}
								else
								{
									if (str[num2] == '"')
									{
										if (flag)
										{
											if (!flag2 && num3 == 0 && type == Type.OBJECT)
											{
												item = str.Substring(num + 1, num2 - num - 1);
											}
											flag = false;
										}
										else
										{
											if (num3 == 0 && type == Type.OBJECT)
											{
												num = num2;
											}
											flag = true;
										}
									}
									if (!flag)
									{
										if (type == Type.OBJECT && num3 == 0 && str[num2] == ':')
										{
											num = num2 + 1;
											flag2 = true;
										}
										if (str[num2] == '[' || str[num2] == '{')
										{
											num3++;
										}
										else if (str[num2] == ']' || str[num2] == '}')
										{
											num3--;
										}
										if ((str[num2] == ',' && num3 == 0) || num3 < 0)
										{
											flag2 = false;
											string text = str.Substring(num, num2 - num).Trim(WHITESPACE);
											if (text.Length > 0)
											{
												if (type == Type.OBJECT)
												{
													keys.Add(item);
												}
												if (maxDepth != -1)
												{
													list.Add(Create(text, (maxDepth >= -1) ? (maxDepth - 1) : (-2), storeExcessLevels, false));
												}
												else if (storeExcessLevels)
												{
													list.Add(CreateBakedObject(text));
												}
											}
											num = num2 + 1;
										}
									}
								}
							}
						}
					}
				}
				else
				{
					type = Type.NULL;
				}
			}
			else
			{
				type = Type.NULL;
			}
		}

		public void Add(bool val)
		{
			Add(Create(val));
		}

		public void Add(float val)
		{
			Add(Create(val));
		}

		public void Add(int val)
		{
			Add(Create(val));
		}

		public void Add(string str)
		{
			Add(CreateStringObject(str));
		}

		public void Add(AddJSONContents content)
		{
			Add(Create(content));
		}

		public void Add(JSONObject obj)
		{
			if ((bool)obj)
			{
				if (type != Type.ARRAY)
				{
					type = Type.ARRAY;
					if (list == null)
					{
						list = new List<JSONObject>();
					}
				}
				list.Add(obj);
			}
		}

		public void AddField(string name, bool val)
		{
			AddField(name, Create(val));
		}

		public void AddField(string name, float val)
		{
			AddField(name, Create(val));
		}

		public void AddField(string name, int val)
		{
			AddField(name, Create(val));
		}

		public void AddField(string name, long val)
		{
			AddField(name, Create(val));
		}

		public void AddField(string name, AddJSONContents content)
		{
			AddField(name, Create(content));
		}

		public void AddField(string name, string val)
		{
			AddField(name, CreateStringObject(val));
		}

		public void AddField(string name, JSONObject obj)
		{
			if ((bool)obj)
			{
				if (type != Type.OBJECT)
				{
					if (keys == null)
					{
						keys = new List<string>();
					}
					if (type == Type.ARRAY)
					{
						for (int i = 0; i < list.Count; i++)
						{
							keys.Add(i + string.Empty);
						}
					}
					else if (list == null)
					{
						list = new List<JSONObject>();
					}
					type = Type.OBJECT;
				}
				keys.Add(name);
				list.Add(obj);
			}
		}

		public void SetField(string name, string val)
		{
			SetField(name, CreateStringObject(val));
		}

		public void SetField(string name, bool val)
		{
			SetField(name, Create(val));
		}

		public void SetField(string name, float val)
		{
			SetField(name, Create(val));
		}

		public void SetField(string name, int val)
		{
			SetField(name, Create(val));
		}

		public void SetField(string name, JSONObject obj)
		{
			if (HasField(name))
			{
				list.Remove(this[name]);
				keys.Remove(name);
			}
			AddField(name, obj);
		}

		public void RemoveField(string name)
		{
			if (keys.IndexOf(name) > -1)
			{
				list.RemoveAt(keys.IndexOf(name));
				keys.Remove(name);
			}
		}

		public bool GetField(out bool field, string name, bool fallback)
		{
			field = fallback;
			return GetField(ref field, name, null);
		}

		public bool GetField(ref bool field, string name, FieldNotFound fail = null)
		{
			if (type == Type.OBJECT)
			{
				int num = keys.IndexOf(name);
				if (num >= 0)
				{
					field = list[num].b;
					return true;
				}
			}
			fail?.Invoke(name);
			return false;
		}

		public bool GetField(out float field, string name, float fallback)
		{
			field = fallback;
			return GetField(ref field, name, null);
		}

		public bool GetField(ref float field, string name, FieldNotFound fail = null)
		{
			if (type == Type.OBJECT)
			{
				int num = keys.IndexOf(name);
				if (num >= 0)
				{
					field = list[num].n;
					return true;
				}
			}
			fail?.Invoke(name);
			return false;
		}

		public bool GetField(out int field, string name, int fallback)
		{
			field = fallback;
			return GetField(ref field, name, null);
		}

		public bool GetField(ref int field, string name, FieldNotFound fail = null)
		{
			if (IsObject)
			{
				int num = keys.IndexOf(name);
				if (num >= 0)
				{
					field = (int)list[num].n;
					return true;
				}
			}
			fail?.Invoke(name);
			return false;
		}

		public bool GetField(out long field, string name, long fallback)
		{
			field = fallback;
			return GetField(ref field, name, null);
		}

		public bool GetField(ref long field, string name, FieldNotFound fail = null)
		{
			if (IsObject)
			{
				int num = keys.IndexOf(name);
				if (num >= 0)
				{
					field = (long)list[num].n;
					return true;
				}
			}
			fail?.Invoke(name);
			return false;
		}

		public bool GetField(out uint field, string name, uint fallback)
		{
			field = fallback;
			return GetField(ref field, name, null);
		}

		public bool GetField(ref uint field, string name, FieldNotFound fail = null)
		{
			if (IsObject)
			{
				int num = keys.IndexOf(name);
				if (num >= 0)
				{
					field = (uint)list[num].n;
					return true;
				}
			}
			fail?.Invoke(name);
			return false;
		}

		public bool GetField(out string field, string name, string fallback)
		{
			field = fallback;
			return GetField(ref field, name, null);
		}

		public bool GetField(ref string field, string name, FieldNotFound fail = null)
		{
			if (IsObject)
			{
				int num = keys.IndexOf(name);
				if (num >= 0)
				{
					field = list[num].str;
					return true;
				}
			}
			fail?.Invoke(name);
			return false;
		}

		public void GetField(string name, GetFieldResponse response, FieldNotFound fail = null)
		{
			if (response != null && IsObject)
			{
				int num = keys.IndexOf(name);
				if (num >= 0)
				{
					response(list[num]);
					return;
				}
			}
			fail?.Invoke(name);
		}

		public JSONObject GetField(string name)
		{
			if (IsObject)
			{
				for (int i = 0; i < keys.Count; i++)
				{
					if (keys[i] == name)
					{
						return list[i];
					}
				}
			}
			return null;
		}

		public bool HasFields(string[] names)
		{
			if (!IsObject)
			{
				return false;
			}
			for (int i = 0; i < names.Length; i++)
			{
				if (!keys.Contains(names[i]))
				{
					return false;
				}
			}
			return true;
		}

		public bool HasField(string name)
		{
			if (!IsObject)
			{
				return false;
			}
			for (int i = 0; i < keys.Count; i++)
			{
				if (keys[i] == name)
				{
					return true;
				}
			}
			return false;
		}

		public void Clear()
		{
			type = Type.NULL;
			if (list != null)
			{
				list.Clear();
			}
			if (keys != null)
			{
				keys.Clear();
			}
			str = string.Empty;
			n = 0f;
			b = false;
		}

		public JSONObject Copy()
		{
			return Create(Print(false), -2, false, false);
		}

		public void Merge(JSONObject obj)
		{
			MergeRecur(this, obj);
		}

		private static void MergeRecur(JSONObject left, JSONObject right)
		{
			if (left.type == Type.NULL)
			{
				left.Absorb(right);
			}
			else if (left.type == Type.OBJECT && right.type == Type.OBJECT)
			{
				for (int i = 0; i < right.list.Count; i++)
				{
					string text = right.keys[i];
					if (right[i].isContainer)
					{
						if (left.HasField(text))
						{
							MergeRecur(left[text], right[i]);
						}
						else
						{
							left.AddField(text, right[i]);
						}
					}
					else if (left.HasField(text))
					{
						left.SetField(text, right[i]);
					}
					else
					{
						left.AddField(text, right[i]);
					}
				}
			}
			else if (left.type == Type.ARRAY && right.type == Type.ARRAY)
			{
				if (right.Count > left.Count)
				{
					Debug.LogError((object)"Cannot merge arrays when right object has more elements");
				}
				else
				{
					for (int j = 0; j < right.list.Count; j++)
					{
						if (left[j].type == right[j].type)
						{
							if (left[j].isContainer)
							{
								MergeRecur(left[j], right[j]);
							}
							else
							{
								left[j] = right[j];
							}
						}
					}
				}
			}
		}

		public void Bake()
		{
			if (type != Type.BAKED)
			{
				str = Print(false);
				type = Type.BAKED;
			}
		}

		public IEnumerable BakeAsync()
		{
			if (type != Type.BAKED)
			{
				foreach (string item in PrintAsync(false))
				{
					if (item == null)
					{
						yield return (object)item;
					}
					else
					{
						str = item;
					}
				}
				type = Type.BAKED;
			}
		}

		public string Print(bool pretty = false)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Stringify(0, stringBuilder, pretty);
			return stringBuilder.ToString();
		}

		public IEnumerable<string> PrintAsync(bool pretty = false)
		{
			StringBuilder builder = new StringBuilder();
			printWatch.Reset();
			printWatch.Start();
			foreach (IEnumerable item in StringifyAsync(0, builder, pretty))
			{
				IEnumerable enumerable = item;
				yield return (string)null;
			}
			yield return builder.ToString();
		}

		private IEnumerable StringifyAsync(int depth, StringBuilder builder, bool pretty = false)
		{
			int num;
			depth = (num = depth) + 1;
			if (num > 100)
			{
				Debug.Log((object)"reached max depth!");
			}
			else
			{
				if (printWatch.Elapsed.TotalSeconds > 0.00800000037997961)
				{
					printWatch.Reset();
					yield return (object)null;
					printWatch.Start();
				}
				switch (type)
				{
				case Type.BAKED:
					builder.Append(str);
					break;
				case Type.STRING:
					builder.AppendFormat("\"{0}\"", str);
					break;
				case Type.NUMBER:
					if (useInt)
					{
						builder.Append(this.i.ToString());
					}
					else if (float.IsInfinity(this.n))
					{
						builder.Append("\"INFINITY\"");
					}
					else if (float.IsNegativeInfinity(this.n))
					{
						builder.Append("\"NEGINFINITY\"");
					}
					else if (float.IsNaN(this.n))
					{
						builder.Append("\"NaN\"");
					}
					else
					{
						builder.Append(this.n.ToString());
					}
					break;
				case Type.OBJECT:
					builder.Append("{");
					if (list.Count > 0)
					{
						if (pretty)
						{
							builder.Append("\n");
						}
						for (int m = 0; m < list.Count; m++)
						{
							string key = keys[m];
							JSONObject obj = list[m];
							if ((bool)obj)
							{
								if (pretty)
								{
									for (int k = 0; k < depth; k++)
									{
										builder.Append("\t");
									}
								}
								builder.AppendFormat("\"{0}\":", key);
								foreach (IEnumerable item in obj.StringifyAsync(depth, builder, pretty))
								{
									yield return (object)item;
								}
								builder.Append(",");
								if (pretty)
								{
									builder.Append("\n");
								}
							}
						}
						if (pretty)
						{
							builder.Length -= 2;
						}
						else
						{
							builder.Length--;
						}
					}
					if (pretty && list.Count > 0)
					{
						builder.Append("\n");
						for (int i = 0; i < depth - 1; i++)
						{
							builder.Append("\t");
						}
					}
					builder.Append("}");
					break;
				case Type.ARRAY:
					builder.Append("[");
					if (list.Count > 0)
					{
						if (pretty)
						{
							builder.Append("\n");
						}
						for (int j = 0; j < list.Count; j++)
						{
							if ((bool)list[j])
							{
								if (pretty)
								{
									for (int l = 0; l < depth; l++)
									{
										builder.Append("\t");
									}
								}
								foreach (IEnumerable item2 in list[j].StringifyAsync(depth, builder, pretty))
								{
									yield return (object)item2;
								}
								builder.Append(",");
								if (pretty)
								{
									builder.Append("\n");
								}
							}
						}
						if (pretty)
						{
							builder.Length -= 2;
						}
						else
						{
							builder.Length--;
						}
					}
					if (pretty && list.Count > 0)
					{
						builder.Append("\n");
						for (int n = 0; n < depth - 1; n++)
						{
							builder.Append("\t");
						}
					}
					builder.Append("]");
					break;
				case Type.BOOL:
					if (b)
					{
						builder.Append("true");
					}
					else
					{
						builder.Append("false");
					}
					break;
				case Type.NULL:
					builder.Append("null");
					break;
				}
			}
		}

		private void Stringify(int depth, StringBuilder builder, bool pretty = false)
		{
			if (depth++ <= 100)
			{
				switch (type)
				{
				case Type.BAKED:
					builder.Append(str);
					break;
				case Type.STRING:
					builder.AppendFormat("\"{0}\"", str);
					break;
				case Type.NUMBER:
					if (useInt)
					{
						builder.Append(this.i.ToString());
					}
					else if (float.IsInfinity(this.n))
					{
						builder.Append("\"INFINITY\"");
					}
					else if (float.IsNegativeInfinity(this.n))
					{
						builder.Append("\"NEGINFINITY\"");
					}
					else if (float.IsNaN(this.n))
					{
						builder.Append("\"NaN\"");
					}
					else
					{
						builder.Append(this.n.ToString());
					}
					break;
				case Type.OBJECT:
					builder.Append("{");
					if (list.Count > 0)
					{
						if (pretty)
						{
							builder.Append("\n");
						}
						for (int i = 0; i < list.Count; i++)
						{
							string arg = keys[i];
							JSONObject jSONObject = list[i];
							if ((bool)jSONObject)
							{
								if (pretty)
								{
									for (int j = 0; j < depth; j++)
									{
										builder.Append("\t");
									}
								}
								builder.AppendFormat("\"{0}\":", arg);
								jSONObject.Stringify(depth, builder, pretty);
								builder.Append(",");
								if (pretty)
								{
									builder.Append("\n");
								}
							}
						}
						if (pretty)
						{
							builder.Length -= 2;
						}
						else
						{
							builder.Length--;
						}
					}
					if (pretty && list.Count > 0)
					{
						builder.Append("\n");
						for (int k = 0; k < depth - 1; k++)
						{
							builder.Append("\t");
						}
					}
					builder.Append("}");
					break;
				case Type.ARRAY:
					builder.Append("[");
					if (list.Count > 0)
					{
						if (pretty)
						{
							builder.Append("\n");
						}
						for (int l = 0; l < list.Count; l++)
						{
							if ((bool)list[l])
							{
								if (pretty)
								{
									for (int m = 0; m < depth; m++)
									{
										builder.Append("\t");
									}
								}
								list[l].Stringify(depth, builder, pretty);
								builder.Append(",");
								if (pretty)
								{
									builder.Append("\n");
								}
							}
						}
						if (pretty)
						{
							builder.Length -= 2;
						}
						else
						{
							builder.Length--;
						}
					}
					if (pretty && list.Count > 0)
					{
						builder.Append("\n");
						for (int n = 0; n < depth - 1; n++)
						{
							builder.Append("\t");
						}
					}
					builder.Append("]");
					break;
				case Type.BOOL:
					if (b)
					{
						builder.Append("true");
					}
					else
					{
						builder.Append("false");
					}
					break;
				case Type.NULL:
					builder.Append("null");
					break;
				}
			}
			else
			{
				Debug.Log((object)"reached max depth!");
			}
		}

		public override string ToString()
		{
			return Print(false);
		}

		public string ToString(bool pretty)
		{
			return Print(pretty);
		}

		public Dictionary<string, string> ToDictionary()
		{
			if (type == Type.OBJECT)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				for (int i = 0; i < list.Count; i++)
				{
					JSONObject jSONObject = list[i];
					switch (jSONObject.type)
					{
					case Type.STRING:
						dictionary.Add(keys[i], jSONObject.str);
						break;
					case Type.NUMBER:
						dictionary.Add(keys[i], jSONObject.n + string.Empty);
						break;
					case Type.BOOL:
						dictionary.Add(keys[i], jSONObject.b + string.Empty);
						break;
					default:
						Debug.LogWarning((object)("Omitting object: " + keys[i] + " in dictionary conversion"));
						break;
					}
				}
				return dictionary;
			}
			Debug.Log((object)"Tried to turn non-Object JSONObject into a dictionary");
			return null;
		}

		public JSONObjectEnumer GetEnumerator()
		{
			return new JSONObjectEnumer(this);
		}

		public static implicit operator WWWForm(JSONObject obj)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Expected O, but got Unknown
			WWWForm val = new WWWForm();
			for (int i = 0; i < obj.list.Count; i++)
			{
				string text = i + string.Empty;
				if (obj.type == Type.OBJECT)
				{
					text = obj.keys[i];
				}
				string text2 = obj.list[i].ToString();
				if (obj.list[i].type == Type.STRING)
				{
					text2 = text2.Replace("\"", string.Empty);
				}
				val.AddField(text, text2);
			}
			return val;
		}

		public static implicit operator bool(JSONObject o)
		{
			return o != null;
		}
	}
}
