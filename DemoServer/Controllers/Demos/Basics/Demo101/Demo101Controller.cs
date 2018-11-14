﻿using System.Threading.Tasks;
using DemoServer.Utils;
using Microsoft.AspNetCore.Mvc;
#region Usings
using Raven.Client.Documents;
using Raven.Client.Exceptions;
#endregion
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace DemoServer.Controllers.Demos.Basics.Demo101
{
    public class Demo101Controller : DemoCodeController
    {
        public Demo101Controller(HeadersAccessor headersAccessor) : base(headersAccessor)
        {
        }

        public override Task SetPrerequisites()
        {
            return Task.CompletedTask;
            //TODO: set up the database
        }

        [HttpPost]
        public IActionResult Run(RunParams runParams)
        {
            var firstName = runParams.FirstName;
            var lastName = runParams.LastName;

            var serverURL = "http://localhost:8080";
            var databaseName = "DemoExample";
            //TODO: replace databaseName with user database

            #region Demo
            #region Step_1
            var documentStore = new DocumentStore
            {
                Urls = new[] { serverURL }, //for example: serverUrl = "http://localhost:8080"
                Database = databaseName
            };
            #endregion

            #region Step_2
            documentStore.Initialize();
            #endregion

            #region Step_3
            try
            {
                documentStore.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(databaseName)));
            }
            catch (ConcurrencyException)
            {
                // Database already exists
            }
            #endregion

            var newCompany = new Company
            {
                Name = "Hibernating Rhinos",
                Phone = "(+972)052-5933777",
                Contact = new Contact
                {
                    Name = firstName + " " + lastName,
                    Title = "CEO"
                }
            };

            #region Step_4
            using (var session = documentStore.OpenSession())
            #endregion
            {
                #region Step_5
                session.Store(newCompany);
                session.SaveChanges();
                #endregion
            }
            #endregion

            return Ok("The database was created successfully");
        }

        public class Company
        {
            public string Name { get; set; }
            public string Phone { get; set; }
            public Contact Contact { get; set; }
        }

        public class Contact
        {
            public string Name { get; set; }
            public string Title { get; set; }
        }

        public class RunParams
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}