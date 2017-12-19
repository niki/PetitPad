using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace app
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(true);
			Application.Run(new Form1());
		}
	}
}
