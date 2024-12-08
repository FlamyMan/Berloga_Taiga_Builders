using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.ServerRequests
{
    public static class RequestBehavior
    {
        public const string JsonContentType = "application/json";

        public const int GlobalTimeout = 1;
        public static async Task<string> GetRaw(string url)
        {
            using var www = UnityWebRequest.Get(url);
            www.timeout = GlobalTimeout;
            var op = www.SendWebRequest();
            while (!op.isDone)
                await Task.Yield();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Get failed {www.error}\n Got Text: {www.downloadHandler.text}");
                return null;
            }

            return www.downloadHandler.text;
        }

        public static async Task<string> PostRaw(string url, string data, string contentType, Dictionary<string, string> headers = null)
        {
            using var www = UnityWebRequest.Post(url, data, contentType);
            www.timeout = GlobalTimeout;
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    www.SetRequestHeader(item.Key, item.Value);
                }
            }
            var operation = www.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Post failed {www.error}\n Got Text: {www.downloadHandler.text}");
                return null;
            }

            return www.downloadHandler.text;
        }

        public static async Task<string> PutRaw(string url, string data)
        {
            using var www = UnityWebRequest.Put(url, data);
            www.timeout = GlobalTimeout;
            www.SetRequestHeader("Content-Type", JsonContentType);
            var operation = www.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Put failed {www.error}\n Got text: {www.downloadHandler.text}");
                return null;
            }

            return www.downloadHandler.text;
        }

        public static async Task DeleteRaw(string url)
        {
            using var www = UnityWebRequest.Delete(url);
            www.timeout = GlobalTimeout;
            var operation = www.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Delete failed {www.error}\n Got Text: {www.downloadHandler.text}");
            }
        }

        public static async Task<TResult> Get<TResult>(string url)
        {
            try
            {
                string raw = await GetRaw(url);
                return JsonConvert.DeserializeObject<TResult>(raw);
            }
            catch (JsonException e)
            {
                Debug.LogError(e);
                return default;
            }
            catch (ArgumentNullException)
            {
                return default;
            }
        }

        public static async Task<TResult> Post<TResult>(string url, string data, string contentType, Dictionary<string, string> headers = null)
        {
            try
            {
                string raw = await PostRaw(url, data, contentType, headers);
                return JsonConvert.DeserializeObject<TResult>(raw);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (JsonException e)
            {
                Debug.Log(e);
                return default;
            }
        }

        public static async Task<TResult> Put<TResult>(string url, string data)
        {
            try
            {
                string raw = await PutRaw(url, data);
                return JsonConvert.DeserializeObject<TResult>(raw);
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (JsonException e)
            {
                Debug.Log(e);
                return default;
            }
        }

        public static async Task<TResult> Post<TResult>(string url, object data, Dictionary<string, string> headers = null)
        {
            string json = JsonConvert.SerializeObject(data);
            return await Post<TResult>(url, json, JsonContentType, headers);
        }

        public static async Task<TResult> Put<TResult>(string url, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            return await Put<TResult>(url, json);
        }
    }
}
