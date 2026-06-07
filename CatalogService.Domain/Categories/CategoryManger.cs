using System.Text.RegularExpressions;

using Volo.Abp.DependencyInjection;

namespace CatalogService.Categories;

public class CategoryManager(ICategoryRepository categoryRepository) : ITransientDependency
{
    public async Task<Category> CreateCategoryAsync(string name, string description)
    {
        var createItem = new Category() { Name = name, Slug = GenerateSlug(name) };
        return await categoryRepository.InsertAsync(createItem);

    }
    public async Task<Category> UpdateCategoryAsync(Guid id, string name, string description)
    {
        var founded = await categoryRepository.GetAsync(id);
        founded.Name = name;
        founded.Description = description;
        founded.Slug = GenerateSlug(founded.Name);

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
