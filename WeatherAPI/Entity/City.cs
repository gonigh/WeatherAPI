using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherAPI.Models
{
    [Table("city")]
    public class City
    {
        [Column("cityid")]
        public int cityid { get; set; }
        [Column("parentid")]
        public int parentid { get; set; }
        [Column("citycode")]
        public String citycode { get; set; }
        [Column("city")]
        public String city { get; set; }


    }

}
