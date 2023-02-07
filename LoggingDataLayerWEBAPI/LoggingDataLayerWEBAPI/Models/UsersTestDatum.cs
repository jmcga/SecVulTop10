using System;
using System.Collections.Generic;

#nullable disable

namespace LoggingDataLayerWEBAPI.Models
{
    public partial class UsersTestDatum
    {
        public int TestDataId { get; set; }
        public int UserId { get; set; }
        public string TestData { get; set; }
    }
}
