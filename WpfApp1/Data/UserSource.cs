using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Model;

namespace WpfApp1.Data
{
    class UserSource
    {
        public static User GetUser()
        {
            var rand = new Random((int) DateTime.Now.Ticks);
            return new User
            {
                Address = new Address
                {
                    City = "Moscow",
                    Country = "Russia",
                    Zipcode = 123456
                },

                Age = rand.Next(10, 50),
                Name = "TestUser",
                Randoms = new ObservableCollection<int>(Enumerable.Range(0, 5)
                                                                   .Select(p => rand.Next(1000))),
                DoubleRandoms = Enumerable.Range(0, 5)
                                          .Select(p => rand.NextDouble()).ToArray(),

                Addresses = new ObservableCollection<Address>
                {
                    new Address
                    {
                        City = "test",
                        Country = "test country",
                        Zipcode = 123456
                    },
                    new Address
                    {
                        City = "test1",
                        Country = "test country1",
                        Zipcode = 654321
                    },
                    new Address
                    {
                        City = "test2",
                        Country = "test country2",
                        Zipcode = 546457
                    }
                }
            };
        }

        public static Button GetButton()
        {
            return new Button
            {
                Content = "Test button",
                Margin = new Thickness(5)
            };
        }
    }
}
