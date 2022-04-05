using System.Diagnostics.Tracing;
using System.Drawing;
using System;

class App
{
    public static void Main(string[] args)
    {

    }
}

enum Direction8
{
    UP = 1,
    UP_RIGHT = 2,
    RIGHT = 3,
    DOWN_RIGHT = 4,
    DOWN = 5,
    DOWN_LEFT = 6,
    LEFT = 7,
    UP_LEFT = 8
}

enum Direction4
{
    UP = 1,
    RIGHT = 2,
    DOWN = 3,
    LEFT = 4,

}

//Cwiczenie 1
//Zdefiniuj metodę NextPoint, która powinna zwracać krotkę ze współrzędnymi piksela przesuniętego jednostkowo w danym kierunku względem piksela point.
//Krotka screenSize zawiera rozmiary ekranu (width, height)
//Przyjmij, że początek układu współrzednych (0,0) jest w lewym górnym rogu ekranu, a prawy dolny ma współrzęne (witdh, height) 
//Pzzykład
//dla danych wejściowych 
//(int, int) point1 = (2, 4);
//Direction4 dir = Direction4.UP;
//var point2 = NextPoint(dir, point1);
//w point2 powinny być wartości (2, 3);
//Jeśli nowe położenie jest poza ekranem to metoda powinna zwrócić krotkę z point
class Exercise1
{
    public static (int, int) NextPoint(Direction4 direction, (int, int) point, (int, int) screenSize)
    {
        (int, int) resultPoint = point;
        resultPoint = direction switch
        {
            Direction4.UP => (resultPoint.Item1, resultPoint.Item2 - 1),
            Direction4.RIGHT => (resultPoint.Item1 + 1, resultPoint.Item2),
            Direction4.DOWN => (resultPoint.Item1, resultPoint.Item2 + 1),
            Direction4.LEFT => (resultPoint.Item1 - 1, resultPoint.Item2),
            _ => (resultPoint.Item1, resultPoint.Item2)
        };

        if ((resultPoint.Item1 > screenSize.Item1) || (resultPoint.Item2 > screenSize.Item2))
        {
            return point;
        }
        else
        {
            return resultPoint;
        }


    }

}
//Cwiczenie 2
//Zdefiniuj metodę DirectionTo, która zwraca kierunek do piksela o wartości value z punktu point. W tablicy screen są wartości
//pikseli. Początek układu współrzędnych (0,0) to lewy górny róg, więc punkt o współrzęnych (1,1) jest powyżej punktu (1,2) 
//Przykład
// Dla danych weejsciowych
// static int[,] screen =
// {
//    {1, 0, 0},
//    {0, 0, 0},
//    {0, 0, 0}
// };
// (int, int) point = (1, 1);
// po wywołaniu - Direction8 direction = DirectionTo(screen, point, 1);
// w direction powinna znaleźć się stała UP_LEFT
class Exercise2
{
    static int[,] screen =
    {
        {1, 0, 0},
        {0, 0, 0},
        {0, 0, 0}
    };

    private static (int, int) point = (1, 1);

    private Direction8 direction = DirectionTo(screen, point, 1);

    public static Direction8 DirectionTo(int[,] screen, (int, int) point, int value)
    {
        int x = 0;
        int y = 0;
        (int, int) placeOfPixel = (0, 0);
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (screen[i, j] == value)
                {
                    placeOfPixel = (i, j);
                    x = placeOfPixel.Item1;
                    y = placeOfPixel.Item2;
                }
            }
        }
        /*
        Tutaj próbowałem wyrażeniem switch, ale musi do tego być użyta stała, więc cały czas wyświetlało mi błąd :(
        int pointX = point.Item1;
        int pointY = point.Item2;
        
        int xDirection = x switch
        {
            < pointX => 0,
            pointX => 1,
            > pointX => 2
        };
        int yDirection = placeOfPixel switch
        {
            placeOfPixel.Item2 < point.Item2 => 0, //w lewo
            placeOfPixel.Item2 == point.Item2 => 1,
            placeOfPixel.Item2 > point.Item2 => 2
        };
        */
        int xDirection;
        if (x < point.Item1)
        {
            xDirection = 0;
        }
        else if (x == point.Item1)
        {
            xDirection = 1;
        }
        else
        {
            xDirection = 2;
        }


        int yDirection;
        if (y < point.Item1)
        {
            yDirection = 0;
        }
        else if (y == point.Item1)
        {
            yDirection = 1;
        }
        else
        {
            yDirection = 2;
        }

        (int, int) directionTuple = (xDirection, yDirection);
        if (directionTuple == (1, 1))
        {
            throw new Exception("point is on the same place as value");
        }
        Direction8 direction = directionTuple switch
        {
            (0, 0) => Direction8.UP_LEFT,
            (0, 1) => Direction8.UP,
            (0, 2) => Direction8.UP_RIGHT,
            (1, 0) => Direction8.LEFT,
            (1, 2) => Direction8.RIGHT,
            (2, 0) => Direction8.DOWN_LEFT,
            (2, 1) => Direction8.DOWN,
            (2, 2) => Direction8.DOWN_RIGHT

        };

        return direction;
        // sprawdzam czy jest w bezposrednim sasiedztwie ten punkt 1
        // sprawdzam ktore z wspolrzednych wieksze i ktore mniejsze (lub takie same) i na podstawie tego "składam" kierunek
    }
}

//Cwiczenie 3
//Zdefiniuj metodę obliczającą liczbę najczęściej występujących aut w tablicy cars
//Przykład
//dla danych wejściowych
// Car[] _cars = new Car[]
// {
//     new Car(),
//     new Car(Model: "Fiat", true),
//     new Car(),
//     new Car(Power: 100),
//     new Car(Model: "Fiat", true),
//     new Car(Power: 125),
//     new Car()
// };
//wywołanie CarCounter(Car[] cars) powinno zwrócić 3
record Car(string Model = "Audi", bool HasPlateNumber = false, int Power = 100);

class Exercise3
{
    public static int CarCounter(Car[] cars)
    {
        int biggestCounter = 0;
        int actualCounter = 0;
        foreach (Car car in cars)
        {
            foreach (Car car2 in cars)
            {
                if (car == car2)
                {
                    actualCounter++;
                }

            }
            if (actualCounter > biggestCounter)
            {
                biggestCounter = actualCounter;
            }
            actualCounter = 0;
        }

        return biggestCounter;
    }
}

record Student(string LastName, string FirstName, char Group, string StudentId = "");
//Cwiczenie 4
//Zdefiniuj metodę AssignStudentId, która każdemu studentowi nadaje pole StudentId wg wzoru znak_grupy-trzycyfrowy-numer.
//Przykład
//dla danych wejściowych
//Student[] students = {
//  new Student("Kowal","Adam", 'A'),
//  new Student("Nowak","Ewa", 'A')
//};
//po wywołaniu metody AssignStudentId(students);
//w tablicy students otrzymamy:
// Kowal Adam 'A' - 'A001'
// Nowal Ewa 'A'  - 'A002'
//Należy przyjąc, że są tylko trzy możliwe grupy: A, B i C
class Exercise4
{
    public static void AssignStudentId(Student[] students)
    {

    }
}