using System;

namespace lab_1
{
    public class PersonProperties : IEquatable<PersonProperties>
    {
        public string Ects { get; set; }
        private string _firstName;

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (value != null && value.Length >= 2)
                {
                    _firstName = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Name's too short");
                }
            }
        }


        private PersonProperties(string name)
        {
            _firstName = name;

        }

        public static PersonProperties OfName(string name)
        {
            if (name != null && name.Length >= 2)
            {
                return new PersonProperties(name);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Name is too short");
            }
        }

        public override string ToString()
        {
            return $"Name: {_firstName}";
        }

        public override bool Equals(object obj)
        {
            return obj is PersonProperties properties &&
                   Ects == properties.Ects &&
                   FirstName == properties.FirstName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Ects, FirstName);
        }

        public bool Equals(PersonProperties other)
        {
            return other._firstName.Equals(_firstName) && other.Ects == Ects;
        }



    }

    public enum Currency
    {


        PLN = 1,
        USD = 2,
        EUR = 3
    }





    public class Money : IComparable<Money>
    {
        private readonly decimal _value;

        private readonly Currency _currency;
        public Currency Currency
        {
            get { return _currency; }
        }

        public decimal Value
        {
            get { return _value; }
        }

        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }

        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }

        public static Money? OfWithException(decimal value, Currency currency)
        {
            if (value < 0 || value == null)
            {
                throw new NullReferenceException("is null");
            }
            else
            {
                return new Money(value, currency);

            }
        }

        public static Money ParseValue(string valueStr, Currency currency)
        {
            decimal value = Convert.ToDecimal(valueStr);
            return new Money(value, currency);
        }

        // money * 4 -> *(money, 4)
        public static Money operator *(Money money, decimal factor)
        {
            return Money.Of(money.Value * factor, money.Currency);
        }

        public static Money operator *(decimal factor, Money money)
        {
            return Money.Of(money.Value * factor, money.Currency);
        }

        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentException("Different currencies.");
            }
            return Money.Of((a.Value + b.Value), a.Currency);
        }

        public static bool operator >(Money a, Money b)
        {
            IsCurrencyDifferent(a, b);

            return a.Value > b.Value;

        }

        public static bool operator <(Money a, Money b)
        {
            IsCurrencyDifferent(a, b);

            return a.Value < b.Value;


        }

        private static void IsCurrencyDifferent(Money a, Money b)
        {
            if (a.Currency != b.Currency)
            {
                throw new ArgumentException("Different currencies!");
            }
        }

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }

        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }

        public static explicit operator string(Money money)
        {

            return $"{money.Value} {money.Currency}";
        }

        public override string ToString()
        {
            return $"Value: {_value}, Currency: {_currency}";
        }

        public int CompareTo(Money? other)
        {
            int currencyCon = Currency.CompareTo(other.Currency);
            if (currencyCon == 0)
            {
                return Value.CompareTo(other.Value);

            }
            else
            {
                return currencyCon;
            }
        }
    }

    public static class MoneyExtension
    {
        public static Money ToCurrency(this Money money, Currency currency, decimal course)
        {
            if (money.Currency == currency)
            {
                return Money.Of(money.Value, currency);
            }
            else
            {
                return Money.Of(money.Value / course, currency);
            }
        }
    }

    public class Water
    {
        private int _level { get; set; }
        public int Capacity { get; set; }

        public bool consume(int amount)
        {
            if (amount < 0)
            {
                return false;
            }

            if (this._level + amount > this.Capacity)
            {
                return false;
            }
            this._level -= amount;
            return true;
        }

        public void refuel(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be a negative number.");
            }

            if (this._level + amount > this.Capacity)
            {
                throw new ArgumentException("Amount is too large.");
            }
            this._level += amount;

        }
        //1

        public bool refuel(Water source, int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount cannot be a negative number.");
            }
            if (source._level + amount > source.Capacity)
            {
                throw new ArgumentException("Amount is too large.");
            }
            if (this._level < amount)
            {
                throw new ArgumentException("Amount is too large.");
            }

            this._level -= amount;
            source._level += amount;
            return true;

        }
    }

    public class Student : IComparable<Student>
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public double Average { get; set; }

        private Student(string name, string surname, double average)
        {
            Name = name;
            Surname = surname;
            Average = average;
        }

        public static Student? CreateStudent(string name, string surname, double average)
        {
            return average < 0 ? null : new Student(name, surname, average);
        }



        public int CompareTo(Student other)
        {
            int averageCom = Average.CompareTo(other.Average);
            if (averageCom == 0)
            {
                int surnameCom = Surname.CompareTo(other.Surname);
                if (surnameCom == 0)
                {
                    return Name.CompareTo(other.Name);
                }
                else
                {
                    return surnameCom;
                }

            }
            else
            {
                return averageCom;
            }
        }

        public static explicit operator string(Student student)
        {

            return $"{student.Name} {student.Surname} {student.Average}";
        }

        public override string ToString()
        {
            return $"{Name} {Surname} {Average}";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {


            PersonProperties person = PersonProperties.OfName("Artgygy");
            Console.WriteLine(person.FirstName == null ? "NULL" : "PERSON");

            Object obj = person;
            IEquatable<PersonProperties> ie = person;

            Money? money = Money.Of(13, Currency.PLN) ?? Money.Of(0, Currency.PLN);
            Money result = 5 * money;
            Console.WriteLine(person);
            Console.WriteLine("Money value " + money.Value);
            Console.WriteLine("Money value * 5" + result.Value);
            Console.WriteLine("Is bigger than 5 pln? {0}", money < Money.Of(5, Currency.PLN));

            int c = 5;
            long d = 11111111111L;
            d = c; // rzutowanie niejawne
            c = (int)d; // rzutowanie jawne

            decimal cost = money;
            double price = (double)money;
            string str = (string)money;
            Console.WriteLine("Money value (rzutowanie) " + price);
            Console.WriteLine("Money value (rzutowanie na string) " + str);


            Money[] prices =
            {
                Money.Of(10, Currency.PLN),
                Money.Of(32, Currency.EUR),
                Money.Of(41, Currency.PLN),
                Money.Of(15, Currency.USD),
                Money.Of(33, Currency.USD),
                Money.Of(21, Currency.PLN),
                Money.Of(95, Currency.EUR)

            };

            Array.Sort(prices);
            Console.WriteLine("Sorted currencies list:");
            foreach (var p in prices)
            {
                Console.WriteLine((string)p);
            }

            Console.WriteLine();


            Student[] students =
            {
                Student.CreateStudent("Jan","Kowalski", 3.0),
                Student.CreateStudent("Arkadiusz","Nowak", 3.5),
                Student.CreateStudent("Łukasz","Felis", 2.0),
                Student.CreateStudent("Adam","Małysz", 2.0),
                Student.CreateStudent("Natalia","Nowak", 5.0),
                Student.CreateStudent("Robert","Kubica", 4.0),


            };

            Array.Sort(students);
            Console.WriteLine("Sorted students list:");
            foreach (var s in students)
            {
                Console.WriteLine((string)s);
            }
            Console.WriteLine();

            var exchangeResult = money.ToCurrency(Currency.EUR, 4.1m);
            Console.WriteLine("Your money in EUR: {0}", exchangeResult.Value);

            exchangeResult = exchangeResult.ToCurrency(Currency.EUR, 100m);
            Console.WriteLine("After converting from EUR to EUR {0}", exchangeResult.Value);

        }
    }
}
