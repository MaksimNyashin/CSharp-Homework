using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace NSWriter
{
	public static class Writer
	{
		private static IWriter CurrentWriter = new ConsoleWriter();

		public static void SetWriter(IWriter newWriter)
		{
			CurrentWriter = newWriter;
		}

		public static void WriteLine(object o=null)
		{
			if (o == null)
			{
				o = "";
			}
			CurrentWriter.WriteLine(o);
		}
		public static void Write(object o)
		{
			CurrentWriter.Write(o);
		}
		public static void WriteError(string s, params object[] o)
		{
			CurrentWriter.WriteError(s, o);
		}
		public static void WriteLine(string s, params object[] o)
		{
			CurrentWriter.WriteLine(s, o);
		}
		public static void Write(string s, params object[] o)
		{
			CurrentWriter.Write(s, o);
		}

		public static void WriteList(IEnumerable list)
		{
			bool b = false;
			foreach (object value in list)
			{
				if (b)
                {
                    Write(" ");
                }
                b = true;
                Write(value);
			}
			if (!b)
			{
				WriteError("Список пустой");
				return;
			}
			WriteLine();
		}

		public static void WriteList<T>(IEnumerable<T> list)
        {
            bool b = false;
            foreach (T value in list) 
            {
                if (b)
                {
                    Write(" ");
                }
                b = true;
                Write(value);
            }
            if (!b)
            {
            	WriteError("Список пустой");
				return;
            }
            WriteLine();
        }

        public static void WriteInfo(object o)
        {
        	#if INFO
        		Write(o);
        	#endif
        }

        public static void WriteInfo(string s, params object[] o)
        {
        	#if INFO
        		Write(s, o);
        	#endif
        }
        public static void WriteLineInfo(object o)
        {
        	#if INFO
        		WriteLine(o);
        	#endif
        }

        public static void WriteLineInfo(string s, params object[] o)
        {
        	#if INFO
        		WriteLine(s, o);
        	#endif
        }
	}

	public interface IWriter
	{
		void WriteLine(object o);
		void Write(object o);
		void WriteError(string s, params object[] o);
		void WriteLine(string s, params object[] o);
		void Write(string s, params object[] o);
	}

	public class ConsoleWriter: IWriter
	{
		public ConsoleWriter() {}

		public void WriteError(string s, params object[] o)
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			WriteLine(s, o);
			Console.ForegroundColor = ConsoleColor.White;
		}
		public void WriteLine(object o)
		{
			Console.WriteLine(o);
		}
		public void Write(object o)
		{
			Console.Write(o);
		}
		public void WriteLine(string s, params object[] o)
		{
			Console.WriteLine(s, o);
		}
		public void Write(string s, params object[] o)
		{
			Console.Write(s, o);
		}
	}

	public class LocalWriter: IWriter
	{
		private StringBuilder OutputBuilder = new StringBuilder();

		public LocalWriter() {}
		
		public void WriteError(string s, params object[] o)
		{
			OutputBuilder.Append("Error: ");
			WriteLine(s, o);
		}
		public void WriteLine(object o)
		{
			Write(o);
			OutputBuilder.Append("\n");
		}

		public void Write(object o)
		{
			OutputBuilder.Append(o.ToString());
		}

		public void WriteLine(string s, params object[] o)
		{
			Write(s, o);
			OutputBuilder.Append("\n");
		}
		public void Write(string s, params object[] o)
		{
			OutputBuilder.Append(String.Format(s, o));
		}

		public string GetOutput()
		{
			bool needAppendEndline = false;
			if (OutputBuilder.Length > 0 && OutputBuilder[OutputBuilder.Length - 1] == '\n')
			{
				OutputBuilder.Length--;
				needAppendEndline = true;
			}
			string result = OutputBuilder.ToString();
			if (needAppendEndline)
			{
				OutputBuilder.Length++;	
			}
			return result;
		}
	}
}