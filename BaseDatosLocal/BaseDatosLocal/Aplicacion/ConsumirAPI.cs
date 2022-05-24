using BaseDatosLocal.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BaseDatosLocal.Aplicacion
{
    public class ConsumirAPI
    {
        #region Listar
       
        public  static async Task<List<Persona>> ListarTodos(string urlWebApi, string mediaType, string charSet, string MetodoAPIBuscarTodos)

        {
            var lista = await Task.Run(async () => {
                string jsonData;

                List<Persona> listado = new List<Persona>();
                HttpResponseMessage response = null;                
               
                string baseUrl = urlWebApi;

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseUrl);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

                    jsonData = "{}";
                    try
                    {
                        StringContent content = new StringContent(jsonData, Encoding.UTF8, mediaType);
                        content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                        content.Headers.ContentType.CharSet = charSet;

                        response = await httpClient.PostAsync(MetodoAPIBuscarTodos, content);

                        var resultado = await response.Content.ReadAsStringAsync(); // conviete a String
                        dynamic ResultadoJson = JsonConvert.DeserializeObject(resultado);

                        dynamic vacio = ResultadoJson.Vacio;

                        if (!Convert.ToBoolean(vacio))
                        {
                            var valor = JsonConvert.SerializeObject(ResultadoJson.Mensaje);
                            listado = JsonConvert.DeserializeObject<List<Persona>>(valor);
                        }

                    }
                    catch (ArgumentNullException ex)
                    {
                        string msg = ex.Message;
                    }
                    catch (InvalidOperationException ex)
                    {
                        string msg = ex.Message;
                    }
                    catch (HttpRequestException ex)
                    {
                        string msg = ex.Message;
                    }
                    catch (TaskCanceledException ex)
                    {
                        string msg = ex.Message;
                    }
                }

                return listado;
            });


            return lista;

        }
        #endregion
        #region BuscarUno
        public static async Task<Persona> BuscarPorIdAPIAsync(string urlWebApi, string mediaType, string charSet, string ubicacionMetodoAPI, string valorFiltro)
        {

            Persona persona = null;
            HttpResponseMessage response = null;
            string baseUrl = urlWebApi + "/";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                        
                string jsonData = JsonConvert.SerializeObject(valorFiltro);

                StringContent content = new StringContent(jsonData, Encoding.UTF8, mediaType);
                content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                content.Headers.ContentType.CharSet = charSet;

                response = await httpClient.PostAsync(ubicacionMetodoAPI, content);


                var resultado = await response.Content.ReadAsStringAsync(); // conviete a String

                dynamic ResultadoJson = JsonConvert.DeserializeObject(resultado);

                dynamic vacio = ResultadoJson.Vacio;

                if (!Convert.ToBoolean(vacio))
                {
                    var valor = JsonConvert.SerializeObject(ResultadoJson.Mensaje);
                    persona = JsonConvert.DeserializeObject<Persona>(valor);
                }                
            }


            return persona;
        }
        #endregion
        #region GuardarActualizar  

        public static async Task<int> GuardarActualizarAPI(string urlWebApi, string mediaType, string charSet, string MetodoAPIGuardar,string MetodoApiActualizar, Persona valorGuardado) 
        {

                var resultadoGuardar = await Task.Run(async () => {
                int respuesta = 0;
                string ubicacionMetodoAPI = string.Empty;

                try
                {

                    using (HttpClient httpClient = new HttpClient())
                    {

                        string baseUrl = urlWebApi;
                        httpClient.BaseAddress = new Uri(baseUrl);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                                                        
                        string jsonData = JsonConvert.SerializeObject(valorGuardado);

                        StringContent content = new StringContent(jsonData, Encoding.UTF8, mediaType);
                        content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                        content.Headers.ContentType.CharSet = charSet;


                            if (valorGuardado.Id != 0)
                            {
                                ubicacionMetodoAPI = MetodoApiActualizar;
                            }
                            else
                            {
                                ubicacionMetodoAPI = MetodoAPIGuardar;
                            }

                        using (HttpResponseMessage response = await httpClient.PostAsync(ubicacionMetodoAPI, content))
                        {

                            var resultado = await response.Content.ReadAsStringAsync(); // conviete a String

                            dynamic ResultadoJson = JsonConvert.DeserializeObject(resultado);

                            var vacio = ResultadoJson.Vacio;

                            if (!Convert.ToBoolean(vacio))
                            {
                                var valor = JsonConvert.SerializeObject(ResultadoJson.Mensaje);
                                respuesta = JsonConvert.DeserializeObject<int>(valor);
                            }
                            else
                            {
                                var valor = JsonConvert.SerializeObject(ResultadoJson.Mensaje);
                                respuesta = JsonConvert.DeserializeObject<int>(valor);
                            }
                        }

                    }

                }
                catch (ArgumentNullException ex)
                {
                    string msg = ex.Message;
                }
                catch (InvalidOperationException ex)
                {
                    string msg = ex.Message;
                }
                catch (HttpRequestException ex)
                {
                    string msg = ex.Message;
                }
                catch (TaskCanceledException ex)
                {
                    string msg = ex.Message;
                }

                return respuesta;
            });

            return resultadoGuardar;
        }
        #endregion
        #region Eliminar
        public static async Task<int> Eliminar(string urlWebApi, string mediaType, string charSet, string ubicacionMetodoAPI, int valorFiltro)
        {

            int resultadoEliminacion = 0;
            HttpResponseMessage response = null;
            string baseUrl = urlWebApi + "/";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
                       
                string jsonData = JsonConvert.SerializeObject(valorFiltro);

                StringContent content = new StringContent(jsonData, Encoding.UTF8, mediaType);
                content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                content.Headers.ContentType.CharSet = charSet;

                response = await httpClient.PostAsync(ubicacionMetodoAPI, content);


                string resultado = await response.Content.ReadAsStringAsync(); // conviete a String                

                dynamic ResultadoJson = JsonConvert.DeserializeObject(resultado);

                dynamic vacio = ResultadoJson.Vacio;

                if (!Convert.ToBoolean(vacio))
                {
                    var valor = JsonConvert.SerializeObject(ResultadoJson.Mensaje);
                    resultadoEliminacion = JsonConvert.DeserializeObject<int>(valor);
                }
            }


            return resultadoEliminacion;
        }
        #endregion
    }
}
