#define RUN2
using System;
using System.Collections.Generic;
using NSTester;
using NSReader;
using NSWriter;
using NSWorkers;

namespace NSMainProgramm
{
    class Program
    {
        static int Solution(string[] args)
        {
            
            return 0;
        }

        static void Main(string[] args)
        {
            TestUnit[] tests = {
                new TestUnit(
                    x =>
                    {
                        PersonList pl = new PersonList(
                            new Person("Person", Gender.Male, "Job1", new DateTime(2000, 1, 1)),
                            new Servant("Servant", Gender.Female, "Job2", new DateTime(2001, 2, 1)),
                            new Worker("Worker", Gender.Male, "Job3", new DateTime(1991, 12, 31), "WorkerFactory"),
                            new Engineer("Engineer", Gender.Female, "Job3", new DateTime(1995, 9, 1), "EngineerEnterprise"));
                        foreach (Person person in pl)
                        {
                            person.Show();
                        }
                        return 0;
                    },
                    "", "[Name=Person, Gender=Male, Job=Job1, Experience=22 years]\n[Name=Servant, Gender=Female, Job=Job2, Experience=21 years]\n[Worker: Name=Worker, Gender=Male, Job=Job3, Experience=30 years, factory=WorkerFactory]\n[Engineer: Name=Engineer, Gender=Female, Job=Job3, Experience=26 years, enterprise=EngineerEnterprise]",
                    "Init&Show"),
                new TestUnit(
                    x =>
                    {
                        PersonList pl = new PersonList(
                            new Person("Person", Gender.Male, "Job1", new DateTime(2000, 1, 1)),
                            new Servant("Servant", Gender.Female, "Job2", new DateTime(2001, 2, 1)),
                            new Worker("Worker", Gender.Male, "Job3", new DateTime(1991, 12, 31), "WorkerFactory"),
                            new Engineer("Engineer", Gender.Female, "Job3", new DateTime(1995, 4, 1), "EngineerEnterprise"));
                        Writer.WriteList(pl.GetMaleNames());
                        Writer.WriteList(pl.GetFemaleNames());
                        return 0;
                    },
                    "", "Person Worker\nServant Engineer",
                    "GenderNames(2.1)"),
                new TestUnit(
                    x =>
                    {
                        PersonList pl = new PersonList(
                            new Person("Person1", Gender.Male, "Job1", new DateTime(2000, 1, 1)),
                            new Servant("Servant1", Gender.Female, "Job2", new DateTime(2001, 2, 1)),
                            new Servant("Servant2", Gender.Male, "Job1", new DateTime(1991, 12, 31)),
                            new Servant("Servant3", Gender.Female, "Job1", new DateTime(1995, 4, 1)),
                            new Worker("Worker", Gender.Female, "Job1", new DateTime(2003, 11, 11), "factory1"));
                        Writer.WriteList(pl.GetServantNameByJob("Job1"));
                        return 0;
                    },
                    "", "Servant2 Servant3",
                    "ServantNames(2.5)"),
                new TestUnit(
                    x =>
                    {
                        PersonList pl = new PersonList(
                            new Person("Person1", Gender.Male, "Job1", new DateTime(2000, 1, 1)),
                            new Worker("Worker1", Gender.Female, "Job2", new DateTime(2001, 2, 1), "factory1"),
                            new Worker("Worker2", Gender.Male, "Job1", new DateTime(1991, 12, 31), "factory2"),
                            new Worker("Worker3", Gender.Female, "Job1", new DateTime(1995, 4, 1), "factory3"),
                            new Servant("Servant", Gender.Male, "Job1", new DateTime(2005, 07, 13)));

                        Writer.WriteList(pl.GetWorkerNameByJob("Job1"));
                        return 0;
                    },
                    "", "Worker2 Worker3",
                    "WorkerNames(2.7)"),
                new TestUnit(
                    x =>
                    {
                        PersonList pl = new PersonList(
                            new Person("Person1", Gender.Male, "Job1", new DateTime(2000, 1, 1)),
                            new Worker("Worker1", Gender.Female, "Job2", new DateTime(2001, 2, 1), "factory1"),
                            new Worker("Worker2", Gender.Male, "Job1", new DateTime(1991, 12, 31), "factory2"),
                            new Worker("Worker3", Gender.Female, "Job1", new DateTime(1995, 4, 1), "factory3"),
                            new Servant("Servant", Gender.Male, "Job1", new DateTime(2005, 07, 13)));
                        Writer.WriteLine(pl.CountWorkerByExperience(22));
                        Writer.WriteLine(pl.CountWorkerByExperience(21));
                        Writer.WriteLine(pl.CountWorkerByExperience(10));
                        Writer.WriteLine(pl.CountWorkerByExperience(40));
                        return 0;
                    },
                    "", "2\n3\n3\n0",
                    "WorkerExperience(2.17)"),
                new TestUnit(
                    x =>
                    {
                        PersonList pl = new PersonList(
                            new Person("Person1", Gender.Male, "Job1", new DateTime(2000, 1, 1)),
                            new Worker("Worker1", Gender.Female, "Job2", new DateTime(2001, 2, 1), "ent1"),
                            new Engineer("Engineer2", Gender.Male, "Job1", new DateTime(1991, 12, 31), "ent1"),
                            new Engineer("Engineer3", Gender.Female, "Job1", new DateTime(1995, 4, 1), "ent1"),
                            new Engineer("Engineer1", Gender.Male, "Job1", new DateTime(2005, 07, 13), "ent2")
                            );
                        Writer.WriteLine(pl.CountEngineerByEnterprise("ent1"));
                        Writer.WriteLine(pl.CountEngineerByEnterprise("ent2"));
                        Writer.WriteLine(pl.CountEngineerByEnterprise("ent"));
                        return 0;
                    },
                    "", "2\n1\n0",
                    "EngineerEnterprise(2.19)"),
                new TestUnit(
                    x =>
                    {
                        IExecutable[] ex = new IExecutable[]
                        {
                            new Person("Person", Gender.Male, "Job1", new DateTime(2000, 1, 1)),
                            new Servant("Servant", Gender.Female, "Job2", new DateTime(2001, 2, 1)),
                            new Worker("Worker", Gender.Male, "Job3", new DateTime(1991, 12, 31), "WorkerFactory"),
                            new Engineer("Engineer", Gender.Female, "Job3", new DateTime(1995, 9, 1), "EngineerEnterprise")
                        };
                        foreach (IExecutable e in ex)
                        {
                            e.Execute();
                        }
                        return 0;
                    },
                    "", "Executing: Name=Person, Gender=Male, Job=Job1, Experience=22 years\nExecuting: Name=Servant, Gender=Female, Job=Job2, Experience=21 years\nExecuting: Worker: Name=Worker, Gender=Male, Job=Job3, Experience=30 years, factory=WorkerFactory\nExecuting: Engineer: Name=Engineer, Gender=Female, Job=Job3, Experience=26 years, enterprise=EngineerEnterprise",
                    "IExecutable"),
                new TestUnit(
                    x =>
                    {
                        PersonList pl = new PersonList(
                            new Person("Person", Gender.Male, "Job1", new DateTime(2000, 1, 1)),
                            new Servant("Servant", Gender.Female, "Job2", new DateTime(2001, 2, 1)),
                            new Worker("Worker", Gender.Male, "Job3", new DateTime(1991, 12, 31), "WorkerFactory"),
                            new Engineer("Engineer", Gender.Female, "Job3", new DateTime(1995, 9, 1), "EngineerEnterprise"),
                            new Person("Person2", Gender.Male, "Job1", new DateTime(2000, 1, 2)));
                        pl.Sort();
                        foreach (Person p in pl)
                        {
                            Writer.WriteLine("{0}, {1}", p.Name, p.GetExperienceYears());
                        }
                        Writer.WriteLine();

                        pl = new PersonList(
                            new Person("P1", Gender.Male, "j", new DateTime(2020, 2, 10)),
                            new Person("P2", Gender.Male, "j", new DateTime(2020, 1, 1)),
                            new Person("P3", Gender.Male, "j", new DateTime(2020, 2, 7)),
                            new Person("P4", Gender.Male, "j", new DateTime(2020, 1, 5)),
                            new Person("P5", Gender.Male, "j", new DateTime(2020, 2, 13)),
                            new Person("P6", Gender.Male, "j", new DateTime(2020, 1, 18)),
                            new Person("P7", Gender.Male, "j", new DateTime(2020, 2, 2)),
                            new Person("P8", Gender.Male, "j", new DateTime(2020, 1, 19)),
                            new Person("P9", Gender.Male, "j", new DateTime(2020, 2, 11)),
                            new Person("P10", Gender.Male, "j", new DateTime(2020, 1, 21)),
                            new Person("P11", Gender.Male, "j", new DateTime(2020, 2, 20)),
                            new Person("P12", Gender.Male, "j", new DateTime(2020, 1, 31)),
                            new Person("P13", Gender.Male, "j", new DateTime(2020, 2, 29)),
                            new Person("P14", Gender.Male, "j", new DateTime(2020, 1, 4)),
                            new Person("P15", Gender.Male, "j", new DateTime(2020, 2, 3)),
                            new Person("P16", Gender.Male, "j", new DateTime(2020, 1, 8)),
                            new Person("P17", Gender.Male, "j", new DateTime(2020, 2, 12)),
                            new Person("P18", Gender.Male, "j", new DateTime(2020, 1, 26)),
                            new Person("P19", Gender.Male, "j", new DateTime(2020, 2, 14)));

                        pl.Sort();
                        foreach (Person p in pl)
                        {
                            Writer.WriteLine("{0}", p.Name);
                        }
                        return 0;
                    },
                    "", "Servant, 21\nPerson2, 22\nPerson, 22\nEngineer, 26\nWorker, 30\n\nP13\nP11\nP19\nP5\nP17\nP9\nP1\nP3\nP15\nP7\nP12\nP18\nP10\nP8\nP6\nP16\nP4\nP14\nP2",
                    "IComparable"),
                new TestUnit(
                    x =>
                    {
                        Person[] pl = new Person[]{
                            new Person("P1", Gender.Male, "j", new DateTime(2020, 2, 10)),
                            new Person("P2", Gender.Male, "j", new DateTime(2020, 1, 1)),
                            new Person("P3", Gender.Male, "j", new DateTime(2020, 2, 7)),
                            new Person("P4", Gender.Male, "j", new DateTime(2020, 1, 5)),
                            new Person("P5", Gender.Male, "j", new DateTime(2020, 2, 13)),
                            new Person("P6", Gender.Male, "j", new DateTime(2020, 1, 18)),
                            new Person("P7", Gender.Male, "j", new DateTime(2020, 2, 2)),
                            new Person("P8", Gender.Male, "j", new DateTime(2020, 1, 19)),
                            new Person("P9", Gender.Male, "j", new DateTime(2020, 2, 11)),
                            new Person("P10", Gender.Male, "j", new DateTime(2020, 1, 21)),
                            new Person("P11", Gender.Male, "j", new DateTime(2020, 2, 20)),
                            new Person("P12", Gender.Male, "j", new DateTime(2020, 1, 31)),
                            new Person("P13", Gender.Male, "j", new DateTime(2020, 2, 29)),
                            new Person("P14", Gender.Male, "j", new DateTime(2020, 1, 4)),
                            new Person("P15", Gender.Male, "j", new DateTime(2020, 2, 3)),
                            new Person("P16", Gender.Male, "j", new DateTime(2020, 1, 8)),
                            new Person("P17", Gender.Male, "j", new DateTime(2020, 2, 12)),
                            new Person("P18", Gender.Male, "j", new DateTime(2020, 1, 26)),
                            new Person("P19", Gender.Male, "j", new DateTime(2020, 2, 14))};
                        Array.Sort(pl, new SortByExperience());
                        foreach (Person p in pl)
                        {
                            Writer.WriteLine("{0}", p.Name);
                        }
                        return 0;
                    },
                    "", "P13\nP11\nP19\nP5\nP17\nP9\nP1\nP3\nP15\nP7\nP12\nP18\nP10\nP8\nP6\nP16\nP4\nP14\nP2",
                    "IComparer"),
                new TestUnit(
                    x =>
                    {
                        Person person = new Person("Name", Gender.Female, "Job", new DateTime(2000, 1, 1));
                        (person.Clone() as Person).Show();
                        person = new Servant("Name", Gender.Female, "Job", new DateTime(2000, 1, 1));
                        (person.Clone() as Servant).Show();
                        person = new Worker("Name", Gender.Female, "Job", new DateTime(2000, 1, 1), "factory");
                        (person.Clone() as Worker).Show();
                        (person.Clone() as Person).Show();
                        person = new Engineer("Name", Gender.Female, "Job", new DateTime(2000, 1, 1), "enterprise");
                        (person.Clone() as Engineer).Show();
                        return 0;
                    },
                    "", "[Name=Клон.Name, Gender=Female, Job=Job, Experience=22 years]\n[Name=Клон.Name, Gender=Female, Job=Job, Experience=22 years]\n[Worker: Name=Клон.Name, Gender=Female, Job=Job, Experience=22 years, factory=factory]\n[Worker: Name=Клон.Name, Gender=Female, Job=Job, Experience=22 years, factory=factory]\n[Engineer: Name=Клон.Name, Gender=Female, Job=Job, Experience=22 years, enterprise=enterprise]",
                    "Copy"),
                new TestUnit(
                    x =>
                    {
                        return 0;
                    },
                    "", "",
                    ""),
                };
            #if RUN
                Test.ExecuteSmallTests(tests);
                return;
            #else
                Test.RunSmallTests(tests);
            #endif
        }
    }
}