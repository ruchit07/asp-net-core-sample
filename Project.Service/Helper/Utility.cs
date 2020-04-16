using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Project.Service
{
    public class Utility
    {
        public static string GetCurrentTimeStamp()
        {
            return Convert.ToString(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
        }
    }
}
