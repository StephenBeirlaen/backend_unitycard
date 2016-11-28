using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace UnityCard.API.Helpers
{
    public class GcmHelper
    {
        public static String SendNotification(int retailerId, string retailerName, string message)
        {
            const String GoogleFcmApiKey = "AIzaSyD1IsQnX_3W_k2MnHs_q0t9I8VQohk-PGk";

            const String GoogleFcmURL = "https://fcm.googleapis.com/fcm/send";

            WebRequest fcmRequest;
            fcmRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            fcmRequest.Method = "post";
            fcmRequest.ContentType = "application/json";
            fcmRequest.Headers.Add(String.Format("Authorization: key={0}", GoogleFcmApiKey));

            String body = JsonConvert.SerializeObject(
                new
                {
                    to = "/topics/advertisements/" + retailerId,
                    data = new
                    {
                        message = "AdvertisementUploaded",
                        retailerName = retailerName,
                        title = message
                    }
                }
            );

            Byte[] byteArray = Encoding.UTF8.GetBytes(body);
            fcmRequest.ContentLength = byteArray.Length;

            Stream dataStream = fcmRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = fcmRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }
    }
}