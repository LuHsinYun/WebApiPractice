using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BussinessLogic.Entity.Utilities;
using BussinessLogic.Utilities.Csv.Attributes;
using CsvHelper;

namespace BussinessLogic.Utilities.Csv
{
    /// <summary>
    /// 讀寫CSV套件
    /// </summary>
    public class CsvUtility
    {
        /// <summary>
        /// 讀取 CSV 所有資料至對應entity list
        /// </summary>
        /// <typeparam name="T">CSV資料對應的 entity type</typeparam>
        /// <param name="fileFullName">包含欲讀取檔案路徑的完整檔名</param>
        /// <param name="delimiter">換行符號</param>
        /// <param name="codePage">欲讀取檔案的編碼代號</param>
        /// <param name="ignoreQuotes">ignoreQuotes</param>
        /// <param name="containHeader">是否包含 Header</param>
        /// <param name="headerValidated">是否需要驗證 Header</param>
        /// <param name="allowMissingField">是否允許遺失欄位</param>
        /// <returns>Entity List</returns>
        public List<T> ReadAllRecords<T>(string fileFullName, string delimiter = ",", int codePage = (int)FileEncodeEnum.UTF8, bool ignoreQuotes = false, bool containHeader = true, bool headerValidated = true, bool allowMissingField = false)
        {
            var returnList = new List<T>();

            using (var streamReader = new StreamReader(fileFullName))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Configuration.HasHeaderRecord = containHeader;
                    csvReader.Configuration.Delimiter = delimiter;
                    csvReader.Configuration.Encoding = Encoding.GetEncoding(codePage);
                    csvReader.Configuration.IgnoreBlankLines = true;
                    csvReader.Configuration.IgnoreQuotes = ignoreQuotes;
                    //csvReader.Context.Configuration.BadDataFound = context =>
                    //{
                    //    var logMessage = string.Format("Csv bad data row:{0}", context.RawRecord);
                    //};

                    if (allowMissingField)
                    {
                        csvReader.Configuration.MissingFieldFound = null;
                    }

                    if (headerValidated == false)
                    {
                        csvReader.Configuration.HeaderValidated = null;
                    }

                    returnList = csvReader.GetRecords<T>().ToList();
                }
            }

            return returnList;
        }

        /// <summary>
        /// 將 Entity List 轉為 CSV 檔
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="content">檔案內容</param>
        /// <param name="fileFullName">包含欲輸出檔案路徑的完整檔名</param>
        /// <param name="delimiter">換行符號</param>
        /// <param name="codePage">欲輸出檔案內容的編碼代號</param>
        /// <param name="hasHeaderRecord">輸出檔案是否包含 Header</param>
        /// <param name="shouldQuote">輸出欄位是否需用雙引號包覆</param>
        /// <param name="isUTF8EncodingWithBom">輸出檔案編碼是否為UTF8 BOM</param>
        public void WriteAllRecords<T>(List<T> content, string fileFullName, string delimiter = ",", int codePage = (int)FileEncodeEnum.UTF8, bool hasHeaderRecord = true, bool shouldQuote = true, bool isUTF8EncodingWithBom = false)
        {
            if (shouldQuote == true)
            {
                this.AddingDoubleQuoteToPropertyValue(content);
            }

            var encoding = this.GetEncoding(codePage, isUTF8EncodingWithBom);

            using (var streamReader = new StreamWriter(fileFullName, append: false, encoding: encoding))
            {
                using (var csvWriter = new CsvWriter(streamReader, CultureInfo.InvariantCulture))
                {
                    csvWriter.Configuration.HasHeaderRecord = hasHeaderRecord;
                    csvWriter.Configuration.Delimiter = delimiter;

                    csvWriter.WriteRecords(content);
                }
            }
        }

        /// <summary>
        /// 將 Entity List 繼續寫入 CSV 檔
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="content">檔案內容</param>
        /// <param name="fileFullName">包含欲輸出檔案路徑的完整檔名</param>
        /// <param name="delimiter">換行符號</param>
        /// <param name="codePage">欲輸出檔案內容的編碼代號</param>
        /// <param name="shouldQuote">輸出欄位是否需用雙引號包覆</param>
        /// <param name="isUTF8EncodingWithBom">輸出檔案編碼是否為UTF8 BOM</param>
        public void AppendRecords<T>(List<T> content, string fileFullName, string delimiter = ",", int codePage = (int)FileEncodeEnum.UTF8, bool shouldQuote = true, bool isUTF8EncodingWithBom = false)
        {
            if (shouldQuote == true)
            {
                this.AddingDoubleQuoteToPropertyValue(content);
            }

            var encoding = this.GetEncoding(codePage, isUTF8EncodingWithBom);

            using (var streamReader = new StreamWriter(fileFullName, append: true, encoding: encoding))
            {
                using (var csvWriter = new CsvWriter(streamReader, CultureInfo.InvariantCulture))
                {
                    csvWriter.Configuration.HasHeaderRecord = false;
                    csvWriter.Configuration.Delimiter = delimiter;

                    csvWriter.WriteRecords(content);
                }
            }
        }

        /// <summary>
        /// 針對 entity 內的 string property的值，加上雙引號
        /// </summary>
        /// <typeparam name="T">Entity Type T</typeparam>
        /// <param name="content">Object List</param>
        private void AddingDoubleQuoteToPropertyValue<T>(List<T> content)
        {
            foreach (var entity in content)
            {
                var properties = entity.GetType().GetProperties();

                foreach (var property in properties)
                {
                    var attachDoubleQuoteAttribute = property.GetCustomAttribute<AttachDoubleQuoteAttribute>();

                    if (attachDoubleQuoteAttribute != null)
                    {
                        var baseValue = property.GetValue(entity);

                        if (baseValue == null)
                        {
                            baseValue = $"\"{string.Empty}\"";
                        }

                        property.SetValue(entity, $"\"{baseValue}\"");
                    }
                }
            }
        }

        /// <summary>
        /// 取得編碼方式
        /// </summary>
        /// <param name="codePage">字碼頁識別碼</param>
        /// <param name="isUTF8EncodingWithBom">是否為UTF8 BOM</param>
        /// <returns>編碼方式</returns>
        private Encoding GetEncoding(int codePage, bool isUTF8EncodingWithBom)
        {
            //// UTF8 GetEncoding 回傳會包含 BOM，故需另外建立執行個體並傳入參數 false
            return codePage == (int)FileEncodeEnum.UTF8 ? new UTF8Encoding(isUTF8EncodingWithBom) : Encoding.GetEncoding(codePage);
        }
    }
}