using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Models;
using Services.AdminRepositories;
using UseCase.Admin.PredictorPrices.Data;

namespace Usecase.Admin.PredictorPrices
{
    public class PredictorPrice
    {
        private IAdminRepository adminRepository;
        private MLContext mlContext;

        public PredictorPrice(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
            this.mlContext = new MLContext(seed: 0);
        }

        public void CalculateModel()
        {
            var aparts = adminRepository.GetAllApartment().Result;
            var model = Train(mlContext, aparts);
            Evaluate(mlContext, model);
            TestSinglePrediction(mlContext, model);
        }

        private ITransformer Train(MLContext mlContext, List<Appartment> aparts)
        {
            IDataView data = mlContext.Data.LoadFromEnumerable<Appartment>(aparts);
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Price")
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "TotalSquareEncoded", inputColumnName: "TotalSquare"))
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "RoomsCountEncoded", inputColumnName: "RoomsCount"))
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "FloorEncoded", inputColumnName: "Floor"))
                    .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "DistrictValueEncoded", inputColumnName: "DistrictValue"))
                    .Append(mlContext.Transforms.Concatenate("Features", "TotalSquareEncoded", "RoomsCountEncoded", "FloorEncoded", "DistrictValueEncoded"))
                    .Append(mlContext.Regression.Trainers.FastTree());

            var model = pipeline.Fit(data);
            return model;
        }

        private void Evaluate(MLContext mlContext, ITransformer model)
        {
            IDataView dataView = mlContext.Data.LoadFromEnumerable<Appartment>(this.adminRepository.GetAllApartment().Result);

            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");

            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       RSquared Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError}");
            Console.WriteLine($"*************************************************");
        }

        private void TestSinglePrediction(MLContext mlContext, ITransformer model)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<Appartment, ApartmentPrediction>(model);
            var apartmentSample = new Appartment()
            {
                TotalSquare = 67,
                RoomsCount = 3,
                Floor = 7,
                DistrictValue = 24,
                Price = 0
            };
            var prediction = predictionFunction.Predict(apartmentSample);
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted price: {prediction.Price}");
            Console.WriteLine($"**********************************************************************");
        }
    }
}