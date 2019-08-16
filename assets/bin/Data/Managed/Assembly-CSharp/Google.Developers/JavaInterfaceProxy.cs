using UnityEngine;

namespace Google.Developers
{
	public abstract class JavaInterfaceProxy : AndroidJavaProxy
	{
		public JavaInterfaceProxy(string interfaceName)
			: this(interfaceName)
		{
		}
	}
}
