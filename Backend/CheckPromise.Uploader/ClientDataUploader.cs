using CheckPromise.DTO;
using Checkpromise.Provider;
using CheckPromise.Data.DataContext;
using System.Linq;
using System.Collections.Generic;
using System;

namespace CheckPromise.Uploader
{
	public class ClientDataUploader
	{

		private readonly IClientDataProvider dataProvider;

		private readonly CheckPromiseContext dbContext;

		private readonly DateTime initialDate = new DateTime(2019, 5, 20);

		public ClientDataUploader(IClientDataProvider clientDataProvider, CheckPromiseContext checkPromiseContext) {
			dataProvider = clientDataProvider;
			dbContext = checkPromiseContext;
		}

		private ClientData BuildClientData() {
			ClientData clientData = new ClientData();
			clientData.IndicatorData = BuildIndicatorData();
			clientData.PromiseData = BuildPromiseData();
			return clientData;
		}

		private IEnumerable<Indicator> BuildIndicatorData() {
			return dbContext.Indicator.Select(i => new Indicator(i) {
				GraphData = i.GraphData.OrderBy(gd => gd.Date).Select(gd => new GraphData(gd)).ToList(),
				MediaInfoData = i.MediaInfoData.OrderBy(md => md.Date).Select(md => new MediaInfo(md)).ToList(),
				InitialData = i.Values.Where(v => v.Date.Date.Equals(initialDate.Date)).Select(v => new IndicatorValue(v)).FirstOrDefault(),
				CurrentData = i.Values.Where(v => v.Date.Date.Equals(DateTime.Now.Date)).Select(v => new IndicatorValue(v)).FirstOrDefault()
			}).ToList();
		}

		private IEnumerable<Promise> BuildPromiseData() {
			return dbContext.Promise.Select(p => new Promise(p)).ToList();
		}

		public void UploadData() {			
			dataProvider.Push(BuildClientData());
		}
	}
}
