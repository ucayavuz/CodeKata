using System;
using System.Reflection;
using CodeKata.Case2.Models;
using Newtonsoft.Json.Linq;

namespace CodeKata.Case2
{
    public class Case2
    {
        public void Execute()
        {
            var json = File.ReadAllText(@"..//..//..//response.json");
            JArray jArr = JArray.Parse(json);

            // Json Parse 

            var ocrList = ParseJsonToOcrModel(jArr);

            var resultModels = CreateResultModel(ocrList);

            PrintResultModel(resultModels);

        }


        private void PrintResultModel(List<ResultModel> resultModels)
        {
            foreach (var item in resultModels.OrderBy(x => x.Index))
            {
                string row = "";

                foreach (var item2 in item.SameItems.OrderBy(x => x.X))
                {

                    row += " " + item2.Description;
                }
                Console.WriteLine($"{item.Index}-) " + row);
            }
        }
        private List<ResultModel> CreateResultModel(List<OcrModel> ocrList)
        {
            int currentY = 1000;
            int index = 0;
            List<ResultModel> resultModels = new List<ResultModel>();

            var resultModel = new ResultModel();
            foreach (var ocr in ocrList.OrderBy(x => x.Cordinates.Y))
            {
                if (ocr.Description.Length > 300)
                    continue;

                var row = new RowModel();

                if (Math.Abs(ocr.Cordinates.Y - currentY) < 9)
                {

                }
                else
                {
                    index++;
                    resultModel=new ResultModel();
                }
                currentY = ocr.Cordinates.Y;

                CreaterOrderUpdateModel(resultModels, index, RegisterResultModel(row, ocr, index,resultModel));

            }
            return resultModels;
        }
        private ResultModel RegisterResultModel(RowModel row, OcrModel ocr, int index, ResultModel resultModel)
        {
            row.Description = ocr.Description;
            row.X = ocr.Cordinates.X;

            resultModel.Index = index;
            resultModel.SameItems.Add(row);

            return resultModel;

        }
        private List<OcrModel> ParseJsonToOcrModel(JArray jArr)
        {
            List<OcrModel> ocrModels = new List<OcrModel>();
            foreach (JToken j in jArr)
            {
                var ocr = new OcrModel();
                ocr.Description = j["description"].ToString().Replace('\n', ' ');

                foreach (var k in j.Last().Last().First())
                {
                    ocr.Cordinates.X = Convert.ToInt32(k[2]["x"].ToString());
                    ocr.Cordinates.Y = Convert.ToInt32(k[2]["y"].ToString());
                }

                ocrModels.Add(ocr);
            }

            return ocrModels;

        }

        private void CreaterOrderUpdateModel(List<ResultModel> resultModels, int index, ResultModel resultModel)
        {
            if (!resultModels.Any(x => x.Index == index))
            {
                resultModels.Add(resultModel);
            }
            else
            {
                var rsltMdl = resultModels.FirstOrDefault(x => x.Index == index);

                if (rsltMdl != null)
                    resultModels.Remove(rsltMdl);

                resultModels.Add(resultModel);
            }

        }
    }
}