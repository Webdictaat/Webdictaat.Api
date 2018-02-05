using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Webdictaat.Domain.Google
{
    public interface IGoogleAnalytics
    {
        Task<IList<PageView>> GetPageViews(string subdirectory, DateTime? start = null, DateTime? end = null);
    }

    public class GoogleAnalyticsOptions
    {
        public string LoginEmailAddress { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class GoogleAnalytics : IGoogleAnalytics
    {
        
        private GoogleCredential _credential;

        public GoogleAnalytics(string gooleServiceAccountConfig)
        {
            string[] scopes = new string[] { AnalyticsReportingService.Scope.Analytics };

            using (var stream = new FileStream(gooleServiceAccountConfig, FileMode.Open, FileAccess.Read))
            {
                this._credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(scopes);
            }
        }

        /// <summary>
        /// Async method to get the page vies of a certain subdirecoty within the webdictaat domain.
        /// Can be used to extract the page views for example a specific dictaat. 
        /// </summary>
        /// <param name="subdirectory"></param>
        /// <returns></returns>
        public async Task<IList<PageView>> GetPageViews(string subdirectory, DateTime? start = null, DateTime? end = null)
        {
            //format filter paramters
            string dateFormat = "yyyy-MM-dd";
            string startString = (start != null ? start : DateTime.Now.AddDays(-30)).Value.ToString(dateFormat);
            string endString = (end != null ? end : DateTime.Now).Value.ToString(dateFormat);

            //required to make a valid connection
            var serviceInitializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = _credential,
                ApplicationName = "Google Analytics API Console"
            };


            using (var svc = new AnalyticsReportingService(serviceInitializer))
            {
                var reportRequest = new ReportRequest
                {
                    DimensionFilterClauses = new List<DimensionFilterClause>{
                        new DimensionFilterClause(){
                            Filters = new List<DimensionFilter>{
                                new DimensionFilter(){
                                    DimensionName = "ga:pagePath",
                                    Expressions = new List<String>{String.Format("^/{0}", subdirectory)}
                                }
                            }
                        }
                    },
                    DateRanges = new List<DateRange>{
                        new DateRange(){ StartDate = startString, EndDate = endString }
                    },
                    Dimensions = new List<Dimension> {
                            new Dimension { Name = "ga:pagePath" }
                        },
                    Metrics = new List<Metric> {
                            new Metric{ Expression = "ga:pageViews" }
                        },
                    ViewId = "168601841"
                };
                var getReportsRequest = new GetReportsRequest
                {
                    ReportRequests = new List<ReportRequest> { reportRequest }
                };
                var batchRequest = svc.Reports.BatchGet(getReportsRequest);
                var response = batchRequest.Execute();

                var pageViews = new List<PageView>();
                var data_rows = response.Reports.First().Data.Rows;

                //no data found
                if (data_rows == null)
                    return pageViews;

                //format data
                foreach (var x in data_rows)
                {
                    var pageUri = x.Dimensions.First().Split("#/").ElementAtOrDefault(1);

                    if(pageUri != null)
                        pageViews.Add(new PageView()
                        {
                            PageUri = pageUri,
                            Views = string.Join(", ", x.Metrics.First().Values)
                        });
                };

                return pageViews;
            }
            
        }
    }
}
