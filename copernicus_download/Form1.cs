using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace copernicus_download
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtAuth.Text = "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJYVUh3VWZKaHVDVWo0X3k4ZF8xM0hxWXBYMFdwdDd2anhob2FPLUxzREZFIn0.eyJleHAiOjE3MTI1NjY5NTcsImlhdCI6MTcxMjU2NjA1NywiYXV0aF90aW1lIjoxNzEyNTQ2MTMxLCJqdGkiOiJhMWM1OWE5ZC04MDg1LTQwZWQtYTFlOS1iODI0YTRkMTQyYjciLCJpc3MiOiJodHRwczovL2lkZW50aXR5LmRhdGFzcGFjZS5jb3Blcm5pY3VzLmV1L2F1dGgvcmVhbG1zL0NEU0UiLCJhdWQiOlsiQ0xPVURGRVJST19QVUJMSUMiLCJhY2NvdW50Il0sInN1YiI6IjJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0YiIsInR5cCI6IkJlYXJlciIsImF6cCI6InNoLTVmOGI2MzBiLWIwODMtNDllZC1iMzQwLWI4ZjAxZWNiODFjNCIsIm5vbmNlIjoiNTE3ODQ1ODYxMjE5NzAxOSIsInNlc3Npb25fc3RhdGUiOiIwNDE1NzZjZC00MzNmLTQwNTktOGI3ZS1lN2ZkMzg1MTRjN2EiLCJyZWFsbV9hY2Nlc3MiOnsicm9sZXMiOlsib2ZmbGluZV9hY2Nlc3MiLCJ1bWFfYXV0aG9yaXphdGlvbiIsImRlZmF1bHQtcm9sZXMtY2RhcyIsImNvcGVybmljdXMtZ2VuZXJhbCJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoiQVVESUVOQ0VfUFVCTElDIGVtYWlsIHByb2ZpbGUgdXNlci1jb250ZXh0Iiwic2lkIjoiMDQxNTc2Y2QtNDMzZi00MDU5LThiN2UtZTdmZDM4NTE0YzdhIiwiZ3JvdXBfbWVtYmVyc2hpcCI6WyIvYWNjZXNzX2dyb3Vwcy91c2VyX3R5cG9sb2d5L2NvcGVybmljdXNfZ2VuZXJhbCIsIi9vcmdhbml6YXRpb25zL2RlZmF1bHQtMmFhYjBkOTgtYTU0Ny00OTYzLWE1OTgtMGEzODkxZTNiYzRiL3JlZ3VsYXJfdXNlciJdLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwib3JnYW5pemF0aW9ucyI6WyJkZWZhdWx0LTJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0YiJdLCJuYW1lIjoiQmx1cyBMaSIsInVzZXJfY29udGV4dF9pZCI6ImZkYmVjMWZmLTg0MjEtNDBmNS1hN2FhLTU0ODlkMTkyNjI5OSIsImNvbnRleHRfcm9sZXMiOnt9LCJjb250ZXh0X2dyb3VwcyI6WyIvYWNjZXNzX2dyb3Vwcy91c2VyX3R5cG9sb2d5L2NvcGVybmljdXNfZ2VuZXJhbC8iLCIvb3JnYW5pemF0aW9ucy9kZWZhdWx0LTJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0Yi9yZWd1bGFyX3VzZXIvIl0sInByZWZlcnJlZF91c2VybmFtZSI6InZwbmdhdGVzNUBnbWFpbC5jb20iLCJnaXZlbl9uYW1lIjoiQmx1cyIsInVzZXJfY29udGV4dCI6ImRlZmF1bHQtMmFhYjBkOTgtYTU0Ny00OTYzLWE1OTgtMGEzODkxZTNiYzRiIiwiZmFtaWx5X25hbWUiOiJMaSIsImVtYWlsIjoidnBuZ2F0ZXM1QGdtYWlsLmNvbSJ9.PsJY7jIXxXodFjf3UxqFP-1Knq7wD-QrmpDg-COn_cw7L6RXGYYicEtsfH77nkGjgm2SFbdGmBFqavxF5KJDXP3RfeDZg4JPKT2XoOT6gjmqLNN7BWz5tcgMbtw_aEzf-YqQ72BODHTRhZyQAMdHDO9SJzIZsFU6lpTvTN_qu8EUHEbw8OlH_yLr9F6QxqoyJc6PAgLbl9MUzgc0yQki84jWNj2nDN6ZxUHefen-vS7Aqj63VoPtByTmo9CqFKICIkXrH92a_ErCBRYor1Vnv1accHJOwFq3I3fdYIbDdTfmvhGssx_8_P4MJoBWBoLwCfqGmaMMd57aF7PUCf8E0g";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://sh.dataspace.copernicus.eu/api/v1/process");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Edg/122.0.0.0");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
            //request.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJYVUh3VWZKaHVDVWo0X3k4ZF8xM0hxWXBYMFdwdDd2anhob2FPLUxzREZFIn0.eyJleHAiOjE3MTE2NzU3MzEsImlhdCI6MTcxMTY3NDgzMSwiYXV0aF90aW1lIjoxNzExNjc0ODMxLCJqdGkiOiJmMjFkMzBlNC1hN2FiLTQ5NDgtOTFhOC00YzA4MGQ3NjYwOWUiLCJpc3MiOiJodHRwczovL2lkZW50aXR5LmRhdGFzcGFjZS5jb3Blcm5pY3VzLmV1L2F1dGgvcmVhbG1zL0NEU0UiLCJhdWQiOlsiQ0xPVURGRVJST19QVUJMSUMiLCJhY2NvdW50Il0sInN1YiI6IjJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0YiIsInR5cCI6IkJlYXJlciIsImF6cCI6InNoLTVmOGI2MzBiLWIwODMtNDllZC1iMzQwLWI4ZjAxZWNiODFjNCIsIm5vbmNlIjoiNjI1ODk1MjY4NzQ4NzQxNiIsInNlc3Npb25fc3RhdGUiOiJiY2NlNTE4ZS04NDEwLTRjMTYtODk0Ni04MDY2Yjg1NzcwMTQiLCJyZWFsbV9hY2Nlc3MiOnsicm9sZXMiOlsib2ZmbGluZV9hY2Nlc3MiLCJ1bWFfYXV0aG9yaXphdGlvbiIsImRlZmF1bHQtcm9sZXMtY2RhcyIsImNvcGVybmljdXMtZ2VuZXJhbCJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoiQVVESUVOQ0VfUFVCTElDIGVtYWlsIHByb2ZpbGUgdXNlci1jb250ZXh0Iiwic2lkIjoiYmNjZTUxOGUtODQxMC00YzE2LTg5NDYtODA2NmI4NTc3MDE0IiwiZ3JvdXBfbWVtYmVyc2hpcCI6WyIvYWNjZXNzX2dyb3Vwcy91c2VyX3R5cG9sb2d5L2NvcGVybmljdXNfZ2VuZXJhbCIsIi9vcmdhbml6YXRpb25zL2RlZmF1bHQtMmFhYjBkOTgtYTU0Ny00OTYzLWE1OTgtMGEzODkxZTNiYzRiL3JlZ3VsYXJfdXNlciJdLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwib3JnYW5pemF0aW9ucyI6WyJkZWZhdWx0LTJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0YiJdLCJuYW1lIjoiQmx1cyBMaSIsInVzZXJfY29udGV4dF9pZCI6ImZkYmVjMWZmLTg0MjEtNDBmNS1hN2FhLTU0ODlkMTkyNjI5OSIsImNvbnRleHRfcm9sZXMiOnt9LCJjb250ZXh0X2dyb3VwcyI6WyIvYWNjZXNzX2dyb3Vwcy91c2VyX3R5cG9sb2d5L2NvcGVybmljdXNfZ2VuZXJhbC8iLCIvb3JnYW5pemF0aW9ucy9kZWZhdWx0LTJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0Yi9yZWd1bGFyX3VzZXIvIl0sInByZWZlcnJlZF91c2VybmFtZSI6InZwbmdhdGVzNUBnbWFpbC5jb20iLCJnaXZlbl9uYW1lIjoiQmx1cyIsInVzZXJfY29udGV4dCI6ImRlZmF1bHQtMmFhYjBkOTgtYTU0Ny00OTYzLWE1OTgtMGEzODkxZTNiYzRiIiwiZmFtaWx5X25hbWUiOiJMaSIsImVtYWlsIjoidnBuZ2F0ZXM1QGdtYWlsLmNvbSJ9.nwrsRN6t6erkJOropwiLee-U5mnz3DydD7r7UqwCc5TuW3jH5DuI7HacUtFL16DyzBAN86UEqTPvMn9NIvo4znmotUID8Oe25Z9xmKmEnpJYbXApVMiJtB5jVfEn4ZXp8s9JM7QQvNVjvMIIYoeTTSBhItwt66OQeRPscK4yvWC9r7Hp2otlpBbSitDvzYERYmGzHIeIwzk6yxBIGfdsQsOkM8oqwoAMPDF0WS1k1lYUoI7MMoNyTXyRJuIonls5G72Aa1PHUoDwMTUmYp_jHtBne4NffxDHFG4rjP-t6ot1cmhDhHseD2nnih8ekxXich5rqswYWFqoc0Ig0DIpWA");
            var auth = this.txtAuth.Text;
            if(string.IsNullOrWhiteSpace(auth))
            {
                MessageBox.Show("no token!!");
                return;
            }
            if(!auth.StartsWith("Bearer"))
            {
                MessageBox.Show("token not start with [Bearer] Please check token value!!");
                return;
            }
            request.Headers.Add("Authorization", auth);
            request.Headers.Add("Pragma", "no-cache");
            request.Headers.Add("Referer", "https://browser.dataspace.copernicus.eu/");
            request.Headers.Add("Sec-Ch-Ua", "\"Chromium\";v=\"122\", \"Not(A:Brand\";v=\"24\", \"Microsoft Edge\";v=\"122\"");
            var content = new StringContent("{\r\n    \"input\": {\r\n        \"bounds\": {\r\n            \"properties\": {\r\n                \"crs\": \"http://www.opengis.net/def/crs/EPSG/0/3857\"\r\n            },\r\n            \"bbox\": [\r\n                12875664.540581372,\r\n                3316755.531350368,\r\n                12885448.480201872,\r\n                3326539.4709708695\r\n            ]\r\n        },\r\n        \"data\": [\r\n            {\r\n                \"dataFilter\": {\r\n                    \"timeRange\": {\r\n                        \"from\": \"2022-07-20T00:00:00.000Z\",\r\n                        \"to\": \"2023-01-20T23:59:59.999Z\"\r\n                    },\r\n                    \"mosaickingOrder\": \"mostRecent\",\r\n                    \"previewMode\": \"EXTENDED_PREVIEW\",\r\n                    \"maxCloudCoverage\": 20\r\n                },\r\n                \"processing\": {\r\n                    \"upsampling\": \"BICUBIC\",\r\n                    \"downsampling\": \"NEAREST\"\r\n                },\r\n                \"type\": \"S2L2A\"\r\n            }\r\n        ]\r\n    },\r\n    \"output\": {\r\n        \"width\": 512,\r\n        \"height\": 512,\r\n        \"responses\": [\r\n            {\r\n                \"identifier\": \"default\",\r\n                \"format\": {\r\n                    \"type\": \"image/png\"\r\n                }\r\n            }\r\n        ]\r\n    },\r\n    \"evalscript\": \"//VERSION=3\\nfunction setup() {\\n  return {\\n    input: [\\\"B04\\\",\\\"B03\\\",\\\"B02\\\", \\\"dataMask\\\"],\\n    output: { bands: 4 }\\n  };\\n}\\n\\n// Contrast enhance / highlight compress\\n\\nconst maxR = 3.0; // max reflectance\\nconst midR = 0.13;\\nconst sat = 1.2;\\nconst gamma = 1.8;\\n\\nfunction evaluatePixel(smp) {\\n  const rgbLin = satEnh(sAdj(smp.B04), sAdj(smp.B03), sAdj(smp.B02));\\n  return [sRGB(rgbLin[0]), sRGB(rgbLin[1]), sRGB(rgbLin[2]), smp.dataMask];\\n}\\n\\nfunction sAdj(a) {\\n  return adjGamma(adj(a, midR, 1, maxR));\\n}\\n\\nconst gOff = 0.01;\\nconst gOffPow = Math.pow(gOff, gamma);\\nconst gOffRange = Math.pow(1 + gOff, gamma) - gOffPow;\\n\\nfunction adjGamma(b) {\\n  return (Math.pow((b + gOff), gamma) - gOffPow)/gOffRange;\\n}\\n\\n// Saturation enhancement\\nfunction satEnh(r, g, b) {\\n  const avgS = (r + g + b) / 3.0 * (1 - sat);\\n  return [clip(avgS + r * sat), clip(avgS + g * sat), clip(avgS + b * sat)];\\n}\\n\\nfunction clip(s) {\\n  return s < 0 ? 0 : s > 1 ? 1 : s;\\n}\\n\\n//contrast enhancement with highlight compression\\nfunction adj(a, tx, ty, maxC) {\\n  var ar = clip(a / maxC, 0, 1);\\n  return ar * (ar * (tx/maxC + ty -1) - ty) / (ar * (2 * tx/maxC - 1) - tx/maxC);\\n}\\n\\nconst sRGB = (c) => c <= 0.0031308 ? (12.92 * c) : (1.055 * Math.pow(c, 0.41666666666) - 0.055);\\n\"\r\n}", null, "application/json");
            var jsonFile = "postJson.txt";
            string postJsonFile = "";
            postObjModel  obj=null;
            if (File.Exists(jsonFile))
            {
                postJsonFile = File.ReadAllText(jsonFile);
                if (!string.IsNullOrWhiteSpace(postJsonFile))
                {
                     obj = JsonConvert.DeserializeObject<postObjModel>(postJsonFile);
                    if (obj?.input.bounds.bbox.Length > 0)
                    {
                        var stringContent = JsonConvert.SerializeObject(obj);
                        content =new StringContent( JsonConvert.SerializeObject(obj));
                    }
                    if (x1 != 0)
                    {
                        obj.input.bounds.bbox = new double[] { x1, y1, x2, y2 };
                        content = new StringContent(JsonConvert.SerializeObject(obj));
                    }
                }
            }
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;

            //var jsonStr = await content.ReadAsStringAsync();
            //var jsonObj = JsonConvert.DeserializeObject<postObjModel>(jsonStr);
            //var lo = obj.input.bounds.bbox[0];
            var response = await client.SendAsync(request);
            var code = response.StatusCode;
            if (code == System.Net.HttpStatusCode.Unauthorized)
            {
                MessageBox.Show("token过期，请重新获取并填入左边的Authorized框");
                return;
            }
            if (response.Content.Headers.ContentType.MediaType == "image/png")
            {
                var item =await response.Content.ReadAsStreamAsync();
                var bty =await response.Content.ReadAsByteArrayAsync();
                //var name = response.Content.Headers.ContentDisposition.FileName;
                File.WriteAllBytes(x1.ToString("R")+"_"+y1.ToString("R")+"_"+lyer+".png", bty);
            }
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

        }

        private void txtLyer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCalculate_Click(null, null);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLatLng.Text) || string.IsNullOrWhiteSpace(txtLyer.Text))
            {
                MessageBox.Show("pls input lat and lng,like 39.907500,116.388056 , lyer is 2-10");
                return;
            }
            if (!txtLatLng.Text.Contains(','))
            {
                MessageBox.Show("pls input lat and lng,like 39.907500,116.388056 , split by ,");
                return;
            }
            double lat = 0;
            double lng = 0;
            if (!double.TryParse(txtLatLng.Text.Split(',')[0], out lat))
            {
                MessageBox.Show("pls input lat and lng,like 39.907500,116.388056 , split by ,");
                return;
            }
            if (!double.TryParse(txtLatLng.Text.Split(',')[1], out lng))
            {
                MessageBox.Show("pls input lat and lng,like 39.907500,116.388056 , split by ,");
                return;
            }
            if (lat > 85.0 || lat < -85 || lng > 180.0 || lng < -180)
            {
                MessageBox.Show("pls input lat and lng,like 39.907500,116.388056 , split by ,");
                return;
            }
            lyer = 2;
            if (!int.TryParse(txtLyer.Text, out lyer))
            {
                MessageBox.Show("pls input lat and lng,like 39.907500,116.388056 , lyer is 2-10");
                return;
            }


            //var EarthR = 6378137; //the Equatorial radius of the Earth
            //var DeltaRange = EarthR / Math.Pow(2, lyer - 2);//the X and Y change value in BBOX 

            DeltaRange = EarthR * Math.PI / Math.Pow(2, lyer - 2);
            txtDeltaRange.Text = DeltaRange.ToString("N9");

            var temX = lng / 180 * EarthR * Math.PI; //x value in Mercator projection
            x1 = temX - temX % DeltaRange;
            x2 = x1 + DeltaRange;

            var temY = EarthR * Math.Log(Math.Tan(Math.PI / 4 + lat * Math.PI / 180 / 2)); //the y value in Mercator projection ,reference 墨卡托投影 - virtual bricks的文章 - 知乎 https://zhuanlan.zhihu.com/p/358213607
            y1 = temY - temY % DeltaRange;
            y2 = y1 + DeltaRange;

            var tem = x1.ToString("R") + "," + y1.ToString("R") + "," + x2.ToString("R") + "," + y2.ToString("R");
            txtBbox.Text = tem;

        }

        /// <summary>
        /// the Equatorial radius of the Earth
        /// 6378137
        /// </summary>
        double EarthR = 6378137;

        /// <summary>
        /// the X and Y change value in BBOX 
        /// </summary>
        double DeltaRange;

        /// <summary>
        /// lyer number
        /// </summary>
        int lyer = 2;


        /// <summary>
        /// BBox coordinate
        /// [x1,y1,x2,y2]
        /// </summary>
        double x1, y1, x2, y2;

        private void button4_Click(object sender, EventArgs e)
        {
            ImportImage(".");
        }


        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="filePath">图片文件夹路径</param>
        public void ImportImage(string filePath)
        {
            string conn = "server=.;database=tests;Uid=sa;Pwd=123dugu@456;";
            var dir = new System.IO.DirectoryInfo(filePath);
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                myconn.Open();
                using (SqlCommand mycomm = new SqlCommand())
                {
                    foreach (FileInfo item in dir.GetFiles("*.png"))//循环读取文件夹内的png文件
                    {
                        //var pic = getJpgSize(item.FullName);
                        var name = Path.GetFileNameWithoutExtension(item.FullName);
                        if (!((name.Contains("_") && name.Split('_').Count() == 3)))
                            continue;
                        var nameSplit = name.Split('_');
                        var z = int.Parse(nameSplit[0]);
                        var x = int.Parse(nameSplit[1]);
                        var y = int.Parse(nameSplit[2]);
                        var quadKey = TileMath.TileXYToQuadKey(x,y,z);
                        string str = string.Format("insert into ImageFiles (QuadKey,Name,Type,Image) values('{0}','{1}','{2}',@file)", quadKey, Path.GetFileNameWithoutExtension(item.FullName), item.Extension.Substring(1));//插入数据
                        mycomm.CommandText = str;
                        mycomm.Connection = myconn;
                        FileStream fs = new FileStream(item.FullName, FileMode.Open);
                        BinaryReader br = new BinaryReader(fs);
                        Byte[] byData = br.ReadBytes((int)fs.Length);
                        fs.Close();
                        mycomm.Parameters.Add("@file", SqlDbType.Binary, byData.Length);
                        mycomm.Parameters["@file"].Value = byData;
                        mycomm.ExecuteNonQuery();
                        mycomm.Parameters.Clear();
                    }
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            byte[] testPic = null;
            string conn = "server=.;database=tests;Uid=sa;Pwd=123dugu@456;";
            var valuse = txtPos.Text.Split(',');
            var z = int.Parse(valuse[0]);
            var x = int.Parse(valuse[1]);
            var y = int.Parse(valuse[2]);

            using (SqlConnection myconn = new SqlConnection(conn))
            {
                myconn.Open();
                using (SqlCommand mycomm = new SqlCommand())
                {
                    var quadKey = TileMath.TileXYToQuadKey(x, y, z);
                    txtQuadKey.Text = quadKey;
                    string str = string.Format("select * from ImageFilesG where QuadKey = '{0}'",quadKey);
                    mycomm.CommandText = str;
                    mycomm.Connection = myconn;

                    SqlDataReader data = mycomm.ExecuteReader();
                    if (data.HasRows)
                    {
                        data.Read();
                        testPic = (byte[])data["Image"];
                    }
                }
            }
            if (testPic == null || testPic.Length == 0)
            {
                txtQuadKey.Text = "NoValue!";
                return;
            }
            File.WriteAllBytes("testPic" + ".png", testPic);
            picBox.Image = Image.FromStream(new MemoryStream(testPic));
            picBox.Visible = true;
            picBox.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var filePath = @"H:\mp1";
            string conn = "server=HD-ZHOU;database=tests;Uid=sa;Pwd=123dugu@456;";
            var dir = new System.IO.DirectoryInfo(filePath);
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                myconn.Open();
                using (SqlCommand mycomm = new SqlCommand())
                {
                    foreach (var dirZ in dir.GetDirectories())
                    {
                        var z = dirZ.Name;
                        int lyer = int.Parse(z);
                        if (lyer < 16)
                            continue;
                        foreach (var dirX in dirZ.GetDirectories())
                        {
                            var x = dirX.Name;
                            int lyerX = int.Parse(x);
                            if (lyer == 16 && lyerX < 53544)
                                continue;
                            foreach (var dirY in dirX.GetDirectories())
                            {
                                var y = dirY.Name;
                                int lyerY = int.Parse(y);
                                if (lyer == 16 && lyerX == 53544 && lyerY <=27893)
                                    continue;
                                foreach (FileInfo item in dirY.GetFiles("*.png"))//循环读取文件夹内的png文件
                                {
                                    //var pic = getJpgSize(item.FullName);
                                    //var name = Path.GetFileNameWithoutExtension(item.FullName);
                                    //if (!((name.Contains("_") && name.Split('_').Count() == 3)))
                                    //    continue;
                                    //var nameSplit = name.Split('_');
                                    //var z = int.Parse(nameSplit[0]);
                                    //var x = int.Parse(nameSplit[1]);
                                    //var y = int.Parse(nameSplit[2]);
                                    var name = z + "_" + x + "_" + y ;
                                    var quadKey = TileMath.TileXYToQuadKey(int.Parse(x), int.Parse(y), int.Parse(z));
                                    string str = string.Format("insert into ImageFilesG (QuadKey,Name,Type,Image) values('{0}','{1}','{2}',@file)", quadKey, name, item.Extension.Substring(1));//插入数据
                                    mycomm.CommandText = str;
                                    mycomm.Connection = myconn;
                                    FileStream fs = new FileStream(item.FullName, FileMode.Open);
                                    BinaryReader br = new BinaryReader(fs);
                                    Byte[] byData = br.ReadBytes((int)fs.Length);
                                    fs.Close();
                                    mycomm.Parameters.Add("@file", SqlDbType.Binary, byData.Length);
                                    mycomm.Parameters["@file"].Value = byData;
                                    mycomm.ExecuteNonQuery();
                                    mycomm.Parameters.Clear();
                                }
                            }
                        }
                        
                    }
                    
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://sh.dataspace.copernicus.eu/api/v1/process");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Edg/122.0.0.0");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
            request.Headers.Add("Pragma", "no-cache");
            request.Headers.Add("Referer", "https://browser.dataspace.copernicus.eu/");
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJYVUh3VWZKaHVDVWo0X3k4ZF8xM0hxWXBYMFdwdDd2anhob2FPLUxzREZFIn0.eyJleHAiOjE3MTI1NjY5NTcsImlhdCI6MTcxMjU2NjA1NywiYXV0aF90aW1lIjoxNzEyNTQ2MTMxLCJqdGkiOiJhMWM1OWE5ZC04MDg1LTQwZWQtYTFlOS1iODI0YTRkMTQyYjciLCJpc3MiOiJodHRwczovL2lkZW50aXR5LmRhdGFzcGFjZS5jb3Blcm5pY3VzLmV1L2F1dGgvcmVhbG1zL0NEU0UiLCJhdWQiOlsiQ0xPVURGRVJST19QVUJMSUMiLCJhY2NvdW50Il0sInN1YiI6IjJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0YiIsInR5cCI6IkJlYXJlciIsImF6cCI6InNoLTVmOGI2MzBiLWIwODMtNDllZC1iMzQwLWI4ZjAxZWNiODFjNCIsIm5vbmNlIjoiNTE3ODQ1ODYxMjE5NzAxOSIsInNlc3Npb25fc3RhdGUiOiIwNDE1NzZjZC00MzNmLTQwNTktOGI3ZS1lN2ZkMzg1MTRjN2EiLCJyZWFsbV9hY2Nlc3MiOnsicm9sZXMiOlsib2ZmbGluZV9hY2Nlc3MiLCJ1bWFfYXV0aG9yaXphdGlvbiIsImRlZmF1bHQtcm9sZXMtY2RhcyIsImNvcGVybmljdXMtZ2VuZXJhbCJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoiQVVESUVOQ0VfUFVCTElDIGVtYWlsIHByb2ZpbGUgdXNlci1jb250ZXh0Iiwic2lkIjoiMDQxNTc2Y2QtNDMzZi00MDU5LThiN2UtZTdmZDM4NTE0YzdhIiwiZ3JvdXBfbWVtYmVyc2hpcCI6WyIvYWNjZXNzX2dyb3Vwcy91c2VyX3R5cG9sb2d5L2NvcGVybmljdXNfZ2VuZXJhbCIsIi9vcmdhbml6YXRpb25zL2RlZmF1bHQtMmFhYjBkOTgtYTU0Ny00OTYzLWE1OTgtMGEzODkxZTNiYzRiL3JlZ3VsYXJfdXNlciJdLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwib3JnYW5pemF0aW9ucyI6WyJkZWZhdWx0LTJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0YiJdLCJuYW1lIjoiQmx1cyBMaSIsInVzZXJfY29udGV4dF9pZCI6ImZkYmVjMWZmLTg0MjEtNDBmNS1hN2FhLTU0ODlkMTkyNjI5OSIsImNvbnRleHRfcm9sZXMiOnt9LCJjb250ZXh0X2dyb3VwcyI6WyIvYWNjZXNzX2dyb3Vwcy91c2VyX3R5cG9sb2d5L2NvcGVybmljdXNfZ2VuZXJhbC8iLCIvb3JnYW5pemF0aW9ucy9kZWZhdWx0LTJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0Yi9yZWd1bGFyX3VzZXIvIl0sInByZWZlcnJlZF91c2VybmFtZSI6InZwbmdhdGVzNUBnbWFpbC5jb20iLCJnaXZlbl9uYW1lIjoiQmx1cyIsInVzZXJfY29udGV4dCI6ImRlZmF1bHQtMmFhYjBkOTgtYTU0Ny00OTYzLWE1OTgtMGEzODkxZTNiYzRiIiwiZmFtaWx5X25hbWUiOiJMaSIsImVtYWlsIjoidnBuZ2F0ZXM1QGdtYWlsLmNvbSJ9.PsJY7jIXxXodFjf3UxqFP-1Knq7wD-QrmpDg-COn_cw7L6RXGYYicEtsfH77nkGjgm2SFbdGmBFqavxF5KJDXP3RfeDZg4JPKT2XoOT6gjmqLNN7BWz5tcgMbtw_aEzf-YqQ72BODHTRhZyQAMdHDO9SJzIZsFU6lpTvTN_qu8EUHEbw8OlH_yLr9F6QxqoyJc6PAgLbl9MUzgc0yQki84jWNj2nDN6ZxUHefen-vS7Aqj63VoPtByTmo9CqFKICIkXrH92a_ErCBRYor1Vnv1accHJOwFq3I3fdYIbDdTfmvhGssx_8_P4MJoBWBoLwCfqGmaMMd57aF7PUCf8E0g");
            request.Headers.Add("Sec-Ch-Ua", "\"Chromium\";v=\"122\", \"Not(A:Brand\";v=\"24\", \"Microsoft Edge\";v=\"122\"");
            var content = new StringContent("{\r\n    \"input\": {\r\n        \"bounds\": {\r\n            \"properties\": {\r\n                \"crs\": \"http://www.opengis.net/def/crs/EPSG/0/3857\"\r\n            },\r\n            \"bbox\": [\r\n             -20037508.342789244, 0, 0, 20037508.342789244\r\n\r\n\r\n\r\n            ]\r\n        },\r\n        \"data\": [\r\n            {\r\n                \"dataFilter\": {\r\n                    \"timeRange\": {\r\n                        \"from\": \"2023-10-01T00:00:00.000Z\",\r\n                        \"to\": \"2023-10-01T23:59:59.999Z\"\r\n                    },\r\n                    \"mosaickingOrder\": \"mostRecent\",\r\n                    \"previewMode\": \"EXTENDED_PREVIEW\"\r\n                },\r\n                \"processing\": {\r\n                    \"upsampling\": \"BILINEAR\",\r\n                    \"downsampling\": \"NEAREST\"\r\n                },\r\n                \"type\": \"byoc-6af2d932-8f18-4bed-a31b-d32bc49d43a0\"\r\n            }\r\n        ]\r\n    },\r\n    \"output\": {\r\n        \"width\": 512,\r\n        \"height\": 512,\r\n        \"responses\": [\r\n            {\r\n                \"identifier\": \"default\",\r\n                \"format\": {\r\n                    \"type\": \"image/png\"\r\n                }\r\n            }\r\n        ]\r\n    },\r\n    \"evalscript\": \"//VERSION=3\\nfunction setup() {\\n  return {\\n    input: [\\\"B04\\\",\\\"B03\\\",\\\"B02\\\", \\\"dataMask\\\"],\\n    output: { bands: 4 }\\n  };\\n}\\n\\n// Contrast enhance / highlight compress\\n\\nconst maxR = 3.0; // max reflectance\\nconst midR = 0.13;\\nconst sat = 1.2;\\nconst gamma = 1.8;\\nconst scalefac = 10000;\\n\\nfunction evaluatePixel(smp) {\\n  const rgbLin = satEnh(sAdj(smp.B04/scalefac), sAdj(smp.B03/scalefac), sAdj(smp.B02/scalefac));\\n  return [sRGB(rgbLin[0]), sRGB(rgbLin[1]), sRGB(rgbLin[2]), smp.dataMask];\\n}\\n\\nfunction sAdj(a) {\\n  return adjGamma(adj(a, midR, 1, maxR));\\n}\\n\\nconst gOff = 0.01;\\nconst gOffPow = Math.pow(gOff, gamma);\\nconst gOffRange = Math.pow(1 + gOff, gamma) - gOffPow;\\n\\nfunction adjGamma(b) {\\n  return (Math.pow((b + gOff), gamma) - gOffPow)/gOffRange;\\n}\\n\\n// Saturation enhancement\\nfunction satEnh(r, g, b) {\\n  const avgS = (r + g + b) / 3.0 * (1 - sat);\\n  return [clip(avgS + r * sat), clip(avgS + g * sat), clip(avgS + b * sat)];\\n}\\n\\nfunction clip(s) {\\n  return s < 0 ? 0 : s > 1 ? 1 : s;\\n}\\n\\n//contrast enhancement with highlight compression\\nfunction adj(a, tx, ty, maxC) {\\n  var ar = clip(a / maxC, 0, 1);\\n  return ar * (ar * (tx/maxC + ty -1) - ty) / (ar * (2 * tx/maxC - 1) - tx/maxC);\\n}\\n\\nconst sRGB = (c) => c <= 0.0031308 ? (12.92 * c) : (1.055 * Math.pow(c, 0.41666666666) - 0.055);\"\r\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            if (response.Content.Headers.ContentType.MediaType == "image/png")
            {
                var item = await response.Content.ReadAsStreamAsync();
                var bty = await response.Content.ReadAsByteArrayAsync();
                //var name = response.Content.Headers.ContentDisposition.FileName;
                //File.WriteAllBytes(x1.ToString("R") + "_" + y1.ToString("R") + "_" + lyer + ".png", bty);
                File.WriteAllBytes("test" + ".png", bty);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            for (int z = 2; z <= 14; z++)
            {
                if (z < 6)
                    continue;
                DeltaRange =2* EarthR * Math.PI / Math.Pow(2, z - 1);
                var times = 2 * EarthR * Math.PI / DeltaRange;
                for (int x = 0; x < times; x++)
                {
                    if (z == 6 && x < 22)
                        continue;
                    //var soureY = EarthR * Math.PI;
                    decimal soureY = 20037508.34278071M;
                    var DRy =2* soureY / (decimal)Math.Pow(2, z - 1);
                    var timesY = 2*soureY / DRy;

                    for (int y = 0; y < timesY; y++)
                    {
                        if (z == 6 && x == 22 && y<=24)
                            continue;
                        x1 = -EarthR * Math.PI + x * DeltaRange;
                        y1 = EarthR * Math.PI - (y + 1) * DeltaRange;
                        var y1d = soureY - (y + 1) * DRy;
                        x2 = x1 + DeltaRange;
                        y2 = y1 + DeltaRange;
                        var y2d = y1d + DRy;
                        if (!File.Exists(z + "_" + x + "_" + y + ".png"))
                        {
                            var picName = z + "_" + x + "_" + y;
                            txtPos.Text = picName;
                            var isOk = await DownloadTile(z + "_" + x + "_" + y);
                            if (!isOk)
                                return;
                        }
                    }
                }

            }
        }

        async Task<bool> DownloadTile(string fileName)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://sh.dataspace.copernicus.eu/api/v1/process");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Edg/122.0.0.0");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6");
            //request.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJYVUh3VWZKaHVDVWo0X3k4ZF8xM0hxWXBYMFdwdDd2anhob2FPLUxzREZFIn0.eyJleHAiOjE3MTE2NzU3MzEsImlhdCI6MTcxMTY3NDgzMSwiYXV0aF90aW1lIjoxNzExNjc0ODMxLCJqdGkiOiJmMjFkMzBlNC1hN2FiLTQ5NDgtOTFhOC00YzA4MGQ3NjYwOWUiLCJpc3MiOiJodHRwczovL2lkZW50aXR5LmRhdGFzcGFjZS5jb3Blcm5pY3VzLmV1L2F1dGgvcmVhbG1zL0NEU0UiLCJhdWQiOlsiQ0xPVURGRVJST19QVUJMSUMiLCJhY2NvdW50Il0sInN1YiI6IjJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0YiIsInR5cCI6IkJlYXJlciIsImF6cCI6InNoLTVmOGI2MzBiLWIwODMtNDllZC1iMzQwLWI4ZjAxZWNiODFjNCIsIm5vbmNlIjoiNjI1ODk1MjY4NzQ4NzQxNiIsInNlc3Npb25fc3RhdGUiOiJiY2NlNTE4ZS04NDEwLTRjMTYtODk0Ni04MDY2Yjg1NzcwMTQiLCJyZWFsbV9hY2Nlc3MiOnsicm9sZXMiOlsib2ZmbGluZV9hY2Nlc3MiLCJ1bWFfYXV0aG9yaXphdGlvbiIsImRlZmF1bHQtcm9sZXMtY2RhcyIsImNvcGVybmljdXMtZ2VuZXJhbCJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoiQVVESUVOQ0VfUFVCTElDIGVtYWlsIHByb2ZpbGUgdXNlci1jb250ZXh0Iiwic2lkIjoiYmNjZTUxOGUtODQxMC00YzE2LTg5NDYtODA2NmI4NTc3MDE0IiwiZ3JvdXBfbWVtYmVyc2hpcCI6WyIvYWNjZXNzX2dyb3Vwcy91c2VyX3R5cG9sb2d5L2NvcGVybmljdXNfZ2VuZXJhbCIsIi9vcmdhbml6YXRpb25zL2RlZmF1bHQtMmFhYjBkOTgtYTU0Ny00OTYzLWE1OTgtMGEzODkxZTNiYzRiL3JlZ3VsYXJfdXNlciJdLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwib3JnYW5pemF0aW9ucyI6WyJkZWZhdWx0LTJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0YiJdLCJuYW1lIjoiQmx1cyBMaSIsInVzZXJfY29udGV4dF9pZCI6ImZkYmVjMWZmLTg0MjEtNDBmNS1hN2FhLTU0ODlkMTkyNjI5OSIsImNvbnRleHRfcm9sZXMiOnt9LCJjb250ZXh0X2dyb3VwcyI6WyIvYWNjZXNzX2dyb3Vwcy91c2VyX3R5cG9sb2d5L2NvcGVybmljdXNfZ2VuZXJhbC8iLCIvb3JnYW5pemF0aW9ucy9kZWZhdWx0LTJhYWIwZDk4LWE1NDctNDk2My1hNTk4LTBhMzg5MWUzYmM0Yi9yZWd1bGFyX3VzZXIvIl0sInByZWZlcnJlZF91c2VybmFtZSI6InZwbmdhdGVzNUBnbWFpbC5jb20iLCJnaXZlbl9uYW1lIjoiQmx1cyIsInVzZXJfY29udGV4dCI6ImRlZmF1bHQtMmFhYjBkOTgtYTU0Ny00OTYzLWE1OTgtMGEzODkxZTNiYzRiIiwiZmFtaWx5X25hbWUiOiJMaSIsImVtYWlsIjoidnBuZ2F0ZXM1QGdtYWlsLmNvbSJ9.nwrsRN6t6erkJOropwiLee-U5mnz3DydD7r7UqwCc5TuW3jH5DuI7HacUtFL16DyzBAN86UEqTPvMn9NIvo4znmotUID8Oe25Z9xmKmEnpJYbXApVMiJtB5jVfEn4ZXp8s9JM7QQvNVjvMIIYoeTTSBhItwt66OQeRPscK4yvWC9r7Hp2otlpBbSitDvzYERYmGzHIeIwzk6yxBIGfdsQsOkM8oqwoAMPDF0WS1k1lYUoI7MMoNyTXyRJuIonls5G72Aa1PHUoDwMTUmYp_jHtBne4NffxDHFG4rjP-t6ot1cmhDhHseD2nnih8ekxXich5rqswYWFqoc0Ig0DIpWA");
            var auth = this.txtAuth.Text.Replace("\r\n","").Replace("\r","");
            if (string.IsNullOrWhiteSpace(auth))
            {
                MessageBox.Show("no token!!");
                return false;
            }
            if (!auth.StartsWith("Bearer"))
            {
                MessageBox.Show("token not start with [Bearer] Please check token value!!");
                return false;
            }
            request.Headers.Add("Authorization", auth);
            request.Headers.Add("Pragma", "no-cache");
            request.Headers.Add("Referer", "https://browser.dataspace.copernicus.eu/");
            request.Headers.Add("Sec-Ch-Ua", "\"Chromium\";v=\"122\", \"Not(A:Brand\";v=\"24\", \"Microsoft Edge\";v=\"122\"");
            var content = new StringContent("{\r\n    \"input\": {\r\n        \"bounds\": {\r\n            \"properties\": {\r\n                \"crs\": \"http://www.opengis.net/def/crs/EPSG/0/3857\"\r\n            },\r\n            \"bbox\": [\r\n                12875664.540581372,\r\n                3316755.531350368,\r\n                12885448.480201872,\r\n                3326539.4709708695\r\n            ]\r\n        },\r\n        \"data\": [\r\n            {\r\n                \"dataFilter\": {\r\n                    \"timeRange\": {\r\n                        \"from\": \"2022-07-20T00:00:00.000Z\",\r\n                        \"to\": \"2023-01-20T23:59:59.999Z\"\r\n                    },\r\n                    \"mosaickingOrder\": \"mostRecent\",\r\n                    \"previewMode\": \"EXTENDED_PREVIEW\",\r\n                    \"maxCloudCoverage\": 20\r\n                },\r\n                \"processing\": {\r\n                    \"upsampling\": \"BICUBIC\",\r\n                    \"downsampling\": \"NEAREST\"\r\n                },\r\n                \"type\": \"S2L2A\"\r\n            }\r\n        ]\r\n    },\r\n    \"output\": {\r\n        \"width\": 512,\r\n        \"height\": 512,\r\n        \"responses\": [\r\n            {\r\n                \"identifier\": \"default\",\r\n                \"format\": {\r\n                    \"type\": \"image/png\"\r\n                }\r\n            }\r\n        ]\r\n    },\r\n    \"evalscript\": \"//VERSION=3\\nfunction setup() {\\n  return {\\n    input: [\\\"B04\\\",\\\"B03\\\",\\\"B02\\\", \\\"dataMask\\\"],\\n    output: { bands: 4 }\\n  };\\n}\\n\\n// Contrast enhance / highlight compress\\n\\nconst maxR = 3.0; // max reflectance\\nconst midR = 0.13;\\nconst sat = 1.2;\\nconst gamma = 1.8;\\n\\nfunction evaluatePixel(smp) {\\n  const rgbLin = satEnh(sAdj(smp.B04), sAdj(smp.B03), sAdj(smp.B02));\\n  return [sRGB(rgbLin[0]), sRGB(rgbLin[1]), sRGB(rgbLin[2]), smp.dataMask];\\n}\\n\\nfunction sAdj(a) {\\n  return adjGamma(adj(a, midR, 1, maxR));\\n}\\n\\nconst gOff = 0.01;\\nconst gOffPow = Math.pow(gOff, gamma);\\nconst gOffRange = Math.pow(1 + gOff, gamma) - gOffPow;\\n\\nfunction adjGamma(b) {\\n  return (Math.pow((b + gOff), gamma) - gOffPow)/gOffRange;\\n}\\n\\n// Saturation enhancement\\nfunction satEnh(r, g, b) {\\n  const avgS = (r + g + b) / 3.0 * (1 - sat);\\n  return [clip(avgS + r * sat), clip(avgS + g * sat), clip(avgS + b * sat)];\\n}\\n\\nfunction clip(s) {\\n  return s < 0 ? 0 : s > 1 ? 1 : s;\\n}\\n\\n//contrast enhancement with highlight compression\\nfunction adj(a, tx, ty, maxC) {\\n  var ar = clip(a / maxC, 0, 1);\\n  return ar * (ar * (tx/maxC + ty -1) - ty) / (ar * (2 * tx/maxC - 1) - tx/maxC);\\n}\\n\\nconst sRGB = (c) => c <= 0.0031308 ? (12.92 * c) : (1.055 * Math.pow(c, 0.41666666666) - 0.055);\\n\"\r\n}", null, "application/json");
            var jsonFile = "postJson.txt";
            string postJsonFile = "";
            postObjModel obj = null;
            if (File.Exists(jsonFile))
            {
                postJsonFile = File.ReadAllText(jsonFile);
                if (!string.IsNullOrWhiteSpace(postJsonFile))
                {
                    obj = JsonConvert.DeserializeObject<postObjModel>(postJsonFile);
                    if (obj?.input.bounds.bbox.Length > 0)
                    {
                        var stringContent = JsonConvert.SerializeObject(obj);
                        content = new StringContent(JsonConvert.SerializeObject(obj), null, "application/json");
                    }
                    if (x1 !=null)
                    {
                        obj.input.bounds.bbox = new double[] { x1, y1, x2, y2 };
                        content = new StringContent(JsonConvert.SerializeObject(obj), null, "application/json");
                    }
                }
            }
            //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            request.Content = content;

            //var jsonStr = await content.ReadAsStringAsync();
            //var jsonObj = JsonConvert.DeserializeObject<postObjModel>(jsonStr);
            //var lo = obj.input.bounds.bbox[0];
            var response = await client.SendAsync(request);
            var code = response.StatusCode;
            if (code == System.Net.HttpStatusCode.Unauthorized)
            {
                MessageBox.Show("token过期，请重新获取并填入左边的Authorized框");
                return false;
            }
            if (response.Content.Headers.ContentType.MediaType == "image/png")
            {
                var item = await response.Content.ReadAsStreamAsync();
                var bty = await response.Content.ReadAsByteArrayAsync();
                //var name = response.Content.Headers.ContentDisposition.FileName;
                //File.WriteAllBytes(x1.ToString("R") + "_" + y1.ToString("R") + "_" + lyer + ".png", bty);
                File.WriteAllBytes(fileName+ ".png", bty);
                return true;
            }
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return true;
        }
    }
}
