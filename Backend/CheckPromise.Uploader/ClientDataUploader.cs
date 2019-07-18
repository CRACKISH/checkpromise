using CheckPromise.DTO;
using Checkpromise.Provider;
using CheckPromise.Data.DataContext;
using System.Linq;

namespace CheckPromise.Uploader
{
	public class ClientDataUploader
	{

		private readonly IClientDataProvider dataProvider;

		private readonly CheckPromiseContext dbContext;

		public ClientDataUploader(IClientDataProvider clientDataProvider, CheckPromiseContext checkPromiseContext) {
			dataProvider = clientDataProvider;
			dbContext = checkPromiseContext;
		}

		private ClientData BuildClientData() {
			ClientData clientData = new ClientData();
			clientData.IndicatorData = dbContext.Indicator.Select(i => new Indicator(i)).ToList();
			clientData.PromiseData = dbContext.Promise.Select(p => new Promise(p)).ToList();
			return clientData;
		}

		public void UploadData() {			
			dataProvider.Push(BuildClientData());
		}
	}
}
