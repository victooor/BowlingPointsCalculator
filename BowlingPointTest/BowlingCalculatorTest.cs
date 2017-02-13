using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingPoint;
using System.Collections.Generic;

namespace BowlingPointTest
{
    [TestClass]
    public class BowlingCalculatorTest
    {
        public BowlingCalculator calculator;

        [TestInitialize]
        public void TestSetUp()
        {
            calculator = new BowlingCalculator();
        }


        [TestMethod]
        public void CalculateScore_When_All_Balls_Gutterball()
        {
            // Arrange
            var expected = new List<int>();

            // Act
            calculator.CalculateScore();
            var actual = (List<int>)calculator.currentScore;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScore_When_All_Balls_1()
        {
            // Arrange
            var expected = new List<int> { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
            for (int i = 0; i < 10; i++) 
            {
                calculator.AddFrame(new BowlingPoint.Frame(1, 1));
            }
            
            // Act
            calculator.CalculateScore();
            var actual = (List<int>)calculator.currentScore;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScore_When_Strike_And_Rest_Balls_1()
        {
            // Arrange
            var expected = new List<int> { 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
            calculator.AddFrame(new BowlingPoint.Frame(10, 0));
            for (int i = 0; i < 9; i++)
            {
                calculator.AddFrame(new BowlingPoint.Frame(1, 1));
            }

            // Act
            calculator.CalculateScore();
            var actual = (List<int>)calculator.currentScore;

            // Asset
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScore_When_All1_And_Last_Spare()
        {
            // Arrange
            var expected = new List<int> { 2, 4, 6, 8, 10, 12, 14, 16, 18, 32};
            for (int i = 0; i < 9; i++)
            {
                calculator.AddFrame(new BowlingPoint.Frame(1, 1));
            }
            calculator.AddFrame(new BowlingPoint.Frame(3, 7));
            calculator.AddFrame(new BowlingPoint.Frame(4, 0));

            // Act
            calculator.CalculateScore();
            var actual = (List<int>)calculator.currentScore;

            // Arrange
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScore_When_All1_And_Last_Spare_NoBonusBall()
        {
            // Arrange
            var expected = new List<int> { 2, 4, 6, 8, 10, 12, 14, 16, 18 };
            for (int i = 0; i < 9; i++)
            {
                calculator.AddFrame(new BowlingPoint.Frame(1, 1));
            }
            calculator.AddFrame(new BowlingPoint.Frame(3, 7));

            // Act
            calculator.CalculateScore();
            var actual = (List<int>)calculator.currentScore;

            // Arrange
            CollectionAssert.AreEqual(expected, actual);
        }
        

        [TestMethod]
        public void CalculateScore_When_All1_And_Last_Spare_But_Not_LastBall()
        {
            // Arrange
            var expected = new List<int> { 2, 4, 6 };
            for (int i = 0; i < 3; i++)
            {
                calculator.AddFrame(new BowlingPoint.Frame(1, 1));
            }
            calculator.AddFrame(new BowlingPoint.Frame(3, 7));

            // Act
            calculator.CalculateScore();
            var actual = (List<int>)calculator.currentScore;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void CalculateScore_When_TwoStrike_And_Rest_Balls_1()
        {
            // Arrange
            var expected = new List<int> { 21, 33, 35, 37, 39, 41, 43, 45, 47, 49 };
            calculator.AddFrame(new BowlingPoint.Frame(10, 0));
            calculator.AddFrame(new BowlingPoint.Frame(10, 0));
            for (int i = 0; i < 8; i++)
            {
                calculator.AddFrame(new BowlingPoint.Frame(1, 1));
            }

            // Act
            calculator.CalculateScore();
            var actual = (List<int>)calculator.currentScore;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScore_When_Spare_And_Rest_Balls_1()
        {
            // Arrange
            var expected = new List<int> { 11, 13, 15, 17, 19, 21, 23, 25, 27, 29 };
            calculator.AddFrame(new BowlingPoint.Frame(3, 7));
            for (int i = 0; i < 9; i++)
            {
                calculator.AddFrame(new BowlingPoint.Frame(1, 1));
            }

            // Act
            calculator.CalculateScore();
            var actual = (List<int>)calculator.currentScore;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScore_When_All_Strikes()
        {
            // Arrange
            var expected = new List<int> { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300 };
            for (int i = 0; i < 10; i++)
            {
                calculator.AddFrame(new BowlingPoint.Frame(10, 0));
            }
            calculator.AddFrame(new BowlingPoint.Frame(10, 10));

            // Act
            calculator.CalculateScore();
            var actual = (List<int>)calculator.currentScore;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScore_When_FailingLiveTest()
        {
            // Arrange
            var expected = new List<int> { 16, 25, 31, 37 };
            calculator.AddFrame(new BowlingPoint.Frame(0, 10));
            calculator.AddFrame(new BowlingPoint.Frame(6, 3));
            calculator.AddFrame(new BowlingPoint.Frame(5, 1));
            calculator.AddFrame(new BowlingPoint.Frame(3, 3));
            calculator.AddFrame(new BowlingPoint.Frame(10, 0));
            calculator.AddFrame(new BowlingPoint.Frame(10, 0));

            // Act
            calculator.CalculateScore();
            var actual = (List<int>)calculator.currentScore;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScore_When_Given_Example()
        {
            // Arrange
            var expected = new List<int> { 20, 40, 58, 67, 84, 91, 98, 111, 116, 123 };
            calculator.AddFrame(new BowlingPoint.Frame(3, 7));
            calculator.AddFrame(new BowlingPoint.Frame(10, 0));
            calculator.AddFrame(new BowlingPoint.Frame(8, 2));
            calculator.AddFrame(new BowlingPoint.Frame(8, 1));
            calculator.AddFrame(new BowlingPoint.Frame(10, 0));
            calculator.AddFrame(new BowlingPoint.Frame(3, 4));
            calculator.AddFrame(new BowlingPoint.Frame(7, 0));
            calculator.AddFrame(new BowlingPoint.Frame(5, 5));
            calculator.AddFrame(new BowlingPoint.Frame(3, 2));
            calculator.AddFrame(new BowlingPoint.Frame(2, 5));

            // Act
            calculator.CalculateScore();
            var actual = (List<int>)calculator.currentScore;

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

    }
}
