using System.Collections.Generic;
using System.Threading.Tasks;
using JwtAuthAspNet7WebAPI.Core.DbContext;
using JwtAuthAspNet7WebAPI.Core.Dtos;
using JwtAuthAspNet7WebAPI.Core.Entities;
using JwtAuthAspNet7WebAPI.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class DocumentService : IDocumentService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public DocumentService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<List<Document>> GetAllDocumentsAsync()
    {
        return await _context.Documents.ToListAsync();
    }

    public async Task<Document> GetDocumentByIdAsync(int id)
    {
        return await _context.Documents.FindAsync(id);
    }

    public async Task<Document> CreateDocumentAsync(Document document)
    {
        var user = await _userManager.FindByNameAsync(document.EmployeeUserName);
        if (user == null)
        {
            throw new InvalidOperationException("Employee not found");
        }

        document.EmployeeUserName = user.UserName;

        _context.Documents.Add(document);
        await _context.SaveChangesAsync();
        return document;
    }




    public async Task<Document> UpdateDocumentAsync(int id, Document document)
    {
        // Check if the provided id matches the id of the document
        if (id != document.Id)
        {
            return null; // Ids don't match, return null
        }

        // Check if the document exists in the database
        var existingDocument = await _context.Documents.FindAsync(id);
        if (existingDocument == null)
        {
            return null; // Document not found, return null
        }

        // Update properties of the existing document
        existingDocument.Title = document.Title;
        existingDocument.Description = document.Description;

        try
        {
            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Handle concurrency exception if necessary
            return null;
        }

        // Return the updated document
        return existingDocument;
    }


    public async Task DeleteDocumentAsync(int id)
    {
        var document = await _context.Documents.FindAsync(id);
        if (document == null)
        {
            return;
        }

        _context.Documents.Remove(document);
        await _context.SaveChangesAsync();
    }

    private bool DocumentExists(int id)
    {
        return _context.Documents.Any(e => e.Id == id);
    }
}
