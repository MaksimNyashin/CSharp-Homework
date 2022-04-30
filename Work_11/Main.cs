#define RUN2
using System;
using NSTester;
using NSReader;
using NSWriter;
using NSCollections;

namespace NSMainProgramm
{
    class Program
    {
        static int Solution(string[] args)
        {
            bool running = true;
            while (running)
            {
                int inp = Reader.ReadInt("1) ArrayList\n2) Stack<Person>\n3) Проверка скорости\n0) Выход\n> ");
                switch (inp)
                {
                    case 1:
                        PersonArrayListInteractor.MainProgramm();
                        break;
                    case 2:
                        PersonStackInteractor.MainProgramm();
                        break;
                    case 3:
                        new TestCollection(Reader.ReadInt(">"));
                        break;
                    case 0:
                        running = false;
                        break;
                }
            }
            
            return 0;
        }

        static void Main(string[] args)
        {
            string p1 = "1 1 M\nM Mjob\n2022 3 30\n";
            string p2 = "1 1 Aaa\nF C\n1999 10 15\n";
            string s1 = "1 2 Ser\nM M\n2020 3 20\n";
            string s2 = "1 2 New\nM newJob\n1997 10 1\n";
            string w1 = "1 3 F\nF job\n2000 1 1 fac\n";
            string e1 = "1 4 Eng\nF jOOb\n2000 1 1 ent\n";
            string e2 = "1 4 Ger\nF j00b\n2019 9 20 ent\n";
            string e3 = "1 4 Ita\nM j000b\n1999 12 31 not ent\n";
            string people = p1 + w1 + s1 + e1;
            TestUnit[] tests = {
                new TestUnit(
                    x =>
                    {
                        PersonArrayListInteractor.MainProgramm();
                        return 0;
                    },
                    "3 4\n" + p1 + w1 + "3 10 0",
                    "Error: Список пустой\nError: Список пустой\n[Name=M, Gender=Male, Job=Mjob, Experience=0 years]\n[Worker: Name=F, Gender=Female, Job=job, Experience=22 years, Factory=fac]\nError: Такой команды нет",
                    "AL.Create"),
                new TestUnit(
                    x =>
                    {
                        PersonArrayListInteractor.MainProgramm();
                        return 0;
                    },
                    // 4 - write list, 2 - delete, 5 - too big index, 1 - delete index, 2 - delete, -3 - too small index
                    // -1 - delete index, 4 - write list, 0 - exit
                    people + "4 2 5 1 2 -3 -1 4 2 0 2 0 2 0",
                    "(M) (F) (Ser) (Eng)\nError: Некорректный индекс\nError: Некорректный индекс\n(M) (Ser)\nError: Нечего удалять",
                    "AL.Delete"),
                new TestUnit(
                    x =>
                    {
                        PersonArrayListInteractor.MainProgramm();
                        return 0;
                    },
                    // 5 - sort, 4 - write list, p2 - new person, 5 - sort, 4 - write list, 5 - sort(sorted), 0 - exit
                    people + "5 4\n" + p2 + "5 4 5 0", "(Eng) (F) (M) (Ser)\n(Aaa) (Eng) (F) (M) (Ser)\nError: Сортировка не нужна",
                    "AL.Sort"),
                new TestUnit(
                    x =>
                    {
                        PersonArrayListInteractor.MainProgramm();
                        return 0;
                    },
                    // 6 - BinSearch(not sorted), 5 - sort, 4 - write list, 6 F\n- BinSearch, 6 New\n - BinSearch(no such name)
                    // s2 - new servant, 6 - BinSearch(modified and not sorted), 5 - sort, 6 New\n - BinSearch, 0 - exit
                    people + "6 5 4 6 F\n6 New\n" + s2 + "6 5 6 New\n0",
                    "Error: Нужна сортировка\n(Eng) (F) (M) (Ser)\n[Worker: Name=F, Gender=Female, Job=job, Experience=22 years, Factory=fac]\nError: Не найден человек с таким именем\nError: Нужна сортировка\n[Servant: Name=New, Gender=Male, Job=newJob, Experience=24 years]",
                    "AL.BinSearch"),
                new TestUnit(
                    x =>
                    {
                        PersonArrayListInteractor.MainProgramm();
                        return 0;
                    },
                    people + s2 + e3 + e2 + "5 7 0 7 1 7 2 10 7 3 ent\n0",
                    "Error: Такой операции нет\n(Ita)\n(M)\n(New)\n(Ser)\n(New)\n2",
                    "Al.Operations"),
                new TestUnit(
                    x =>
                    {
                        PersonStackInteractor.MainProgramm();
                        return 0;
                    },
                    "4 3\n" + p1 + w1 + "3 10 0",
                    "Error: Список пустой\nError: Список пустой\n[Worker: Name=F, Gender=Female, Job=job, Experience=22 years, Factory=fac]\n[Name=M, Gender=Male, Job=Mjob, Experience=0 years]\nError: Такой команды нет",
                    "St.Create"),
                new TestUnit(
                    x =>
                    {
                        PersonStackInteractor.MainProgramm();
                        return 0;
                    },
                    e1 + e2 + "4 2 4 2 2 0",
                    "(Ger) (Eng)\n(Eng)\nError: Нечего удалять",
                    "St.Delete"),
                new TestUnit(
                    x =>
                    {
                        PersonStackInteractor.MainProgramm();
                        return 0;
                    },
                    people + "5 F\n5 New\n" + s2 + "5 New\n0",
                    "[Worker: Name=F, Gender=Female, Job=job, Experience=22 years, Factory=fac]\nError: Не найден человек с таким именем\n[Servant: Name=New, Gender=Male, Job=newJob, Experience=24 years]",
                    "St.Find"),
                new TestUnit(
                    x =>
                    {
                        PersonStackInteractor.MainProgramm();
                        return 0;
                    },
                    people + s2 + e3 + e2 + "6 1 6 0 6 2 10 6 3 ent\n0",
                    "(Ita)\n(New)\n(Ser)\n(M)\nError: Такой операции нет\n(New)\n2",
                    "St.Operations"),
                new TestUnit(
                    x =>
                    {
                        return 0;
                    },
                    "",
                    "",
                    ""),
            };
            #if RUN
                Solution(null);
                // Test.ExecuteSmallTests(tests, true);
                // PersonArrayListInteractor.MainProgramm();
                return;
            #else
                Test.RunSmallTests(tests);
            #endif
        }
    }
}