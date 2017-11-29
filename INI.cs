
using System;
using System.Collections;

namespace nk
{
	/// <summary>
	/// INIファイルの読み込み
	/// </summary>
	public class IniReader
	{
		public const string _LF = "\n";
		public const string _CRLF = "\r\n";


		protected string    _Sepa = "`"; // セクション、キーのセパレータ (使わないような文字にすること)

		protected string    _FileName = "";   //!< ファイル名
		protected string    _Encoding = "";   //!< エンコード
		protected string    _ReturnCode = ""; //!< 改行コード
		protected Hashtable _Data = new Hashtable(); //!< セクション、キーをキーにしたハッシュテーブル
		protected string    _Header = "";


		/*!
		 * 値を設定
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @param[in] value 値
		 * @return Non
		 */
		protected void setValue(string sectionName, string keyName, string value) {
			// ハッシュテーブルに登録
			//  ハッシュテーブルのキー名は大文字小文字を区別しないようにする
			//  値に大文字小文字を区別した状態のタグを付加する
			string tag = sectionName + _Sepa + keyName;
			_Data[tag.ToUpper()] = tag + "]" + value;
		}


		/*!
		 * 値を取得
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @return string
		 */
		protected string getValue(string sectionName, string keyName) {
			string tag = sectionName + _Sepa + keyName;
			object data = _Data[ tag.ToUpper() ];

			if (data == null) {
				return null;
			} else {
				int sepaPos = data.ToString().IndexOf("]");
				
				//string key = data.ToString().Substring( 0, sepaPos ); // キー名(セクション名付き)
				string value = data.ToString().Substring( sepaPos + 1 ); // 値

				return value;
			}
		}


		/*!
		 * オープン
		 * @param[in] fileName ファイル名
		 * @param[in] encoding エンコード形式
		 * @param[in] returnCode 改行形式
		 * @param[in] new_ 新規に作成するかどうかのフラグ
		 * @return Non
		 */
		public void Open(string fileName, string encoding, string returnCode = _CRLF, bool new_ = false) {
			_FileName = fileName;
			_Encoding = encoding;
			_ReturnCode = returnCode;
			_Data.Clear();


			// ファイルを新規作成、または開く
			if (!new_ && System.IO.File.Exists( fileName )) {
				System.IO.StreamReader sr = new System.IO.StreamReader( fileName, System.Text.Encoding.GetEncoding( encoding ) );


				string sectionName = "";

				for (;;) {
					string line = sr.ReadLine();
					
					if (line == null) {
						break;
					}


					// コメント処理
					int commentPos = line.IndexOf("#");
					
					if (commentPos >= 0) {
						line = line.Substring( 0, commentPos );
					}


					// 余計な文字を取り除く
					line = line.TrimStart( new char[] {' ', '\t'} );
					line = line.TrimEnd( new char[] {'\r', '\n'} );
					


					
					if (line.Length == 0) { // 空行
					} else if (line.IndexOf("[") == 0) { // セクション
						int endPos = line.LastIndexOf("]"); // セクションは最初に見つかった [ から 最初に見つかった ] までとする

						if (endPos > 0) {
							sectionName = line.Substring( 1, endPos - 1 );
						}
					} else { // キー
						int sepaPos = line.IndexOf("="); // キーは最初に見つかった = までとする

						if (sepaPos >= 0) {
							string key = line.Substring( 0, sepaPos );		// キー
							string value = line.Substring( sepaPos + 1 );	// 値

							// 余計な文字を取り除く
							key = key.Trim( new char[] {' ', '\t'} );
							value = value.Trim( new char[] {' ', '\t'} );

							// ハッシュテーブルに登録
							//  ハッシュテーブルのキー名は大文字小文字を区別しないようにする
							//  値に大文字小文字を区別した状態のタグを付加する
							setValue( sectionName, key, value );
						}
					}
				}

				sr.Close();
			}
		}


		/*!
		 * セクションの列挙
		 */
		public string[] EnumSection() {
			string[] sections = new string[ _Data.Count ];

			int i = 0;
			foreach (string htkey in _Data.Keys) {
				int endPos = htkey.LastIndexOf( _Sepa );
				sections[i] = htkey.Substring( 0, endPos );
				i ++;
			}

			return sections;
		}


		/*!
		 * セクションの削除
		 * @param[in] sectionName セクション名
		 */
		public void DeleteContainSection(string sectionName) {
			foreach (string htkey in _Data.Keys) {
				int endPos = htkey.LastIndexOf( _Sepa );
				string key = htkey.Substring( 0, endPos );

				if (key.IndexOf( sectionName ) >= 0) {
					_Data.Remove( htkey );
				}
			}
		}


		/*!
		 * クリア
		 */
		public void Clear() {
			_Data.Clear();
		}


		/*!
		 * 文字列の読み込み
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @param[in] def デフォルト値
		 * @return string
		 */
		public string ReadString(string sectionName, string keyName, string def) {
			string value = getValue( sectionName, keyName );

			if (value != null) {
				return nk.Utils.FromUnicode( value );
			} else {
				return def;
			}
		}


		/*!
		 * 文字列の読み込み
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @param[in] def デフォルト値
		 * @return string[]
		 */
		public string[] ReadStringAny(string sectionName, string keyName, string[] def) {
			string value = getValue( sectionName, keyName );

			if (value != null) {
				return nk.Utils.FromUnicode( value ).Split('|');
			} else {
				return def;
			}
		}


		/*!
		 * int型の読み込み
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @param[in] def デフォルト値
		 * @return int
		 */
		public int ReadInteger(string sectionName, string keyName, int def) {
			string value = getValue( sectionName, keyName );

			if (value != null) {
				return Convert.ToInt32( value );
			} else {
				return def;
			}
		}


		/*!
		 * double型の読み込み
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @param[in] def デフォルト値
		 * @return double
		 */
		public double ReadDouble(string sectionName, string keyName, double def) {
			string value = getValue( sectionName, keyName );

			if (value != null) {
				return Convert.ToDouble( value );
			} else {
				return def;
			}
		}


		/*!
		 * bool型の読み込み
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @param[in] def デフォルト値
		 * @return bool
		 */
		public bool ReadBool(string sectionName, string keyName, bool def) {
			string value = getValue( sectionName, keyName );

			if (value != null) {
				return (value.ToUpper() == "TRUE");
			} else {
				return def;
			}
		}

		public void Close() {
		}
	}


	/*!
	 * INIファイルの書き込み
	 * (IniReader を継承して IniWriter を作るのはよくない)
	 */
	public class IniWriter : IniReader
	{
		string[] ConvAny1 = null;
		string[] ConvAny2 = null;
		int      ConvAnyLength = 0;

		string   LineFeedCode = null;



		/*!
		 * ヘッダの設定
		 * @param[in] header ファイルの先頭にコメントとして残す文字列
		 * @return Non
		 */
		public void SetHeader(string header) {
			_Header = header;
		}


		/*!
		 * 文字配列１から文字配列２の文字へ変換
		 * @param[in] s
		 * @return string
		 */
		private string convAny(string s) {
			if (ConvAny1 == null || ConvAny2 == null || ConvAnyLength == 0) {
				return s;
			}

			for (int i = 0; i < ConvAnyLength; i++) {
				s = s.Replace( ConvAny1[i], ConvAny2[i] );
			}

			return s;
		}


		/*!
		 * 変換配列の設定
		 * @param[in] any1 変換元文字列群
		 * @param[in] any2 変換先文字列群
		 * @param[in] length 文字の数
		 */
		public void SetConvArray(string[] any1, string[] any2, int length) {
			ConvAny1 = any1;
			ConvAny2 = any2;
			ConvAnyLength = length;
		}


		/*!
		 * 改行コードの変換
		 * @param[in] s
		 * @return string
		 */
		private string convLineFeedCode(string s) {
			// 改行コードの変換
			if (LineFeedCode != null) {
				s = s.Replace("\r\n", "\n");		// CRLF を LF にする
				s = s.Replace("\n", LineFeedCode);	// LF を LineFeedCode にする
			}

			return s;
		}


		/*!
		 * 改行コードを設定する (null でそのまま)
		 * @param[in] code
		 */
		public void SetLineFeedCode(string code) {
			LineFeedCode = code;
		}

			
		/*!
		 * 文字列の書き込み
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @param[in] data データ
		 * @return Non
		 */
		public void WriteString(string sectionName, string keyName, string data) {
			setValue(sectionName, keyName, nk.Utils.ToUnicode( convAny( convLineFeedCode( data))));
		}


		/*!
		 * 文字列の書き込み
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @param[in] data データ
		 * @return Non
		 */
		public void WriteStringAny(string sectionName, string keyName, string[] data) {
			string s = nk.Utils.ToUnicode( convAny( convLineFeedCode( data[0] ) ) );
			
			for (int i = 1; i < data.Length; i++) {
				if (data[i] == null) {
					s += "|";
				} else {
					s += "|" + nk.Utils.ToUnicode( convAny( convLineFeedCode( data[i] ) ) );
				}
			}

			setValue( sectionName, keyName, s );
		}

			
		/*!
		 * int型の書き込み
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @param[in] data データ
		 * @return Non
		 */
		public void WriteInteger(string sectionName, string keyName, int data) {
			setValue( sectionName, keyName, data.ToString() );
		}

			
		/*!
		 * double型の書き込み
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @param[in] data データ
		 * @return Non
		 */
		public void WriteDouble(string sectionName, string keyName, double data) {
			setValue( sectionName, keyName, data.ToString() );
		}

			
		/*!
		 * bool型の書き込み
		 * @param[in] sectionName セクション名
		 * @param[in] keyName キー名
		 * @param[in] data データ
		 * @return Non
		 */
		public void WriteBool(string sectionName, string keyName, bool data) {
			setValue( sectionName, keyName, data.ToString() );
		}

			
		/*!
		 * 閉じる(保存)
		 */
		public void Close() {
			string[] any = new string[ _Data.Count ];

			// キー項目の列挙
			{
				int i = 0;
				foreach (string htkey in _Data.Keys) {
					string data = _Data[ htkey ].ToString();
					int sepaPos = data.IndexOf("]");
						
					string key = data.Substring( 0, sepaPos );		// キー名(セクション名付き)
					string value = data.Substring( sepaPos + 1 );	// 値

					any[i] = key + "=" + value;

					i ++;
				}
			}

			// ソート
			Array.Sort( any );

			{
				string sectionName = "";

				System.IO.StreamWriter sw = new System.IO.StreamWriter( _FileName, false, System.Text.Encoding.GetEncoding( _Encoding ) );

				sw.Write( _Header );
				sw.Write( _ReturnCode );

				for (int i = 0; i < any.Length; i++) {
					int sectionPos = any[i].LastIndexOf( _Sepa );
					
					if (sectionPos < 0) {
						continue;
					}

					string sectionName2 = any[i].Substring( 0, sectionPos );

					if (sectionName != sectionName2) {
						sw.Write( _ReturnCode );
						sw.Write( "[" + sectionName2 + "]" + _ReturnCode );
						sectionName = sectionName2;
					}

					sw.Write( any[i].Substring( sectionPos + 1 ) + _ReturnCode );
				}

				sw.Close();
			}
		}
	}
}
