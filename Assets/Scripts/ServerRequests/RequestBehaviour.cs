using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.ServerRequests
{
    public static class RequestBehavior
    {
        public const string JsonContentType = "application/json";

        public static async Task<string> GetRaw(string url)
        {
            using var www = UnityWebRequest.Get(url);
            var op = www.SendWebRequest();
            while (!op.isDone)
                await Task.Yield();
            if(www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Get failed {www.error}");
            }

            return www.downloadHandler.text;
        }

        public static async Task<string> PostRaw(string url, string data, string contentType, Dictionary<string, string> headers = null)
        {
            using var www = UnityWebRequest.Post(url, data, contentType);
            foreach (var item in headers)
            {
                www.SetRequestHeader(item.Key, item.Value);
            }
            var operation = www.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Post failed {www.error}");
            }

            return www.downloadHandler.text;
        }

        public static async Task<string> PutRaw(string url, string data)
        {
            using var www = UnityWebRequest.Put(url, data);
            var operation = www.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Put failed {www.error}");
            }

            return www.downloadHandler.text;
        }

        public static async Task DeleteRaw(string url)
        {
            using var www = UnityWebRequest.Delete(url);
            var operation = www.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Delete failed {www.error}");
            }
        }

        public static async Task<TResult> Get<TResult>(string url)
        {
            string raw = await GetRaw(url);
            return JsonConvert.DeserializeObject<TResult>(raw);
        }

        public static async Task<TResult> Post<TResult>(string url, string data, string contentType)
        {
            string raw = await PostRaw(url, data, contentType);
            return JsonConvert.DeserializeObject<TResult>(raw);
        }

        public static async Task<TResult> Put<TResult>(string url, string data)
        {
            string raw = await PutRaw(url, data);
            return JsonConvert.DeserializeObject<TResult>(raw);
        }

        public static async Task<TResult> Post<TResult>(string url, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            return await Post<TResult>(url, json, JsonContentType);
        }

        public static async Task<TResult> Put<TResult>(string url, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            return await Put<TResult>(url, json);
        }
    }
}
