﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CodingDojo_BowlingScore;

namespace CodingDojo_BowlingScore_Test
{
    [TestClass]
    public class BowlingFrameTests
    {
        [TestMethod]
        public void Bowling_Frame_Should_Throw_Exception_If_You_Try_To_Roll_4_Balls()
        {
            var frame = new BowlingFrame(1);
            bool exceptionThrown = false;
            
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    frame.throwBall(i);
                }
            }
            catch(BowlingFrameOverException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void Frame_Should_Throw_Exception_If_You_Try_To_Throw_3_Balls_Before_Frame_10()
        {
            var frame = new BowlingFrame(9);
            bool exceptionThrown = false;

            try
            {
                for (int i = 0; i < 4; i++)
                {
                    frame.throwBall(4);
                }
            }
            catch (BowlingFrameOverException e)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void Frame_With_Two_Small_Balls_Should_Not_Be_A_Spare_Or_Strike()
        {
            var frame = new BowlingFrame(1);
            frame.throwBall(3);
            frame.throwBall(5);
            Assert.IsFalse(frame.IsSpare);
            Assert.IsFalse(frame.IsStrike);
        }

        [TestMethod]
        public void Frame_With_Two_Balls_That_Add_Up_To_Ten_Should_Be_A_Spare()
        {
            var frame = new BowlingFrame(1);
            frame.throwBall(6);
            frame.throwBall(4);
            Assert.IsTrue(frame.IsSpare);
            Assert.IsTrue(frame.IsStrike);
        }

        [TestMethod]
        public void Frame_With_First_Ball_Knocking_Over_10_Pins_Should_Be_A_Strike()
        {
            var frame = new BowlingFrame(1);
            frame.throwBall(10);
            Assert.IsTrue(frame.IsStrike);
            Assert.IsFalse(frame.IsSpare);
        }

        [TestMethod]
        public void Frame_With_Second_Ball_Knocking_Over_10_Pins_Should_Be_A_Spare()
        {
            var frame = new BowlingFrame(1);
            frame.throwBall(0);
            frame.throwBall(10);
            Assert.IsTrue(frame.IsSpare);
            Assert.IsFalse(frame.IsStrike);
        }

        [TestMethod]
        public void Frame_With_Strike_Should_End_If_Not_Tenth_Frame()
        {
            var frame = new BowlingFrame(1);
            frame.throwBall(10);
            Assert.IsTrue(frame.IsOver);
        }

        [TestMethod]
        public void Frame_With_Spare_Should_End_If_Not_Tenth_Frame()
        {
            var frame = new BowlingFrame(1);
            frame.throwBall(8);
            frame.throwBall(2);
            Assert.IsTrue(frame.IsOver);
        }

        public void Tenth_Frame_Should_Allow_3_Throws_If_Strike()
        {
            var frame = new BowlingFrame(10);
            frame.throwBall(10);
            Assert.IsFalse(frame.IsOver);
            frame.throwBall(10);
            Assert.IsFalse(frame.IsOver);
            frame.throwBall(10);
            Assert.IsTrue(frame.IsOver);
        }

        public void Tenth_Frame_Should_Allow_3_Throws_If_Spare()
        {
            var frame = new BowlingFrame(10);
            frame.throwBall(6);
            Assert.IsFalse(frame.IsOver);
            frame.throwBall(4);
            Assert.IsFalse(frame.IsOver);
            frame.throwBall(8);
            Assert.IsTrue(frame.IsOver);
        }

        public void Frame_Should_Throw_Exception_If_Throws_Add_Up_To_More_Than_10()
        {
            var frame = new BowlingFrame(4);
            bool exceptionWasThrown = false;

            try
            {
                frame.throwBall(7);
                frame.throwBall(4);
            }
            catch (InvalidBowlingFrameException)
            {
                exceptionWasThrown = true;
            }

            Assert.IsTrue(exceptionWasThrown);
        }
    }
}