using Com.Moonlay.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Packing.Inventory.Data.Models.Inventory
{
    public class ProductSKUInventoryDocumentModel : StandardEntity
    {
        public ProductSKUInventoryDocumentModel()
        {

        }

        public ProductSKUInventoryDocumentModel(
            string documentNo,
            DateTimeOffset date,
            string referenceNo,
            string referenceType,
            int storageId,
            string storageName,
            string storageCode,
            string type,
            string remark
            )
        {
            DocumentNo = documentNo;
            Date = date;
            ReferenceNo = referenceNo;
            ReferenceType = referenceType;
            StorageId = storageId;
            StorageName = storageName;
            StorageCode = storageCode;
            Type = type;
            Remark = remark;
        }

        [MaxLength(64)]
        public string DocumentNo { get; private set; }
        public DateTimeOffset Date { get; private set; }
        [MaxLength(64)]
        public string ReferenceNo { get; private set; }
        [MaxLength(256)]
        public string ReferenceType { get; private set; }
        public int StorageId { get; private set; }
        [MaxLength(512)]
        public string StorageName { get; private set; }
        [MaxLength(64)]
        public string StorageCode { get; private set; }
        [MaxLength(32)]
        public string Type { get; private set; }
        public string Remark { get; private set; }
    }
}
