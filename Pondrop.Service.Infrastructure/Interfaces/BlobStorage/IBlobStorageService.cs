using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pondrop.Service.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadImageAsync(string fileName, string base64, string userId = "", string mimeType = null);
}
