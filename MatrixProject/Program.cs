using System;
using System.Collections;

namespace MatrixProject
{
    class Program
    {
        static int tourNumber = 0; static double totalPath = 0;

        static void Main(string[] args)
        {
            int height = 100; int width = 100; int n = 20;
            Random r = new Random();
            int [] visitedIndexes = new int[n];
            double [][] pointsArray = generatePoints(n, r, width, height);
            generateDistanceMatrix(n, pointsArray);

            int startingIndex = r.Next(n);
            double[] startingPoint = pointsArray[startingIndex];
            double[][] selectedPoints = new double[n][];
            selectedPoints[0] = startingPoint;
            double minDistance = Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height, 2));
            selectedPoints = findNearestNeighbour(pointsArray ,r, n, startingPoint, selectedPoints, minDistance, width, height);
            
            Console.WriteLine();
            Console.WriteLine();
            foreach(double[] element in selectedPoints)
            {
                Console.WriteLine("{0:0.00} {1:0.00}", element[0], element[1]);
            }
            Console.WriteLine(totalPath);

            Console.WriteLine();

            generateDistanceMatrix(n, selectedPoints);

            Console.WriteLine();
            Console.WriteLine();
            visitedIndexes = findVisitedIndex(selectedPoints, pointsArray, visitedIndexes);
            for (int i = 0; i < visitedIndexes.Length; i++)
            {
                Console.WriteLine(visitedIndexes[i]);
            }
        }

        static double[][] generatePoints(int n, Random r, int width, int height)
        {
            double[][] points = new double[n][];

            for (int i = 0; i < n; ++i)
            {
                double[] coordinates = {r.NextDouble() * width, r.NextDouble() * height};
                points[i] = coordinates;
                Console.WriteLine("{0:0.00} {1:0.00}", coordinates[0], coordinates[1]);
                  
            }
            return points;

        }

        static void generateDistanceMatrix(int n, double [][] pointsArray)
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

        
        static double[][] findNearestNeighbour(double [][] pointsArray, Random r, int n, double [] point,
            double [][] selectedPoints, double minDistance, int width, int height)
        {

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
                minDistance = Math.Sqrt(Math.Pow(width, 2) + Math.Pow(height, 2));
                findNearestNeighbour(pointsArray, r, n, selectedPoint, selectedPoints, minDistance, width, height);
            }

            return selectedPoints;
        }
        
        
        static double calculateEuclideanDistance(double x1, double x2, double y1, double y2)
        {
            double d = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            return d;
        }

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

        static int[] findVisitedIndex(double[][] selectedPoints, double[][] pointsArray, int[] visitedIndexes)
        {
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
