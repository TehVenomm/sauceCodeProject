using System;
using System.Collections.Generic;

namespace Network
{
	[Serializable]
	public class AccessoryPlaceInfo
	{
		public List<string> ids = new List<string>();

		public List<string> parts = new List<string>();

		public uint GetId(int index)
		{
			if (index >= ids.Count)
			{
				return 0u;
			}
			uint result = 0u;
			if (uint.TryParse(ids[index], out result))
			{
				return result;
			}
			return 0u;
		}

		public int GetPart(int index)
		{
			if (index >= parts.Count)
			{
				return 0;
			}
			int result = 0;
			if (int.TryParse(parts[index], out result))
			{
				return result;
			}
			return 0;
		}

		public void Add(string _uuid, string _part)
		{
			ids.Add(_uuid);
			parts.Add(_part);
		}

		public void Del(string _uuid, string _part)
		{
			int num = 0;
			int count = ids.Count;
			while (true)
			{
				if (num < count)
				{
					if (ids[num] == _uuid && parts[num] == _part)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			ids.RemoveAt(num);
			parts.RemoveAt(num);
		}

		public void Clear()
		{
			ids.Clear();
			parts.Clear();
		}

		public void Copy(AccessoryPlaceInfo src)
		{
			Clear();
			if (src != null)
			{
				if (!src.ids.IsNullOrEmpty())
				{
					ids.AddRange(src.ids);
				}
				if (!src.parts.IsNullOrEmpty())
				{
					parts.AddRange(src.parts);
				}
			}
		}

		public bool IsEqual(AccessoryPlaceInfo src)
		{
			if (ids.Count != src.ids.Count)
			{
				return false;
			}
			int i = 0;
			for (int count = src.ids.Count; i < count; i++)
			{
				string item = src.ids[i];
				if (!ids.Contains(item))
				{
					return false;
				}
				string b = src.parts[i];
				int index = ids.IndexOf(item);
				string a = parts[index];
				if (a != b)
				{
					return false;
				}
			}
			return true;
		}

		public List<CharaInfo.UserAccessory> ConvertAccessory()
		{
			List<CharaInfo.UserAccessory> list = new List<CharaInfo.UserAccessory>();
			int i = 0;
			for (int count = ids.Count; i < count; i++)
			{
				CharaInfo.UserAccessory userAccessory = new CharaInfo.UserAccessory();
				userAccessory.uniqId = ids[i];
				userAccessory.place = int.Parse(parts[i]);
				userAccessory.accessoryId = (int)MonoBehaviourSingleton<InventoryManager>.I.accessoryInventory.GetTableID(userAccessory.uniqId);
				list.Add(userAccessory);
			}
			return list;
		}
	}
}
