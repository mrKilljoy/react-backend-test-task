using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using react_backend_test_task_data.Models;
using react_backend_test_task_data.Services;
using react_backend_test_task_data.Services.Interfaces;
using react_backend_test_task.Models;

namespace react_backend_test_task.Controllers;

/// <summary>
/// Provides endpoints for interacting with tree nodes.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TreeNodeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITreeNodeRepository _repository;

    /// <inheritdoc />
    public TreeNodeController(IMapper mapper, ITreeNodeRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    /// <summary>
    /// Creates a new tree node.
    /// </summary>
    /// <param name="request">The request object.</param>
    /// <returns>Information about the new node.</returns>
    [HttpPost("create")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TreeNodeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status500InternalServerError)]
    public async Task<TreeNodeDto> Create([FromBody] CreateTreeNodeRequest request)
    {
        var result = await _repository.Create(new TreeNode
        {
            Name = request.NodeName,
            ParentId = request.ParentNodeId,
            TreeId = request.TreeId
        });
        
        return _mapper.Map<TreeNodeDto>(result);
    }
    
    /// <summary>
    /// Renames a tree node.
    /// </summary>
    /// <param name="request">The request object.</param>
    /// <returns>Information about the renamed node.</returns>
    [HttpPost("rename")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TreeNodeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status500InternalServerError)]
    public async Task<TreeNodeDto> Rename([FromBody] RenameNodeRequest request)
    {
        var result = await _repository.Rename(request.NodeId, request.NewNodeName);
        
        return _mapper.Map<TreeNodeDto>(result);
    }

    /// <summary>
    /// Deletes a tree node.
    /// </summary>
    /// <param name="request">The request object.</param>
    /// <returns>No content if the operation has been completed successfully.</returns>
    [HttpDelete("delete")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteTreeNodeRequest request)
    {
        await _repository.Delete(request.TreeId, request.NodeId);
        
        return NoContent();
    }
}