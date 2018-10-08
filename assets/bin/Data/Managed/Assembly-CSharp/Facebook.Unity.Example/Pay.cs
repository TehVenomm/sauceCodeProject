using System;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal class Pay : MenuBase
	{
		private string payProduct = string.Empty;

		protected override void GetGui()
		{
			LabelAndTextField("Product: ", ref payProduct);
			if (Button("Call Pay"))
			{
				CallFBPay();
			}
			GUILayout.Space(10f);
		}

		private unsafe void CallFBPay()
		{
			FacebookDelegate<IPayResult> val = new FacebookDelegate<IPayResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			Canvas.Pay(payProduct, "purchaseitem", 1, (int?)null, (int?)null, (string)null, (string)null, (string)null, val);
		}
	}
}
