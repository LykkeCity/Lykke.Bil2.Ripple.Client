using System;
using Newtonsoft.Json;

namespace Lykke.Bil2.Ripple.Client
{
    public class AccountInfoRequest : RippleRequestBase<AccountInfoRequest.AccountInfoParams, AccountInfoRequest.AccountInfoResult>
    {
        public AccountInfoRequest(string account) : base("account_info", new AccountInfoParams(account))
        {
        }

        public class AccountInfoParams
        {
            public AccountInfoParams(string account)
            {
                Account = account ?? throw new ArgumentNullException(nameof(account));
            }

            public string Account { get; }
        }

        public class AccountInfoResult : RippleRequestResultBase
        {
            [JsonProperty("account_data")]
            public AccountData AccountData { get; set; }

            [JsonProperty("ledger_current_index")]
            public long LedgerCurrentIndex { get; set; }

            public bool Validated { get; set; }
        }

        public class AccountData
        {
            public string Account { get; set; }
            public string Balance { get; set; }
            public long Flags { get; set; }
            public long Sequence { get; set; }
        }
    }
}