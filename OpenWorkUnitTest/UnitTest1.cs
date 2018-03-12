using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Openwork.Domain.Core;
using Openwork.Persistence;
using System.Collections.Generic;
using MongoDB.Driver;
using OpenWork.Persistence;

namespace OpenWorkUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        List<Person> _persons = new List<Person>();
        List<PersonNoSql> _personsNoSql = new List<PersonNoSql>();


        [TestInitialize]
        public void CreatePersons()
        {
            for (int i = 0; i < 10; i++)
            {
                Person p = CreatePerson();
                PersonNoSql p1 = p.CloneNoSqlPerson();
                PersistenceFacade.Instance.SaveOrUpdate(p1);
                PersistenceFacade.Instance.SaveOrUpdate(p);
                
                _persons.Add(p);
                _personsNoSql.Add(p1);
            }
        }

        [TestCleanup]
        public void DeletePersons()
        {
            foreach (var item in _persons)
            {
                PersistenceFacade.Instance.Delete<Person>(item);
            }
            foreach (var item1 in _personsNoSql)
            {
                PersistenceFacade.Instance.Delete<PersonNoSql>(item1);
            }
        }

        [TestMethod]
        public void TestQuery()
        {
           

         

            //eseguo un aquery su tutti
            IList<Person> persons = PersistenceFacade.Instance.Find<Person>("");
            IList<PersonNoSql> personsNoSql = PersistenceFacade.Instance.Find<PersonNoSql>("");

            Assert.AreEqual(persons.Count, personsNoSql.Count);
        }

        private static Person CreatePerson()
        {
            Person p = new Person();

            p.Name = Faker.Name.FullName(Faker.NameFormats.Standard);
            p.Description = Faker.Lorem.Sentence(10);
            p.Number = Faker.RandomNumber.Next(100000).ToString();
            p.RepositoryId = Guid.NewGuid().ToString();


            p.Fields = new Dictionary<string, object>
            {
                { "Fax", Faker.Phone.Number().ToString() },
                { "Tags", new object []{ Faker.Internet.DomainName(), Faker.RandomNumber.Next(100)}},
                { "Email", Faker.Internet.Email() },
                { "Notes", Faker.Lorem.Paragraph() },
                { "Phone", Faker.Phone.Number() },
                { "Email2", Faker.Internet.Email() },
                { "Address",

                    new Dictionary<string, object>
                    {
                        { "Type", Faker.RandomNumber.Next(10) },
                        { "Url", Faker.Internet.DomainName() },
                        { "Name", Faker.Company.Name() },
                        { "DefaultValue", new Dictionary<string, object>() },
                        { "Fields",
                            new Dictionary<string, object>
                            {
                                { "City", Faker.Address.City() },
                                { "Route", Faker.Address.StreetName() },
                                { "Country", Faker.Address.StreetAddress() },
                                { "Locality", Faker.Address.CityPrefix() },
                                { "PostalCode", Faker.Address.ZipCode() },
                                { "StreetNumber", Faker.Address.CitySufix() },
                                { "CountryShortName", Faker.Address.Country() },
                                { "AdministrativeAreaLevel1", Faker.Internet.UserName() },
                                { "AdministrativeAreaLevel2", Faker.Internet.UserName() },
                                { "AdministrativeAreaLevel1ShortName", Faker.Internet.UserName() },
                                { "AdministrativeAreaLevel2ShortName", Faker.Internet.UserName() }
                            }
                        },
                        { "ModelReference",
                            new Dictionary<string, object>
                            {
                                { "Type", Faker.RandomNumber.Next(10) },
                                { "Url", Faker.Internet.DomainName() },
                                { "Name", Faker.Company.Name() }
                            }
                        }
                    }
                },
                { "Picture",
                    new Dictionary<string, object>
                    {
                        { "Type", Faker.RandomNumber.Next(10) },
                        { "Url", Faker.Internet.DomainName() },
                        { "Name", Faker.Company.Name() }
                    }
                },
                { "TaxInfo",
                    new Dictionary<string, object>
                    {
                        { "Fields",
                            new Dictionary<string, object>
                            {
                                { "VAT", Faker.RandomNumber.Next(1000000,2000000) },
                                { "IBAN", Faker.Company.Name() },
                                { "TaxCode", Faker.Company.Name() }
                            }
                        },
                        { "ModelReference",
                            new Dictionary<string, object>
                            {
                                { "Type", Faker.RandomNumber.Next(10) },
                                { "Url", Faker.Internet.DomainName() },
                                { "Name", Faker.Company.Name() }
                            }
                        }
                    }
                },
                { "Website",
                    new Dictionary<string, object>
                    {
                        { "Type", Faker.RandomNumber.Next(10) },
                        { "Url", Faker.Internet.DomainName() },
                        { "Name", Faker.Company.Name() }
                    }
                },
                { "LegalEmail", Faker.Internet.Email() },
                { "MobilePhone", Faker.Phone.Number() },
                { "TabBusiness", Faker.Company.BS() },
                { "BusinessInfo",
                    new Dictionary<string, object>
                    {
                        { "Fields",
                            new Dictionary<string, object>
                            {
                                { "Employees", Faker.RandomNumber.Next(10, 50) },
                                { "RevenueTrend", ""},
                                { "StockMarkets", new object []{ Faker.Internet.DomainName(), Faker.RandomNumber.Next(100)}},
                                { "FiscalYearEnd", Faker.RandomNumber.Next(1990, 2018) },
                                { "AnnualRevenues", Faker.RandomNumber.Next(10000000)},
                                { "FoundationYear", Faker.RandomNumber.Next(1990, 2018) },
                                { "OrganizationType",
                                    new Dictionary<string, object>
                                    {
                                        { "Type", Faker.RandomNumber.Next(10) },
                                        { "Url", Faker.Internet.DomainName() },
                                        { "Name", Faker.Company.Name() }
                                    }
                                },
                                 { "AnnualBalanceSheet", Faker.RandomNumber.Next(-100, 100) },
                                { "FinancialViability", ""}
                            }
                        },
                        { "ModelReference",
                            new Dictionary<string, object>
                            {
                                { "Type", Faker.RandomNumber.Next(10) },
                                { "Url", Faker.Internet.DomainName() },
                                { "Name", Faker.Company.Name() }
                            }
                        }
                    }
                },
                { "CheckAddress", Faker.RandomNumber.Next(10) > 5 ? true: false },
                { "LegalStructure",
                    new Dictionary<string, object>
                    {
                        { "Type", Faker.RandomNumber.Next(10) },
                        { "Url", Faker.Internet.DomainName() },
                        { "Name", Faker.Company.Name() }
                    }
                },
                { "LinkedResources",new string []{ Faker.Name.First(), Faker.Name.First()}},
                { "SecondaryOffice", Faker.RandomNumber.Next(10) > 5 ? true: false },
                { "OrganizationName", Faker.Company.Name() },
                { "ParentOrganization",
                    new Dictionary<string, object>
                    {
                        { "Type", Faker.RandomNumber.Next(10) },
                        { "Url", Faker.Internet.DomainName() },
                        { "Name", Faker.Company.Name() }
                    }
                }
            };
            return p;
        }






    }
}
