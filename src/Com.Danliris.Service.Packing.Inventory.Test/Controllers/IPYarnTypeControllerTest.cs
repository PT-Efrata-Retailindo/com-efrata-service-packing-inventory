using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.IPYarnType;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Com.Danliris.Service.Packing.Inventory.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers
{
    public class IPYarnTypeControllerTest
    {
        private IIPYarnTypeService _serviceMock;
        private IIdentityProvider _identityProvider;
        private IPYarnTypeController _controllerMock;
        private IValidateService _validateMock;

        public IPYarnTypeControllerTest(bool isException = false,bool isValidate = false)
        {
            if (isException)
            {
                _serviceMock = GetGlobalDefaulExceptionMock().Object;
                //_validateMock = GetGlobalValidate().Object;
            }
            else
            {
                _serviceMock = GetGlobalDefaultMock().Object;
                //_validateMock = GetGlobalExceptionValidate().Object;
            }
            if(isValidate)
                _validateMock = GetGlobalExceptionValidate().Object;

            else
                _validateMock = GetGlobalValidate().Object;

            _identityProvider = GetGlobalIndetityProvider().Object;
            _controllerMock = GetController();
        }
        

        //private IPWidthTypeController GetController(IIPWidthTypeService service, IIdentityProvider identityProvider)
        //{
        //    var claimPrincipal = new Mock<ClaimsPrincipal>();
        //    var claims = new Claim[]
        //    {
        //        new Claim("username", "unittestusername")
        //    };
        //    claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

        //    var controller = new IPWidthTypeController(service, identityProvider)
        //    {
        //        ControllerContext = new ControllerContext()
        //        {
        //            HttpContext = new DefaultHttpContext()
        //            {
        //                User = claimPrincipal.Object

        //            }
        //        }
        //    };
        //    controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
        //    controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";
        //    controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

        //    return controller;
        //}

        public IPYarnTypeController GetController()
        {
            var claimPrincipal = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            claimPrincipal.Setup(claim => claim.Claims).Returns(claims);

            var controller = new IPYarnTypeController(_serviceMock, _identityProvider, _validateMock)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = claimPrincipal.Object

                    }
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");

            return controller;
        }

        private int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        private IPYarnTypeViewModel ViewModel
        {
            get
            {
                return new IPYarnTypeViewModel
                {
                    Id = 1,
                    Code = "1",
                    YarnType = "Testing"
                };
            }
        }
        private IPYarnTypeViewModel NotValidViewModel
        {
            get
            {
                return new IPYarnTypeViewModel
                {
                    Id = 1,
                    Code = "1",
                    YarnType = null
                };
            }
        }

        private Mock<IIPYarnTypeService> GetGlobalDefaultMock()
        {
            var serviceMock = new Mock<IIPYarnTypeService>();
            serviceMock.Setup(s => s.ReadAll())
                .Returns(new ListResult<IPYarnTypeViewModel>(new List<IPYarnTypeViewModel> { ViewModel }, 1, 1, 1));
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(ViewModel);
            serviceMock.Setup(s => s.ReadByPage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new ListResult<IPYarnTypeViewModel>(new List<IPYarnTypeViewModel> { ViewModel }, 1, 1, 1));
            serviceMock.Setup(s => s.Create(It.IsAny<IPYarnTypeViewModel>()))
                .ReturnsAsync(1);
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<IPYarnTypeViewModel>()))
                .ReturnsAsync(1);
            serviceMock.Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(1);
            return serviceMock;
        }
        private Mock<IIPYarnTypeService> GetGlobalDefaulExceptionMock()
        {
            var serviceMock = new Mock<IIPYarnTypeService>();
            serviceMock.Setup(s => s.ReadAll())
                .Throws(new Exception());
            serviceMock.Setup(s => s.ReadById(It.IsAny<int>()))
                .Throws(new Exception());
            serviceMock.Setup(s => s.ReadByPage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            serviceMock.Setup(s => s.Create(It.IsAny<IPYarnTypeViewModel>()))
                .Throws(new Exception());
            serviceMock.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<IPYarnTypeViewModel>()))
                .Throws(new Exception());
            serviceMock.Setup(s => s.Delete(It.IsAny<int>()))
                .Throws(new Exception());
            return serviceMock;
        }
        private Mock<IIdentityProvider> GetGlobalIndetityProvider()
        {
            var identityProviderMock = new Mock<IIdentityProvider>();
            return identityProviderMock;
        }
        private Mock<IValidateService> GetGlobalValidate()
        {
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<IPYarnTypeViewModel>()))
                .Verifiable();
            var validateService = validateServiceMock.Object;
            return validateServiceMock;
        }
        private Mock<IValidateService> GetGlobalExceptionValidate()
        {
            var newValid = new ValidationContext(this, null, null);
            var newListValidResult = new List<ValidationResult>();
            var validateServiceMock = new Mock<IValidateService>();
            validateServiceMock.Setup(s => s.Validate(It.IsAny<IPYarnTypeViewModel>()))
                //.Throws(new ServiceValidationException("ErrorValidtasi",It.IsAny<ValidationContext>(),It.IsAny<List<ValidationResult>>()));
                .Throws(new ServiceValidationException("ErrorValidtasi", newValid, newListValidResult));

            var validateService = validateServiceMock.Object;
            return validateServiceMock;

        }
        //private Mock<IValidateService> GetGlobalExceptionValidate()
        //{
        //    var validateServiceMock = new Mock<IValidateService>();
        //    validateServiceMock.Setup(s => s.Validate(It.IsAny<IPWidthTypeViewModel>()))
        //        .Verifiable();
        //    var validateService = validateServiceMock.Object;
        //    return validateServiceMock;
        //}

        [Fact]
        public void Should_Success_Get()
        {
            //v
            var unittest = new IPYarnTypeControllerTest();
            var controller = unittest._controllerMock;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_Get()
        {
            var unittest = new IPYarnTypeControllerTest(true);
            var controller = unittest._controllerMock;
            var response = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public async void Should_Success_GetById()
        {
            //v
            var unittest = new IPYarnTypeControllerTest();
            var controller = unittest._controllerMock;
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async void Should_Exception_GetById()
        {
            //v
            var unittest = new IPYarnTypeControllerTest(true);
            var controller = unittest._controllerMock;
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Should_Success_GetByKeyword()
        {
            //v
            var unittest = new IPYarnTypeControllerTest();
            var controller = unittest._controllerMock;

            var response = controller.GetByKeyword();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void Should_Exception_GetByKeyword()
        {
            var unittest = new IPYarnTypeControllerTest(true);
            var controller = unittest._controllerMock;

            var response = controller.GetByKeyword();

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Success_Post()
        {
            //v
            var unittest = new IPYarnTypeControllerTest();
            var controller = unittest._controllerMock;
            var response = controller.Post(ViewModel);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }
        [Fact]
        public void Should_NotValid_Post()
        {
            //v
            var unittest = new IPYarnTypeControllerTest();
            var controller = unittest._controllerMock;
            controller.ModelState.AddModelError("test", "test");
            var response = controller.Post(NotValidViewModel);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
        [Fact]
        public void Should_ServiceException_Post()
        {
            //v
            var unittest = new IPYarnTypeControllerTest(false,true);
            var controller = unittest._controllerMock;
            //controller.ModelState.AddModelError("test", "test");

            var response = controller.Post(NotValidViewModel);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
        [Fact]
        public void Should_Exception_Post()
        {
            var unittest = new IPYarnTypeControllerTest(true);
            var controller = unittest._controllerMock;
            var response = controller.Post(ViewModel);


            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public void Should_Success_Edit()
        {
            //v
            var unittest = new IPYarnTypeControllerTest();
            var controller = unittest._controllerMock;
            var response = controller.Edit(1, ViewModel);

            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }
        [Fact]
        public void Should_NotValid_Edit()
        {
            //v
            var unittest = new IPYarnTypeControllerTest(true);
            var controller = unittest._controllerMock;
            controller.ModelState.AddModelError("test", "test");
            var response = controller.Edit(1, NotValidViewModel);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
        [Fact]
        public void Should_ServiceException_Edit()
        {
            //v
            var unittest = new IPYarnTypeControllerTest(false, true);
            var controller = unittest._controllerMock;
            //controller.ModelState.AddModelError("test", "test");

            var response = controller.Edit(1, NotValidViewModel);

            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }
        [Fact]
        public void Should_Exception_Edit()
        {
            var unittest = new IPYarnTypeControllerTest(true);
            var controller = unittest._controllerMock;
            var response = controller.Edit(1, ViewModel);


            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
        [Fact]
        public async void Should_Success_Delete()
        {
            //v
            var unittest = new IPYarnTypeControllerTest();
            var controller = unittest._controllerMock;
            var response = await controller.Delete(1);

            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async void Should_Exception_Delete()
        {
            var unittest = new IPYarnTypeControllerTest(true);
            var controller = unittest._controllerMock;
            var response = await controller.Delete(1);


            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
