using System.ComponentModel;
using Swashbuckle.AspNetCore.Annotations;

namespace quanLyNo.Common
{
    public static partial class Enums
    {
        public enum Gender
        {
            [Description("Male")]
            Male = 0,

            [Description("Female")]
            Female = 1,

            [Description("Other")]
            Other = 2
        }
    }
}
