using CheckPromise.DTO;
using Checkpromise.Provider;
using CheckPromise.Data.DataContext;

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
			//clientData.IndicatorData = dbContext.Indicator;
			//clientData.PromiseData = dbContext.Promise;
			return clientData;
		}

		public void UploadData() {			
			dataProvider.Push(BuildClientData());
		}
	}
}
