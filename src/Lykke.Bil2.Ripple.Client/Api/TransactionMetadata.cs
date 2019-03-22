using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                            var accountBalanceChange =
                                previousAccount?.Balance != null && finalAccount?.Balance != null ? long.Parse(finalAccount.Balance) - long.Parse(previousAccount.Balance) :
                                newAccount?.Balance != null ? long.Parse(newAccount.Balance) :
                                0L;

                            return accountBalanceChange == 0L ?
                                new (string, Amount)[] { } :
                                new (string, Amount)[]
                                {
                                    (
                                        account.Account,
                                        new Amount
                                        {
                                            Currency = "XRP",
                                            Value =  accountBalanceChange.ToString("D")
                                        }
                                    )
                                };
                            
                        case LedgerEntryType.RippleState:
                            var newState = entry.NewFields?.ToObject<RippleState>();
                            var previousState = entry.PreviousFields?.ToObject<RippleState>();
                            var finalState = entry.FinalFields?.ToObject<RippleState>();
                            var state = finalState ?? newState;
                            var stateBalanceChange =
                                previousState?.Balance != null && finalState?.Balance != null ? decimal.Parse(finalState.Balance.Value) - decimal.Parse(previousState.Balance.Value) :
                                newState?.Balance != null ? decimal.Parse(newState.Balance.Value) :
                                0m;

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
            /// The settings, XRP balance, and other metadata for one account.
            /// </summary>
            AccountRoot = 0x0061,

            /// <summary>
            /// Links two accounts, tracking the balance of one currency between them.
            /// The concept of a trust line is an abstraction of this object type.
            /// </summary>
            RippleState = 0x0072,

            Offer,

            DirectoryNode
        }
    }
}