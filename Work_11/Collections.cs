using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using NSReader;
using NSWriter;
using NSWorkers;

namespace NSCollections
{

	public class PersonArrayListInteractor
	{
		private const string MainMenu = "1) Добавить элемент\n2) Удалить элемент\n3) Вывести\n4) Вывести коротко\n5) Отсортиовать\n6) Найти заданный элемент\n7) Обработать запрос\n0) Выход\n> ";
		private static IComparer myComp = new MyClass();
		public static void MainProgramm()
		{
			ArrayList Al = new ArrayList();
			bool running = true;
			while (running)
			{
				int inp = Reader.ReadInt(MainMenu);
				switch (inp)
				{
					case 1:
						bool run = true;
						while (run)
						{
							run = false;
							int inp1 = Reader.ReadInt("Выберите класс:\n1) Person\n2) Servant\n3) Worker\n4) Engineer\n>>");
							switch(inp1)
							{
								case 1:
									Al.Add(Person.CreateAndInput());
									break;
								case 2:
									Al.Add(Servant.CreateAndInput());
									break;
								case 3:
									Al.Add(Worker.CreateAndInput());
									break;
								case 4:
									Al.Add(Engineer.CreateAndInput());
									break;
								default:
									run = true;
									break;
							}
						}
						break;
					case 2:
						if (Al.Count == 0)
						{
							Writer.WriteError("Нечего удалять");
						}
						else {
							int inp2 = Reader.ReadInt("Выберите индекс удаляемого элемента\n>> ");
							while (inp2 <= -Al.Count || inp2 >= Al.Count)
							{
								Writer.WriteError("Некорректный индекс");
								inp2 = Reader.ReadInt(">>");
							}
							if (inp2 < 0)
							{
								inp2 += Al.Count;
							}
							Al.RemoveAt(inp2);
						}
						break;
					case 3:
						if (Al.Count == 0) {
							Writer.WriteError("Список пустой");
							break;
						}
						foreach (Person per in Al)
						{
							per.Show();
						}
						break;
					case 4:
						Writer.WriteList(Al);
						break;
					case 5:
						if (!isSorted(Al))
						{
							Al.Sort(myComp);
						}
						else
						{
							Writer.WriteError("Сортировка не нужна");
						}
						break;
					case 6:
						if (isSorted(Al))
						{
							string inpName = Reader.ReadLine("Введите имя для поиска\n>> ");
							int resultNameId = Al.BinarySearch(new Person(inpName, Gender.Male, null, new DateTime()), myComp);
							if (resultNameId > 0)
							{
								(Al[resultNameId] as Person).Show();
							}
							else
							{
								Writer.WriteError("Не найден человек с таким именем");
							}
						}
						else
						{
							Writer.WriteError("Нужна сортировка");
						}
						break;
					case 7:
						int task = Reader.ReadInt("1) Имена мужчин\n2) Имена служащих со стажем не менее заданного \n3) Количество инженеров в заданном подразделении\n>> ");
						switch(task)
						{
							case 1:
								foreach (Person per in Al)
								{
									if (per.GetGender == Gender.Male) {
										Writer.WriteLine(per);
									}
								}
								break;
							case 2:
								int experience = Reader.ReadInt("Минимальный стаж: ");
								foreach (Person per in Al)
								{
									if (per is Servant && per.GetExperienceYears() >= experience)
									{
										Writer.WriteLine(per);
									}
								}
								break;
							case 3:
								string enterprise = Reader.ReadLine("Подразделение: ");
								int cnt = 0;
								foreach (Person per in Al)
								{
									if (per is Engineer && (per as Engineer).Enterprise.CompareTo(enterprise) == 0)
									{
										cnt++;
									}
								}
								Writer.WriteLine(cnt);
								break;
							default:
								Writer.WriteError("Такой операции нет");
								break;
						}
						break;
					case 0:
						running = false;
						break;
					default:
						Writer.WriteError("Такой команды нет");
						break;
				}
			}
		}

		private static bool isSorted(ArrayList Al)
		{
			bool needSort = false;
			Person prev = null;
			foreach (Person per in Al)
			{
				if (per == null)
				{
					needSort = true;
					break;
				}
				if (prev != null)
				{
					if (prev.Name.CompareTo(per.Name) > 0)
					{
						needSort = true;
						break;
					}
				}
				prev = per;
			}
			return !needSort;
		}

		private class MyClass: IComparer
		{
			int IComparer.Compare(Object a, Object b)
			{
				return (a as Person).Name.CompareTo((b as Person).Name);
			}
		}
	}

	public class PersonStackInteractor
	{
		private const string MainMenu = "1) Добавить элемент\n2) Удалить элемент\n3) Вывести\n4) Вывести коротко\n5) Найти заданный элемент\n6) Обработать запрос\n0) Выход\n> ";
		public static void MainProgramm()
		{
			Stack<Person> St = new Stack<Person>();
			bool running = true;
			while (running)
			{
				int inp = Reader.ReadInt(MainMenu);
				switch (inp)
				{
					case 1:
						bool run = true;
						while (run)
						{
							run = false;
							int inp1 = Reader.ReadInt("Выберите класс:\n1) Person\n2) Servant\n3) Worker\n4) Engineer\n>>");
							switch(inp1)
							{
								case 1:
									St.Push(Person.CreateAndInput());
									break;
								case 2:
									St.Push(Servant.CreateAndInput());
									break;
								case 3:
									St.Push(Worker.CreateAndInput());
									break;
								case 4:
									St.Push(Engineer.CreateAndInput());
									break;
								default:
									run = true;
									break;
							}
						}
						break;
					case 2:
						if (St.Count == 0)
						{
							Writer.WriteError("Нечего удалять");
						}
						else 
						{
							St.Pop();
						}
						break;
					case 3:
						if (St.Count == 0) {
							Writer.WriteError("Список пустой");
							break;
						}
						foreach (Person per in St)
						{
							per.Show();
						}
						break;
					case 4:
						Writer.WriteList(St);
						break;
					case 5:
						string inpName = Reader.ReadLine("Введите имя для поиска\n>> ");
						bool found = false;
						foreach (Person per in St)
						{
							if (per.Name.CompareTo(inpName) == 0)
							{
								per.Show();
								found = true;
							}
						}
						if (!found)
						{
							Writer.WriteError("Не найден человек с таким именем");
						}
						break;
					case 6:
						int task = Reader.ReadInt("1) Имена мужчин\n2) Имена служащих со стажем не менее заданного \n3) Количество инженеров в заданном подразделении\n>> ");
						switch(task)
						{
							case 1:
								foreach (Person per in St)
								{
									if (per.GetGender == Gender.Male) {
										Writer.WriteLine(per);
									}
								}
								break;
							case 2:
								int experience = Reader.ReadInt("Минимальный стаж: ");
								foreach (Person per in St)
								{
									if (per is Servant && per.GetExperienceYears() >= experience)
									{
										Writer.WriteLine(per);
									}
								}
								break;
							case 3:
								string enterprise = Reader.ReadLine("Подразделение: ");
								int cnt = 0;
								foreach (Person per in St)
								{
									if (per is Engineer && (per as Engineer).Enterprise.CompareTo(enterprise) == 0)
									{
										cnt++;
									}
								}
								Writer.WriteLine(cnt);
								break;
							default:
								Writer.WriteError("Такой операции нет");
								break;
						}
						break;	
					case 0:
						running = false;
						break;
					default:
						Writer.WriteError("Такой команды нет");
						break;
				}
			}
		}
	}

	public class TestCollection
	{
		LinkedList<Person> LLKey = new LinkedList<Person>();
		LinkedList<string> LLStr = new LinkedList<string>();
		SortedDictionary<Person, Person> SDKey = new SortedDictionary<Person, Person>();
		SortedDictionary<string, Person> SDStr = new SortedDictionary<string, Person>();
		#if TEST2
			Random rand = new Random();
		#else
			Random rand = new Random(14313);
		#endif
		public TestCollection(int num)
		{
			Person[] ps = new Person[4];
			for (int i = 0; i < num; i++)
			{
				Person p = CreatePerson();
				if (i == 0)
				{
					ps[0] = p.Copy() as Person;
				}
				if (i == num / 2)
				{
					ps[1] = p.Copy() as Person;
				}
				if (i == num - 1)
				{
					ps[2] = p.Copy() as Person;
				}
				Person basedP = p.BasePerson;
				string basedStr = basedP.ToString();
				LLKey.AddLast(basedP);
				LLStr.AddLast(basedStr);
				SDKey.Add(basedP, p);
				SDStr.Add(basedStr, p);
			}
			ps[3] = CreatePerson();


			Stopwatch sw = new Stopwatch();
			for (int i = 0; i < 4; i++)
			{
				bool exRes = (i != 3);
				bool res = true;
				Person basedP = ps[i].BasePerson;
				string basedStr = basedP.ToString();
				
				sw = Stopwatch.StartNew();
				res = LLKey.Contains(basedP);
				sw.Stop();
				Writer.Write("{0}\t", sw.ElapsedTicks);
				/*if (res != exRes)
				{
					Writer.WriteError("Ошибка при поиске");
				}*/

				sw = Stopwatch.StartNew();
				res = LLStr.Contains(basedStr);
				sw.Stop();
				Writer.Write("{0}\t", sw.ElapsedTicks);
				/*if (res != exRes)
				{
					Writer.WriteError("Ошибка при поиске");
				}*/
				
				sw = Stopwatch.StartNew();
				res = SDKey.ContainsValue(ps[i]);
				sw.Stop();
				Writer.Write("{0}\t", sw.ElapsedTicks);
				/*if (res != exRes)
				{
					Writer.WriteError("Ошибка при поиске");
				}*/
				
				sw = Stopwatch.StartNew();
				res = SDStr.ContainsKey(basedStr);
				sw.Stop();
				Writer.Write("{0}\t", sw.ElapsedTicks);
				Writer.WriteLine();
				/*if (res != exRes)
				{
					Writer.WriteError("Ошибка при поиске");
				}*/
			}
		}

		private Person CreatePerson()
		{
			string name = getRandString(3 + rand.Next(20));
			Gender gen = rand.Next(2) == 1 ? Gender.Male : Gender.Female;
			string job = getRandString(3 + rand.Next(20));
			DateTime start = new DateTime(1970 + rand.Next(52), 1 + rand.Next(12), 1 + rand.Next(28));
			int tp = rand.Next(4);
			switch (tp)
			{
				case 0:
					return new Person(name, gen, job, start);
				case 1:
					return new Servant(name, gen, job, start);
				case 2:
					return new Worker(name, gen, job, start, getRandString(3 + rand.Next(20)));
				case 3:
					return new Engineer(name, gen, job, start, getRandString(3 + rand.Next(20)));
			}
			return null;
		}

		private string getRandString(int len)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < len; i++) {
				sb.Append((char)(rand.Next(26) + (rand.Next(10) == 0 ? 65 : 97)));
			}
			return sb.ToString();
		}
	}
}