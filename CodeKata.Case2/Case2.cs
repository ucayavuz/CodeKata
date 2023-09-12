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

            var ocrList = new List<OcrModel>();

            foreach (JToken j in jArr)
            {
                var ocr = new OcrModel();
                ocr.Description = j["description"].ToString().Replace('\n', ' ');

                foreach (var k in j.Last().Last().First())
                {
                    ocr.Cordinates.X = Convert.ToInt32(k[2]["x"].ToString());
                    ocr.Cordinates.Y = Convert.ToInt32(k[2]["y"].ToString());
                }

                ocrList.Add(ocr);
            }

            int currentY = 1000;

            List<ResultModel> resultModels = new List<ResultModel>();
            var resultModel = new ResultModel();

            int index = 0;

            foreach (var ocr in ocrList.OrderBy(x => x.Cordinates.Y))
            {
                if (ocr.Description.Length > 300)
                    continue;

                var sameRow = new RowModel();

                // Aynı satır
                if (Math.Abs(ocr.Cordinates.Y - currentY) < 9)
                {
                    sameRow.Description = ocr.Description;
                    sameRow.X = ocr.Cordinates.X;
                    currentY = ocr.Cordinates.Y;
                }

                else
                {
                    index++;
                    resultModel = new ResultModel();

                    sameRow.Description = ocr.Description;
                    sameRow.X = ocr.Cordinates.X;
                    currentY = ocr.Cordinates.Y;
                }

                resultModel.Index = index;
                resultModel.SameItems.Add(sameRow);
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
    }
}