using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using WorkAtUa.DBManager;
using WorkAtUa.Entities;


namespace WorkAtUa.Tools.SearchAgents
{
    class Program
    {
        public static void SearchAgentsEmployee(IDbManager manager)
        {
            List<MySearchAgent> agents = manager.GetSearchAgents();

            Console.WriteLine("SearchAgents!");
            for (int i = 0; i < agents.Count; i++)
            {
                List<MyVacancy> results = manager.FindVacancies(agents[i].Request, "");

                if (results.Count > 0)
                {
                    MyUser u = manager.GetUserById(agents[i].UserId);

                    manager.FilterVacancies(results, u.Id);

                    if (results.Count > 0)
                    {
                        for (int j = 0; j < results.Count; j++)
                        {
                            MailService.SendMessage(u.Email, @"It seems that we found nice vacancy for you :)" + Environment.NewLine + "Header: " + results[j].ProfessionName + Environment.NewLine + "More: localhost:35235/Home/Search/" + results[j].Id);
                            manager.AddSearchResult(agents[i].Id, results[j].Id);
                            Console.WriteLine("To: " + u.Email);
                            Console.WriteLine(@"It seems that we found nice vacancy for you :)" + Environment.NewLine + "Header: " + results[j].ProfessionName + Environment.NewLine + "More: localhost:35235/Home/Search/" + results[j].Id);
                            Console.WriteLine("----------------------------------------------------------------");
                        }
                    }
                }
            }
        }

        public static void SearchAgentsEmployer(IDbManager manager)
        {
            List<MySearchAgentEmployer> agentsEmployer = manager.GetSearchAgentsEmployer();
            Console.WriteLine("SearchAgentEmployer!");

            for (int i = 0; i < agentsEmployer.Count; i++)
            {
                List<MyUser> results = manager.FindResumes(agentsEmployer[i].Request, "");

                if (results.Count > 0)
                {
                    MyUser u = manager.GetUserById(agentsEmployer[i].UserId);

                    manager.FilterResumes(results, u.Id);

                    if (results.Count > 0)
                    {
                        for (int j = 0; j < results.Count; j++)
                        {
                            MailService.SendMessage(u.Email, @"It seems that we found nice resume for you :)" + Environment.NewLine + "Header: " + manager.GetUserDetailById(results[j].UserDetails).Header + Environment.NewLine + "More: localhost:35235/Employer/Resume/" + results[j].Id);
                            manager.AddSearchResultEmployer(agentsEmployer[i].Id, results[j].Id);
                            Console.WriteLine("To: " + u.Email);
                            Console.WriteLine(@"It seems that we found nice resume for you :)" + Environment.NewLine + "Header: " + manager.GetUserDetailById(results[j].UserDetails).Header + Environment.NewLine + "More: localhost:35235/Employer/Resume/" + results[j].Id);
                            Console.WriteLine("----------------------------------------------------------------");
                        }
                    }
                }

            }
           
        }

        static void Main(string[] args)
        {
            IDbManager manager = new DbManager();

            while (true)
            {
                SearchAgentsEmployee(manager);
                SearchAgentsEmployer(manager);
                Thread.Sleep(10000);
            }
        }
    }
}
