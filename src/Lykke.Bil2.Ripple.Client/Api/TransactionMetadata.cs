using System;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Lykke.Bil2.Ripple.Client.Api
{
    /// <summary>
    /// Describes the outcome of the transaction in detail.
    /// </summary>
    public class TransactionMetadata
    {
        /// <summary>
        /// List of ledger objects that were created, deleted, or modified by this transaction, and specific changes to each.
        /// </summary>
        public AffectedLedgerEntry[] AffectedNodes { get; set; }

        /// <summary>
        /// The transaction's position within the ledger that included it. This is zero-indexed.
        /// </summary>
        public uint TransactionIndex { get; set; }

        /// <summary>
        /// A result code indicating whether the transaction succeeded or how it failed.
        /// </summary>
        public string TransactionResult { get; set; }

        /// <summary>
        /// The Currency Amount actually received by the Destination account.
        /// </summary>
        [JsonProperty("delivered_amount")]
        public Amount DeliveredAmount { get; set; }

        /// <summary>
        /// Returns list of balance changes, grouped by address.
        /// </summary>
        public Dictionary<string, Amount[]> GetBalanceChanges()
        {
            // implementation is quite similar to https://ripple.com/dev-blog/calculating-balance-changes-for-transaction/

            if (AffectedNodes == null)
            {
                new Dictionary<string, Amount[]>();
            }

            return AffectedNodes
                .Select(x => x.CreatedNode ?? x.DeletedNode ?? x.ModifiedNode)
                .SelectMany<LedgerEntry, (string address, Amount balance)>(entry =>
                {
                    switch (entry.LedgerEntryType)
                    {
                        case LedgerEntryType.AccountRoot:
                            var newAccount = entry.NewFields?.ToObject<AccountRoot>();
                            var previousAccount = entry.PreviousFields?.ToObject<AccountRoot>();
                            var finalAccount = entry.FinalFields?.ToObject<AccountRoot>();
                            var account = finalAccount ?? newAccount;
                            var accountBalanceChange = previousAccount?.Balance != null && finalAccount?.Balance != null
                                ? decimal.Parse(finalAccount.Balance, CultureInfo.InvariantCulture) - decimal.Parse(previousAccount.Balance, CultureInfo.InvariantCulture)
                                : newAccount?.Balance != null
                                    ? decimal.Parse(newAccount.Balance, CultureInfo.InvariantCulture)
                                    : 0M;

                            return accountBalanceChange == 0M ?
                                new (string, Amount)[] { } :
                                new (string, Amount)[]
                                {
                                    (
                                        account.Account,
                                        new Amount
                                        {
                                            Currency = "XRP",
                                            Value =  (accountBalanceChange / 1_000_000M).ToString("F6", CultureInfo.InvariantCulture)
                                        }
                                    )
                                };
                            
                        case LedgerEntryType.RippleState:
                            var newState = entry.NewFields?.ToObject<RippleState>();
                            var previousState = entry.PreviousFields?.ToObject<RippleState>();
                            var finalState = entry.FinalFields?.ToObject<RippleState>();
                            var state = finalState ?? newState;
                            var stateBalanceChange = previousState?.Balance != null && finalState?.Balance != null
                                ? decimal.Parse(finalState.Balance.Value, CultureInfo.InvariantCulture) - decimal.Parse(previousState.Balance.Value, CultureInfo.InvariantCulture)
                                :   newState?.Balance != null
                                    ? decimal.Parse(newState.Balance.Value, CultureInfo.InvariantCulture)
                                    : 0m;

                            if (stateBalanceChange == 0m)
                            {
                                return new (string, Amount)[] { };
                            }

                            // the sign of the balances are oriented with respect to the low node

                            var lo =
                            (
                                address: state.LowLimit.Counterparty,
                                balance: new Amount
                                {
                                    Currency = state.Balance.Currency,
                                    Counterparty = state.HighLimit.Counterparty,
                                    Value = stateBalanceChange.ToString("F")
                                }
                            );

                            var hi =
                            (
                                address: state.HighLimit.Counterparty,
                                balance: new Amount
                                {
                                    Currency = state.Balance.Currency,
                                    Counterparty = state.LowLimit.Counterparty,
                                    Value = decimal.Negate(stateBalanceChange).ToString("F")
                                }
                            );

                            return new (string, Amount)[] { lo, hi };

                        default:
                            return new (string, Amount)[] { };
                    }
                })
                .GroupBy
                (
                    x => x.address,
                    x => x.balance
                )
                .ToDictionary
                (
                    g => g.Key,
                    g => g.ToArray()
                );
        }

        /// <summary>
        /// Describes object changes made by transaction.
        /// </summary>
        public class AffectedLedgerEntry
        {
            /// <summary>
            /// Not null if transaction creates new object.
            /// </summary>
            public LedgerEntry CreatedNode { get; set; }

            /// <summary>
            /// Not null if transactiob changes an object.
            /// </summary>
            public LedgerEntry ModifiedNode { get; set; }

            /// <summary>
            /// Not null if transaction deletes an object.
            /// </summary>
            public LedgerEntry DeletedNode { get; set; }
        }

        /// <summary>
        /// Describes object changes made by transaction.
        /// </summary>
        public class LedgerEntry
        {
            /// <summary>
            /// Type of affected ledger object.
            /// </summary>
            public LedgerEntryType LedgerEntryType { get; set; }

            /// <summary>
            /// Final values after change for modified or deleted object.
            /// </summary>
            public JObject FinalFields { get; set; }

            /// <summary>
            /// New values for created object.
            /// </summary>
            public JObject NewFields { get; set; }

            /// <summary>
            /// Final values before change for modified or deleted object.
            /// </summary>
            public JObject PreviousFields { get; set; }
        }

        /// <summary>
        /// https://developers.ripple.com/ledger-object-types.html
        /// </summary>
        public enum LedgerEntryType
        {
            /// <summary>
            /// Indicates that this is an AccountRoot object.
            /// </summary>
            AccountRoot = 0x0061,

            /// <summary>
            /// Indicates that this object describes the status of amendments to the XRP Ledger.
            /// </summary>
            Amendments = 0x0066,

            /// <summary>
            /// Indicates that this object is a Check object.
            /// </summary>
            Check = 0x0043,

            /// <summary>
            /// Indicates that this is a DepositPreauth object.
            /// </summary>
            DepositPreauth = 0x0070,

            /// <summary>
            /// Indicates that this object is part of a Directory.
            /// </summary>
            DirectoryNode = 0x0064,

            /// <summary>
            /// Indicates that this object is an Escrow object.
            /// </summary>
            Escrow = 0x0075,

            /// <summary>
            /// Indicates that this object contains the ledger's fee settings.
            /// </summary>
            FeeSettings = 0x0073,

            /// <summary>
            /// Indicates that this object is a list of ledger hashes.
            /// </summary>
            LedgerHashes = 0x0068,

            /// <summary>
            /// Indicates that this object describes an order to trade currency.
            /// </summary>
            Offer = 0x006F,

            /// <summary>
            /// Indicates that this object is a payment channel object.
            /// </summary>
            PayChannel = 0x0078,

            /// <summary>
            /// Indicates that this object is a RippleState object.
            /// </summary>
            RippleState = 0x0072,

            /// <summary>
            /// Indicates that this object is a SignerList object.
            /// </summary>
            SignerList = 0x0053
        }
    }
}