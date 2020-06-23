using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Support
{
    public static class Helper
    {
        public static T Get<T>(string route, Dictionary<string, string> values = null)
        {
            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> response = client.GetAsync($"{route}{(values?.Count > 0 ? "?" + string.Join("&", values.Select(x => string.Format("{0}={1}", x.Key, x.Value))) : string.Empty)}");
                response.Wait();

                Task<string> responseString = response.Result.Content.ReadAsStringAsync();
                responseString.Wait();

                if (response.Result.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(responseString.Result);

                throw new Exception("Erro ao processar requisição");
            }
        }

        public static byte[] DownloadAsync(string route, Dictionary<string, string> values = null)
        {
            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> response = client.GetAsync($"{route}{(values?.Count > 0 ? "?" + string.Join("&", values.Select(x => string.Format("{0}={1}", x.Key, x.Value))) : string.Empty)}");
                response.Wait();

                var responseString = response.Result.Content.ReadAsByteArrayAsync();
                responseString.Wait();

                if (response.Result.IsSuccessStatusCode)
                    return responseString.Result;

                throw new Exception("Erro ao processar requisição");
            }
        }

        public static string GetDescription(this Enum enumerationValue)
        {
            Type type = enumerationValue.GetType();
            MemberInfo member = type.GetMembers().Where(w => w.Name == Enum.GetName(type, enumerationValue)).FirstOrDefault();
            var attribute = member?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attribute?.Description != null ? attribute.Description : enumerationValue.ToString();
        }

        public static HttpResponseMessage ExcelResult(this byte[] _excelData, string fileName, DownloadFileInfo downloadFileType)
        {
            if (_excelData == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            fileName = downloadFileType.GetFileName(fileName);

            var message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new ByteArrayContent(_excelData);
            message.Content.Headers.ContentLength = _excelData.Length;
            message.Content.Headers.ContentType = new MediaTypeHeaderValue(downloadFileType.ContentType);
            message.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName,
                Size = _excelData.Length
            };
            return message;
        }
    }
}
