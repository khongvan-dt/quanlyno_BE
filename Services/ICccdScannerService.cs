using System;
using System.IO;
using Tesseract;

namespace quanLyNo_BE.Services
{
    public interface ICccdScannerService
    {
        string PerformOcr(string imagePath);
    }

    
    }

