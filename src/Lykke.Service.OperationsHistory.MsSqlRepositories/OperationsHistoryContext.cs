﻿using System.Data.Common;
using Lykke.Common.MsSql;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories
{
    public class OperationsHistoryContext : MsSqlContext
    {
        private const string Schema = "operation_history";

        internal DbSet<TransactionHistoryEntity> TransactionHistories { get; set; }
        internal DbSet<TransferEntity> Transfers { get; set; }
        internal DbSet<BonusCashInEntity> BonusCashIns { get; set; }
        internal DbSet<CampaignEntity> Campaigns { get; set; }
        internal DbSet<BurnRuleEntity> BurnRules { get; set; }
        internal DbSet<PaymentTransferEntity> PaymentTransfers { get; set; }
        internal DbSet<CustomerTierEntity> CustomerTiers { get; set; }
        internal DbSet<PaymentTransferRefundEntity> RefundedPaymentTransfers { get; set; }
        internal DbSet<PartnersPaymentEntity> PartnersPayments { get; set; }
        internal DbSet<PartnersPaymentRefundEntity> RefundedPartnersPayments { get; set; }
        internal DbSet<ReferralStakeEntity> ReferralStakes { get; set; }
        internal DbSet<ReleasedReferralStakeEntity> ReleasedReferralStakes { get; set; }
        internal DbSet<LinkedWalletTransferEntity> LinkedWalletTransfers { get; set; }
        internal DbSet<FeeCollectedOperationEntity> FeeCollectedOperations { get; set; }
        internal DbSet<LinkWalletOperationEntity> LinkWalletOperations { get; set; }
        internal DbSet<VoucherPurchasePaymentEntity> VoucherPurchasePayments { get; set; }

        public OperationsHistoryContext() : base(Schema)
        {
        }

        public OperationsHistoryContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {
        }

        public OperationsHistoryContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {
        }

        protected override void OnLykkeModelCreating(ModelBuilder modelBuilder)
        {
            var transferEntityBuilder = modelBuilder.Entity<TransferEntity>();

            transferEntityBuilder.HasIndex(t => t.SenderCustomerId).IsUnique(false);
            transferEntityBuilder.HasIndex(t => t.ReceiverCustomerId).IsUnique(false);

            var bonusCashInEntityBuilder = modelBuilder.Entity<BonusCashInEntity>();

            bonusCashInEntityBuilder.HasIndex(b => b.CustomerId).IsUnique(false);
            bonusCashInEntityBuilder.HasIndex(b => b.Timestamp).IsUnique(false);

            var historyEntityBuilder = modelBuilder.Entity<TransactionHistoryEntity>();

            historyEntityBuilder.Property(p => p.Id).ValueGeneratedOnAdd();
            historyEntityBuilder.HasIndex(h => h.CustomerId).IsUnique(false);
            historyEntityBuilder.HasIndex(h => h.TransactionId).IsUnique(false);
            historyEntityBuilder.HasIndex(h => h.Timestamp).IsUnique(false);

            var paymentTransferEntityBuilder = modelBuilder.Entity<PaymentTransferEntity>();

            paymentTransferEntityBuilder.HasIndex(t => t.Timestamp).IsUnique(false);
            paymentTransferEntityBuilder.HasIndex(t => t.CustomerId).IsUnique(false);

            var paymentTransferRefundEntityBuilder = modelBuilder.Entity<PaymentTransferRefundEntity>();

            paymentTransferRefundEntityBuilder.HasIndex(t => t.Timestamp).IsUnique(false);
            paymentTransferRefundEntityBuilder.HasIndex(t => t.CustomerId).IsUnique(false);

            var partnersPaymentsEntityBuilder = modelBuilder.Entity<PartnersPaymentEntity>();

            partnersPaymentsEntityBuilder.HasIndex(t => t.Timestamp).IsUnique(false);
            partnersPaymentsEntityBuilder.HasIndex(t => t.CustomerId).IsUnique(false);

            var partnersPaymentsRefundEntityBuilder = modelBuilder.Entity<PartnersPaymentRefundEntity>();

            partnersPaymentsRefundEntityBuilder.HasIndex(t => t.Timestamp).IsUnique(false);
            partnersPaymentsRefundEntityBuilder.HasIndex(t => t.CustomerId).IsUnique(false);


            var referralStakesEntityBuild = modelBuilder.Entity<ReferralStakeEntity>();

            referralStakesEntityBuild.HasIndex(t => t.Timestamp).IsUnique(false);
            referralStakesEntityBuild.HasIndex(t => t.CustomerId).IsUnique(false);


            var releasedReferralStakesEntityBuild = modelBuilder.Entity<ReleasedReferralStakeEntity>();

            releasedReferralStakesEntityBuild.HasIndex(t => t.Timestamp).IsUnique(false);
            releasedReferralStakesEntityBuild.HasIndex(t => t.CustomerId).IsUnique(false);

            var feeCollectedOperationEntityBuild = modelBuilder.Entity<FeeCollectedOperationEntity>();

            feeCollectedOperationEntityBuild.HasIndex(t => t.Timestamp).IsUnique(false);
            feeCollectedOperationEntityBuild.HasIndex(t => t.CustomerId).IsUnique(false);

            var linkWalletOperationBuilder = modelBuilder.Entity<LinkWalletOperationEntity>();

            linkWalletOperationBuilder.HasIndex(t => t.Timestamp).IsUnique(false);
            linkWalletOperationBuilder.HasIndex(t => t.CustomerId).IsUnique(false);
            linkWalletOperationBuilder.Property(e => e.Direction)
                .HasConversion(new EnumToStringConverter<LinkWalletDirection>());

            var voucherPurchasePaymentBuilder = modelBuilder.Entity<VoucherPurchasePaymentEntity>();

            voucherPurchasePaymentBuilder.HasIndex(t => t.Timestamp).IsUnique(false);
            voucherPurchasePaymentBuilder.HasIndex(t => t.CustomerId).IsUnique(false);
        }
    }
}
