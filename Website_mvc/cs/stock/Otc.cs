using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website_mvc.cs.stock
{
    public class Otc
    {
        parseway parseway = new parseway();

        /// <summary>
        /// Parse OTC turnover data
        /// </summary>
        /// <param name="date">Example : 109/08/24 </param>
        /// <returns></returns>
        public List<Models.otcTurnoverate> ParseOTCTurnoverInfo(string date)
        {
            List<Models.otcTurnoverate> result = new List<Models.otcTurnoverate>();
            HtmlWeb webClient = new HtmlWeb();
            var doc = webClient.Load($"https://www.tpex.org.tw/web/stock/aftertrading/daily_turnover/trn_result.php?l=zh-tw&t=D&d={date}&s=0,asc,1&o=htm");
            var table = doc.DocumentNode.SelectSingleNode("/html/body//td[contains(text(),'上櫃股票個股')]");
            if (table != null)
            {
                table = parseway.FindTable(table);
                var tbody = table.SelectNodes(".//tbody//tr");
                if (tbody != null)
                {
                    foreach (var tr in tbody)
                    {
                        Models.otcTurnoverate otc = new Models.otcTurnoverate();
                        List<string> Info = new List<string>();
                        foreach (var td in tr.SelectNodes(".//td"))
                        {
                            Info.Add(td.InnerText.Trim());
                        }
                        //otc.s_date = Convert.ToDateTime(date);
                        otc.s_date = date;
                        otc.s_rank = Convert.ToInt32(Info[0]);
                        otc.s_id = Info[1];
                        otc.s_name = Info[2];
                        otc.s_deal = Info[3];
                        otc.s_market = Info[4];
                        otc.s_turnoverate = Convert.ToDouble(Info[5]);
                        //  0=> 排行 1=> 股票代號  5=> 周轉率
                        result.Add(otc);
                    }
                    return result;
                }
            }
            return null;
        }
    }
}