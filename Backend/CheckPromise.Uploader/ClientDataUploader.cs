using Checkpromise.Provider;

namespace CheckPromise.Uploader
{
	public class ClientDataUploader
	{

		private readonly IClientDataProvider dataProvider;

		public ClientDataUploader(IClientDataProvider clientDataProvider) {
			dataProvider = clientDataProvider;
		}

		public void UploadData() {
			dataProvider.Push();
		}
	}
}
