﻿using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalPriceCuttingNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.ShippingLocalSalesNote;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingLocalPriceCuttingNote
{
    public class GarmentShippingLocalPriceCuttingNoteControllerGetByIdTest : GarmentShippingLocalPriceCuttingNoteControllerTest
    {
        [Fact]
        public async Task GetById_Ok()
        {
            var serviceMock = new Mock<IGarmentShippingLocalPriceCuttingNoteService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentShippingLocalPriceCuttingNoteViewModel());
            var service = serviceMock.Object;

            var serviceSalesNoteMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            var serviceSalesNote = serviceSalesNoteMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, serviceSalesNote, identityProvider, validateService);
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingLocalPriceCuttingNoteService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;

            var serviceSalesNoteMock = new Mock<IGarmentShippingLocalSalesNoteService>();
            var serviceSalesNote = serviceSalesNoteMock.Object;

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, serviceSalesNote, identityProvider, validateService);
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
