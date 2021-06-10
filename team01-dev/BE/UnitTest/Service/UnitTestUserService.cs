using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    [TestClass]
    public class UnitTestUserService
    {
        public IUserService _userService;
        public UnitTestUserService(IUserService userService)
        {
            _userService = userService;
        }
        //TestGetInformation
        [TestMethod]
        public void TestIInformationNotNull()
        {
            var result = _userService.GetUser("1");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIInformationValueNotExist()
        {
            var result = _userService.GetUser("-1");
            Assert.IsNull(result);
        }

        //TestLogin
        [TestMethod]
        public void TestILoginNull()
        {
            var result = _userService.Login(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestILoginTrue()
        {
            var result = _userService.Login(new UserLogin() { UserName = "admin1", Password = "123456"});
            Assert.IsTrue(result);
        }
        D
    }
}
