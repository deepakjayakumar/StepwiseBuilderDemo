namespace StepwiseBuilderDemo
{

    public enum CarType
    {
        Sedan,
        CrossOver
    }

    public class Car
    {
        public CarType CarType;
        public int WheelSize;

        public override string? ToString()
        {
            return $"{nameof(CarType)}:{CarType}, {nameof(WheelSize)} : {WheelSize}";
        }
    }

    public interface ISpecifycarTpe
    {
        ISpecifyWheelType OfType(CarType carType);
    }

    public interface ISpecifyWheelType
    {
       IBuildCar WithWheels(int wheelSize);
    }

    public interface IBuildCar
    {
        public Car Build();
    }

    public class CarBuilder
    {

        private class Impl
            : ISpecifycarTpe, ISpecifyWheelType, IBuildCar
        {
            private Car car = new Car();    
            public Car Build()
            {
                return car;
            }

            public ISpecifyWheelType OfType(CarType carType)
            {
                car.CarType = carType; return this;
            }

            public IBuildCar WithWheels(int wheelSize)
            {
                switch(car.CarType)
                {
                    case CarType.Sedan when wheelSize < 15 || wheelSize > 17:
                    case CarType.CrossOver when wheelSize < 17 || wheelSize > 22:
                        throw new ArgumentException("Wheel size dows not match");


                }
                car.WheelSize = wheelSize; return this;
            }
        }
        public static ISpecifycarTpe Create()
        {
            return new Impl();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var car = CarBuilder.Create().OfType(CarType.Sedan).WithWheels(16).Build();
             
            Console.WriteLine(car.ToString());
        }
    }
}