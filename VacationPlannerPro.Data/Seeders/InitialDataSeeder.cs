using VacationPlannerPro.Data.Entities;
using VacationPlannerPro.Data.Enums;

namespace VacationPlannerPro.Data.Seeders
{
    public static class InitialDataSeeder
    {
        public static async Task SeedInitialData(ApplicationDbContext context)
        {
            if (!context.Projects.Any())
            {
                var projects = new List<Project>
                {
                    new Project { Name = "Website Redesign", Description = "Complete overhaul of the company website.", StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddMonths(2) },
                    new Project { Name = "AI Research", Description = "Exploration of new AI algorithms.", StartDate = DateTime.Now.AddDays(-60), EndDate = DateTime.Now.AddMonths(4) },
                    new Project { Name = "Marketing Campaign", Description = "Launch of a new product with an extensive marketing campaign.", StartDate = DateTime.Now.AddDays(-15), EndDate = DateTime.Now.AddMonths(1) }
                };

                context.Projects.AddRange(projects);
                await context.SaveChangesAsync();
            }

            if (!context.Leaders.Any())
            {
                var leaders = new List<Leader>
                {
                    new Leader { FullName = "Ivan Ivanov", Age = 34, ProfessionId = Guid.Parse("9127378C-3F5C-4BB7-D7AD-08DD17CB8669") }, // Software Engineer
                    new Leader { FullName = "Maria Georgieva", Age = 42, ProfessionId = Guid.Parse("91A43665-1E0B-4CBF-D7AE-08DD17CB8669") }, // Data Scientist
                    new Leader { FullName = "Dimitar Stoyanov", Age = 39, ProfessionId = Guid.Parse("E4B73396-8200-4370-D7AF-08DD17CB8669") }, // Mechanical Engineer
                    new Leader { FullName = "Petar Petrov", Age = 36, ProfessionId = Guid.Parse("B32982D4-DE40-494A-D7B0-08DD17CB8669") }, // Civil Engineer
                    new Leader { FullName = "Stoyanka Dimitrova", Age = 45, ProfessionId = Guid.Parse("7DBE5832-F39E-40FC-D7B1-08DD17CB8669") } // Electrical Engineer
                };

                context.Leaders.AddRange(leaders);
                await context.SaveChangesAsync();
            }

            if (!context.Teams.Any())
            {
                var projects = context.Projects.ToList(); 
                var leaders = context.Leaders.ToList(); 

                var teams = new List<Team>
                {
                    new Team { Name = "Development Team", ProjectId = projects[0].Id, LeaderId = leaders[0].Id, NumberOfWorkers = 5 },
                    new Team { Name = "AI Research Team", ProjectId = projects[1].Id, LeaderId = leaders[1].Id, NumberOfWorkers = 3 },
                    new Team { Name = "Marketing Team", ProjectId = projects[2].Id, LeaderId = leaders[4].Id, NumberOfWorkers = 2 }
                };

                context.Teams.AddRange(teams);
                await context.SaveChangesAsync();
            }

            if (!context.Workers.Any())
            {
                var workers = new List<Worker>
                {
                    new Worker { FullName = "Stefan Kolev", Age = 28, ProfessionId = Guid.Parse("9127378C-3F5C-4BB7-D7AD-08DD17CB8669") }, // Software Engineer
                    new Worker { FullName = "Todor Vasilev", Age = 32, ProfessionId = Guid.Parse("91A43665-1E0B-4CBF-D7AE-08DD17CB8669") }, // Data Scientist
                    new Worker { FullName = "Kristina Petrova", Age = 27, ProfessionId = Guid.Parse("E4B73396-8200-4370-D7AF-08DD17CB8669") }, // Mechanical Engineer
                    new Worker { FullName = "Georgi Hristov", Age = 31, ProfessionId = Guid.Parse("B32982D4-DE40-494A-D7B0-08DD17CB8669") }, // Civil Engineer
                    new Worker { FullName = "Violeta Stoyanova", Age = 29, ProfessionId = Guid.Parse("7DBE5832-F39E-40FC-D7B1-08DD17CB8669") }, // Electrical Engineer
                    new Worker { FullName = "Boris Dimitrov", Age = 34, ProfessionId = Guid.Parse("91A43665-1E0B-4CBF-D7AE-08DD17CB8669") }, // Data Scientist
                    new Worker { FullName = "Elena Ivanova", Age = 26, ProfessionId = Guid.Parse("B32982D4-DE40-494A-D7B0-08DD17CB8669") }, // Civil Engineer
                    new Worker { FullName = "Zlatina Georgieva", Age = 30, ProfessionId = Guid.Parse("9127378C-3F5C-4BB7-D7AD-08DD17CB8669") }, // Software Engineer
                    new Worker { FullName = "Nikola Dimitrov", Age = 35, ProfessionId = Guid.Parse("7DBE5832-F39E-40FC-D7B1-08DD17CB8669") }, // Electrical Engineer
                    new Worker { FullName = "Lilia Atanasova", Age = 33, ProfessionId = Guid.Parse("E4B73396-8200-4370-D7AF-08DD17CB8669") } // Mechanical Engineer
                };

                context.Workers.AddRange(workers);
                await context.SaveChangesAsync();
            }

            if (!context.Vacations.Any())
            {
                var workers = context.Workers.ToList();

                var vacations = new List<Vacation>
                {
                    new Vacation { StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(5), Type = VacationTypeEnum.PaidLeave, WorkerId = workers[0].Id },
                    new Vacation { StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddDays(-5), Type = VacationTypeEnum.UnpaidLeave, WorkerId = workers[3].Id }
                };

                context.Vacations.AddRange(vacations);
                await context.SaveChangesAsync();
            }
        }
    }
}
