using CheckPromise.DTO;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Checkpromise.Provider
{
	public class ClientDataFtpProvider : IClientDataProvider
	{

		private readonly string ftpServer = "95.216.242.245";

		private WebClient GetWebClient() {
			var webClient = new WebClient();
			webClient.Credentials = new NetworkCredential("checkpro", "ftpPassword");
			return webClient;
		}

		public void Push(ClientData clientData)
		{
			using (WebClient webClient = GetWebClient()) {
				var ftpPath = $"ftp://{ftpServer}/assets/data/data.json";
				webClient.UploadData(ftpPath, WebRequestMethods.Ftp.UploadFile, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clientData)));
			}
		}
	}
}
