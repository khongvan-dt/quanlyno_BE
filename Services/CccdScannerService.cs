// using System;
// using System.IO;
// // using IronBarCode;

// namespace quanLyNo_BE.Services
// {
//     // public interface ICccdService
//     // {
//     //     byte[] GenerateQRCodeImage(string text);
//     //     string DecodeQRCode(byte[] qrCodeImage);
//     // }

//     public class CccdService 
//     // : ICccdService
//     {
//         // public byte[] GenerateQRCodeImage(string text)
//         // {
//         //     if (string.IsNullOrEmpty(text))
//         //         throw new ArgumentException("Text cannot be null or empty.", nameof(text));

//         //     var qrCode = QRCodeWriter.CreateQrCode(text, 200);
//         //     using (var ms = new MemoryStream())
//         //     {
//         //         qrCode.SaveAsPng(ms);
//         //         return ms.ToArray();
//         //     }
//         // }

//         // public string DecodeQRCode(byte[] qrCodeImage)
//         // {
//         //     if (qrCodeImage == null || qrCodeImage.Length == 0)
//         //         throw new ArgumentException("Image cannot be null or empty.", nameof(qrCodeImage));

//         //     using (var ms = new MemoryStream(qrCodeImage))
//         //     {
//         //         var qrCode = QRCodeReader.Read(ms);
//         //         return qrCode?.DecodedText ?? throw new InvalidOperationException("Unable to decode QR code.");
//         //     }
//         // }
    
    
//     }
// }
