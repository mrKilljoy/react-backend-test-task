using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using react_backend_test_task_data.Models;
using react_backend_test_task_data.Services;
using react_backend_test_task_data.Services.Interfaces;
using react_backend_test_task.Models;

namespace react_backend_test_task.Controllers;

/// <summary>
/// Provides endpoints for interacting with tree models.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TreeController : ControllerBase
{
    private readonly ITreeRepository _repository;
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public TreeController(IMapper mapper, ITreeRepository repository)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Get a tree by its name. If the tree does not exist, a new one will be created.
    /// </summary>
    /// <param name="treeName">The tree name.</param>
    /// <returns>The tree.</returns>
    [HttpGet("{treeName}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TreeNodeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status500InternalServerError)]
    public async Task<TreeNodeDto> Get([FromRoute]string treeName)
    {
        var item = await _repository.Get(treeName);
        if (item is null)
        {
            item = await _repository.Create(new Tree
            {
                Name = treeName,
                Nodes = new List<TreeNode>
                {
                    new()
                    {
                        Name = "Root",
                    }
                }
            });
        }

        return _mapper.Map<TreeNodeDto>(item);
    }
}