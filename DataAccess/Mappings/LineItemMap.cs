using Core.Models;
using FluentNHibernate.Mapping;

namespace DataAccess.Mappings
{
    public class LineItemMap : ClassMap<LineItemModel>
    {
        public LineItemMap()
        {
            Table("LineItem_Tab");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Name);

            References(x => x.Order) ;

        }
    }
}
