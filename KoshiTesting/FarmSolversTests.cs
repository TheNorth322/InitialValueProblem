﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using InitialValueProblemSolvers;
using System;
using System.Xml.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace InitialValueProblemSolvers.Tests
{
    class TestSolver : Solver
    {
        public TestSolver(string _Name, Behavior _Behaviour) : base(_Name, _Behaviour) { }

        public override void SolveKoshiTask(InitialValueProblem Task)
        {
            return; // as if the code here has worked and returns
        }
    }

    [TestClass()]
    public class FarmTests
    {
        private Farm _anInstance = new Farm();

        public void GenerateTestName(string Name="Default")
        {
            Behavior Behavior = (Behavior)Convert.ToByte(1);
            Solver testSolver = new TestSolver(Name, Behavior);

            _anInstance.Solvers.Add(testSolver);
        }

        [TestMethod()]
        public void AddSolverTest()
        {
            var Name = "Testing";
            Behavior Behavior = (Behavior)Convert.ToByte(1);
            Solver testSolver = new TestSolver(Name, Behavior);

            _anInstance.AddSolver(testSolver);

            Assert.AreEqual(testSolver.Name, Name, "Solver changes name");
            Assert.AreEqual(testSolver.Behavior, Behavior, "Solver changes name");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "There is no such solver")]
        public void FindSolverByName_ThrowExceptionTest()
        {
            var NonExistentName = "non-existent name";
            _anInstance.FindSolverByName(NonExistentName);
        }

        [TestMethod()]
        public void FindSolverByNameTest()
        {
            var TestName = "test_name";
            GenerateTestName(TestName);
            int index = _anInstance.FindSolverByName(TestName); //returns an index, right?

            Assert.AreEqual(_anInstance.Solvers[index].Name, TestName);
        }

        [TestMethod()]
        public void SolveTaskTest()
        {
            GenerateTestName();
            
            InitialValueProblem Task = new InitialValueProblem(1.0, 1.0, 1.1, 1.0);
            _anInstance.SolveTask(Task);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "There is no such solver")]
        public void DeleteSolver_ThrowExceptionTest()
        {
            var NonExistentName = "non-existent name";
            _anInstance.DeleteSolver(NonExistentName);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException),
            "There is no such solver")]
        public void DeleteSolverTest()
        {
            var ExistentName = "test_name";
            GenerateTestName(ExistentName);

            _anInstance.DeleteSolver(ExistentName);

            _anInstance.FindSolverByName(ExistentName);
        }

        [TestMethod()]
        [ExpectedException(typeof(ApplicationException),
            "There is already such solver")]
        public void CheckNameRepeatTest()
        {
            var Name = "Repiting name";
            Behavior Behavior = (Behavior)(1);

            _anInstance.Solvers.Add(new TestSolver(Name, Behavior));
            _anInstance.CheckNameRepeat(Name);
        }
    }
}