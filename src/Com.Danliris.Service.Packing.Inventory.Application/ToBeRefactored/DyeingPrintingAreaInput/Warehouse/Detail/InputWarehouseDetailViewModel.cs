using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.Warehouse.Detail
{
    public class InputWarehouseDetailViewModel : BaseViewModel
    {
        public InputWarehouseDetailViewModel()
        {
            WarehousesProductionOrders = new HashSet<InputWarehouseProductionOrderDetailViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public ICollection<InputWarehouseProductionOrderDetailViewModel> WarehousesProductionOrders { get; set; }
    }
}
