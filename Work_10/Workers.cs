using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using NSReader;
using NSWriter;

namespace NSWorkers
{
	public enum Gender
	{
		Female,
		Male
	}

	public interface IExecutable
	{
		void Execute();
	}

	public class Person: IExecutable, IComparable, ICloneable
	{
		protected string name;
		public string Name { get { return name; }}
		protected Gender gender;
		public Gender GetGender { get { return gender; }}
		protected string job;
		public string Job { get { return job; }}
		protected DateTime Start;

		public Person(string name, Gender gender, string job, DateTime start)
		{
			this.name = name;
			this.gender = gender;
			this.job = job;
			Start =  start;
		}

		protected virtual string GetStringToShow()
		{
			return String.Format("Name={0}, Gender={1}, Job={2}, Experience={3} years", Name, GetGender, Job, GetExperienceYears());
		}

		public void Show()
		{
			Writer.WriteLine("[{0}]", GetStringToShow());
		}

		public override string ToString()
		{
			return String.Format("({0})", Name);
		}

		public int GetExperience()
		{
			return (int)((DateTime.UtcNow - Start).TotalDays) + 1;
		}

		public int GetExperienceYears()
		{
			return (int)(Math.Floor(GetExperience() / 365.25));
		}

		public int CompareTo(object obj)
		{
			if (!(obj is Person))
			{
				return -1;
			}
			int ret = GetExperience() - (obj as Person).GetExperience();
			if (ret != 0)
			{
				return ret;
			}
			ret = Name.CompareTo((obj as Person).Name);
			if (ret != 0)
			{
				return ret;
			}
			ret = Job.CompareTo((obj as Person).Job);
			if (ret == 0) {
				Show();
				(obj as Person).Show();
			}
			return ret;
		}

		public void Execute()
		{
			Writer.WriteLine("Executing: {0}", GetStringToShow());
		}

		public virtual object Clone()
		{
			return new Person("Клон." + name, gender, job, Start);
		}

		public virtual object Copy()
		{
			return new Person(name, gender, job, Start);
		}

		protected static ArrayList Input()
		{
			ArrayList al = new ArrayList();
			al.Add(Reader.ReadLine("Имя: "));
			String gen = Reader.ReadString("Пол: ");
			while (gen != "M" && gen != "F")
			{
				Writer.WriteError("Пол Может обозначаться только \"M\" и \"F\"(без кавычек)");
				gen = Reader.ReadString("Пол: ");
			}
			al.Add(gen == "M" ? Gender.Male : Gender.Female);
			al.Add(Reader.ReadLine("Профессия: "));
			Writer.WriteLineInfo("Дата начала работы");
			bool running = true;
			do
			{
				try
				{
					int y = Reader.ReadInt("Год: ");
					int m = Reader.ReadInt("Месяц: ");
					int d = Reader.ReadInt("День: ");
					DateTime st = new DateTime(y, m, d);
					running = false;
					al.Add(st);
				}
				catch (ArgumentOutOfRangeException e)
				{
					Writer.WriteError(e.Message);
				}
			}
			while (running);
			return al;
		}

		public static Person CreateAndInput()
		{
			ArrayList al = Input();
			return new Person((string)al[0], (Gender)al[1], (string)al[2], (DateTime)al[3]);
		}

		public virtual Person BasePerson
		{
			get
			{
				return new Person(name, gender, job, Start);
			}
		}
	}

	public class SortByExperience: IComparer
	{
		int IComparer.Compare(object object1, object object2)
		{
			if (!(object1 is Person) || !(object2 is Person))
			{
				int res = 0;
				if (object1 is Person)
				{
					res++;
				}
				if (object2 is Person)
				{
					res--;
				}
				return res;
			}
			return (object1 as Person).CompareTo(object2);
		}
	}

	public class Servant: Person
	{
		public Servant(string name, Gender gender, string job, DateTime start): base(name, gender, job, start) {}

		public override object Clone()
		{
			return new Servant("Клон." + name, gender, job, Start);
		}
		public override object Copy()
		{
			return new Servant(name, gender, job, Start);
		}
		private new static ArrayList Input()
		{
			return Person.Input();
		}
		protected override string GetStringToShow()
		{
			return String.Format("Servant: {0}", base.GetStringToShow());
		}
		public new static Servant CreateAndInput()
		{
			ArrayList al = Input();
			return new Servant((string)al[0], (Gender)al[1], (string)al[2], (DateTime)al[3]);
		}
		public override Person BasePerson
		{
			get
			{
				return new Person(name, gender, job, Start);
			}
		}
	}

	class Worker: Person
	{
		string factory;
		public string Factory { get { return factory; } }

		public Worker(string name, Gender gender, string job, DateTime start, string factory): base(name, gender, job, start)
		{
			this.factory = factory;
		}
		protected override string GetStringToShow()
		{
			return String.Format("Worker: {0}, Factory={1}", base.GetStringToShow(), Factory);
		}
		public override object Clone()
		{
			return new Worker("Клон." + name, gender, job, Start, factory);
		}
		public override object Copy()
		{
			return new Worker(name, gender, job, Start, factory);
		}
		private new static ArrayList Input()
		{
			var al = Person.Input();
			al.Add(Reader.ReadLine("Фабрика: "));
			return al;
		}
		public new static Worker CreateAndInput()
		{
			ArrayList al = Input();
			return new Worker((string)al[0], (Gender)al[1], (string)al[2], (DateTime)al[3], (string)al[4]);
		}
		public override Person BasePerson
		{
			get
			{
				return new Person(name, gender, job, Start);
			}
		}
	}

	class Engineer: Person
	{
		string enterprise;
		public string Enterprise { get { return enterprise; }}

		public Engineer(string name, Gender gender, string job, DateTime start, string enterprise): base(name, gender, job, start)
		{
			this.enterprise = enterprise;
		}

		protected override string GetStringToShow()
		{
			return String.Format("Engineer: {0}, Enterprise={1}", base.GetStringToShow(), Enterprise);
		}
		public override object Clone()
		{
			return new Engineer("Клон." + name, gender, job, Start, enterprise);
		}
		public override object Copy()
		{
			return new Engineer(name, gender, job, Start, enterprise);
		}
		private new static ArrayList Input()
		{
			var al = Person.Input();
			al.Add(Reader.ReadLine("Предприятие: "));
			return al;
		}
		public new static Engineer CreateAndInput()
		{
			ArrayList al = Input();
			return new Engineer((string)al[0], (Gender)al[1], (string)al[2], (DateTime)al[3], (string)al[4]);
		}
		public override Person BasePerson
		{
			get
			{
				return new Person(name, gender, job, Start);
			}
		}
	}

	public class PersonList: IEnumerable
	{
		private int SORT_LIMIT = 16;
		List <Person> People = new List<Person>();
		public int Length { get {return People.Count(); }}
		public PersonList() {}

		public PersonList(params Person[] people)
		{
			People.AddRange(people);
		}

		public PersonList(List <Person> people)
		{
			People = people;
		}

		private List<T> FilterAndZip<T>(Func<Person, bool> functionFilter, Func<Person, T> functionZip)
		{
			return People
				.Where(functionFilter)
				.ZipOne(functionZip)
				.ToList();
		}

		private List<string> GetGenderNames(Gender current)
		{
			return FilterAndZip(x => x.GetGender == current, x => x.Name);
		}

		// 1
		public List<string> GetMaleNames()
		{
			return GetGenderNames(Gender.Male);
		}

		// 1
		public List<string> GetFemaleNames()
		{
			return GetGenderNames(Gender.Female);
		}

		// 4
		public List<string> GetServantNameByExperience(int years)
		{
			return FilterAndZip(
				x => x is Servant && (x as Servant).GetExperienceYears() >= years,
				x => x.Name);
		}

		private List<string> GetByJob<T>(string job)
		{
			return FilterAndZip(
				x => x is T && x.Job == job,
				x => x.Name);
		}

		// 5
		public List<string> GetServantNameByJob(string job)
		{
			return GetByJob<Servant>(job);
		}

		// 7
		public List<string> GetWorkerNameByJob(string job)
		{
			return GetByJob<Worker>(job);
		}

		private int FilterAndCount(Func<Person, bool> functionFilter)
		{
			return People
				.Where(functionFilter)
				.Count();
		}

		// 17
		public int CountWorkerByExperience(int years)
		{
			return FilterAndCount(x => (x is Worker) && x.GetExperienceYears() >= years);
		}

		public Person this[int index]
		{
			get { return People[index]; }
			set { People[index] = value; }
		}

		// 19
		public int CountEngineerByEnterprise(string enterprise)
		{
			return FilterAndCount(x => (x is Engineer) && (x as Engineer).Enterprise == enterprise);
		}

		public IEnumerator GetEnumerator()
		{
			foreach (Person person in People)
			{
				yield return person;
			}
		}

		private void SortSmall(int l, int r)
		{
			for (int i = l; i < r; i++)
			{
				for (int j = l; j < r - 1; j++)
				{
					if (People[j].CompareTo(People[j + 1]) > 0) 
					{
						Person pp = People[j];
						People[j] = People[j + 1];
						People[j + 1] = pp;
					}
				}
			}
		}

		private void SortBig(int l, int r)
		{
			if (r - l <= SORT_LIMIT)
			{
				SortSmall(l, r);
				return;
			}
			int m = (l + r) / 2;
			SortBig(l, m);
			SortBig(m, r);
			Person[] tmp = new Person[r - l];
			int l1 = l, r1 = m;
			for (int i = 0; i < r - l; i++)
			{
				if (r1 == r || (l1 != m && People[l1].CompareTo(People[r1]) < 0))
				{
					tmp[i] = People[l1];
					l1++;
				}
				else
				{
					tmp[i] = People[r1];
					r1++;
				}
			}
			for (int i = l; i < r; i++)
			{
				People[i] = tmp[i - l];
			}
		}

		public void Sort()
		{
			// SortBig(0, pl.Length);
			People.Sort();
		}
	}

	public static class HelpClass
	{
		public static IEnumerable<TResult> ZipOne<T1, TResult>(
		this IEnumerable<T1> source,
		Func<T1, TResult> func)
		{
			using (var e = source.GetEnumerator())
			{
				while (e.MoveNext())
				{
					yield return func(e.Current);
				}
			}
		}
	}
}