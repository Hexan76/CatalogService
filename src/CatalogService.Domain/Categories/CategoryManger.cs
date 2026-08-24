using System.Text.RegularExpressions;

namespace CatalogService.Categories;

public class CategoryManager(ICategoryRepository categoryRepository)
{
    public async Task<Category> CreateCategoryAsync(string name, string description, long businessTypeId)
    {
        var createItem = new Category() { Name = name, Slug = GenerateSlug(name) };
        createItem.CategoryBusinessTypes.Add(new() { BusinessTypeId = businessTypeId, CategoryId = createItem.Id });
        return await categoryRepository.InsertAsync(createItem);

    }
    public async Task<Category> UpdateCategoryAsync(long id, string name, string description, long businessTypeId)
    {
        var founded = await categoryRepository.GetAsync(id);
        founded.Name = name;
        founded.Description = description;
        founded.Slug = GenerateSlug(founded.Name);
        founded.CategoryBusinessTypes.Add(new() { BusinessTypeId = businessTypeId, CategoryId = id });

        return await categoryRepository.UpdateAsync(founded);

    }
    static string GenerateSlug(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        text = text.Trim();

        text = Regex.Replace(text, @"\s+", "-");

        text = Regex.Replace(text, @"[^a-zA-Z0-9\u0600-\u06FF\-]", "");

        text = Regex.Replace(text, @"-+", "-");

        return text.ToLowerInvariant();
    }
}
