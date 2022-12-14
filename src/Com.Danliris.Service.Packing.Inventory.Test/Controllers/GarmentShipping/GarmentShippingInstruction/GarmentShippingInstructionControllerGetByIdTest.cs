using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInstruction;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.Utilities;
using Com.Danliris.Service.Packing.Inventory.Infrastructure.IdentityProvider;
using Moq;
using System;
using Xunit;
using System.Net;
using System.Threading.Tasks;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentShippingInvoice;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.GarmentPackingList;
using Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.GarmentShipping.CoverLetter;

namespace Com.Danliris.Service.Packing.Inventory.Test.Controllers.GarmentShipping.GarmentShippingInstruction
{
    public class GarmentShippingInstructionControllerGetByIdTest : GarmentShippingInstructionControllerTest
    {
        [Fact]
        public async Task GetById_Ok()
        {
            var serviceMock = new Mock<IGarmentShippingInstructionService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .ReturnsAsync(new GarmentShippingInstructionViewModel());
            var service = serviceMock.Object;
            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var coverletterServiceMock = new Mock<IGarmentCoverLetterService>();

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService, coverletterServiceMock.Object, packingListServiceMock.Object, invoiceServiceMock.Object);
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task GetById_Exception_InternalServerError()
        {
            var dataUtil = ViewModel;

            var serviceMock = new Mock<IGarmentShippingInstructionService>();
            serviceMock
                .Setup(s => s.ReadById(It.IsAny<int>()))
                .Throws(new Exception());
            var service = serviceMock.Object;
            var packingListServiceMock = new Mock<IGarmentPackingListService>();
            var invoiceServiceMock = new Mock<IGarmentShippingInvoiceService>();
            var coverletterServiceMock = new Mock<IGarmentCoverLetterService>();

            var identityProviderMock = new Mock<IIdentityProvider>();
            var identityProvider = identityProviderMock.Object;

            var validateServiceMock = new Mock<IValidateService>();
            var validateService = validateServiceMock.Object;

            var controller = GetController(service, identityProvider, validateService, coverletterServiceMock.Object, packingListServiceMock.Object, invoiceServiceMock.Object);
            var response = await controller.GetById(1);

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
