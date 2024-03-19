using Newtonsoft.Json;  
using System;

namespace WpfApp9
{
    public class AjaxResult  
    {  
        [JsonProperty(PropertyName = "code")]  
        public int Code { get; set; }  
  
        [JsonProperty(PropertyName = "msg")]  
        public string Message { get; set; }  
  
        [JsonProperty(PropertyName = "data")]  
        public object Data { get; set; } // 这里使用 object 作为占位符，你可能需要替换为具体的类型  
    }  
}

