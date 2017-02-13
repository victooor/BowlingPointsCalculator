using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using BowlingPoint;
using System.Configuration;
using BowlingPointsApplication.Model;

namespace BowlingPointsApplication
{
    class Program
    {
        private static IScoresDataReader reader = new WebScoresDataReader(ConfigurationManager.AppSettings["getUrl"]);
        private static IScoresDataWriter writer = new WebScoresDataWriter(ConfigurationManager.AppSettings["postUrl"]);
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string token;

        static void Main(string[] args)
        {
            try
            {
                BowlingCalculator calculator = new BowlingCalculator();
                string responseData = reader.GetScores();
                var inputObject = JsonConvert.DeserializeObject<InputData>(responseData);

                AddFramesForCalculation(inputObject, calculator);

                calculator.CalculateScore();

                OutputData outputObject = PrepareCalculatedScoresForValidation(inputObject.token, calculator);
                string serializedScores = JsonConvert.SerializeObject(outputObject);
                var calculatorValidator = JsonConvert.DeserializeObject<BowlingCalculatorValidator>(writer.PostScores(serializedScores));
                
                if(!calculatorValidator.success)
                    log.Warn("Calculated WRONGLY for Input: " + responseData + ", Output: " + serializedScores);
                else
                    log.Info("Calculated CORRECTLY for Input: " + responseData + ", Output: " + serializedScores);
            }
            catch (Exception e)
            {
                log.Warn("Error occured - " + e);
            }
        }

        private static OutputData PrepareCalculatedScoresForValidation(string token, BowlingCalculator calculator)
        {
            OutputData toPost = new OutputData();
            toPost.token = token;
            toPost.points = new List<int>();
            for (int i = 0; i < calculator.currentScore.Count; i++)
            {
                toPost.points.Add(calculator.currentScore[i]);
            }
            return toPost;
        }

        private static void AddFramesForCalculation(InputData inputObject, BowlingCalculator calculator)
        {
            foreach (var point in inputObject.points)
            {
                calculator.AddFrame(new Frame(point[0], point[1]));
            }
        }
    }
}
