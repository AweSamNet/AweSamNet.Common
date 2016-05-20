using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AweSamNet.Common.Configuration;
using AweSamNet.Common.Logging;
using AweSamNet.Common.Tests.Logging.Providers;
using AweSamNet.Common.Utilities;
using Moq;
using NUnit.Framework;

namespace AweSamNet.Common.Tests.Efficiency
{
    [TestFixture]
    public class GetValueTest
    {
        public class PersonTestCase
        {
            public Person Person { get; set; }
            public Expression<Func<Person, object>> Expression { get; set; }
            public int TimesToRun { get; set; }
        }

        public class Address
        {
            public string City { get; set; }
            public int StreetNumber;
        }

        public class Person
        {
            public Person()
            {
                Addresses = new List<Address>();
            }
            public List<Address> Addresses { get; set; }
            public string Name { get; set; }
        }

        private IEnumerable _objectExpressions 
        {
            get
            {
                yield return new PersonTestCase
                {
                    Person = new Person
                    {
                        Addresses = new List<Address>
                        {
                            new Address
                            {
                                City = "Montreal",
                                StreetNumber = 5
                            }
                        },
                        Name = "Sam Lombardo"
                    },
                    Expression = (x) => x.Name,
                    TimesToRun = 10000
                };
                yield return new PersonTestCase
                {
                    Person = new Person
                    {
                        Addresses = new List<Address>
                        {
                            new Address
                            {
                                City = "Montreal",
                                StreetNumber = 5
                            }
                        },
                        Name = "Sam Lombardo"
                    },
                    Expression = (x) => x.Addresses.FirstOrDefault(),
                    TimesToRun = 100000
                };
                yield return new PersonTestCase
                {
                    Person = new Person
                    {
                        Addresses = new List<Address>
                        {
                            new Address
                            {
                                City = "Montreal",
                                StreetNumber = 5
                            }
                        },
                        Name = "Sam Lombardo"
                    },
                    Expression = (x) => 42,
                    TimesToRun = 10000
                };

                yield return new PersonTestCase
                {
                    Person = new Person
                    {
                        Addresses = new List<Address>
                        {
                            new Address
                            {
                                City = "Montreal",
                                StreetNumber = 5
                            }
                        },
                        Name = "Sam Lombardo"
                    },
                    Expression = (x) => string.Format("string to return {0}", "with format"),
                    TimesToRun = 10000
                };

            }
        }

        [Test, TestCaseSource("_objectExpressions")]
        public void get_value_should_be_faster_than_expression_compile(PersonTestCase testCase)
        {
            DateTime getValueStart = DateTime.Now;
            for (int i = 0; i < testCase.TimesToRun; i++)
            {
                var value = testCase.Person.GetValue(testCase.Expression);
            }
            var getValueTotalTime = DateTime.Now - getValueStart;

            DateTime compileStart = DateTime.Now;
            for (int i = 0; i < testCase.TimesToRun; i++)
            {
                var value = testCase.Expression.Compile()(testCase.Person);
            }
            var compileTotalTime = DateTime.Now - compileStart;

            Assert.IsTrue(
                getValueTotalTime < compileTotalTime, "GetValue(...) is not faster than Compile.  Expression: {0}, GetValueTime: {1:ss.mmmm}, CompileTime: {2:ss.mmmm}",
                testCase.Expression,
                getValueTotalTime,
                compileTotalTime);
        }
    }
}