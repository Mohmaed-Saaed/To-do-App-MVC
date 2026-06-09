using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public partial class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TaskItem, DTOGetAllTaskItem>()
                .Map(dest => dest.CategoryName,
                src => src.Category != null ? src.Category.Name : "Uncategorized");


        }
    }
}
