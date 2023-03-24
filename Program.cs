
// See https://aka.ms/new-console-template for more information
using System.Text;
using System.Text.Json;

Console.WriteLine("Inicio do programa");

Console.WriteLine("Montando o objeto de login");
//post de login na servicelayer usando o httpclient
HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
var client = new HttpClient(clientHandler);
Console.WriteLine("Fazendo o login");
using StringContent jsonContent = new(
       JsonSerializer.Serialize(new
       {

           CompanyDB = "SBO_PRD_SMOLLAN",
           Password = "Ib@admin1",
           UserName = "manager"

       }),
       Encoding.UTF8,
       "application/json");
       
var response = await client.PostAsync("https://10.100.38.78:50000/b1s/v1/Login", jsonContent);
Console.WriteLine("Verificando o login");
//verifica se o login foi bem sucedido
if (response.IsSuccessStatusCode)
{
    //obtem o token de acesso
    var token = await response.Content.ReadAsStringAsync();
    Console.WriteLine(token);
    
}else{
    Console.WriteLine("Erro no login");
}




