using AutoMapper;
using react_backend_test_task_data.Models;
using react_backend_test_task.Models;

namespace react_backend_test_task.Infrastructure.Automapper;

public class TreeNodeDtoProfile : Profile
{
    public TreeNodeDtoProfile()
    {
        CreateMap<Tree, TreeNodeDto>()
            .ConvertUsing<TreeNodeDtoConverter>();

        CreateMap<TreeNode, TreeNodeDto>()
            .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
            .ForMember(x => x.Name, y => y.MapFrom(x => x.Name))
            .ForMember(x => x.Children, y => y.MapFrom(x => new List<TreeNodeDto>()));
    }
    
    private class TreeNodeDtoConverter : ITypeConverter<Tree, TreeNodeDto>
    {
        public TreeNodeDto Convert(Tree source, TreeNodeDto destination, ResolutionContext context)
        {
            var result = new TreeNodeDto()
            {
                Id = source.Id,
                Name = source.Name,
                Children = new List<TreeNodeDto>()
            };
            var dictionary = new Dictionary<Guid, TreeNodeDto>();
            Guid? rootId = default;
            foreach (var node in source.Nodes)
            {
                if (!node.ParentId.HasValue)
                {
                    rootId = node.Id;
                }
                if (!dictionary.ContainsKey(node.Id))
                {
                    var mappedNode = context.Mapper.Map<TreeNodeDto>(node);
                    dictionary.Add(node.Id, mappedNode);

                    if (node.ParentId.HasValue && dictionary.ContainsKey(node.ParentId.Value))
                    {
                        dictionary[node.ParentId.Value].Children.Add(mappedNode);
                    }
                }
            }

            if (rootId.HasValue)
            {
                result.Children.Add(dictionary[rootId.Value]);
            }

            return result;
        }
    }
}