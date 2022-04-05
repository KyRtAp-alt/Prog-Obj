using System;

namespace lab2
{

    abstract class AbstractMessage
    {
        public string Content { get; init; }
        abstract public void Send();
    }

    class EmailMessage : AbstractMessage
    {
        public string To { get; init; }
        public string From { get; init; }
        public string Subject { get; init; }

        public override void Send()
        {
            Console.WriteLine($"Sending email from {From} with content {Content}");
        }
    }

    class SmsMessage : AbstractMessage
    {
        public string PhoneNumber { get; init; }
        public override void Send()
        {
            Console.WriteLine($"Sending sms to {PhoneNumber} with content {Content}");
        }
    }

    class MessangerMessage : AbstractMessage
    {
        public override void Send()
        {
            Console.WriteLine($"Sending message: {Content}");
        }
    }

    interface IFly
    {
        void Fly();
    }

    interface ISwim
    {
        void Swim();
    }

    interface IFlyAndSwim : IFly, ISwim
    {
        //zawiera zarówno Fly() i Swim()
    }

    class Duck : IFlyAndSwim
    {
        public void Fly()
        {

        }
        public void Swim()
        {

        }

    }

    class Wasp : IFly
    {
        public void Fly()
        {

        }
    }

    interface IEngine
    {

    }

    class Hydroplane : IFlyAndSwim, IEngine
    {
        public void Fly()
        {

        }
        public void Swim()
        {

        }
    }

    interface IAggregate
    {
        IIterator createIterator();

    }

    interface IIterator
    {
        bool HasNext();
        string GetNext();

    }

    class StringAggregate : IAggregate
    {
        internal string[] names;

        public StringAggregate(string[] names)
        {
            this.names = names;
        }
        public IIterator createIterator()
        {
            return new StringIterator(this);
        }
    }

    class StringIterator : IIterator
    {
        private StringAggregate aggregate;
        private int index = 0;
        public StringIterator(StringAggregate aggregate)
        {
            this.aggregate = aggregate;
        }
        public string GetNext()
        {
            return aggregate.names[index++];
        }

        public bool HasNext()
        {
            return aggregate.names.Length > index;
        }
    }

    class ArrayIntAggregate : IAggregate
    {
        internal int[] array = { 1, 2, 3, 4, 5 };




        public IIterator createIterator()
        {
            return new ArrayIntIterator(this);
        }

    }

    class ArrayIntIterator : IIterator
    {

        private int _index = 0;
        private int _indexReverse = 0;

        private ArrayIntAggregate _aggregate;

        public ArrayIntIterator(ArrayIntAggregate aggregate)
        {
            this._aggregate = aggregate;
            _indexReverse = _aggregate.array.Length - 1;
        }

        public string GetNext()
        {
            throw new Exception();
        }

        public int GetNextInt()
        {
            return _aggregate.array[_index++];
        }

        public int ReverseIterator()
        {

            _index++;
            return _aggregate.array[_indexReverse--];
        }

        public bool HasNext()
        {
            return _aggregate.array.Length > _index;
        }
    }


    class SimpleAggregate : IAggregate
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public IIterator createIterator()
        {
            return new SimpleIterator(this);
        }
    }

    class SimpleIterator : IIterator
    {

        private SimpleAggregate aggregate;
        private int count = 0;

        public SimpleIterator(SimpleAggregate aggregate)
        {
            this.aggregate = aggregate;
        }

        public string GetNext()
        {
            switch (++count)
            {
                case 1:
                    return aggregate.FirstName;

                case 2:
                    return aggregate.LastName;

                default:
                    throw new Exception();

            }
        }

        public bool HasNext()
        {
            return count < 2;
        }
    }

    public abstract class Vehicle
    {
        public double Weight { get; init; }
        public int MaxSpeed { get; init; }
        protected int _mileage;
        public int Mealeage
        {
            get { return _mileage; }
        }
        public abstract decimal Drive(int distance);
        public override string ToString()
        {
            return $"Vehicle{{ Weight: {Weight}, MaxSpeed: {MaxSpeed}, Mileage: {_mileage} }}";
        }
    }

    public abstract class Scooter : Vehicle
    {

    }

    public class ElectricScooter : Scooter
    {
        public int BatteriesLevel { get; set; }
        public int MaxRange = 100;

        public void ChargeBatteries()
        {
            BatteriesLevel = 100;
        }
        public override decimal Drive(int distance)
        {
            if (distance <= BatteriesLevel)
            {
                _mileage += distance;
                BatteriesLevel -= distance;
                return (decimal)(distance / (double)MaxSpeed);
            }

            return -1;
        }
    }

    public class KickScooter : Scooter
    {
        public override decimal Drive(int distance)
        {
            return -1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AbstractMessage[] messages = new AbstractMessage[4];
            messages[0] = new EmailMessage() { Content = "hello", From = "asdf@bcd.com", To = "ff@dd.com", Subject = "hello!" };
            messages[1] = new SmsMessage() { Content = "hello", PhoneNumber = "333555444" };
            messages[2] = new EmailMessage() { Content = "hi", From = "aasddde@eef.com", To = "sadff@fd.com", Subject = "hi!" };
            messages[3] = new MessangerMessage() { Content = "messanger" };
            int mailCounter = 0;
            foreach (var message in messages)
            {
                message.Send();
                // sposob 1
                /*if(message is EmailMessage)
                {
                    mailCounter ++;
                    EmailMessage email = (EmailMessage) message;
                }*/

                //sposob 2
                EmailMessage email = message as EmailMessage;
                mailCounter += email == null ? 0 : 1;

            }
            Console.WriteLine($"Liczba wysłanych emaili: {mailCounter}");

            IFly[] flyingObjects = new IFly[3];
            flyingObjects[0] = new Duck();
            flyingObjects[1] = new Hydroplane();
            flyingObjects[2] = new Wasp();

            ISwim swimming = flyingObjects[0] as ISwim;
            int swimCounter = 0;
            foreach (IFly s in flyingObjects)
            {
                ISwim amISwimming = s as ISwim;
                swimCounter += amISwimming == null ? 0 : 1;
            }
            Console.WriteLine();
            Console.WriteLine($"Swimming objects in array: {swimCounter}");

            string[] names = { "Adam", "Ewa", "Karol" };

            //Działamy na agregatorach i iteratorach, mimo że nie są jeszcze skonkretyzowane
            IAggregate aggregate;
            //Na obu tych aggregatach kod niżej działa poprawnie, mimo że są różne
            aggregate = new StringAggregate(names);
            aggregate = new SimpleAggregate() { FirstName = "Asdf", LastName = "Ghjk" };


            IIterator iterator = aggregate.createIterator();
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.GetNext());
            }
            Console.WriteLine();


            aggregate = new ArrayIntAggregate();
            ArrayIntIterator intIterator = (ArrayIntIterator)aggregate.createIterator();


            Console.WriteLine();
            Console.WriteLine("Cw 3:");

            while (intIterator.HasNext())
            {
                Console.WriteLine(intIterator.ReverseIterator());
            }



        }
    }
}
