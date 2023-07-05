using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyProtocolsAPI_DanielRamírez.Attributes
{

    //esta clase ayuda a alimitar la forma en que se puede consumir un recurso de controlador (un end point)
    //

    [AttributeUsage(validOn: AttributeTargets.All)]
    public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {

        //Especificamos cuál es el clave:valor dentro de appsettings que queremos usar como apikey
        private readonly string _apiKey = "Progra6ApiKey";


        public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next) 
        {
            //aca validamos que en el body (en tipo json) del request vaya la info de la apikey
            //si no va la info presentamos un mensaje de error indicándo que falta ApiKey y que no se puede consumir el recurso

            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401, Content = "Llamada no contiene información de seguridad..."
                };
            return;
            }

            //si viene info de seguridad falta validar que sea la correcta
            //para esto lo primero es extraer el valor de Progra6ApiKey dentro de appsettings. json
            //para poder conparar contra lo que viene el request
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var ApiKeyValue = appSettings.GetValue<String>(_apiKey);

            //comparar que las apikey sean iguales

            if (!ApiKeyValue.Equals(ApiSalida)) 
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey inválida..."
                };
                return;
            }

            await next();


        }


    }
}
