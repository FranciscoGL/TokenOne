using System.Collections.Generic;
using System.Linq;
using Skate.Api.Models;

namespace Skate.Api.Services
{
    public class SkaterService
    {
        private static readonly List<SkaterModel> Data;

        static SkaterService()
        {
            Data = new List<SkaterModel>
            {
                new SkaterModel {Id = 1, Name = "Paul Rodriguez"},
                new SkaterModel {Id = 2, Name = "Luan Oliveira"},
                new SkaterModel {Id = 3, Name = "Kevin Hoefler"},
                new SkaterModel {Id = 4, Name = "Leaticia Buffoni"},
                new SkaterModel {Id = 5, Name = "Felipe Gustavo"},
                new SkaterModel {Id = 6, Name = "Arto Saari"},
                new SkaterModel {Id = 7, Name = "Santa Cruz Headress"},
                new SkaterModel {Id = 8, Name = "Lucas Puig"},
                new SkaterModel {Id = 9, Name = "Landy Cruz"},
                new SkaterModel {Id = 10, Name = "Brandon Biebel"},
                new SkaterModel {Id = 11, Name = "Neen Williams"},
                new SkaterModel {Id = 12, Name = "Erik Koston"},
                new SkaterModel {Id = 13, Name = "Rayne Switzer"},
                new SkaterModel {Id = 14, Name = "Baker Dee"},
                new SkaterModel {Id = 15, Name = "Marc Johnson"},
                new SkaterModel {Id = 16, Name = "Brian Lottis"},
                new SkaterModel {Id = 17, Name = "Tony Hawk"}
            };
        }

        public List<SkaterModel> Get()
        {
            return Data;
        }

        public SkaterModel Get(int id)
        {
            return Data.FirstOrDefault(s => s.Id == id);
        }
    }
}