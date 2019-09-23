using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _365AutomatedTests.Framework
{
    public class DBHelper
    {
        public Dictionary<string, string> getOrderDataByOrderNumber(string orderNumber)
        {
            MSDatabaseConnector msDatabaseConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            String query = $@"use [LW08v6]
                select CREATED_ON, X_ORDER_CODE, X_LAST_NAME, X_FIRST_NAME, X_MIDDLE_NAME, X_EMAIL_ADDRESS, 
                X_CELL_PHONE_NUMBER from orders with (nolock) where X_ORDER_CODE='" + orderNumber + "'";
            var results = msDatabaseConnectorLW.QueryExecutorTable(query);
            
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("createdOn", DateTime.Parse(results.Rows[0].ItemArray[0].ToString()).ToString("dd.MM.yyyy HH:mm"));
            data.Add("lastName", results.Rows[0].ItemArray[2].ToString());
            data.Add("firstName", results.Rows[0].ItemArray[3].ToString());
            data.Add("middleName", results.Rows[0].ItemArray[4].ToString());
            data.Add("emailAddress", results.Rows[0].ItemArray[5].ToString());
            data.Add("cellPhoneNumber", results.Rows[0].ItemArray[6].ToString());
            return data;
        }

        public Dictionary<string, string> getOrderDataBySampleNumber(string sampleNumber, DateTime dateFrom, DateTime dateTo)
        {
            MSDatabaseConnector msDatabaseConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            String query = $@"use [LW08v6]
                select CREATED_ON, X_ORDER_CODE, X_LAST_NAME, X_FIRST_NAME, X_MIDDLE_NAME, X_EMAIL_ADDRESS, 
                X_CELL_PHONE_NUMBER from orders with (nolock) where created_on>='" + dateFrom.ToString("yyyy-MM-dd") + "T00:00:00' and created_on<='" +
                dateTo.ToString("yyyy-MM-dd") + "T23:59:59' and order_num in (select order_num from sample with (nolock) where label_id='" + sampleNumber + "')";
            var results = msDatabaseConnectorLW.QueryExecutorTable(query);

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("createdOn", DateTime.Parse(results.Rows[0].ItemArray[0].ToString()).ToString("dd.MM.yyyy HH:mm"));
            data.Add("orderNumber", results.Rows[0].ItemArray[1].ToString());
            data.Add("lastName", results.Rows[0].ItemArray[2].ToString());
            data.Add("firstName", results.Rows[0].ItemArray[3].ToString());
            data.Add("middleName", results.Rows[0].ItemArray[4].ToString());
            data.Add("emailAddress", results.Rows[0].ItemArray[5].ToString());
            data.Add("cellPhoneNumber", results.Rows[0].ItemArray[6].ToString());
            return data;
        }

        public Dictionary<string, string> getOrderDataByExternalSampleNumber(string externalSampleNumber, DateTime dateFrom, DateTime dateTo)
        {
            MSDatabaseConnector msDatabaseConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            String query = $@"use [LW08v6]
                select CREATED_ON, X_ORDER_CODE, X_LAST_NAME, X_FIRST_NAME, X_MIDDLE_NAME, X_EMAIL_ADDRESS, 
                X_CELL_PHONE_NUMBER from orders with (nolock) where created_on>='" + dateFrom.ToString("yyyy-MM-dd") + "T00:00:00' and created_on<='" +
                dateTo.ToString("yyyy-MM-dd") + "T23:59:59' and order_num in (select order_num from sample with (nolock) where x_old_sample_id='" + externalSampleNumber + "')";
            var results = msDatabaseConnectorLW.QueryExecutorTable(query);

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("createdOn", DateTime.Parse(results.Rows[0].ItemArray[0].ToString()).ToString("dd.MM.yyyy HH:mm"));
            data.Add("orderNumber", results.Rows[0].ItemArray[1].ToString());
            data.Add("lastName", results.Rows[0].ItemArray[2].ToString());
            data.Add("firstName", results.Rows[0].ItemArray[3].ToString());
            data.Add("middleName", results.Rows[0].ItemArray[4].ToString());
            data.Add("emailAddress", results.Rows[0].ItemArray[5].ToString());
            data.Add("cellPhoneNumber", results.Rows[0].ItemArray[6].ToString());
            return data;
        }

        public string getOrderCountByOrderNumberPart(string orderNumberPart)
        {
            MSDatabaseConnector msDatabaseConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            String query = $@"use [LW08v6]
                select count(*) from orders with (nolock)
                where X_ORDER_CODE like '%" + orderNumberPart + "%'";
            var orderCount = msDatabaseConnectorLW.QueryExecutorScalar(query);
            return orderCount;
        }

        public string getOrderCountBySampleNumberPart(string sampleNumberPart, DateTime dateFrom, DateTime dateTo)
        {
            MSDatabaseConnector msDatabaseConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            String query = $@"use [LW08v6]
                select count(*) from orders with (nolock) 
                where created_on>='" + dateFrom.ToString("yyyy-MM-dd") + "T00:00:00' and created_on<='" + 
                dateTo.ToString("yyyy-MM-dd") + "T23:59:59' and order_num in (select order_num from sample with (nolock) where label_id like '%" + 
                sampleNumberPart + "%')";
            var orderCount = msDatabaseConnectorLW.QueryExecutorScalar(query);
            return orderCount;
        }

        public string getOrderCountByExternalSampleNumberPart(string externalSampleNumberPart, DateTime dateFrom, DateTime dateTo)
        {
            MSDatabaseConnector msDatabaseConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            String query = $@"use [LW08v6]
                select count(*) from orders with (nolock) 
                where created_on>='" + dateFrom.ToString("yyyy-MM-dd") + "T00:00:00' and created_on<='" +
                dateTo.ToString("yyyy-MM-dd") + "T23:59:59' and order_num in (select order_num from sample with (nolock) where x_old_sample_id like '%" +
                externalSampleNumberPart + "%')";
            var orderCount = msDatabaseConnectorLW.QueryExecutorScalar(query);
            return orderCount;
        }

        public void removeAllConclusionsFromOrder(string orderNumber)
        {
            MSDatabaseConnector msDatabaseConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            String query = $@"use [LW08v6]
                delete [X_ComplexEndings] where OrderItem_ID in (
                select id from [dbo].[X_ORDER_ITEMS] oi 
                where oi.ORDER_NUM in (select order_num from [dbo].[ORDERS] where x_order_code='" + orderNumber + "'))";
            msDatabaseConnectorLW.NonQueryExecutor(query);
        }

        public string getCounclusionCountByOrderNumber(string orderNumber)
        {
            MSDatabaseConnector msDatabaseConnectorLW = new MSDatabaseConnector(Config.MSDbLW);
            String query = $@"use [LW08v6]
                select count(*) from [X_ComplexEndings] (nolock) where OrderItem_ID in (
                select id from [dbo].[X_ORDER_ITEMS] (nolock) oi 
                where oi.ORDER_NUM in (select order_num from [dbo].[ORDERS] (nolock) where x_order_code='" + orderNumber + "'))";
            var conclusionCount = msDatabaseConnectorLW.QueryExecutorScalar(query);
            return conclusionCount;
        }
    }
}
