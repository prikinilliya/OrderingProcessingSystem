using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingProcessingSystem.Mappers
{
    interface IMapper
    {
        object MapToObject(object valueToMap);

        object MapToXml(object valueToMap);
        object MapToXml(IEnumerable<object> valueToMap);

    }
}
