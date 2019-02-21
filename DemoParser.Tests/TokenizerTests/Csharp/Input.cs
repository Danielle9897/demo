﻿#region Usings
using Raven.Client.Documents;
using Raven.Client.Exceptions;
#endregion
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace DemoSrc.CSharp.Basics.Demo101
{
    public class Demo101Controller : DemoCodeController<EmptyParameters, EmptyParameters>
    {
        public override void SetPrerequisites()
        {
        }

        public override EmptyParameters Run(EmptyParameters input)
        {
            #region Demo
            var serverURL = "http://localhost:8080";
            var databaseName = "Demo";

            #region Step_1
            var documentStore = new DocumentStore
            {
                Urls = new[] { serverURL },
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
                Phone = "(+972)52-5486969",
                Contact = new Contact
                {
                    Name = "Ayende",
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

            return new EmptyParameters();
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
    }
}
