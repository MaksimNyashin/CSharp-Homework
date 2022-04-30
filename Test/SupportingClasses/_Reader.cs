using System;
using System.Globalization;
using System.Collections.Generic;
using NSTester;
using NSWriter;

namespace NSReader
{
	public static class Reader
	{

		private static Stack<ReaderUnit> Units = new Stack<ReaderUnit>();

		static Reader()
		{
			Units.Push(new ReaderUnit(new ConsoleTester()));
		}

		private static void init(string output)
		{
			Writer.WriteInfo(output);
			while (GetUnit().GetID() >= GetUnit().Length())
			{
				GetUnit().SetTokens(GetUnit().GetTester().GetNext().Replace("\r", "").Split());
				GetUnit().ZeroID();
			}
		}

		private static string GetNextToken(string output)
		{
			init(output);
			return GetUnit().GetToken();
		}

		public static int ReadInt(string output="")
		{
			bool parsed = false;
			int result = 0;
			while (!parsed)
			{
				String token = GetNextToken(output);
				parsed = int.TryParse(token, out result);
				if (!parsed)
				{
					Writer.WriteError("Введено \"{0}\" вместо переменной типа int", token);
				}
			}
			return result;
		}

		public static long ReadLong(string output="")
		{
			bool parsed = false;
			long result = 0;
			while (!parsed)
			{
				String token = GetNextToken(output);
				parsed = long.TryParse(token, out result);
				if (!parsed)
				{
					Writer.WriteError("Введено \"{0}\" вместо переменной типа long", token);
				}
			}
			return result;
		}

		public static float ReadFloat(string output="")
		{
			bool parsed = false;
			float result = 0;
			while (!parsed)
			{
				String token = GetNextToken(output);
				parsed = float.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
				if (!parsed)
				{
					Writer.WriteError("Введено \"{0}\" вместо переменной типа float", token);
				}
			}
			return result;
		}

		public static double ReadDouble(string output="")
		{
			bool parsed = false;
			double result = 0;
			while (!parsed)
			{
				String token = GetNextToken(output);
				parsed = double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
				if (!parsed)
				{
					Writer.WriteError("Введено \"{0}\" вместо переменной типа double", token);
				}
			}
			return result;
		}

		public static string ReadString(string output="")
		{
			return GetNextToken(output);
		}

		public static string ReadLine(string output="")
		{
			init(output);
			return GetUnit().JoinTillEnd();
		}

		public static void AddTester(ITester tester)
		{
			Units.Push(new ReaderUnit(tester));
		}
		public static void PopTester()
		{
			if (Units.Count > 1)
			{
				Units.Pop().GetTester().Close();
			} else {
				Writer.WriteError("Запрещено извлекать Тестер из почти пустого стека");
			}
		}

		private static ReaderUnit GetUnit()
		{
			return Units.Peek();
		}

		private class ReaderUnit
		{
			private int ID;
			private ITester Tester;
			private string[] Tokens;

			public ReaderUnit(ITester tester) {
				ID = 0;
				Tester = tester;
				Tokens = new string[0];
			}

			public int GetID() 
			{
				return ID;
			}
			public void IncID()
			{
				ID++;
			}
			public void ZeroID()
			{
				ID = 0;
			}
			public ITester GetTester()
			{
				return Tester;
			}
			public string GetToken()
			{
				return Tokens[ID++];
			}
			public int Length()
			{
				return Tokens.Length;
			}
			public void SetTokens(String[] val)
			{
				Tokens = val;
			}
			public string JoinTillEnd()
			{
				string result = string.Join(" ", Tokens, ID, Tokens.Length - ID);
				ID = Tokens.Length;
				return result;
			}
		}
	}
}