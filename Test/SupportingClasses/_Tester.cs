using System;
using NSReader;
using NSWriter;

namespace NSTester 
{
	public interface ITester
	{
		string GetNext();
		void Close();
	}

	public class StringTester : ITester
	{
		private string[] Lines;
		private int ID;

		public StringTester(String[] lines)
		{
			Lines = lines;
			ID = 0;
		}

		public StringTester(String all_Lines)
		{
			Lines = all_Lines.Split('\n') ;
			ID = 0;
		}

		public string GetNext()
		{
			return Lines[ID++];
		}

		public void Close() {}
	}

	public class ConsoleTester: ITester
	{
		public ConsoleTester() {}

		public string GetNext()
		{
			return Console.ReadLine();
		}

		public void Close() {}
	}

	public class ShortFileTester: ITester
	{
		private string[] Lines;
		private int ID;
		public ShortFileTester(string file)
		{
			Lines = System.IO.File.ReadAllText(file).Split('\n');
			ID = 0;
		}

		public string GetNext()
		{
			return Lines[ID++];
		}

		public void Close() {}
	}

	public class Test
	{
		private int CurrentID;
		private ITester CurrentTester;
		private Func<string[], int> CurrentSolution;
		private string[] CurrentArgs;
		private string ExpectedResult;
		private string Name;

		public Test(int id, Func<string[], int> solution, string testString, String expectedResult, string[] args=null, string name="")
		{
			init(id, solution, new StringTester(testString), expectedResult, args, name);
		}

		public Test(int id, Func<string[], int> solution, string[] testStrings, string expectedResult, string[] args = null, string name="")
		{
			init(id, solution, new StringTester(testStrings), expectedResult, args, name);
		}

		private void init(int id, Func<string[], int> solution, ITester testTester, string expectedResult, string[] args, string name)
		{
			CurrentID = id;
			CurrentTester = testTester;
			CurrentSolution = solution;
			CurrentArgs = args;
			ExpectedResult = expectedResult;
			Name = ". " + name;
		}

		public void run()
		{
			LocalWriter writer = new LocalWriter();
			try
			{
				Writer.SetWriter(writer);
				Reader.AddTester(CurrentTester);
				int result = CurrentSolution(CurrentArgs);
				string dot = Name == null ? "" : ".";
				if (result == 0)
				{
					String output = writer.GetOutput();
					bool correct = output.Equals(ExpectedResult);
					if (correct)
					{
						Console.ForegroundColor = ConsoleColor.DarkGreen;
						Console.WriteLine("{0}{1}:\tOK", CurrentID, Name);
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.DarkRed;
						Console.WriteLine("{0}{1}:\tNO", CurrentID, Name);
						Console.WriteLine("\tOutput:");
						string[] erResSplit = ExpectedResult.Split('\n');
						int ind = 0;
						foreach (string val in output.Split('\n'))
						{
							bool right = false;
							if (ind< erResSplit.Length && erResSplit[ind].Equals(val))
							{
								right = true;
							}
							if (!right) 
							{
								Console.ForegroundColor = ConsoleColor.Red;
							}
							Console.WriteLine("\"{0}\"", val);
							if (!right)
							{
								Console.ForegroundColor = ConsoleColor.DarkRed;
							}
							ind++;
						}
						Console.WriteLine("\tExpected:");
						foreach (string val in erResSplit)
						{
							Console.WriteLine("\"{0}\"", val);
						}
					}
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("{0}{1}:\tNO", CurrentID, Name);
					Console.WriteLine("\tNon-zero exit code: {0}", result);
				}
				
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("{0}{1}:\tNO", CurrentID, Name);
				Console.WriteLine(writer.GetOutput());
				Console.WriteLine("{0}", e);
			}
			Console.ForegroundColor = ConsoleColor.White;
			Reader.PopTester();
		}

		public void execute(bool fromConsole)
		{
			Writer.SetWriter(new ConsoleWriter());
			if (fromConsole)
			{
				Reader.AddTester(new ConsoleTester());
			}
			else
			{
				Reader.AddTester(CurrentTester);
			}
			int result = CurrentSolution(CurrentArgs);
			if (result != 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\tNon-zero exit code: {0}", result);
			}
			Console.ForegroundColor = ConsoleColor.White;
			Reader.PopTester();
		}
		
		public static void RunTests(Func<string[], int> function, string[] tests)
	    {
	        for (int i = 0; i< tests.Length; i += 2)
	        {
	            new Test(i / 2 + 1, function, tests[i], tests[i+ 1]).run();
	        }
	        Console.WriteLine();
	    }

	    public static void RunSmallTests(TestUnit[] tests)
	    {
	    	for (int i = 0; i< tests.Length; i++)
	    	{
	    		new Test(i + 1, tests[i].GetFunction(), tests[i].GetInput(), tests[i].GetOutput(), 
	    			tests[i].GetArgs(), tests[i].GetName()).run();
	    	}
	    }

	    public static void ExecuteSmallTests(TestUnit[] tests, bool fromConsole=false)
	    {
	    	for (int i = 0; i < tests.Length; i++)
	    	{
	    		new Test(i + 1, tests[i].GetFunction(), tests[i].GetInput(), tests[i].GetOutput(), 
	    			tests[i].GetArgs(), tests[i].GetName()).execute(fromConsole);
	    	}
	    }
	}

	public class TestUnit
	{
		private Func<string[], int> Function;
		private string Input;
		private string Output;
		private string Name;
		private string[] Args;

		public TestUnit(Func<string[], int> function, string input, string output, string name = "", string[] args=null)
		{
			Function = function;
			Input = input;
			Output = output;
			Name = name;
			Args = args;
		}

		public Func<string[], int> GetFunction()
		{
			return Function;
		}

		public string GetInput()
		{
			return Input;
		}

		public string GetOutput()
		{
			return Output;
		}

		public string GetName()
		{
			return Name;
		}

		public string[] GetArgs()
		{
			return Args;
		}
	}
}