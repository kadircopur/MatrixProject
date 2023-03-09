using System;
using System.Collections.Generic;

namespace MatrixProject
{
    class Program
    {
        static int tourNumber = 0; 
        static double totalPath = 0;
        static int height = 100; 
        static int width = 100; 
        static int n = 20;
        static Random r = new Random();

        static void Main(string[] args)
        {
            double [][] pointsArray = generatePoints(); // Random points' values
            List<int> startingIndexes = new List<int>();
            Console.WriteLine("\nDISTANCE MATRIX");
            Console.WriteLine("---------------");
            generateDistanceMatrix(pointsArray);
            Console.WriteLine("\n");

            // 10 defa dönecek --> 10 farklı başlangıç noktası için
            for (int i = 0; i < 10; i++)
            {
                tourNumber = 0;
                totalPath = 0;
                int startingIndex = r.Next(n);
                
                // Changes the starting index if it is used
                while (startingIndexes.Contains(startingIndex))
                {
                    startingIndex = r.Next(n);
                }
               
                startingIndexes.Add(startingIndex);
                double[] startingPoint = pointsArray[startingIndex];
                double[][] selectedPoints = new double[n][];
                selectedPoints[0] = startingPoint;
                tourNumber++;
                selectedPoints = findNearestNeighbour(pointsArray, startingPoint, selectedPoints);

                Console.WriteLine("\n");
                Console.WriteLine("Tur Sayısı: {0}", i + 1);

                int[] visitedIndexes = findVisitedIndex(selectedPoints, pointsArray);
                Console.WriteLine("\nUğranılan Noktalar");
                Console.WriteLine("------------------");
                
                for (int j = 0; j < visitedIndexes.Length; j++)
                {
                    Console.Write("{0}  ", visitedIndexes[j]);
                }
                
                Console.WriteLine("\n\nToplam Yol Uzunluğu = {0:0.00}", totalPath);
                Console.WriteLine("");
            }
            
        }

        // Generates random x and y values
        static double[][] generatePoints()
        {
            double[][] points = new double[n][];

            Console.WriteLine("\nRandom Generated Points");
            Console.WriteLine("------------------------");
            for (int i = 0; i < n; ++i)
            {
                double[] coordinates = {r.NextDouble() * width, r.NextDouble() * height};
                points[i] = coordinates;
                Console.WriteLine("X değeri: {0:0.00}  Y değeri: {1:0.00}", coordinates[0], coordinates[1]);
            }
            return points;
        }

        static void generateDistanceMatrix(double [][] pointsArray)
        {
            double[,] distanceMatrix = new double[n, n];

            for (int i = 0; i < n; ++i)
            {
                double x1 = pointsArray[i][0];
                double y1 = pointsArray[i][1];

                for (int j = 0; j < n; ++j)
                {
                    double x2 = pointsArray[j][0];
                    double y2 = pointsArray[j][1];
                    distanceMatrix[i, j] = calculateEuclideanDistance(x1, x2, y1, y2);
                }
            }
            for (int i = 0; i < n; ++i)
            {
                Console.WriteLine();
                for (int j = 0; j < n; ++j)
                {
                    Console.Write(String.Format("{0:0.00} \t", distanceMatrix[i, j]));
                }
            }          
        }
        
        static double[][] findNearestNeighbour(double [][] pointsArray, double [] point, double [][] selectedPoints)
        {
            double minDistance = Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height, 2)); // It is the maximum distance of two points in given space that could be

            double x1 = point[0]; double x2 = 0;
            double y1 = point[1]; double y2 = 0;
            double[] selectedPoint = new double[2];
           
            for (int i = 0; i < pointsArray.Length; ++i)
            {
                x2 = pointsArray[i][0];
                y2 = pointsArray[i][1];
                    
                if (!isHave(pointsArray[i], selectedPoints) && calculateEuclideanDistance(x1, x2, y1, y2) < minDistance)
                {
                    selectedPoint[0] = x2; selectedPoint[1] = y2;
                    minDistance = calculateEuclideanDistance(x1, x2, y1, y2);
                }
            }
            
            if (selectedPoints[n-1] == null)
            {
                totalPath += minDistance;
                selectedPoints[tourNumber] = selectedPoint;
                tourNumber++; 
                findNearestNeighbour(pointsArray, selectedPoint, selectedPoints);
            }

            return selectedPoints;
        }
        
        // Calculates distance between two points
        static double calculateEuclideanDistance(double x1, double x2, double y1, double y2)
        {
            double d = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            return d;
        }

        // Checks if the given point is inside the selected points
        static Boolean isHave(double [] point, double[][] selectedPoints)
        {
            for (int i = 0; i < selectedPoints.Length; ++i)
            {   
                if (selectedPoints[i] != null && (selectedPoints[i][0] == point[0]) && (selectedPoints[i][1] == point[1]))
                {
                    return true;
                }
            }
            return false;
        }

        // Finds the indexes of selected points from points array
        static int[] findVisitedIndex(double[][] selectedPoints, double[][] pointsArray)
        {
            int[] visitedIndexes = new int[n];

            for (int i = 0; i < pointsArray.Length; i++)
            {
                for (int j = 0; j < pointsArray.Length; j++)
                {
                    if (pointsArray[j][0] == selectedPoints[i][0] && pointsArray[j][1] == selectedPoints[i][1])
                    {
                        visitedIndexes[i] = j;
                    }
                }
            }
            return visitedIndexes;
        }
    }
}
