﻿using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Calculator.BusinessLogic.Models
{
    public class CalculationHistoryModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Equation { get; set; }
        public decimal Result { get; set; }
        public DateTime CalculationDate { get; set; }
    }
}
