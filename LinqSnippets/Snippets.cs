using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;



namespace LinqSnippets
{
    public class Snippets
    {
        static public void BasicLinq()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A4",
                "Fiat Punto",
                "Seat ibiza"
            };

            // 1. SELECT * de todos los autos

            var carlist = from car in cars select car;

            foreach (var car in carlist)
            {
                Console.WriteLine(car);
            }

            // 2. SELECT WHERE solo los AUDI

            var audilist = from car in cars where car.Contains("Audi") select car;

            foreach (var audi in audilist)
            {
                Console.WriteLine(audi);
            }
        }
        //Ejemplo con numeros

        static public void LinqNumbers()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 3, 5, 6, 7, 8, 9 };

            //Obtener cada  numero multiplicado por 3
            // take obtenemos todos menos el 9
            //Ordenamos en forma ascendente

            var processedNumberList =
                numbers
                .Select(num => num * 3) // { 3,6,9,etc..}
                .Where(num => num != 9) // {all but the 9}
                .OrderBy(num => num); // at the end, we order ascending
        }

        static public void SearchEamples()
        {
            List<string> textList = new List<string>
            {
            "a",
            "bx",
            "c",
            "d",
            "e",
            "cj",
            "f",
            "c"
            };

            // 1. Encontrar el primero de los elementos

            var first = textList.First();

            // 2. Primer elemento que tenga la letra "C"

            var cText = textList.First(text => text.Equals("c"));

            // 3. Primer elemento que contenga la "J"

            var jText = textList.First(text => text.Contains("j"));

            // 4. Primer elemento que contenga la "Z" y sino un valor por defecto

            var fistOrDefaultText = textList.FirstOrDefault(text => text.Contains("z")); // Nos va a dar una lista vacia o un valor que contiene la "Z

            // 5. Vamos a elegir el ultimo elemento o devolver un valor nulo

            var lastOrDefaultText = textList.LastOrDefault(text => text.Contains("z"));

            // 6. Solicitar solo un valor de los repetidos

            var uniqueText = textList.Single();
            var uniqueOrDefaultText = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };

            int[] otherEvetNumbers = { 0, 2, 6 };


            // si quisieramos obtener el 4 y el 8 que son los que no se repiten

            var myEventNumbers = evenNumbers.Except(otherEvetNumbers); // {4,8} 

        }

        // SELECTS un poco mas complejos
        static public void MultipleSelects()
        {

            // SELECT MANY Seleccion multiple

            string[] myOpinions =
                {
                "Option 1, text 1",
                "Option 2, text 2",
                "Option 3, text 3"
                };

            var myOpinionSelections = myOpinions.SelectMany(opinion => opinion.Split(opinion));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id= 1,
                    Name = "Enterprise 1",
                    Employees= new[]
                    {
                        new Employee {
                            Id= 1,
                            Name = "Lolito",
                            Email = "lolito_@example.com",
                            Salary = 3000},

                        new Employee {
                            Id= 2,
                            Name = "Ricardo",
                            Email = "Ricardo_@example.com",
                            Salary = 2000},

                        new Employee {
                            Id= 3,
                            Name = "Franco",
                            Email = "franquito_@example.com",
                            Salary = 5000}

                    }
                },

                 new Enterprise()
                {
                    Id = 2,
                    Name = "Enterprise 2",
                    Employees = new[]
                    {
                        new Employee {
                            Id= 4,
                            Name = "Ana",
                            Email = "Anita@example.com",
                            Salary = 3500},

                        new Employee {
                            Id= 5,
                            Name = "Rogelio",
                            Email = "Rogelio@example.com",
                            Salary = 2100},

                        new Employee {
                            Id= 6,
                            Name = "Alexis",
                            Email = "Alessit@example.com",
                            Salary = 3000}

                    }
                 }

            };

            // Obtener todos los empleados de todas las empresas

            var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

            // Saber si tenemos una lista vacia o cualquier lista esta vacia

            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());


            // Al menos una o todas al menos tengan empleados de mas de 1000 de salario


            bool hasEmployeeWithSalaryMoreThanOrEqual1000 =
                enterprises.Any(enterprise =>
                                        enterprise.Employees.Any(employee =>
                                                                      employee.Salary >= 1000));
        }

        //Colecciones

        static public void linqCollections()
        {

            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            // INNER JOIN van a ser los elmentos que sean compartidos entre las dos listas

            var commonResult = from element in firstList
                               join secondElement in secondList
                               on element equals secondElement
                               select new { element, secondElement };

            // otra forma


           /* var commonresult2 = firstList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element, secondElement) => new(element, secondElement)
                );*/


            // OUTER JOIN - LEFT
            var leftOuterJoin = from element in firstList
                                join secondElement in secondList
                                on element equals secondElement
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };


            // Otra forma

            var leftOuterJoin2 = from element in firstList
                                 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondElement };



            // OUTER JOIN RIGTH

            var rigthOuterJoin = from secondElement in firstList
                                 join element in firstList
                                 on secondElement equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where secondElement != temporalElement
                                 select new { Element = secondElement };

            // UNION

            var inuionList = leftOuterJoin.Union(rigthOuterJoin);
        }

        // Saltar elementos para buscar mas rapido las busquedad o paginados


        static public void SkiptakeLinq()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            var skipTwoFirstValues = myList.Skip(2); // Me devuelve todos menos los dos primeros numeros

            var skipLastTwoValues = myList.SkipLast(2); // Me devuleve todos menos los ultimos dos elementos

            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); // Me va a devolver los elementos mayores que 4 con 4 inclusive

            // TAKE

            var takeFirstTwoValues = myList.Take(2); // Me devuelve solo los dos primeros elementos


            var takeLAstTwoValues = myList.TakeLast(2); // Me devuelve los ultimos dos elementos


            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); // Me devuelve todos los elementos que no sean mayor que 4 
        }


        // Paging o paginacion con Skip & Take

        static public IEnumerable<T> GetPage<T>(IEnumerable<T> colection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return colection.Skip(startIndex).Take(resultsPerPage);
        }

        // Variables

        static public void LinqVariables()
        {

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };


            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquared = Math.Pow(number, 2)
                               where nSquared > average
                               select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number: {0} Square: {1} ", number, Math.Pow(number, 2));
            }

        }

        // ZIP 

        static public void ZipLinq()
        {

            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringsNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringsNumbers, (number, word) => number + "=" + word);

            // {"1 = one", "2 = two", ...}

        }


        // Repeat & range

        static public void repeatRangeLinq()
        {

            // Generar una coleccion de valores del 1 al 1000 --> Range

            IEnumerable<int> first1000 = Enumerable.Range(1, 1000);


            // Repeat un valor N veces

            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); // {"X","X","X","X","X"}


        }


        static public void studentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {

                    Id = 1,
                     Name = "Alexis",
                     Grade = 90,
                     Certified = true,
                },
                 new Student
                {

                    Id = 2,
                     Name = "Franco",
                     Grade = 50,
                     Certified = true,
                },
                  new Student
                {

                    Id = 3,
                     Name = "Ramon",
                     Grade = 30,
                     Certified = false,
                },
                   new Student
                {

                    Id = 4,
                     Name = "Marcos",
                     Grade = 60,
                     Certified = true,
                }
            };
            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudents = from student in classRoom
                                       where student.Certified == false
                                       select student;

            var appovedStudents = from student in classRoom
                                  where student.Grade >= 50 && student.Certified == true
                                  select student.Name;


        }


        // All



        static public void AllLinq()
        {
            var numbers = new List<int>() { 1, 2, 3, 4, 5 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10); // true

            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); // false

            var emptyList = new List<int>() { };

            bool alNumbersAreGreaterThan0 = numbers.All(x => x >= 0); //true

        }

        // Aggregate 
        static public void aggregateQueries()
        {

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Sum all numbers

            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);


            string[] words = { "Hello", "my", "name", "is", "Alexis" };

            string greeting = words.Aggregate((prevGreeting, current) => prevGreeting + current);

        }

        // Distinct


        static public void distictValues()
        {

            int[] numbers = { 1, 2, 3, 4, 5, 5, 4, 3, 2, 1 };

            IEnumerable<int> distinctValues = numbers.Distinct();

        }


        // GroupBy

        static public void groupByExamples()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // Obtener los valores pares

            var grouped = numbers.GroupBy(x => x % 2 == 0);


            // Vamos a tener dos ejemplos
            // 1. El grupo que no cumple
            // 2. el grupo que si cumple


            foreach (var group in grouped)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value); // 1,3,5,7,9 ... 2,4,6,8 primero los que no lo cumplen y luego el que cumple
                }
            }



            // Another example

            var classRoom = new[]
           {
                new Student
                {

                    Id = 1,
                     Name = "Alexis",
                     Grade = 90,
                     Certified = true,
                },
                 new Student
                {

                    Id = 2,
                     Name = "Franco",
                     Grade = 50,
                     Certified = true,
                },
                  new Student
                {

                    Id = 3,
                     Name = "Ramon",
                     Grade = 30,
                     Certified = false,
                },
                   new Student
                {

                    Id = 4,
                     Name = "Marcos",
                     Grade = 60,
                     Certified = true,
                }
            };


            var certifiedQuery = classRoom.GroupBy(student => student.Certified && student.Grade >= 50);

            // We obtain two groups
            // 1. Not certified students
            // 2. certified Students

            foreach (var group in grouped)
            {
                Console.WriteLine("-----{0}------", group.Key);
                foreach (var student in group)
                {
                    Console.WriteLine(student);
                }
            }

        }


        static public void relationsLinq()
        {

            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 1,
                            Created = DateTime.Now,
                            Title = "My first comment",
                            Content = "My first content"
                        },
                        new Comment()
                        {
                            Id = 2,
                            Created = DateTime.Now,
                            Title = "My two comment",
                            Content = "My two content"
                        },
                        new Comment()
                        {
                            Id = 3,
                            Created = DateTime.Now,
                            Title = "My three comment",
                            Content = "My three content"
                        },
                        new Comment()
                        {
                            Id = 4,
                            Created = DateTime.Now,
                            Title = "My four comment",
                            Content = "My four content"
                        }




                    }
                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Id = 5,
                            Created = DateTime.Now,
                            Title = "My five comment",
                            Content = "My first content"
                        },
                        new Comment()
                        {
                            Id = 6,
                            Created = DateTime.Now,
                            Title = "My six comment",
                            Content = "My two content"
                        },
                        new Comment()
                        {
                            Id = 7,
                            Created = DateTime.Now,
                            Title = "My seven comment",
                            Content = "My three content"
                        },
                        new Comment()
                        {
                            Id = 8,
                            Created = DateTime.Now,
                            Title = "My eigth comment",
                            Content = "My four content"
                        }




                    }


                }

            };


#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            var commentsContent = posts.SelectMany(post
                 => post.Comments,
                 (post, comment) => new {PostId = post.Id, CommentContent = comment.Content});
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo

        }
    }
}