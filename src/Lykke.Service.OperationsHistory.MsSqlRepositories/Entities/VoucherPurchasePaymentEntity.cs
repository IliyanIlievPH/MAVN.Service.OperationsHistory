﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Numerics;
using JetBrains.Annotations;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("voucher_purchase_payments")]
    public class VoucherPurchasePaymentEntity : IVoucherPurchasePayment
    {
        [Key]
        [Column("transfer_id")]
        public Guid TransferId { get; set; }

        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Column("spend_rule_id")]
        public Guid SpendRuleId { get; set; }

        [Column("voucher_id")]
        public Guid VoucherId { get; set; }

        [Required]
        [Column("amount")]
        public Money18 Amount { get; set; }

        [Required]
        [Column("asset_symbol", TypeName = "varchar(10)")]
        public string AssetSymbol { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [UsedImplicitly]
        public VoucherPurchasePaymentEntity()
        {
        }

        public VoucherPurchasePaymentEntity(IVoucherPurchasePayment model)
        {
            TransferId = model.TransferId;
            CustomerId = model.CustomerId;
            SpendRuleId = model.SpendRuleId;
            VoucherId = model.VoucherId;
            Amount = model.Amount;
            AssetSymbol = model.AssetSymbol;
            Timestamp = model.Timestamp;
        }
    }
}
