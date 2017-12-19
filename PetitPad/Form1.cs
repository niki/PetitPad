/* $Id$ */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace app
{
	public partial class Form1 : Form
	{
		//自分自身のAssemblyを取得
		static System.Reflection.Assembly asm =
			System.Reflection.Assembly.GetExecutingAssembly();
		//バージョンの取得
		static System.Version ver = asm.GetName().Version;

		static string _Ver = asm.GetName().Name + " v" +
								ver.Major.ToString() + "." +
								ver.Minor.ToString();// + "." +
                                //ver.Build.ToString() + "." +
                                //ver.Revision.ToString();
		
		static string _AppVer = _Ver
#if DEBUG
										+ ".Dev"
#endif
								    ;

		static string _RegKeyName = @"Software"
									+ @"\PetitPad"
#if DEBUG
										+ ".Dev"
#endif
									;

		static bool		_regOp = true;

		static int		_oldTabIndex = -1;			// 直前のタブインデックス
		static bool		_tabTop = false;			// タブの位置
		static bool		_scrollBarV = false;		// スクロールバー (縦)
		static bool		_scrollBarH = false;		// スクロールバー (横)

		static int		_renameTabIndex = -1;		// 名前変更のタブインデックス
		static string	_renameTabText = "";		// 名前変更タブのテキスト

		static string	_fontName = "Arial";		// フォント名
		static double	_fontSize = 9;				// フォントサイズ

//		static string	_clipboardText = "";		// クリップボードにコピーするテキスト

//		static bool		_browserDocReadFinished = true;	// ブラウザ、ドキュメント読み込み完了フラグ
//		static int		_browserWidth = 1024;		// ブラウザの横幅
//		static int		_thumbnailWidth = 192;		// サムネイルの横幅

//		static bool		_tabChanged = false;		// タブを変更した
		
		static int[]	_tabSelectStart = new int[12];

		// マウスポインタの位置を保存する
		static Point	_mousePoint;

		

		/*!
		 * 真偽文字列を真偽に変換
		 * @param[in] boolean 真偽文字列
		 * @return bool
		 */
		static public bool ToBoolean(int boolean) {
			if (boolean != 0) {
				return true;
			} else {
				return false;
			}
		}

		

		public Form1() {
			InitializeComponent();

#if false
			this.FitGlass();
#endif
		}

		[System.Runtime.InteropServices.DllImportAttribute("uxtheme.dll")]
		private static extern int SetWindowTheme(IntPtr hwnd, string subAppName, string subIdList);

		[System.Security.Permissions.SecurityPermission(
			System.Security.Permissions.SecurityAction.LinkDemand,
			Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m) {
			//const int WM_LBUTTONDBLCLK = 0x0203;
			const int WM_NCLBUTTONDBLCLK = 0x00A3;
			const int WM_QUERYENDSESSION = 0x11;

			if (m.Msg == WM_NCLBUTTONDBLCLK) { // ダブルクリックは無視する
                return;
			} else if (m.Msg == WM_QUERYENDSESSION) { // ログオフ/シャットダウンの検出
                closeAction();
			}


			base.WndProc (ref m);

#if false
			const int WM_NCHITTEST = 0x84;
			const int HTCLIENT = 1;
			const int HTCAPTION = 2;
			
			//マウスポインタがクライアント領域内にあるか
			if ((m.Msg == WM_NCHITTEST) && (m.Result.ToInt32() == HTCLIENT)) {
				if (! mnuPosSizeFix.Checked) {
					//マウスがタイトルバーにあるふりをする
					m.Result = (IntPtr) HTCAPTION;
					return;
				}
			}
#endif
		}

		/*!
		 * Windowsのコマンドを処理
		 * @param[in,out] msg
		 * @param[in] keyData
		 */
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
			// タブの変更
			if (keyData == Keys.F1) {
				tabControl1.SelectedIndex = 0;
				return true;
			} else if (keyData == Keys.F2) {
				tabControl1.SelectedIndex = 1;
				return true;
			} else if (keyData == Keys.F3) {
				tabControl1.SelectedIndex = 2;
				return true;
			} else if (keyData == Keys.F4) {
				tabControl1.SelectedIndex = 3;
				return true;
			} else if (keyData == Keys.F5) {
				tabControl1.SelectedIndex = 4;
				return true;
			} else if (keyData == Keys.F6) {
				tabControl1.SelectedIndex = 5;
				return true;
			} else if (keyData == Keys.F7) {
				tabControl1.SelectedIndex = 6;
				return true;
			} else if (keyData == Keys.F8) {
				tabControl1.SelectedIndex = 7;
				return true;
			} else if (keyData == Keys.F9) {
				tabControl1.SelectedIndex = 8;
				return true;
			} else if (keyData == Keys.F10) {
				tabControl1.SelectedIndex = 9;
				return true;
			} else if (keyData == Keys.F11) {
				tabControl1.SelectedIndex = 10;
				return true;
			} else if (keyData == Keys.F12) {
				tabControl1.SelectedIndex = 11;
				return true;
			}

			return base.ProcessCmdKey( ref msg, keyData );
		}

		/*!
		 * フォームロード
		 */
		private void Form1_Load(object sender, EventArgs e) {
			// バージョン設定
			mnuVersion.Text = _AppVer;

			
			for (int i = 0; i < _tabSelectStart.Length; i++) {
				_tabSelectStart[i] = 0;
			}
			
			

			string fontName = "Verdana";
			double fontSize = 9.0f;



//			if (_regOp) {
#if false
			{
				Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey( _RegKeyName, false );
				//キーが存在しないときは null が返される
				if (regkey != null) {
					this.Left = (int)regkey.GetValue("Left", 0);
					this.Top = (int)regkey.GetValue("Top", 0);
					this.Width = (int)regkey.GetValue("Width", 0);
					this.Height = (int)regkey.GetValue("Height", 0);

					tabControl1.TabPages[0].Text = (string)regkey.GetValue("TabName0", "F1");
					tabControl1.TabPages[1].Text = (string)regkey.GetValue("TabName1", "F2");
					tabControl1.TabPages[2].Text = (string)regkey.GetValue("TabName2", "F3");
					tabControl1.TabPages[3].Text = (string)regkey.GetValue("TabName3", "F4");
					tabControl1.TabPages[4].Text = (string)regkey.GetValue("TabName4", "F5");
					tabControl1.TabPages[5].Text = (string)regkey.GetValue("TabName5", "F6");
					tabControl1.TabPages[6].Text = (string)regkey.GetValue("TabName6", "F7");
					tabControl1.TabPages[7].Text = (string)regkey.GetValue("TabName7", "F8");
					tabControl1.TabPages[8].Text = (string)regkey.GetValue("TabName8", "F9");
					tabControl1.TabPages[9].Text = (string)regkey.GetValue("TabName9", "F10");
					tabControl1.TabPages[10].Text = (string)regkey.GetValue("TabName10", "F11");
					tabControl1.TabPages[11].Text = (string)regkey.GetValue("TabName11", "F12");

					tabControl1.SelectedIndex = (int)regkey.GetValue("LastTab", 0);
					_tabTop = ToBoolean( (int)regkey.GetValue("TabTop", true) );

					_scrollBarV = ToBoolean( (int)regkey.GetValue("ScrollBarV", 0) );
					_scrollBarH = ToBoolean( (int)regkey.GetValue("ScrollBarH", 0) );

					fontName = (string)regkey.GetValue("FontName");
					fontSize = (float)( (int)regkey.GetValue("FontSize") ) / 1000.0f;

					mnuStayOnTop.Checked = ToBoolean( (int)regkey.GetValue("StayOnTop", 0) );

					mnuDeactive20.Checked = ToBoolean( (int)regkey.GetValue("Deactive20", 0) );
					mnuDeactive40.Checked = ToBoolean( (int)regkey.GetValue("Deactive40", 0) );
					mnuDeactive60.Checked = ToBoolean( (int)regkey.GetValue("Deactive60", 0) );
					mnuDeactive80.Checked = ToBoolean( (int)regkey.GetValue("Deactive80", 0) );
					mnuDeactive100.Checked = ToBoolean( (int)regkey.GetValue("Deactive100", 1) );

					regkey.Close();
				}
			} else {
#else
			{
				string path = System.IO.Path.ChangeExtension(Application.ExecutablePath, ".ini");

				nk.IniReader ini = new nk.IniReader();
				ini.Open(path, "Unicode");

				this.Left = ini.ReadInteger("main", "Left", 0);
				this.Top = ini.ReadInteger("main", "Top", 0);
				this.Width = ini.ReadInteger("main", "Width", 400);
				this.Height = ini.ReadInteger("main", "Height", 200);

				tabControl1.TabPages[0].Text = ini.ReadString("main", "TabName0", "F1");
				tabControl1.TabPages[1].Text = ini.ReadString("main", "TabName1", "F2");
				tabControl1.TabPages[2].Text = ini.ReadString("main", "TabName2", "F3");
				tabControl1.TabPages[3].Text = ini.ReadString("main", "TabName3", "F4");
				tabControl1.TabPages[4].Text = ini.ReadString("main", "TabName4", "F5");
				tabControl1.TabPages[5].Text = ini.ReadString("main", "TabName5", "F6");
				tabControl1.TabPages[6].Text = ini.ReadString("main", "TabName6", "F7");
				tabControl1.TabPages[7].Text = ini.ReadString("main", "TabName7", "F8");
				tabControl1.TabPages[8].Text = ini.ReadString("main", "TabName8", "F9");
				tabControl1.TabPages[9].Text = ini.ReadString("main", "TabName9", "F10");
				tabControl1.TabPages[10].Text = ini.ReadString("main", "TabName10", "F11");
				tabControl1.TabPages[11].Text = ini.ReadString("main", "TabName11", "F12");

				tabControl1.SelectedIndex = ini.ReadInteger("main", "LastTab", 0);
				_tabTop = ini.ReadBool("main", "TabTop", false);

				_scrollBarV = ini.ReadBool("main", "ScrollBarV", true);
				_scrollBarH = ini.ReadBool("main", "ScrollBarH", false);

				fontName = ini.ReadString("main", "FontName", "Arial");
				fontSize = ini.ReadDouble("main", "FontSize", 9);

				mnuStayOnTop.Checked = ini.ReadBool("main", "StayOnTop", false);

				mnuDeactive20.Checked = ini.ReadBool("main", "Deactive20", false);
				mnuDeactive40.Checked = ini.ReadBool("main", "Deactive40", false);
				mnuDeactive60.Checked = ini.ReadBool("main", "Deactive60", false);
				mnuDeactive80.Checked = ini.ReadBool("main", "Deactive80", false);
				mnuDeactive100.Checked = ini.ReadBool("main", "Deactive100", true);

				ini.Close();
			}
#endif

			// タブ位置
			if (! _tabTop) {
				mnuFunctionBarBottom_Click( null, null );
			}

			// スクロールバー
			if (_scrollBarV) {
				mnuScrollBarV_Click( null, null );
			}

			if (_scrollBarH) {
				mnuScrollBarH_Click( null, null );
			}

			// フォント
			_fontName = fontName;
			_fontSize = fontSize;
			textBox1.Font = new Font( fontName, (float)fontSize, FontStyle.Regular );


			//TabControl1のVisual Styleを無効にする
			SetWindowTheme( tabControl1.Handle, "", "" );


			// 常に手前に表示設定
			mnuStayOnTop_Click( null, null );


			tabControl1_SelectedIndexChanged( null, null );

			// フォーカスをテキストボックスに移す
			textBox1.Focus();
		}

		/*!
		 * 終了時に保存する
		 * @param Non
		 * @return Non
		 */
		private void closeAction() {
			// 保存
			saveMemo();


//			if (_regOp) {
#if false
				Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey( _RegKeyName );

				regkey.SetValue("Left", this.Left);
				regkey.SetValue("Top", this.Top);
				regkey.SetValue("Width", this.Width);
				regkey.SetValue("Height", this.Height);

				regkey.SetValue("TabName0", tabControl1.TabPages[0].Text);
				regkey.SetValue("TabName1", tabControl1.TabPages[1].Text);
				regkey.SetValue("TabName2", tabControl1.TabPages[2].Text);
				regkey.SetValue("TabName3", tabControl1.TabPages[3].Text);
				regkey.SetValue("TabName4", tabControl1.TabPages[4].Text);
				regkey.SetValue("TabName5", tabControl1.TabPages[5].Text);
				regkey.SetValue("TabName6", tabControl1.TabPages[6].Text);
				regkey.SetValue("TabName7", tabControl1.TabPages[7].Text);
				regkey.SetValue("TabName8", tabControl1.TabPages[8].Text);
				regkey.SetValue("TabName9", tabControl1.TabPages[9].Text);
				regkey.SetValue("TabName10", tabControl1.TabPages[10].Text);
				regkey.SetValue("TabName11", tabControl1.TabPages[11].Text);

				regkey.SetValue("LastTab", tabControl1.SelectedIndex);
				regkey.SetValue("TabTop", _tabTop ? 1 : 0);	// bool -> int

				regkey.SetValue("ScrollBarV", _scrollBarV ? 1 : 0);	// bool -> int
				regkey.SetValue("ScrollBarH", _scrollBarH ? 1 : 0);	// bool -> int

				regkey.SetValue("FontName", textBox1.Font.Name);
				regkey.SetValue("FontSize", (int)(textBox1.Font.Size * 1000.0f));	// float -> int

				regkey.SetValue("StayOnTop", mnuStayOnTop.Checked ? 1 : 0);	// bool -> int

				regkey.SetValue("Deactive20", mnuDeactive20.Checked ? 1 : 0);	// bool -> int
				regkey.SetValue("Deactive40", mnuDeactive40.Checked ? 1 : 0);	// bool -> int
				regkey.SetValue("Deactive60", mnuDeactive60.Checked ? 1 : 0);	// bool -> int
				regkey.SetValue("Deactive80", mnuDeactive80.Checked ? 1 : 0);	// bool -> int
				regkey.SetValue("Deactive100", mnuDeactive100.Checked ? 1 : 0);	// bool -> int

				regkey.Close();
			} else {
#else
			{
				string path = System.IO.Path.ChangeExtension(Application.ExecutablePath, ".ini");

				nk.IniWriter ini = new nk.IniWriter();
				ini.Open(path, "Unicode");

				ini.WriteInteger("main", "Left", this.Left);
				ini.WriteInteger("main", "Top", this.Top);
				ini.WriteInteger("main", "Width", this.Width);
				ini.WriteInteger("main", "Height", this.Height);

				ini.WriteString("main", "TabName0", tabControl1.TabPages[0].Text);
				ini.WriteString("main", "TabName1", tabControl1.TabPages[1].Text);
				ini.WriteString("main", "TabName2", tabControl1.TabPages[2].Text);
				ini.WriteString("main", "TabName3", tabControl1.TabPages[3].Text);
				ini.WriteString("main", "TabName4", tabControl1.TabPages[4].Text);
				ini.WriteString("main", "TabName5", tabControl1.TabPages[5].Text);
				ini.WriteString("main", "TabName6", tabControl1.TabPages[6].Text);
				ini.WriteString("main", "TabName7", tabControl1.TabPages[7].Text);
				ini.WriteString("main", "TabName8", tabControl1.TabPages[8].Text);
				ini.WriteString("main", "TabName9", tabControl1.TabPages[9].Text);
				ini.WriteString("main", "TabName10", tabControl1.TabPages[10].Text);
				ini.WriteString("main", "TabName11", tabControl1.TabPages[11].Text);

				ini.WriteInteger("main", "LastTab", tabControl1.SelectedIndex);
				ini.WriteBool("main", "TabTop", _tabTop);

				ini.WriteBool("main", "ScrollBarV", _scrollBarV);
				ini.WriteBool("main", "ScrollBarH", _scrollBarH);

				ini.WriteString("main", "FontName", textBox1.Font.Name);
				ini.WriteDouble("main", "FontSize", textBox1.Font.Size);

				ini.WriteBool("main", "StayOnTop", mnuStayOnTop.Checked);

				ini.WriteBool("main", "Deactive20", mnuDeactive20.Checked);
				ini.WriteBool("main", "Deactive40", mnuDeactive40.Checked);
				ini.WriteBool("main", "Deactive60", mnuDeactive60.Checked);
				ini.WriteBool("main", "Deactive80", mnuDeactive80.Checked);
				ini.WriteBool("main", "Deactive100", mnuDeactive100.Checked);

				ini.Close();
			}
#endif

			// 現在のテキストを保存
			saveMemo();
			//tabControl1_SelectedIndexChanged(null, null);	
		}
		
		/*!
		 * フォームクローズ
		 */
		private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
			closeAction();
		}

		/*!
		 * タブをダブルクリック
		 */
		private void tabControl1_MouseDoubleClick(object sender, MouseEventArgs e) {
			//MessageBox.Show(tabControl1.SelectedIndex.ToString());

			int index = tabControl1.SelectedIndex;

			Rectangle rect = tabControl1.GetTabRect(index);
			
			// 名前変更用に用意
			txtTabRename.Parent = this;
			txtTabRename.BringToFront();

			txtTabRename.Font = tabControl1.Font;

			txtTabRename.Left = tabControl1.Left + rect.Left;
			txtTabRename.Top = tabControl1.Top + rect.Top - 1;
			txtTabRename.Width = rect.Width - 1;

			txtTabRename.Text = tabControl1.TabPages[index].Text;
			txtTabRename.SelectionStart = txtTabRename.Text.Length;
			txtTabRename.SelectionLength = 0;

			txtTabRename.Visible = true;
			txtTabRename.Focus();

			_renameTabIndex = index;
			_renameTabText = tabControl1.TabPages[index].Text;
		}

		/*!
		 * 文字列の描画
		 * @param[in] g 
		 * @param[in] s 文字列
		 * @param[in] font Fontオブジェクト
		 * @param[in] brush Brushオブジェクト
		 * @param[in] lx 表示位置X
		 * @param[in] ly 表示位置Y
		 * @param[in] interval 文字間
		 */
		public void DrawIntervalString(Graphics g, string s, Font font, Brush brush, float lx, float ly, float interval) {
			float x = 0;

			for (int i = 0; i < s.Length; i++) {
				char moji = s[i];

				//StringFormatオブジェクトの作成
				StringFormat sf = new StringFormat();

				//計測する文字の範囲を指定する
				CharacterRange[] characterRanges = {new CharacterRange(i, 1)};
				sf.SetMeasurableCharacterRanges(characterRanges);

				//文字列のレイアウト四角形を指定する
				RectangleF layoutRect = new RectangleF(0, 0, 100, 30);

				//文字列に外接するRegion配列を取得する
				Region[] stringRegions = g.MeasureCharacterRanges(s, font, layoutRect, sf);

				RectangleF rect = stringRegions[0].GetBounds(g);

				g.DrawString("" + moji, font, brush, lx + x, ly);

				x += rect.Width + interval;


				sf.Dispose();
			}
		}

		/*!
		 * タブの描画
		 */
		private void tabControl1_DrawItem(object sender, DrawItemEventArgs e) {
			int index = e.Index;
			string caption = tabControl1.TabPages[index].Text;


			SuspendLayout();

			{
				SolidBrush brush = new SolidBrush( textBox1.BackColor );

				if (index == tabControl1.SelectedIndex) {
					//e.Graphics.FillRectangle( Brushes.WhiteSmoke, e.Bounds );
					brush = new SolidBrush( textBox1.BackColor );
					//e.Graphics.FillRectangle( brush, e.Bounds );
					e.Graphics.FillRectangle( brush, e.Bounds.Left, e.Bounds.Top-2, e.Bounds.Width, e.Bounds.Height+2 );
					brush.Dispose();

					// アクティブタブに線をつける
					if (_tabTop) {
						brush = new SolidBrush( Color.FromArgb(255, 200, 60) );
						e.Graphics.FillRectangle( brush, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, 5 );
						brush.Dispose();

						brush = new SolidBrush( Color.FromArgb(230, 139, 44));
						e.Graphics.FillRectangle( brush, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, 3 );
						brush.Dispose();
					} else {
						brush = new SolidBrush( Color.FromArgb(255, 200, 60) );
						e.Graphics.FillRectangle( brush, e.Bounds.Left, e.Bounds.Top + e.Bounds.Height - 5, e.Bounds.Width, 5 );
						brush.Dispose();

						brush = new SolidBrush( Color.FromArgb(230, 139, 44) );
						e.Graphics.FillRectangle( brush, e.Bounds.Left, e.Bounds.Top + e.Bounds.Height - 3, e.Bounds.Width, 3 );
						brush.Dispose();
					}
				} else {
					//e.Graphics.FillRectangle( Brushes.Gainsboro, e.Bounds );
					e.Graphics.FillRectangle( SystemBrushes.Control, e.Bounds );
				}
			}


			// 文字列の描画
			{
				Font font = tabControl1.Font;
				SolidBrush brush = new SolidBrush( textBox1.ForeColor );

				if (index == tabControl1.SelectedIndex) {
					DrawIntervalString( e.Graphics, caption, font, brush, e.Bounds.Left + 4, e.Bounds.Top + 3, -1 );
				} else {
					if (_tabTop) {
						DrawIntervalString( e.Graphics, caption, font, brush, e.Bounds.Left, e.Bounds.Top + 3, -1 );
					} else {
						DrawIntervalString( e.Graphics, caption, font, brush, e.Bounds.Left, e.Bounds.Top + 1, -1 );
					}
				}

				brush.Dispose();
			}

			//TabRenderer.DrawTabItem(e.Graphics, e.Bounds, caption, font, false, System.Windows.Forms.VisualStyles.TabItemState.Selected);

			ResumeLayout(false);
		}

		/*!
		 * メモを保存する
		 * @param Non
		 * @return Non
		 */
		private void saveMemo()
		{
			// 保存
			if (_oldTabIndex >= 0) {
				if (textBox1.Modified) {
					bool saved = false;

					if (! saved) {
//						if (_regOp) {
#if false
						{
							// reg
							Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey( _RegKeyName );
							regkey.SetValue("txt" + _oldTabIndex.ToString(), textBox1.Text, Microsoft.Win32.RegistryValueKind.String);
							regkey.Close();
						} else {
#else
						{
							//*.txt
							string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\" + tabControl1.TabPages[_oldTabIndex].Text + ".txt";
							System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, System.Text.Encoding.GetEncoding("Unicode"));
							sw.Write(textBox1.Text);
							sw.Close();
						}
#endif
						//*.rtf
//						string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\" + tabControl1.TabPages[_oldTabIndex].Text + ".rtf";
//						richTextBox1.SaveFile(path, RichTextBoxStreamType.RichText);
						saved = true;
					}
				}
			}
		}

		/*!
		 * メモを読み込む
		 * @param[in] index 読み込むメモ番号
		 */
		private void loadMemo(int index) {
			bool loaded = false;

			textBox1.Clear();

			if (! loaded) {
//				if (_regOp) {
#if false
					// reg
					Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey( _RegKeyName, false );
					if (regkey != null) {
						textBox1.Text = (string)regkey.GetValue("txt" + index.ToString(), "");
						regkey.Close();

						if (textBox1.Text.Length > 0) {
							loaded = true;
						}
					}
				} else {
#else
				{
					//*.txt
					string path = System.IO.Path.GetDirectoryName( Application.ExecutablePath ) + @"\" + tabControl1.TabPages[index].Text + ".txt";

					if (System.IO.File.Exists( path )) {
						System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("Unicode"));
						textBox1.Text = sr.ReadToEnd();
						textBox1.Font = new System.Drawing.Font(_fontName, (float)_fontSize);
						sr.Close();

						loaded = true;
					}
				}
#endif

				//*.rtf
//				string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\" + tabControl1.TabPages[index].Text + ".rtf";
//
//				if( System.IO.File.Exists( path ) ) {
//					richTextBox1.LoadFile( path, RichTextBoxStreamType.RichText );
//					richTextBox1.Font = new System.Drawing.Font(FontName, (float)FontSize);
//
//					loaded = true;
//				}
			}

				
			textBox1.SelectionStart = 0;
			textBox1.SelectionLength = 0;
		}
		
		/*!
		 * タブの選択
		 */
		private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e) {
			if (_oldTabIndex == e.TabPageIndex) {
				tabControl1_MouseDoubleClick( null, null );
			} else {
				// カーソル位置を保存
				if (_oldTabIndex >= 0) {
					_tabSelectStart[_oldTabIndex] = textBox1.SelectionStart;
				}
			}
		}

		/*!
		 * タブを変更
		 */
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
			int index = tabControl1.SelectedIndex;


			// 保存
			if (sender != null) {
				saveMemo();
			}


			_oldTabIndex = index;


			// 読み込み
			loadMemo( index );


#if false
			richTextBox1.DetectUrls = false;
			richTextBox1.DetectUrls = true;
#endif

			textBox1.Focus();

			// カーソル位置の復元
			textBox1.SelectionStart = _tabSelectStart[index];

//			_tabChanged = true;
		}

		/*!
		 * タブのマウスダウン
		 */
		private void tabControl1_MouseDown(object sender, MouseEventArgs e) {
//			_tabChanged = false;

#if false
			if (e.Button == System.Windows.Forms.MouseButtons.Right) {
				_tabChanged = false;
			} else if (_tabChanged) {
				_tabChanged = false;
			} else {
				tabControl1_MouseDoubleClick( null, null );
			}
#endif

#if false
			for (int i = 0; i < tabControl1.TabCount; i++) {
				if (tabControl1.GetTabRect(i).Contains( e.X, e.Y )) {
					if (OldTabIndex == i) {
						tabControl1_MouseDoubleClick( null, null );
					}

					break;
				}
			}

			_tabChanged = false;
#endif
		}

		/*!
		 * ファンクションバーを上に表示
		 */
		private void mnuFunctionBarTop_Click(object sender, EventArgs e)
		{
			mnuFunctionBarTop.Checked = true;
			mnuFunctionBarBottom.Checked = false;

			tabControl1.Dock = DockStyle.Top;
			tabControl1.Alignment = TabAlignment.Top;

			tabControl1.Height = 23;

			_tabTop = true;
		}

		/*!
		 * ファンクションバーを下に表示
		 */
		private void mnuFunctionBarBottom_Click(object sender, EventArgs e)
		{
			mnuFunctionBarTop.Checked = false;
			mnuFunctionBarBottom.Checked = true;

			tabControl1.Dock = DockStyle.Bottom;
			tabControl1.Alignment = TabAlignment.Bottom;

			tabControl1.Height = 23;

			_tabTop = false;
		}

		/*!
		 * 縦スクロールバーを表示
		 */
		private void mnuScrollBarV_Click(object sender, EventArgs e) {
			mnuScrollBarV.Checked = mnuScrollBarV.Checked ? false : true;

			if (mnuScrollBarV.Checked) {
//				richTextBox1.ScrollBars = richTextBox1.ScrollBars | RichTextBoxScrollBars.Vertical;
				textBox1.ScrollBars = textBox1.ScrollBars | ScrollBars.Vertical;
				_scrollBarV = true;
			} else {
//				richTextBox1.ScrollBars = richTextBox1.ScrollBars & ~RichTextBoxScrollBars.Vertical;
				textBox1.ScrollBars = textBox1.ScrollBars & ~ScrollBars.Vertical;
				_scrollBarV = false;
			}
		}

		/*!
		 * 横スクロールバーを表示
		 */
		private void mnuScrollBarH_Click(object sender, EventArgs e) {
			mnuScrollBarH.Checked = mnuScrollBarH.Checked ? false : true;

			if (mnuScrollBarH.Checked) {
//				richTextBox1.ScrollBars = richTextBox1.ScrollBars | RichTextBoxScrollBars.Horizontal;
				textBox1.ScrollBars = textBox1.ScrollBars | ScrollBars.Horizontal;
				_scrollBarH = true;
			} else {
//				richTextBox1.ScrollBars = richTextBox1.ScrollBars & ~RichTextBoxScrollBars.Horizontal;
				textBox1.ScrollBars = textBox1.ScrollBars & ~ScrollBars.Horizontal;
				_scrollBarH = false;
			}
		}

		/*!
		 * フォントの変更
		 */
		private void mnuFontChange_Click(object sender, EventArgs e) {
			FontDialog d = new FontDialog();
			d.Font = textBox1.Font;

			System.Windows.Forms.DialogResult ret = d.ShowDialog();

			if (ret == System.Windows.Forms.DialogResult.OK) {
				textBox1.Font = d.Font;
				_fontName = d.Font.Name;
				_fontSize = d.Font.Size;
			}

			d.Dispose();
		}

		/*!
		 * タブの名前を変更
		 */
		private void mnuTabRename_Click(object sender, EventArgs e) {
			tabControl1_MouseDoubleClick( null, null );
		}

		/*!
		 * 常に手前に表示
		 */
		private void mnuStayOnTop_Click(object sender, EventArgs e) {
			if (mnuStayOnTop.Checked) {
				this.TopMost = true;
			} else {
				this.TopMost = false;
			}
		}

		/*!
		 * 終了
		 */
		private void mnuExit_Click(object sender, EventArgs e) {
			Close();
		}

		/*!
		 * トレイアイコンをクリックでアクティブ
		 */
		private void notifyIcon1_Click(object sender, EventArgs e) {
			this.Activate();
		}

		/*!
		 * 名前変更ボックスがフォーカスを失った
		 */
		private void txtTabRename_Leave(object sender, EventArgs e) {
			txtTabRename.Visible = false;
		}

		/*!
		 * 名前変更ボックス キーダウン
		 */
		private void txtTabRename_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Escape) {
				txtTabRename.Text = _renameTabText;
				txtTabRename.Visible = false;
			} else if (e.KeyCode == Keys.Enter) {
				txtTabRename.Visible = false;
			}
		}

		/*!
		 * 
		 */
		private void txtTabRename_VisibleChanged(object sender, EventArgs e) {
			if (_renameTabIndex == -1) {
				return;
			}

			if (txtTabRename.Text.Length == 0) {
				return;
			}

			if (! txtTabRename.Visible) {
				int index = _renameTabIndex;
				_renameTabIndex = -1;


//				if (_regOp) {
#if false
					string fromName = tabControl1.TabPages[index].Text;
					string toName = txtTabRename.Text;

					if (fromName == toName) {
						return;
					}

					Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey( _RegKeyName );

					regkey.SetValue("TabName" + index.ToString(), toName);

					regkey.Close();
				} else {
#else
				{
					string ext = ".txt";//".rtf";
					string fromName = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\" + tabControl1.TabPages[index].Text + ext;
					string toName = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\" + txtTabRename.Text + ext;

					if (fromName == toName) {
						return;
					}

					// すでにファイルが存在している場合は削除する
					if (System.IO.File.Exists( toName )) {
						System.IO.File.Delete( toName );
					}

					if (System.IO.File.Exists( fromName )) {
						System.IO.File.Move( fromName, toName );
						//System.IO.File.Delete( fromName );
					}
				}
#endif

				tabControl1.TabPages[index].Text = txtTabRename.Text;
			}
		}

		/*
		 * 不透明度の変更
		 */
		private void mnuDeactive20_Click(object sender, EventArgs e) {
			mnuDeactive20.Checked = true;
			mnuDeactive40.Checked = false;
			mnuDeactive60.Checked = false;
			mnuDeactive80.Checked = false;
			mnuDeactive100.Checked = false;
		}
		private void mnuDeactive40_Click(object sender, EventArgs e) {
			mnuDeactive20.Checked = false;
			mnuDeactive40.Checked = true;
			mnuDeactive60.Checked = false;
			mnuDeactive80.Checked = false;
			mnuDeactive100.Checked = false;
		}
		private void mnuDeactive60_Click(object sender, EventArgs e) {
			mnuDeactive20.Checked = false;
			mnuDeactive40.Checked = false;
			mnuDeactive60.Checked = true;
			mnuDeactive80.Checked = false;
			mnuDeactive100.Checked = false;
		}
		private void mnuDeactive80_Click(object sender, EventArgs e) {
			mnuDeactive20.Checked = false;
			mnuDeactive40.Checked = false;
			mnuDeactive60.Checked = false;
			mnuDeactive80.Checked = true;
			mnuDeactive100.Checked = false;
		}
		private void mnuDeactive100_Click(object sender, EventArgs e) {
			mnuDeactive20.Checked = false;
			mnuDeactive40.Checked = false;
			mnuDeactive60.Checked = false;
			mnuDeactive80.Checked = false;
			mnuDeactive100.Checked = true;
		}

		/*
		 * フォームがアクティブになった時に不透明にする
		 */
		private void Form1_Activated(object sender, EventArgs e) {
			this.Opacity = 1.0;
		}

		/*
		 * フォームが非アクティブになった時に不透明度を設定する
		 */
		private void Form1_Deactivate(object sender, EventArgs e) {
			if (mnuDeactive20.Checked) {
				this.Opacity = 0.2;
			} else if (mnuDeactive40.Checked) {
				this.Opacity = 0.4;
			} else if (mnuDeactive60.Checked) {
				this.Opacity = 0.6;
			} else if (mnuDeactive80.Checked) {
				this.Opacity = 0.8;
			} else if (mnuDeactive100.Checked) {
				this.Opacity = 1.0;
			}

			this.Refresh();
		}

		/*!
		 * テキスト入力
		 */
		private void textBox1_KeyDown(object sender, KeyEventArgs e) {
			if (e.Control && e.KeyCode == Keys.A) {
                // すべて選択
                textBox1.SelectAll();
				e.Handled = true;
			} else if (e.Control && e.KeyCode == Keys.D) {
                // 10進数に変換
                string sel = textBox1.SelectedText;

				if (sel.Length > 0) {
					try {
						long d = Convert.ToInt32( sel );
						textBox1.SelectedText = d.ToString("D");
					}
					catch {
						/*None*/
					}

					e.Handled = true;
				}
			} else if (e.Control && e.KeyCode == Keys.H) {
                // 16進数に変換
                string sel = textBox1.SelectedText;

				if (sel.Length > 0) {
					try {
						long d = Convert.ToInt32( sel );
						textBox1.SelectedText = d.ToString("X");
					}
					catch {
						/*None*/
					}

					e.Handled = true;
				}
			} else if (e.Control && e.KeyCode == Keys.E) {
                // 計算
                string sel = textBox1.SelectedText;

				if (sel.Length > 0) {
					DataTable dt = new DataTable();

					try {
						double d = Convert.ToDouble( dt.Compute( sel, null ) );
						textBox1.SelectedText = d.ToString("G");
					}
					catch {
						/*None*/
					}

					dt.Dispose();

					e.Handled = true;
				}
			}
		}

		/*
		 * テキスト キーアップ
		 */
		private void textBox1_KeyUp(object sender, KeyEventArgs e) {
			if (e != null) {
				e.Handled = true;
			}
		}

		/*
		 * テキスト マウスアップ
		 */
		private void textBox1_MouseUp(object sender, MouseEventArgs e) {
			textBox1_KeyUp( sender, null );
		}

		private void Form1_Paint(object sender, PaintEventArgs e) {
#if false
			// black brush for Alpha transparency
			SolidBrush blackBrush = new SolidBrush(Color.Black);

			Graphics g = e.Graphics;

			if (this.IsGlassEnabled()) {
				g.FillRectangle( blackBrush, this.ClientRectangle );
			} else {
				g.FillRectangle( SystemBrushes.Control, this.ClientRectangle );
			}

			blackBrush.Dispose();
#endif
		}

		private void Form1_MouseMove(object sender, MouseEventArgs e) {
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
				// 吸着するサイズ
				Size gap = new Size(16, 16);

				// 移動先のフォーム位置
				Rectangle newPosition = new Rectangle(
					this.Left + e.X - _mousePoint.X,
					this.Top + e.Y - _mousePoint.Y,
					this.Width,
					this.Height);
				// 判定用のRECT
				Rectangle newRect = new Rectangle();

				// 作業領域の取得（この作業領域の内側に吸着する）
				Size area = new Size(
					System.Windows.Forms.Screen.GetWorkingArea(this).Width,
					System.Windows.Forms.Screen.GetWorkingArea(this).Height);

				// 画面端の判定用（画面の端の位置に、吸着するサイズ分のRECTを定義する）
				Rectangle rectLeft = new Rectangle(
											0, 
											0, 
											gap.Width, 
											area.Height);
				Rectangle rectTop = new Rectangle(
											0, 
											0, 
											area.Width, 
											gap.Height);
				Rectangle rectRight = new Rectangle(
											area.Width - gap.Width, 
											0, 
											gap.Width, 
											area.Height);
				Rectangle rectBottom = new Rectangle(
											0, 
											area.Height - gap.Height, 
											area.Width, 
											gap.Height);
				// 衝突判定
				// 判定用のRECTを自分のウィンドウの隅に重ねるように移動し、
				// 画面端の判定用のRECTと衝突しているかチェックする。
				// 衝突していた場合は、吸着させるように移動する

				// 左端衝突判定
				{
					newRect = newPosition;
					newRect.Width = gap.Width;

					if (newRect.IntersectsWith(rectLeft)) {
						// 左端に吸着させる
						newPosition.X = 0;
					}
				}
				// 右端衝突判定
				{
					newRect = newPosition;
					newRect.X = newPosition.Right - gap.Width;	// ウィンドウの右隅
					newRect.Width = gap.Width;

					if (newRect.IntersectsWith(rectRight)) {
						// 右端に吸着させる
						newPosition.X = area.Width - this.Width;
					}
				}
				// 上端衝突判定
				{
					newRect = newPosition;
					newRect.Height = gap.Height;
						
					if (newRect.IntersectsWith(rectTop)) {
						// 上端に吸着させる
						newPosition.Y = 0;
					}
				}
				// 下端衝突判定
				{
					newRect = newPosition;
					newRect.Y = newPosition.Bottom - gap.Height; // ウィンドウの下端
					newRect.Height = gap.Height;

					if (newRect.IntersectsWith(rectBottom)) {
						// 下端に吸着させる
						newPosition.Y = area.Height - this.Height;
					}
				}

				// 実際に移動させる
				this.Left = newPosition.Left;
				this.Top = newPosition.Top;
			}
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e) {
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
				//位置を記憶する
				_mousePoint = new Point(e.X, e.Y);
			}
		}

		/*!
		 * メモをテキストファイルにエクスポート
		 */
		private void mnuExportText_Click(object sender, EventArgs e) {
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.SelectedPath = System.IO.Path.GetDirectoryName( Application.ExecutablePath );

			DialogResult ret = dialog.ShowDialog(this);
			if (ret == DialogResult.OK) {
				for (int i = 0; i < 12; i++) {
					string text = "";

					Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey( _RegKeyName, false );
					if (regkey != null) {
						text = (string)regkey.GetValue("txt" + i.ToString(), "");
						regkey.Close();
					}

					if (text.Length > 0) {
						string path = dialog.SelectedPath + @"\" + tabControl1.TabPages[i].Text + ".txt";
						System.IO.StreamWriter sw = new System.IO.StreamWriter(path, false, System.Text.Encoding.GetEncoding("Unicode"));
						sw.Write(text);
						sw.Close();
					}
				}
			}

			dialog.Dispose();
		}

		
	}
}
