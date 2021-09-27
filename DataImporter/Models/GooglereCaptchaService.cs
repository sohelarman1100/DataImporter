using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataImporter.Models
{
    public class GooglereCaptchaService
    {
        private ReCaptchaSettings _settings;
        public GooglereCaptchaService(IOptions<ReCaptchaSettings> settings)
        {
            _settings = settings.Value;
        }

        public virtual async Task<GoogleRespo>verifyreCaptcha(string _Token)
        {
            GooglereCaptchaData _MyData = new GooglereCaptchaData
            {
                response = _Token,
                secret = _settings.ReCAPTCHA_Secret_Key
            };

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_MyData.secret}&response={_MyData.response}");
            var capresp = JsonConvert.DeserializeObject<GoogleRespo>(response);

            return capresp;
        }
    }
}
