using System;
using System.IO;

namespace TennisScoreBoard
{
    class TennisScore
    {
        private const string AdsInPlayer1 = "A-40";
        private const string Deuce = "40-40";

        private const string AdsInPlayer2 = "40-A";
     
        private string directoryPath;
        private string currentSet = "0-0";
        private string completeSet = "";

        public TennisScore(){ 
            directoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }

         public void ReadInputFile() 
         {         
            try
            {
              string filePath = directoryPath + @"\Input.txt";

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)                    
                       CalculateScoreBoard(line);
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void WriteOutputFile(String output)
        {
            try
            {
                string outputFilePath = directoryPath + @"\Output.txt";
                using (StreamWriter outputFile = new StreamWriter(outputFilePath, true))
                {
                    outputFile.WriteLine(output);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be write:");
                Console.WriteLine(e.Message);
            }
        }

        public void CalculateScoreBoard(string line)
        {
            int[] gamePoints = new int[2];
            int[] setPoints = new int[2];
           
            string currentGame = "";

            bool service = true;
            completeSet = "";

            if (!string.IsNullOrEmpty(line))
            {
                for (int i = 0; i < line.Length; i++)
                {
                     if (service)
                         _= (line[i] == 'A') ? gamePoints[0]++ : gamePoints[1]++;
                      else
                        _= (line[i] == 'B') ? gamePoints[0]++ : gamePoints[1]++;

                    currentGame = SetCurrentGame(gamePoints);
 
                    if ((gamePoints[0] >= 4 || gamePoints[1] >=4) && Math.Abs(gamePoints[0] - gamePoints[1]) >= 2 )
                    {                               
                        _= line[i] == 'A' ? setPoints[0]++ : setPoints[1]++;
                        service = !service;
                        currentGame = "";
                        gamePoints[0] = gamePoints[1] = 0;
                    }


                    if (service)
                        currentSet = setPoints[0].ToString() + "-" + setPoints[1].ToString();
                    else
                        currentSet = setPoints[1].ToString() + "-" + setPoints[0].ToString();


                    if ((setPoints[0] >= 6 || setPoints[1] >= 6) && Math.Abs(setPoints[0] - setPoints[1]) >= 2)
                    {
                        completeSet = completeSet+ " " + currentSet;
                        setPoints[0] = setPoints[1]= 0;
                        currentSet = setPoints[0].ToString() + "-" + setPoints[1].ToString();
                    }                                 
                 }              
            }
            String scoreBoard = completeSet + " " + currentSet + " " + currentGame;
            WriteOutputFile(scoreBoard.Trim());
        }
    
        private string SetCurrentGame(int[] points)
        {
            string currentScore = "";
            if (points[0] >= 3 && points[1] >= 3)
            {
                if (points[0] == points[1])
                    currentScore = Deuce;
                else if (points[0] > points[1])
                    currentScore = AdsInPlayer1;
                else
                    currentScore = AdsInPlayer2;
            }

            if (points[0] < 4 && points[1] < 4)
                currentScore = CalculateCurrentScore(points[0]) + "-" + CalculateCurrentScore(points[1]);

            return currentScore;
        }
        private string CalculateCurrentScore(int points)
        {
            switch (points)
            {
                case 3:
                     return "40";
                case 2:
                     return "30";
                case 1:
                     return "15";
                case 0:
                     return "0";
                default:
                     return "";

             }           
        }
    }
}

