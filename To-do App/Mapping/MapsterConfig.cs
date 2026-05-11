using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public static partial class MapsterConfig
    {
        public static void RegisterMappings()
        {
            //TypeAdapterConfig<TaskItem, DTOGetAllTaskItem>
            //    .NewConfig()
            //    .Map(dest => dest.CategoryName, 
            //    src => src.Category != null ? src.Category.Name: "Uncategorized");
        }
    }
}
