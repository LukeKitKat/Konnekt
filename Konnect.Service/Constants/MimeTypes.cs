using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnect.Service.Constants
{
    public class MimeTypes
    {
        public const string JPEG = "image/jpeg";
        public const string PNG = "image/png";
        public const string GIF = "image/gif";

        public static Dictionary<string, string> CombinedImageMimes = new()
        {
            {nameof(JPEG), JPEG},
            {nameof(PNG), PNG},
            {nameof(GIF), GIF}
        };
    }
}
