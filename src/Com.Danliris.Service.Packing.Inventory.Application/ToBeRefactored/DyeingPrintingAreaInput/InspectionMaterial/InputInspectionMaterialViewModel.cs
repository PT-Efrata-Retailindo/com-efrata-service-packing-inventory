using Com.Danliris.Service.Packing.Inventory.Application.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.ToBeRefactored.DyeingPrintingAreaInput.InspectionMaterial
{
    public class InputInspectionMaterialViewModel : BaseViewModel, IValidatableObject
    {
        public InputInspectionMaterialViewModel()
        {
            InspectionMaterialProductionOrders = new HashSet<InputInspectionMaterialProductionOrderViewModel>();
        }

        public string Area { get; set; }
        public string BonNo { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public ICollection<InputInspectionMaterialProductionOrderViewModel> InspectionMaterialProductionOrders { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Area))
                yield return new ValidationResult("Area harus diisi", new List<string> { "Area" });

            if (Date == default(DateTimeOffset))
            {
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });
            }
            else
            {
                if (Id == 0 && !(Date >= DateTimeOffset.UtcNow || ((DateTimeOffset.UtcNow - Date).TotalDays <= 1 && (DateTimeOffset.UtcNow - Date).TotalDays >= 0)))
                {
                    yield return new ValidationResult("Tanggal Harus Lebih Besar atau Sama Dengan Hari Ini", new List<string> { "Date" });
                }
            }

            if (string.IsNullOrEmpty(Shift))
                yield return new ValidationResult("Shift harus diisi", new List<string> { "Shift" });

            if (string.IsNullOrEmpty(Group))
                yield return new ValidationResult("Group harus diisi", new List<string> { "Group" });

            int Count = 0;
            string DetailErrors = "[";

            if (InspectionMaterialProductionOrders.Count == 0)
            {
                yield return new ValidationResult("SPP harus Diisi", new List<string> { "InspectionMaterialProductionOrder" });
            }
            else
            {
                foreach (var item in InspectionMaterialProductionOrders)
                {
                    DetailErrors += "{";

                    if (string.IsNullOrEmpty(item.CartNo))
                    {
                        Count++;
                        DetailErrors += "CartNo: 'No Kereta Harus Diisi!',";
                    }

                    if (item.ProductionOrder == null || item.ProductionOrder.Id == 0)
                    {
                        Count++;
                        DetailErrors += "ProductionOrder: 'SPP Harus Diisi!',";
                    }

                    if (item.InputQuantity == 0)
                    {
                        Count++;
                        DetailErrors += "InputQuantity: 'Qty Terima Harus Lebih dari 0!',";
                    }

                    DetailErrors += "}, ";
                }
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "InspectionMaterialProductionOrders" });
        }
    }
}
