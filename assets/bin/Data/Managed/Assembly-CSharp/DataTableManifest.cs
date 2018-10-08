using System.Collections.Generic;
using System.IO;

public class DataTableManifest
{
	private Dictionary<int, MD5Hash> dict;

	private List<string> fileNames = new List<string>();

	public int version
	{
		get;
		private set;
	}

	public MD5Hash GetTableHash(string filename)
	{
		if (dict == null)
		{
			return MD5Hash.invalidHash;
		}
		MD5Hash value = null;
		int hashCode = filename.ToLower().GetHashCode();
		if (dict.TryGetValue(hashCode, out value))
		{
			return value;
		}
		return MD5Hash.invalidHash;
	}

	public List<string> GetAllFileNames()
	{
		return fileNames;
	}

	private uint GetNameHash(string name)
	{
		MD5Hash mD5Hash = MD5Hash.Calc(name);
		return mD5Hash.GetUIntHashCode();
	}

	public static DataTableManifest Create(string csv, int version)
	{
		Dictionary<int, MD5Hash> dictionary = new Dictionary<int, MD5Hash>();
		List<string> list = new List<string>();
		using (StringReader stringReader = new StringReader(csv))
		{
			string text;
			while ((text = stringReader.ReadLine()) != null)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string[] array = text.Split(',');
					if (array.Length >= 2)
					{
						string text2 = array[0].ToLower();
						string hashString = array[1];
						int hashCode = text2.GetHashCode();
						MD5Hash value = MD5Hash.Parse(hashString);
						dictionary.Add(hashCode, value);
						list.Add(text2);
					}
				}
			}
		}
		DataTableManifest dataTableManifest = new DataTableManifest();
		dataTableManifest.version = version;
		dataTableManifest.dict = dictionary;
		dataTableManifest.fileNames = list;
		return dataTableManifest;
	}
}
