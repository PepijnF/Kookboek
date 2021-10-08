using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AbstractionLayer;
using DataLayer;
using LogicLayer;
using NUnit.Framework;
using Moq;
using User = LogicLayer.User;

namespace TestingLayer
{
    public class TestUserDal
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void UserDalTest()
        {
            UserDal userDal = new UserDal();
            UserDto userDto = userDal.FindByUsername("test").Result;
        }
    }
}