﻿namespace JwtAuthAspNet7WebAPI.Core.Dtos
{
    public class Expence
    {
            public int Id { get; set; }
            public string Description { get; set; }
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }

    }
}
