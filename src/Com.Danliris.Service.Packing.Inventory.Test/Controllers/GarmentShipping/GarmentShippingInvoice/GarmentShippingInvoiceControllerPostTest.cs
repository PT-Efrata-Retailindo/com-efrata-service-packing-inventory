using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingInvoice
{
	public class GarmentShippingInvoiceControllerPostTest : GarmentShippingInvoiceControllerTest
	{
		[Fact]
		public async Task Post_Created()
		{
			var dataUtil = ViewModel;

			var serviceMock = new Mock<IGarmentShippingInvoiceService>();
			serviceMock
				.Setup(s => s.Create(It.IsAny<GarmentShippingInvoiceViewModel>()))
				.ReturnsAsync(1);
			var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentPackingListViewModel());
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
			validateServiceMock
				.Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
				.Verifiable();
			var validateService = validateServiceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var controller = GetController(service, packingListService, identityProvider, validateService);

			var response = await controller.Post(dataUtil);

			Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
		}

		[Fact]
		public async Task Post_ValidationException_BadRequest()
		{
			var dataUtil = ViewModel;

			var serviceMock = new Mock<IGarmentShippingInvoiceService>();
			var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentPackingListViewModel());
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
			validateServiceMock
				.Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
				.Throws(GetServiceValidationExeption());
			var validateService = validateServiceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var controller = GetController(service, packingListService, identityProvider, validateService);
			var response = await controller.Post(dataUtil);

			Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
		}

		[Fact]
		public async Task Post_Exception_InternalServerError()
		{
			var dataUtil = ViewModel;

			var serviceMock = new Mock<IGarmentShippingInvoiceService>();
			serviceMock
				.Setup(s => s.Create(It.IsAny<GarmentShippingInvoiceViewModel>()))
				.ThrowsAsync(new Exception());
			var service = serviceMock.Object;

            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            packingListServiceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentPackingListViewModel());
            var packingListService = packingListServiceMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
			validateServiceMock
				.Setup(s => s.Validate(It.IsAny<GarmentShippingInvoiceViewModel>()))
				.Verifiable();
			var validateService = validateServiceMock.Object;

			var identityProviderMock = new Mock<IIdentityProvider>();
			var identityProvider = identityProviderMock.Object;

			var controller = GetController(service, packingListService, identityProvider, validateService);
			var response = await controller.Post(dataUtil);

			Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
		}
	}
}
