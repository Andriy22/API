namespace WebApi
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DBase : DbContext
    {
        // Your context has been configured to use a 'DBase' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebApi.Models.DBase' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DBase' 
        // connection string in the application configuration file.
        public DBase()
            : base("name=DBase")
        {
            Database.SetInitializer<DBase>(new CustomInit<DBase>());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Worker> Workers { get; set; }
    }

    internal class CustomInit<T> : DropCreateDatabaseIfModelChanges<DBase>
    {
        protected override void Seed(DBase context)
        {
            base.Seed(context);
            context.Workers.Add(new Worker
            {
                Address = "Rivne, Soborna str.,56",
                FirstName = "Petro",
                LastName = "Petrenko",
                Gender = "Montagnik",
                Salary = 9999m
            });
            context.Workers.Add(new Worker
            {
                Address = "Lutsk, Soborna str.,112",
                FirstName = "Ivan",
                LastName = "Ivanenko",
                Gender = "male",
                Salary = 19999m
            });
            context.Workers.Add(new Worker
            {
                Address = "Kyiv, Nezalegnosti maidan,1",
                FirstName = "Vitalik",
                LastName = "Klichko",
                Gender = "Mer",
                Salary = 1005000m
            });

        }
    }

    public class Worker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public string Gender { get; set; }
    }
}