using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public enum GroupByEnum
    {
        [Description("Conta")]
        AccountNumber = 1,

        [Description("Ativo")]
        Active,

        [Description("TipoOperacao")]
        OperationType,
    }
}
