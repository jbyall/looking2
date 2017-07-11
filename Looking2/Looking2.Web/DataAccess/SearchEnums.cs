using Looking2.Web.Domain;
using Looking2.Web.Services;
using Looking2.Web.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Looking2.Web.DataAccess
{
    public enum SearchOperator
    {
        And,
        Or
    }
}
