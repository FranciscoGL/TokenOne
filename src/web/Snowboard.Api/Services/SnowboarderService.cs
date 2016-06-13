using System.Collections.Generic;
using Snowboard.Api.Models;

namespace Snowboard.Api.Services
{
    public class SnowboarderService
    {
        private static readonly List<SnowboarderModel> Data;

        static SnowboarderService()
        {
            Data = new List<SnowboarderModel>
            {
                new SnowboarderModel {Id = 2, Name = "Luan Oliveira"},
                new SnowboarderModel {Id = 3, Name = "Kevin Hoefler"},
                new SnowboarderModel {Id = 4, Name = "Leaticia Buffoni"},
                new SnowboarderModel {Id = 5, Name = "Felipe Gustavo"},
                new SnowboarderModel {Id = 1, Name = "Paul Rodriguez"},
                new SnowboarderModel {Id = 6, Name = "Arto Saari"},
                new SnowboarderModel {Id = 7, Name = "Santa Cruz Headress"},
                new SnowboarderModel {Id = 8, Name = "Lucas Puig"},
                new SnowboarderModel {Id = 9, Name = "Landy Cruz"},
                new SnowboarderModel {Id = 10, Name = "Brandon Biebel"},
                new SnowboarderModel {Id = 11, Name = "Neen Williams"},
                new SnowboarderModel {Id = 13, Name = "Rayne Switzer"},
                new SnowboarderModel {Id = 14, Name = "Baker Dee"},
                new SnowboarderModel {Id = 16, Name = "Brian Lottis"},
                new SnowboarderModel {Id = 17, Name = "Tony Hawk"}
            };
        }

        public List<SnowboarderModel> Get()
        {
            return Data;
        }
    }
}