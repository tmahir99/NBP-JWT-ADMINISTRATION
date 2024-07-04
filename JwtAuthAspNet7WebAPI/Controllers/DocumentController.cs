using JwtAuthAspNet7WebAPI.Core.Dtos;
using JwtAuthAspNet7WebAPI.Core.Interfaces;
using JwtAuthAspNet7WebAPI.Core.OtherObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthAspNet7WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]

        public async Task<IActionResult> GetAllDocuments()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]

        public async Task<IActionResult> GetDocumentById(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

        [HttpPost]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]
        public async Task<IActionResult> CreateDocument(Document document)
        {
            var createdDocument = await _documentService.CreateDocumentAsync(document);

            if (createdDocument != null)
            {
                return CreatedAtAction(nameof(GetDocumentById), new { id = createdDocument.Id }, createdDocument);
            }
            else
            {
                return BadRequest("Failed to create the document.");
            }
        }



        [HttpPut("{id}")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]

        public async Task<IActionResult> UpdateDocument(int id, Document documentDto)
        {
            var existingDocument = await _documentService.GetDocumentByIdAsync(id);
            if (existingDocument == null)
            {
                return NotFound();
            }

            // Map properties from documentDto to existingDocument
            existingDocument.Title = documentDto.Title;
            existingDocument.Description = documentDto.Description;
            // Update other properties as needed

            var updatedDocument = await _documentService.UpdateDocumentAsync(id, existingDocument);
            if (updatedDocument == null)
            {
                return BadRequest();
            }

            return Ok(updatedDocument);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = StaticUserRoles.ADMIN + "," + StaticUserRoles.SUPERADMIN)]

        public async Task<IActionResult> DeleteDocument(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            await _documentService.DeleteDocumentAsync(id);

            return NoContent();
        }
    }
}
