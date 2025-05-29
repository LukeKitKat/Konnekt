using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konnect.Service.Services._Internal
{
    internal class CommonFileHandler
    {
        private const long fileMaxSize = 40000000;

        internal async Task<byte[]> GetFileBytesAsync(IBrowserFile file)
        {
            if (file?.Size > 0 && file?.Size <= fileMaxSize)
            {
                using (var ms = new MemoryStream())
                {
                    await file.OpenReadStream(fileMaxSize).CopyToAsync(ms);
                    return ms.ToArray();
                }
            }
            else
            {
                throw new Exception("File exceeded the maximum allowed size");
            }    
        }
    }
}
