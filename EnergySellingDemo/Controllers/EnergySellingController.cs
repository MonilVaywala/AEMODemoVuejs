using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnergySellingDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnergySellingController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        private readonly ILogger<EnergySellingController> _logger;
        public EnergySellingController(ILogger<EnergySellingController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IEnumerable<EnergySellingDTO> Get()
        {
            string cacheKey = "energySellinglist";
            List<EnergySellingDTO> energySellinglist;
            List<EnergySellingDTO> energySellinglist1 = new List<EnergySellingDTO>();


            ////Get Value from Cache
            if (_memoryCache.TryGetValue(cacheKey, out energySellinglist))
            {
                energySellinglist1 = energySellinglist.ToList();
            }

            return energySellinglist1.ToArray();
        }

        [HttpPost]
        public string Post([FromBody] EnergySellingDTO energySellingDTO)
        {
            string cacheKey = "energySellinglist";
            List<EnergySellingDTO> energySellinglist;

            /*If Date is weekoff day then allow 10 % discount on actual price. */
            if (IsItWeekOffDay(energySellingDTO.Date))
            {
                var tenPercentDiscount = (energySellingDTO.Price * 10) / 100;
                energySellingDTO.Price = (energySellingDTO.Price - tenPercentDiscount);
                energySellingDTO.isDiscount = true;
            }
            else {
                energySellingDTO.isDiscount = false;
            }

            var energy = new List<EnergySellingDTO>();

            if (_memoryCache.TryGetValue(cacheKey, out energySellinglist))
            {
                energy = energySellinglist.ToList();
            }
            if (energy != null && energy.Count > 0)
            {
                //update list
                energy.Add(energySellingDTO);
            }
            else
            {
                //Add
                energy.Add(energySellingDTO);

            }
            energySellinglist = energy;

            ////Set Value to Cache
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(30));
            _memoryCache.Set(cacheKey, energySellinglist, cacheEntryOptions);
            return "Inserted Successfully";
        }

        /// <summary>
        /// Check weather the day is weekoff
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsItWeekOffDay(DateTime date)
        {
            /* return true if day is weekoff else false */
            return (date.DayOfWeek == DayOfWeek.Saturday) || (date.DayOfWeek == DayOfWeek.Sunday) ? true : false;
        }
    }
}
