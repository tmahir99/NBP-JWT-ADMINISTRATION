using JwtAuthAspNet7WebAPI.Core.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace JwtAuthAspNet7WebAPI.Core.Interfaces
{
    public interface IDocumentService
    {
        Task<List<Document>> GetAllDocumentsAsync();
        Task<Document> GetDocumentByIdAsync(int id);
        Task<Document> CreateDocumentAsync(Document document);
        Task<Document> UpdateDocumentAsync(int id, Document document);
        Task DeleteDocumentAsync(int id);
    }
}