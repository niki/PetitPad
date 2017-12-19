
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Diagnostics;	//Process.Start
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace nk {

	public class Utils
	{
		// 改行
		public const string _CR   = "\r";
		public const string _LF   = "\n";
		public const string _CRLF = "\r\n";
		public const string _BR   = "&lt;br&gt;";



		/*!
		 * インクリメンタルサーチ
		 * @param[in] text1 検索対象文字列
		 * @param[in] ptn 検索パターン (& で And, | で Or 扱い)
		 * @param[in] ignoreCase 大文字小文字を無視する
		 * @retval true 成功
		 * @retval false 失敗
		 */
		static public bool ISearch(string[] text1, string ptn, bool ignoreCase) {
			if (ptn.Length == 0) {
				return true;
			}

			// 大文字小文字の区別なし
			if (ignoreCase) {
				ptn = ptn.ToUpper();

				for (int i = 0; i < text1.Length; i++) {
					text1[i] = text1[i].ToUpper();
				}
			}


			bool retOr = false;

			// or判定
			//   |トークンはどれか1つでもOKならいい
			string[] tokenOr = ptn.Split('|');

			for (int i = 0; i < tokenOr.Length; i++) {
				if (tokenOr[i].Length == 0) {
					continue;	// トークンなし
				}

				bool retAnd = true;

				// and判定
				//   &トークンはすべてOKでなくてはいけない
				string[] tokenAnd = tokenOr[i].Split('&');

				for (int j = 0; j < tokenAnd.Length; j++) {
					if (tokenAnd[j].Length == 0) {
						continue;	// トークンなし
					}

					bool retGroup = false;

					// テキスト群から見つかるか
					for (int k = 0; k < text1.Length; k++) {
						if (text1[k].IndexOf( tokenAnd[j] ) >= 0) {
							retGroup = true;	// 見つかった
							break;
						}
					}

					if (retGroup) {
						// OK
					} else {
						retAnd = false;	// &は1つでも見つからなければアウト
						break;
					}
				}

				// &トークンすべてOKなら
				if (retAnd) {
					retOr = true;
				}
			}

			return retOr;
		}


		/*!
		 * プロセス実行
		 * @param[in] exePath 実行するパス
		 * @param[in] param 引数に渡す文字列
		 * @param[in] workPath 作業パス
		 */
		static public void Run(string exePath, string param, string workPath) {
			try
			{
				Process p = new Process();
				p.StartInfo.FileName = exePath;
				p.StartInfo.Arguments = param;
				p.StartInfo.WorkingDirectory = workPath;
				p.Start();
			}
			catch
			{
				MessageBox.Show("Process error, (%s)" + exePath);
			}
		}


		/*!
		 * 新しいファイルのコピー
		 * @param[in] from コピー元
		 * @param[in] to コピー先
		 * @param[in] overWriteNewDate trueのとき、コピー元がコピー先より新しい場合のみコピーする
		 */
		static public void NewFileCopy(string from, string to, bool overWriteNewDate)
		{
			for (;;) {
				try
				{
					bool copy = false;

					if (overWriteNewDate) {
						DateTime ctFrom = System.IO.File.GetLastWriteTime( from );
						DateTime ctTo = System.IO.File.GetLastWriteTime( to );

						if (ctFrom.CompareTo( ctTo ) > 0) {
							copy = true;
						}
					} else {
						copy = true;
					}

					if (copy) {
						System.IO.File.Copy( from, to, true );
					}
					
					break;
				}
				catch
				{
					if (MessageBox.Show( "Failed to copy the files. Do you want to retry?\n\nFile: " + to, "Confirmation", MessageBoxButtons.YesNo )
							== System.Windows.Forms.DialogResult.No)
					{
						break;
					}
				}
			}
		}


		/*!
		 * 真偽文字列を真偽に変換
		 * @param[in] boolean 真偽文字列
		 * @return bool
		 */
		static public bool ToBoolean(string boolean) {
			if (boolean == "True") {
				return true;
			} else {
				return false;
			}
		}


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


		/*!
		 * HTML形式へ変換
		 * @param[in] text 文字列
		 * @return string
		 */
		static public string ToHTML(string text) {
			string ret = text;
			ret = ret.Replace("&", "&amp;");
			ret = ret.Replace("\t", "&t;");
			ret = ret.Replace(_LF, _BR);		// 改行コードをタグに変換
			ret = ret.Replace("\r", "");		// CRをなしにする
			ret = ret.Replace("©", "&copy;");	//©	&copy;	&#169;	著作権記号
			ret = ret.Replace("®", "&reg;");	//®	&reg;	&#174;	登録商標記号
			ret = ret.Replace("™", "&trade;");	//™	&trade;	&#8482;	商標記号
			ret = ret.Replace("\"", "&quot;");	//"	&quot;	&#34;	引用符
			ret = ret.Replace(" ", "&nbsp;");	// 	&nbsp;	&#160;	改行なし空白
			ret = ret.Replace(">", "&gt;");		//>	&gt;	&#62;	大なり
			ret = ret.Replace("<", "&lt;");		//<	&lt;	&#60;	小なり
			return ret;
		}


		/*!
		 * HTML形式から変換
		 * @param[in] text 文字列
		 * @return string
		 */
		static public string FromHTML(string text) {
			string ret = text;
			ret = ret.Replace("&amp;", "&");
			ret = ret.Replace("&t;", "\t");
			ret = ret.Replace(_BR, _CRLF);		// 改行コードをタグに変換
			ret = ret.Replace("&copy;", "©");	//©	&copy;	&#169;	著作権記号
			ret = ret.Replace("&reg;", "®");	//®	&reg;	&#174;	登録商標記号
			ret = ret.Replace("&trade;", "™");	//™	&trade;	&#8482;	商標記号
			ret = ret.Replace("&quot;", "\"");	//"	&quot;	&#34;	引用符
			ret = ret.Replace("&nbsp;", " ");	// 	&nbsp;	&#160;	改行なし空白
			ret = ret.Replace("&gt;", ">");		//>	&gt;	&#62;	大なり
			ret = ret.Replace("&lt;", "<");		//<	&lt;	&#60;	小なり
			return ret;
		}


		/*!
		 * Unicodeコード文字列へ変換
		 * @param[in] text 文字列
		 * @return string
		 */
		static public string ToUnicode(string text) {
			string ret = text;
			ret = ret.Replace(_CR, "\\u000d");
			ret = ret.Replace(_LF, "\\u000a");
			ret = ret.Replace("#", "\\u0023"); // コメントに使用
			ret = ret.Replace("=", "\\u003d"); // キー、値のセパレータに使用
			ret = ret.Replace("|", "\\u007c"); // 文字列配列のセパレータに使用
			ret = ret.Replace("[", "\\u005b"); // セクション(前)に使用
			ret = ret.Replace("]", "\\u005d"); // セクション(後)に使用
			return ret;
		}


		/*!
		 * Unicodeコード文字列から変換
		 * @param[in] text 文字列
		 * @return string
		 */
		static public string FromUnicode(string text) {
			string ret = text;
			ret = ret.Replace("\\u000d", _CR);
			ret = ret.Replace("\\u000a", _LF);
			ret = ret.Replace("\\u0023", "#");
			ret = ret.Replace("\\u003d", "=");
			ret = ret.Replace("\\u007c", "|");
			ret = ret.Replace("\\u005b", "[");
			ret = ret.Replace("\\u005d", "]");
			return ret;
		}


		/*!
		 * エクセルで使用するデータ形式に変換する
		 * @param[in] text 文字列
		 * @return string
		 */
		static public string ToXLS(string text) {
			string ret = text;

			if (ret == null) {
				return "";
			}

			// 改行がある場合は両端を " でくくる
			if (ret.IndexOf( _LF ) >= 0) {
				ret = ret.Insert(0, "\"");
				ret = ret + "\"";
			}

			// 中改行は LF にする
			ret = ret.Replace( _CRLF, _LF );

			return ret;
		}


		/*!
		 * テキストのハイライト
		 * @param[in] sender RichTextBoxオブジェクト
		 * @param[in] s ハイライトする開始文字列
		 * @param[in] e ハイライト終了の文字列
		 * @param[in] foreColor 前景色
		 * @param[in] backColor 背景色
		 * @param[in] includeToken s, eもハイライトの対称にするか
		 */
		static public void TextHighlight(object sender, string s, string e, Color foreColor, Color backColor, bool includeToken) {
			RichTextBox rt = (RichTextBox)sender;

			int pos1 = 0;		// 括りはじめの見つかった位置
			int pos2 = 0;		// 括りおわりの見つかった位置

			for (;;) {
				// 括りはじめ群から探す
				pos1 = rt.Text.IndexOf( s, pos1 );

				if (pos1 >= 0) {
					if (e != null) {
						// 括りおわり群から探す
						pos2 = rt.Text.IndexOf( e, pos1 + s.Length );

						if (pos2 >= 0) {
							int startPos;
							int len;

							// 見つかった位置を選択開始の位置にする
							if (includeToken) {
								startPos = pos1;
							} else {
								startPos = pos1 + s.Length;
							}

							// 探した文字列の長さを選択する文字列の長さにする
							if (includeToken) {
								len = pos2 - startPos + e.Length;
							} else {
								len = pos2 - startPos;
							}

							if (len > 0) {
								int npos = rt.Text.IndexOf( "\n", startPos, len );

								// 間に改行があるときはノーカウント
								if (npos >= 0 && e != "\n") {
									pos1 = npos + 1;
								} else {
									rt.SelectionStart = startPos;
									rt.SelectionLength = len;
									rt.SelectionBackColor = backColor;
									rt.SelectionColor = foreColor;
									pos1 += len;
								}
							} else {
								pos1 ++;
							}
						} else {
							break;
						}
					} else {
						int startPos;
						int len;

						// 見つかった位置を選択開始の位置にする
						if (includeToken) {
							startPos = pos1;
						}
						else {
							startPos = pos1 + s.Length;
						}

						len = s.Length;

						// 探した文字列の長さを選択する文字列の長さにする
						rt.SelectionStart = startPos;
						rt.SelectionLength = len;
						rt.SelectionBackColor = backColor;
						rt.SelectionColor = foreColor;

						pos1 += len;
					}
				} else {
					break;
				}
			}
		}


		/*!
		 * 条件演算子の取得
		 * @param[in] exp 式
		 * @return string
		 */
		static public string GetOperator(string exp) {
			string op = "";
			string temp = exp.ToUpper();
			temp = temp.Replace(" ", "");

			if (temp.IndexOf(">=") >= 0) {		// 以上
				op = ">=";
			} else if (temp.IndexOf("<=") >= 0) {	// 以下
				op = "<=";
			} else if (temp.IndexOf(">") >= 0) {	// よりも大きい
				op = ">";
			} else if (temp.IndexOf("<") >= 0) {	// 未満
				op = "<";
			} else if (temp.IndexOf("==") >= 0) {	// 同じ
				op = "==";
			} else if (temp.IndexOf("<>") >= 0) {	// 違う
				op = "<>";
			}

			return op;
		}


		/*!
		 * 文字列の削除
		 * @param[in,out] s 文字列バッファ
		 * @param[in] a 検索文字列1
		 * @param[in] b 検索文字列2
		 * @param[in] bdel bを削除するかどうか
		 */
		static public void RemoveString(ref StringBuilder s, string a, string b, bool bdel) {
			for (;;) {
				int pos1 = s.ToString().IndexOf( a );

				if (pos1 < 0) {
					break;
				}

				int pos2 = s.ToString().IndexOf( b, pos1 + 1 );

				if (pos2 < 0) {
					break;
				}
				
				if (bdel) {
					s.Remove( pos1, pos2 - pos1 + b.Length );
				} else {
					s.Remove( pos1, pos2 - pos1 );
				}
			}
		}


		static public string GetXMLElement(string xml, string tag) {
			int pos = 0;

			for (;;) {	//1
				int startPos1 = xml.IndexOf( "<", pos );

				if (startPos1 >= 0) {
					int startPos2 = xml.IndexOf( ">", startPos1 + 1 );

					if (startPos2 >= 0) {
						// < ～ > までの位置が判明
						pos = startPos2 + 1;

						if (xml.IndexOf( tag, startPos1 + 1, startPos2 - (startPos1 + 1)) >= 0) {
							// 開始タグが見つかった


							for (;;) {	//2
								int endPos1 = xml.IndexOf( "</", pos );

								if (endPos1 >= 0) {
									int endPos2 = xml.IndexOf( ">", endPos1 + 2 );

									if (endPos2 >= 0) {
										// </ ～ > までの位置が判明
										pos = endPos2 + 2;

										if (xml.IndexOf( tag, endPos1 + 2, endPos2 - (endPos1 + 2)) >= 0) {
											// 閉じタグも見つかった


											return xml.Substring( startPos2 + 1, endPos1 - (startPos2 + 1) );
										}
									} else {
										break;	//2
									}
								} else {
									break;	//2
								}
							}
						}
					} else {
						break;	//1
					}
				} else {
					break;	//1
				}
			}

			return null;
		}


		public class RadioCheckItem : MenuItem
		{
			public MenuItem[] GroupMenuItems
			{
				get;
				set;
			}
			public string Tag = string.Empty;
			public RadioCheckItem(string strgText) : base(strgText) 
			{ 
				this.RadioCheck = true;
				this.Click += ehOnClick;
			}
			protected void ehOnClick(object sender, EventArgs e)
			{
				if (this.GroupMenuItems != null) {
					foreach (MenuItem mi in this.GroupMenuItems) {
						if (sender.Equals(mi)) {
							mi.Checked = true;
						} else {
							mi.Checked = false;
						}
					}
				}
			}
		}
	}
}
